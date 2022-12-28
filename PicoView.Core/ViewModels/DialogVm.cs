// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System.ComponentModel;
using PicoView.Core.Dispatchers;

namespace PicoView.Core.ViewModels;

public abstract class DialogVm : Vm, IDataErrorInfo
{
    public abstract string this[string columnName] { get; }

    public abstract string Error { get; }

    public virtual IRelayCommand Valid { get; }
    private void _valid()
    {
        if (string.IsNullOrEmpty(Error))
        {
            Dispatcher.Pool.ViewsDispatcher.SetPresentationResult(this, true);
            Dispatcher.Pool.ViewsDispatcher.HidePresentation(this);
        }
        else
        {
            Dispatcher.Pool.MessagesDispatcher.ShowWarning(Error);
        }
    }

    protected DialogVm()
    {
        Valid = Dispatcher.Pool.CommandsDispatcher.GetCommand(_valid);
    }
}