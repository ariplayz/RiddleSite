using CommunityToolkit.Mvvm.ComponentModel;

namespace RiddleSite.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty] public static bool showVerifyQuestion = false;
    [ObservableProperty] public static bool showRiddle = false;
    [ObservableProperty] public static string verifyAnswer1 = "";
    [ObservableProperty] public static string verifyAnswer2 = "";
}