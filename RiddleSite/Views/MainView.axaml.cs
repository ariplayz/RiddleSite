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
        var a3 = vm.VerifyAnswer3?.Trim();

        if (a1 == "10/30/2025" &&
            (a2 == "sweetie" || a2 == "Sweetie" || a2 == "sweetheart" || a2 == "Sweetheart") &&
            (a3 == "homie" || a3 == "Homie" || a3 == "home slice" || a3 == "Home Slice" || a3 == "homeslice" || a3 == "Homeslice" || a3 == "Home slice" || a3 == "home Slice"))
        {
            vm.ShowRiddle = true;
            vm.ShowInitialPart = false;
            vm.ShowVerifyQuestion = false;
        }
    }
    
}