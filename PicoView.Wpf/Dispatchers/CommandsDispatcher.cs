// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using PicoView.Core;
using PicoView.Core.Dispatchers;
using PicoView.Core.Properties;
using PicoView.Wpf.Commands;

namespace PicoView.Wpf.Dispatchers;

public sealed class CommandsDispatcher : ICommandsDispatcher
{
    [NotNull]
    public IRelayCommand GetCommand(Action action, string text, [CanBeNull] Func<bool> canExecute)
    {
        return new RelayCommand(action, text, canExecute);
    }

    [NotNull]
    public IRelayCommand GetCommand(Action action, [CanBeNull] Func<bool> canExecute)
    {
        return new RelayCommand(action, canExecute);
    }

    [NotNull]
    public IRelayCommand GetCommand(Action action, string text)
    {
        return new RelayCommand(action, text);
    }

    [NotNull]
    public IRelayCommand<T> GetCommand<T>(Action<T> action, string text, [CanBeNull] Func<bool> canExecute)
    {
        return new RelayCommand<T>(action, text, canExecute);
    }

    [NotNull]
    public IRelayCommand<T> GetCommand<T>(Action<T> action, [CanBeNull] Func<bool> canExecute)
    {
        return new RelayCommand<T>(action, canExecute);
    }

    [NotNull]
    public IRelayCommand<T> GetCommand<T>(Action<T> action, [NotNull] Func<T, bool> canExecute)
    {
        return new RelayCommand<T>(action, canExecute);
    }

    [NotNull]
    public IRelayCommand<T> GetCommand<T>(Action<T> action, string text)
    {
        return new RelayCommand<T>(action, text);
    }

    [NotNull]
    public IRelayCommandTree GetCommandTree(Action action, string text, [CanBeNull] Func<bool> canExecute)
    {
        return new RelayCommandTree(action, text, canExecute);
    }

    [NotNull]
    public IRelayCommandTree GetCommandTree(Action action, string text, [CanBeNull] Func<bool> canExecute, bool isRoot)
    {
        return new RelayCommandTree(action, text, canExecute) { IsRoot = isRoot };
    }

    [NotNull]
    public IRelayCommandTree GetCommandTree(string text)
    {
        return new RelayCommandTree(text);
    }
}