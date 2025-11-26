using System.Threading;
using System.Threading.Tasks;
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

    private async void VerifyAlbaButton(object? sender, RoutedEventArgs e)
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
            // Ensure any prior error message is hidden when verification succeeds
            vm.ShowIncorrectErrorMessage = false;
            vm.ShowRiddle = true;
            vm.ShowInitialPart = false;
            vm.ShowVerifyQuestion = false;
            // Optional: clear inputs so stale values aren't shown if user navigates back
            vm.VerifyAnswer1 = string.Empty;
            vm.VerifyAnswer2 = string.Empty;
            vm.VerifyAnswer3 = string.Empty;
        }
        else
        {
            vm.ShowIncorrectErrorMessage = true;
            // Use a non-blocking delay so the UI can update and show the error message
            await Task.Delay(2000);
            vm.ShowIncorrectErrorMessage = false;
        }
    }
    
}