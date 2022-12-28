// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System.Windows.Input;
using PicoView.Core;
using PicoView.Core.Properties;

namespace PicoView.Wpf.Commands;

public class RelayCommand : IRelayCommand, ICommand
{
    public event EventHandler CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    public string Text { get; private set; }

    public bool HasAction => _action != null;

    public RelayCommand(Action action, [CanBeNull] Func<bool> canExecute = null) : this(action, string.Empty, canExecute)
    {
    }

    public RelayCommand(Action action, string text, [CanBeNull] Func<bool> canExecute = null)
    {
        Text = text;
        _action = action;
        _canExecute = canExecute;
    }

    public override string ToString()
    {
        if (!string.IsNullOrEmpty(Text))
        {
            return Text;
        }

        return base.ToString();
    }

    bool IRelayCommand.CanExecute(object parameter)
    {
        return _canExecute?.Invoke() ?? true;
    }

    void IRelayCommand.Execute(object parameter)
    {
        _action();
    }

    bool ICommand.CanExecute(object parameter)
    {
        return _canExecute?.Invoke() ?? true;
    }

    void ICommand.Execute(object parameter)
    {
        _action();
    }

    private readonly Action _action;
    private readonly Func<bool> _canExecute;
}

public sealed class RelayCommand<T> : IRelayCommand<T>, ICommand
{
    public event EventHandler CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    public string Text { get; private set; }

    public bool HasAction => _action != null;

    public RelayCommand(Action<T> action, [CanBeNull] Func<bool> canExecute = null) : this(action, string.Empty, canExecute)
    {
    }

    public RelayCommand(Action<T> action, [CanBeNull] Func<T, bool> canExecute) : this(action, string.Empty, canExecute)
    {
    }

    public RelayCommand(Action<T> action, string text, [CanBeNull] Func<bool> canExecute = null)
    {
        _useArgument = false;
        _action = action;
        _canExecute = canExecute;
        Text = text;
    }

    public RelayCommand(Action<T> action, string text, [CanBeNull] Func<T, bool> canExecute)
    {
        _useArgument = true;
        _action = action;
        _canExecuteWithArgument = canExecute;
        Text = text;
    }

    public override string ToString()
    {
        if (!string.IsNullOrEmpty(Text))
        {
            return Text;
        }

        return base.ToString();
    }

    bool IRelayCommand<T>.CanExecute(object parameter)
    {
        if (_useArgument)
        {
            return _canExecuteWithArgument?.Invoke((T)parameter) ?? true;
        }

        return _canExecute?.Invoke() ?? true;
    }

    void IRelayCommand<T>.Execute(object parameter)
    {
        _action((T)parameter);
    }

    bool ICommand.CanExecute(object parameter)
    {
        if (_useArgument)
        {
            return _canExecuteWithArgument?.Invoke((T)parameter) ?? true;
        }

        return _canExecute?.Invoke() ?? true;
    }

    void ICommand.Execute(object parameter)
    {
        _action((T)parameter);
    }

    private readonly Action<T> _action;
    private readonly Func<bool> _canExecute;
    private readonly Func<T, bool> _canExecuteWithArgument;
    private readonly bool _useArgument;
}