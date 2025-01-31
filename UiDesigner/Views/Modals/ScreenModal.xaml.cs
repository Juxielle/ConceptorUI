using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using ConceptorUI.Application.Dto.UiDto;
using ConceptorUI.Application.Screens;
using UiDesigner.Application.Screens;
using UiDesigner.Constants;

namespace ConceptorUI.Views.Modals;

public partial class ScreenModal
{
    public ICommand? ScreenChangedCommand;
    private ObservableCollection<ScreenUiDto> _screens;
    
    public ScreenModal()
    {
        InitializeComponent();
        
        LoadScreens();
    }

    private async void LoadScreens()
    {
        var resultScreen = await new GetScreensQueryHandler().Handle(new GetScreensQuery
        {
            Path = Env.FileScreen
        });
        
        if(resultScreen.IsFailure) return;
        _screens = new ObservableCollection<ScreenUiDto>(resultScreen.Value);
        Items.ItemsSource = _screens;
    }

    private void OnScreenClick(object sender, MouseButtonEventArgs e)
    {
        var tag = ((FrameworkElement)sender).Tag.ToString();
        var screen = _screens.ToList().Find(s => s.Label == tag);
        ScreenChangedCommand?.Execute(screen);
        Close();
    }
}