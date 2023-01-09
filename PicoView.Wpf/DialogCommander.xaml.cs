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

    public DialogCommander()
    {
        InitializeComponent();
    }

    private void OnClickOk(object sender, RoutedEventArgs e)
    {
        Valid.Execute(null);
    }
}
