using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ConceptorUI.Inputs;
using ConceptorUI.Models;
using ConceptorUI.Views.Widgets;
using TextDecorations = ConceptorUI.Enums.TextDecorations;

namespace ConceptorUI.Views.Modals;

public partial class TextTyping
{
    private static TextTyping? _obj;
    public ICommand? TextChangedCommand;

    private readonly List<int> _ids;
    private int _selectedTextIndex;
    private readonly List<TextItem> _textItems;
    private bool _allowModify;
    private bool _allowModifyComboBox;

    public TextTyping()
    {
        _selectedTextIndex = 0;
        _textItems = [];
        
        InitializeComponent();

        _obj = this;
        _ids = [];
        _allowModify = true;
        _allowModifyComboBox = true;

        foreach (var fontFamily in Fonts.SystemFontFamilies)
        {
            CFontFamily.Items.Add(
                new ComboBoxItem { Content = fontFamily.Source }
            );
        }
    }

    public static TextTyping Instance => _obj ?? new TextTyping();

    public void Refresh(object sender)
    {
        TextItems.Children.Clear();
        _textItems.Clear();

        var groups = (sender as List<List<GroupProperties>>)!;
        var group0 = groups![0].Find(g => g.Name == GroupNames.Text.ToString());

        _selectedTextIndex = Convert.ToInt32(group0!.GetValue(PropertyNames.CurrentTextIndex));
        if (groups.Count > 1)
        {
            for (var i = 0; i < groups.Count; i++)
            {
                if (i == 0) continue;
                var group = groups[i].Find(g => g.Name == GroupNames.Text.ToString());

                var text = group!.GetValue(PropertyNames.Text);
                var fontFamily = group.GetValue(PropertyNames.FontFamily);
                var fontWeight = group.GetValue(PropertyNames.FontWeight);
                var fontStyle = group.GetValue(PropertyNames.FontStyle);
                var foreground = group.GetValue(PropertyNames.Color);

                var textDecorationUnderline = group.GetValue(PropertyNames.TextUnderline);
                var textDecorationOverline = group.GetValue(PropertyNames.TextOverline);
                var textDecorationThrough = group.GetValue(PropertyNames.TextThrough);
                var textDecoration = textDecorationUnderline == "1"
                    ? TextDecorations.Underline.ToString()
                    : textDecorationOverline == "1"
                        ? TextDecorations.OverLine.ToString()
                        : textDecorationThrough == "1"
                            ? TextDecorations.Strikethrough.ToString()
                            : TextDecorations.None.ToString();
                AddTextItem(text, fontFamily, fontWeight, fontStyle, foreground, textDecoration);
            }

            OnSelectedItemChanged(_selectedTextIndex);
        }

        Show();
    }

    private void TextChanged(object sender, EventArgs e)
    {
        if (!_allowModify)
        {
            _allowModify = true;
            return;
        }

        var text = (sender as TextBox)!.Text;
        _textItems[_selectedTextIndex].Text = text;

        TextChangedCommand?.Execute(
            new dynamic[] { GroupNames.Text, PropertyNames.Text, text }
        );
    }

    private void OnMouseDown(object sender, MouseButtonEventArgs e)
    {
        var tag = ((FrameworkElement)sender).Tag.ToString();

        switch (tag)
        {
            case "AddText":
                const string text = "Text numéro";
                AddTextItem(text);
                TextChangedCommand?.Execute(
                    new dynamic[] { GroupNames.Text, PropertyNames.Add, text }
                );
                break;
            case "Close":
                Hide();
                break;
        }
    }

