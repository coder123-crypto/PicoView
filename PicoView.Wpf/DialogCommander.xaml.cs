using System.Windows;
using PicoView.Core;

namespace PicoView.Wpf;

public partial class DialogCommander
{
    public static readonly DependencyProperty ValidProperty = DependencyProperty.Register(
        nameof(Valid),
        typeof(IRelayCommand),
        typeof(DialogCommander),
        new FrameworkPropertyMetadata(default(IRelayCommand), FrameworkPropertyMetadataOptions.AffectsArrange)
    );

    public IRelayCommand Valid
    {
        get => (IRelayCommand) GetValue(ValidProperty);
        set => SetValue(ValidProperty, value);
    }

    public static readonly DependencyProperty ButtonsProperty = DependencyProperty.Register(
        nameof(Buttons),
        typeof(DialogCommanderButtons),
        typeof(DialogCommander),
        new FrameworkPropertyMetadata(DialogCommanderButtons.Ok | DialogCommanderButtons.Cancel, FrameworkPropertyMetadataOptions.AffectsArrange, ButtonsChanged)
    );

    private static void ButtonsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is DialogCommander commander)
        {
            commander.ButtonsChanged();
        }
    }

    private void ButtonsChanged()
    {
        OkButton.Visibility = Buttons.HasFlag(DialogCommanderButtons.Ok) ? Visibility.Visible : Visibility.Collapsed;
        CancelButton.Visibility = Buttons.HasFlag(DialogCommanderButtons.Cancel) ? Visibility.Visible : Visibility.Collapsed;
    }

    public DialogCommanderButtons Buttons
    {
        get => (DialogCommanderButtons) GetValue(ButtonsProperty);
        set => SetValue(ButtonsProperty, value);
    }

    public DialogCommander()
    {
        InitializeComponent();
    }

    private void OnClickOk(object sender, RoutedEventArgs e)
    {
        Valid.Execute(null);
    }
}
