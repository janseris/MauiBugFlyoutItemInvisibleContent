using DynamicMenu.ViewModels;

namespace DynamicMenu;

public partial class AppShell : Shell
{
    public AppShell(AppShellViewModel viewModel)
    {
        InitializeComponent();
        this.BindingContext = viewModel;
    }
}
