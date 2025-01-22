using System.Collections.ObjectModel;
using ConceptorUI.Application.Dto.UiDto;
using ConceptorUI.Application.Screens;
using ConceptorUI.Constants;

namespace ConceptorUI.Views.Modals;

public partial class ScreenModal
{
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
        Items.ItemsSource = new ObservableCollection<ScreenUiDto>(resultScreen.Value);
    }
}