// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using PicoView.Core.ViewModels;

namespace PicoView.Core.Dispatchers;

public interface IViewsDispatcher
{
    void RegisterViewType<TVm, TWin>(bool rootWindow = false) where TVm : IViewModel where TWin : IView;

    void ShowView(IViewModel vm, IOwner owner = null, EventHandler closedEvent = null);

    bool? ShowModalView(IViewModel vm, IOwner owner = null);

    void HidePresentation(IViewModel vm);

    void SetPresentationResult(IViewModel vm, bool? dialogResult);

    void Invoke(Action action);
}