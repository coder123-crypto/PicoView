// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System.ComponentModel;

namespace PicoView.Core.ViewModels;

public abstract class LongOperationVm : Vm, ILongOperationVm
{
    public bool IsLongOperation
    {
        get => _isLongOperation;
        protected set
        {
            _isLongOperation = value;
            OnPropertyChanged();
        }
    }
    private bool _isLongOperation;

    public string LongOperationContent
    {
        get => _longOperationContent;
        protected set
        {
            _longOperationContent = value;
            OnPropertyChanged();
        }
    }
    private string _longOperationContent = string.Empty;


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

    private readonly BackgroundWorker _worker;
}