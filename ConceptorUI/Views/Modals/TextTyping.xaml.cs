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

namespace ConceptorUI.Views.Modals;

public partial class TextTyping
{
    private static TextTyping? _obj;
    public ICommand? TextChangedCommand;

    private List<int> _ids;

    public TextTyping()
    {
        InitializeComponent();

        _obj = this;
        _ids = [];
    }

    public static TextTyping Instance => _obj ?? new TextTyping();

    public void Refresh(string text)
    {
        TextField.Text = text;
        Show();
    }

    private void TextChanged(object sender, EventArgs e)
    {
        var text = (sender as TextBox)!.Text;

        // if (TextFormated == null) return;
        // TextFormated.Text = text;

        TextChangedCommand?.Execute(
            new dynamic[] { GroupNames.Text, PropertyNames.Text, text }
        );
    }

    private void BtnClick(object sender, RoutedEventArgs e)
    {
        var tag = (sender as Button)!.Tag.ToString();

        switch (tag)
        {
            case "AddText":
                break;
        }
    }

    private void OnMouseDown(object sender, MouseButtonEventArgs e)
    {
        var tag = ((FrameworkElement)sender).Tag.ToString();

        switch (tag)
        {
            case "AddText":
                var id = GetId();

                var textItem = new TextItem
                {
                    Height = 35,
                    Tag = id,
                    Text = $"Text numéro {id}",
                    Margin = new Thickness(0, 10, 0, 0),
                    Command = new RelayCommand(TextItemEventHandle)
                };
                TextItems.Children.Add(textItem);
                break;
            case "Close":
                Hide();
                break;
        }
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
                foreach (var child in TextItems.Children)
                    (child as TextItem)!.BorderBrush =
                        (new BrushConverter().ConvertFrom("#8c8c8a") as SolidColorBrush)!;

                var textItem = TextItems.Children[index] as TextItem;
                textItem!.BorderBrush = (new BrushConverter().ConvertFrom("#35A9BF") as SolidColorBrush)!;
                
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

    protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
    {
        DragMove();
    }

    private int GetId()
    {
        var newId = 0;
        while (true)
        {
            var found = false;
            foreach (var id in _ids.Where(id => id == newId))
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