// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

namespace PicoView.Core.ViewModels;

public abstract class LongOperationProgressVm : LongOperationVm
{
    public int LongOperationProgress
    {
        get => _longOperationProgress;
        protected set
        {
            _longOperationProgress = value;
            OnPropertyChanged();
        }
    }
    private int _longOperationProgress;

    protected void SetProgress(int progress)
    {
        LongOperationProgress = progress;
    }
}