    private void BtnMouseDown(object sender, MouseButtonEventArgs e)
    {
        var tag = (sender as Border)!.Tag.ToString()!;
        var index = 0;
        var propertyName = PropertyNames.None;

        switch (tag)
        {
            case "FontWeight":
                propertyName = PropertyNames.FontWeight;
                index = _textItems[_selectedTextIndex].FontWeight == FontWeights.Normal ? 1 : 0;
                _textItems[_selectedTextIndex].FontWeight = index == 0 ? FontWeights.Normal : FontWeights.Bold;
                break;
            case "FontStyle":
                propertyName = PropertyNames.FontStyle;
                index = _textItems[_selectedTextIndex].FontStyle == FontStyles.Normal ? 1 : 0;
                _textItems[_selectedTextIndex].FontStyle = index == 0 ? FontStyles.Normal : FontStyles.Italic;
                break;
            case "TextUnderline":
                propertyName = PropertyNames.TextUnderline;
                index = _textItems[_selectedTextIndex].TextDecoration == TextDecorations.None.ToString() ? 1 : 0;
                _textItems[_selectedTextIndex].TextDecoration =
                    index == 1 ? TextDecorations.Underline.ToString() : TextDecorations.None.ToString();
                break;
            case "TextOverline":
                propertyName = PropertyNames.TextOverline;
                index = _textItems[_selectedTextIndex].TextDecoration == TextDecorations.None.ToString() ? 1 : 0;
                _textItems[_selectedTextIndex].TextDecoration =
                    index == 1 ? TextDecorations.OverLine.ToString() : TextDecorations.None.ToString();
                break;
            case "TextThrough":
                propertyName = PropertyNames.TextThrough;
                index = _textItems[_selectedTextIndex].TextDecoration == TextDecorations.None.ToString() ? 1 : 0;
                _textItems[_selectedTextIndex].TextDecoration =
                    index == 1 ? TextDecorations.Strikethrough.ToString() : TextDecorations.None.ToString();
                break;
        }

        if (propertyName == PropertyNames.None) return;

        ChangeColor(_selectedTextIndex);
        TextChangedCommand?.Execute(
            new dynamic[] { GroupNames.Text, propertyName, index.ToString() }
        );
    }

    private void TextItemEventHandle(object sender)
    {
        var dictionary = (Dictionary<string, object>)sender;
        var action = dictionary["action"];
        var id = Convert.ToInt32(dictionary["tag"]);
        var count = TextItems.Children.Count;
        var index = 0;

        for (var i = 0; i < TextItems.Children.Count; i++)
            if (id == Convert.ToInt32(((TextItem)TextItems.Children[i]).Tag))
                index = i;

        switch (action)
        {
            case "Click":
                _selectedTextIndex = index;
                OnSelectedItemChanged(index);

                TextChangedCommand?.Execute(
                    new dynamic[] { GroupNames.Text, PropertyNames.CurrentTextIndex, index.ToString() }
                );
                break;
            case "MoveUp":
                if (index == 0) return;
                var item = TextItems.Children[index];
                TextItems.Children.RemoveAt(index);
                TextItems.Children.Insert(index - 1, item);
                break;
            case "MoveDown":
                if (index == count - 1) return;
                var item2 = TextItems.Children[index];
                TextItems.Children.RemoveAt(index);
                TextItems.Children.Insert(index + 1, item2);
                break;
            case "Delete":
                if (count == 1) return;
                TextItems.Children.RemoveAt(index);
                break;
        }
    }

    private void OnSelectedItemChanged(int index)
    {
        foreach (var child in TextItems.Children)
        {
            var textI = child as TextItem;
            textI!.BorderBrush = (new BrushConverter().ConvertFrom("#8c8c8a") as SolidColorBrush)!;
            textI.Background = Brushes.White;
        }

        _allowModify = false;
        _allowModifyComboBox = false;
        TextField.Text = _textItems[index].Text;
        CFontFamily.SelectedIndex = Convert.ToInt32(ManageEnums.GetFfIndex(_textItems[index].FontFamily.ToString()));
        ChangeColor(index);

        var textItem = TextItems.Children[index] as TextItem;
        textItem!.BorderBrush = (new BrushConverter().ConvertFrom("#35A9BF") as SolidColorBrush)!;
        textItem.Background = Brushes.Beige;
    }

