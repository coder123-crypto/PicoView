// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System.Windows;
using System.Windows.Media;
using PicoView.Core.ViewModels;

namespace PicoView.Wpf;

public interface IWindow : IView
{
    bool? DialogResult { get; set; }

    object DataContext { get; set; }

    Window Owner { get; set; }

    WindowStartupLocation WindowStartupLocation { get; set; }

    ImageSource Icon { get; set; }

    bool Activate();

    void Show();

    bool? ShowDialog();

    void Close();

    event EventHandler Closed;
}