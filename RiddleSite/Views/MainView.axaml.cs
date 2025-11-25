using Avalonia.Controls;
using Avalonia.Interactivity;
using RiddleSite;
using RiddleSite.ViewModels;

namespace RiddleSite.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
    }
    private void GetRiddleButton(object? sender, RoutedEventArgs e)
    {
        if (DataContext is MainViewModel vm)
        {
            vm.ShowVerifyQuestion = true;
            vm.ShowInitialPart = false;
            vm.ShowRiddle = false;
        }
    }

    private void VerifyAlbaButton(object? sender, RoutedEventArgs e)
    {
        if (DataContext is not MainViewModel vm)
            return;

        var a1 = vm.VerifyAnswer1?.Trim();
        var a2 = vm.VerifyAnswer2?.Trim();

        if (a1 == "10/30/2025" &&
            (a2 == "sweetie" || a2 == "Sweetie" || a2 == "sweetheart" || a2 == "Sweetheart"))
        {
            vm.ShowRiddle = true;
            vm.ShowInitialPart = false;
            vm.ShowVerifyQuestion = false;
        }
    }
    
}