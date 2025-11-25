using CommunityToolkit.Mvvm.ComponentModel;

namespace RiddleSite.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    // Use instance observable properties so Avalonia bindings receive notifications.
    [ObservableProperty]
    private bool showVerifyQuestion = false;

    [ObservableProperty]
    private bool showRiddle = false;

    [ObservableProperty]
    private string verifyAnswer1 = string.Empty;

    [ObservableProperty]
    private string verifyAnswer2 = string.Empty;

    [ObservableProperty] 
    private bool showInitialPart = true;

}