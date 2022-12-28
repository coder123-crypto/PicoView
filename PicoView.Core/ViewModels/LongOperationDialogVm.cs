// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System.ComponentModel;
using PicoView.Core.Dispatchers;

namespace PicoView.Core.ViewModels;

public abstract class LongOperationDialogVm : Vm, ILongOperationVm, IDataErrorInfo
{
    public abstract string this[string columnName] { get; }

    public abstract string Error { get; }

    public IRelayCommand Valid { get; }
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

    public bool IsLongOperation
    {
        get => _isLongOperation;
        protected set
        {
            _isLongOperation = value;
            OnPropertyChanged();
        }
    }
    private string _longOperationContent;

    public string LongOperationContent
    {
        get => _longOperationContent;
        protected set
        {
            _longOperationContent = value;
            OnPropertyChanged();
        }
    }
    private bool _isLongOperation;

    protected void StartLongOperation(string content)
    {
        LongOperationContent = content;
        IsLongOperation = true;
    }
    void ILongOperationVm.StartLongOperation(string content)
    {
        StartLongOperation(content);
    }

    protected void StopLongOperation()
    {
        IsLongOperation = false;
    }
    void ILongOperationVm.StopLongOperation()
    {
        StopLongOperation();
    }

    protected LongOperationDialogVm()
    {
        Valid = Dispatcher.Pool.CommandsDispatcher.GetCommand(_valid);
    }
}