using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using ConceptorUI.Application.Dto.UiDto;
using ConceptorUI.Application.Screens;
using ConceptorUI.Constants;

namespace ConceptorUI.Views.Modals;

public partial class ScreenModal
{
    public ICommand? ScreenChangedCommand;
    private int _clickCount;
    private ObservableCollection<ScreenUiDto> _screens;
    
    public ScreenModal()
    {
        InitializeComponent();
        _clickCount = 0;
        
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
        _clickCount++;
        if(_clickCount < 2) return;
        
        var tag = ((FrameworkElement)sender).Tag.ToString();
        var screen = _screens.ToList().Find(s => s.Label == tag);
        ScreenChangedCommand?.Execute(screen);
        _clickCount = 0;
        Close();
    }
}