    private void OnSelectedChanged(object sender, SelectionChangedEventArgs e)
    {
        if (!_allowModifyComboBox)
        {
            _allowModifyComboBox = true;
            return;
        }

        var comboBox = (sender as ComboBox)!;
        var tag = comboBox.Tag != null ? comboBox.Tag.ToString()! : "";
        var propertyName = PropertyNames.None;
        string value = null!;

        switch (tag)
        {
            case "FontFamily":
                propertyName = PropertyNames.FontFamily;
                value = (comboBox.SelectedValue as ComboBoxItem) != null
                    ? (comboBox.SelectedValue as ComboBoxItem)!.Content.ToString()!
                    : null!;
                
                if(_textItems.Count > _selectedTextIndex)
                    _textItems[_selectedTextIndex].FontFamily = new FontFamily(value);
                break;
        }

        if (propertyName == PropertyNames.None) return;

        TextChangedCommand?.Execute(
            new dynamic[] { GroupNames.Text, propertyName, value! }
        );
    }

    private void ChangeColor(int index)
    {
        FontStyle.Foreground = _textItems[index].FontStyle.ToString() == "Normal"
            ? new BrushConverter().ConvertFrom("#8c8c8a") as SolidColorBrush
            : new BrushConverter().ConvertFrom("#6739b7") as SolidColorBrush;

        FontWeight.Foreground = _textItems[index].FontWeight.ToString() == "Normal"
            ? new BrushConverter().ConvertFrom("#8c8c8a") as SolidColorBrush
            : new BrushConverter().ConvertFrom("#6739b7") as SolidColorBrush;

        TextUnderline.Foreground = _textItems[index].TextDecoration == TextDecorations.Underline.ToString()
            ? new BrushConverter().ConvertFrom("#6739b7") as SolidColorBrush
            : new BrushConverter().ConvertFrom("#8c8c8a") as SolidColorBrush;

        TextOverline.Foreground = _textItems[index].TextDecoration == TextDecorations.OverLine.ToString()
            ? new BrushConverter().ConvertFrom("#6739b7") as SolidColorBrush
            : new BrushConverter().ConvertFrom("#8c8c8a") as SolidColorBrush;

        TextThrough.Foreground = _textItems[index].TextDecoration == TextDecorations.Strikethrough.ToString()
            ? new BrushConverter().ConvertFrom("#6739b7") as SolidColorBrush
            : new BrushConverter().ConvertFrom("#8c8c8a") as SolidColorBrush;
    }

    protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
    {
        DragMove();
    }

    private void AddTextItem(string text = "My text",
        string fontFamily = "Arial",
        string fontWeight = "0",
        string fontStyle = "0",
        string foreground = "#000000",
        string textDecoration = "None")
    {
        var id = GetId();

        var textItem = new TextItem
        {
            Height = 35,
            Tag = id,
            Text = text,
            FontFamily = new FontFamily(fontFamily),
            FontStyle = fontStyle == "1" ? FontStyles.Italic : FontStyles.Normal,
            FontWeight = fontWeight == "1" ? FontWeights.Bold : FontWeights.Normal,
            Foreground = (new BrushConverter().ConvertFrom(foreground) as SolidColorBrush)!,
            TextDecoration = textDecoration,
            Margin = new Thickness(0, 6, 0, 0),
            Command = new RelayCommand(TextItemEventHandle)
        };

        TextItems.Children.Add(textItem);
        _textItems.Add(textItem);
    }

    private int GetId()
    {
        var newId = 0;
        while (true)
        {
            var found = false;
            foreach (var unused in _ids.Where(id => id == newId))
            {
                found = true;
            }

            if (!found) break;
            newId++;
        }

        _ids.Add(newId);
        return newId;
    }
}