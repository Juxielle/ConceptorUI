using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ConceptorUI.Models;

namespace ConceptorUI.Views.Modals;

public partial class TextTyping
{
    private static TextTyping? _obj;
    public ICommand? TextChangedCommand;

    public TextTyping()
    {
        InitializeComponent();

        _obj = this;
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

    private new void MouseDown(object sender, EventArgs e)
    {
    }

    private void BtnClick(object sender, RoutedEventArgs e)
    {
        var tag = (sender as Button)!.Tag.ToString();

        switch (tag)
        {
            case "AddText": break;
            case "Close":
                break;
        }
    }

    private void OnClose(object sender, MouseButtonEventArgs e)
    {
        Hide();
    }

    protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
    {
        DragMove();
    }
}