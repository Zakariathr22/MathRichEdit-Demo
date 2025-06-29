using Microsoft.UI.Xaml;
using Microsoft.UI.Windowing;

namespace MathRichEdit_Demo;

public sealed partial class MainWindow : Window
{
    public MainWindow()
    {
        this.InitializeComponent();

        OverlappedPresenter presenter = OverlappedPresenter.Create();
        presenter.IsResizable = false;
        presenter.IsMaximizable = false;
        AppWindow.SetPresenter(presenter);
        AppWindow.Resize(new Windows.Graphics.SizeInt32(816, 764));
        AppWindow.TitleBar.ExtendsContentIntoTitleBar = true;
        AppWindow.TitleBar.PreferredHeightOption = TitleBarHeightOption.Tall;
        AppWindow.SetTaskbarIcon("Assets/Icon.png");

        MainFrame.Navigate(typeof(InitialPage));
    }
}
