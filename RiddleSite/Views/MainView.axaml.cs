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
        MainViewModel.showVerifyQuestion = true;
    }

    private void VerifyAlbaButton(object? sender, RoutedEventArgs e)
    {
        if (MainViewModel.verifyAnswer1 == "10/30/2025" && (MainViewModel.verifyAnswer2 == "sweetie" || MainViewModel.verifyAnswer2 == "Sweetie" || MainViewModel.verifyAnswer2 == "sweetheart" || MainViewModel.verifyAnswer2 == "Sweetheart"))
        {
            MainViewModel.showRiddle = true;
        } 
    }
    
}