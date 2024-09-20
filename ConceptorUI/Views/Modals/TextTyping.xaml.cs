using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ConceptorUI.Models;

namespace ConceptorUI.Views.Modals;

public partial class TextTyping
{
    public ICommand? TextChangedCommand;

    public TextTyping(string text)
    {
        InitializeComponent();
        TextField.Text = text;
    }

    private void TextChanged(object sender, EventArgs e)
    {
        var text = (sender as TextBox)!.Text;

        if (TextFormated == null) return;
        TextFormated.Text = text;
        
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
                MainWindow.Instance.DisplayTextPage(false);
                break;
        }
    }

    private void OnClose(object sender, MouseButtonEventArgs e)
    {
        Close();
    }
}