// // This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// // PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System.Windows;
using System.Windows.Threading;
using PicoView.Wpf.Properties;

namespace PicoView.Wpf;

internal static class Helper
{
    public static void CheckApplicationDispatcher(out Dispatcher dispatcher)
    {
        if (Application.Current.Dispatcher is { } d)
        {
            dispatcher = d;
            return;
        }

        throw new NullReferenceException(Errors.ApplicationDispatcherNull);
    }

    public static void CheckMainWindow(out Window window)
    {
        if (Application.Current.MainWindow is { } w)
        {
            window = w;
            return;
        }
        
        throw new NullReferenceException(Errors.MainWindowNull);
    }
}