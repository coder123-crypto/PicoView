// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

namespace PicoView.Core.Dispatchers;

public interface ICommandsDispatcher
{
    IRelayCommand GetCommand(Action action, string text, Func<bool> canExecute);

    IRelayCommand GetCommand(Action action, Func<bool> canExecute = null);

    IRelayCommand GetCommand(Action action, string text);

    IRelayCommand<T> GetCommand<T>(Action<T> action, string text, Func<bool> canExecute);

    IRelayCommand<T> GetCommand<T>(Action<T> action, Func<bool> canExecute = null);

    IRelayCommand<T> GetCommand<T>(Action<T> action, Func<T, bool> canExecute);

    IRelayCommand<T> GetCommand<T>(Action<T> action, string text);

    IRelayCommandTree GetCommandTree(string text);

    IRelayCommandTree GetCommandTree(Action action, string text, Func<bool> canExecute = null);

    IRelayCommandTree GetCommandTree(Action action, string text, Func<bool> canExecute, bool isRoot);
}