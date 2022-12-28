// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System.Windows;
using PicoView.Core.Dispatchers;
using PicoView.Core.Properties;
using PicoView.Core.ViewModels;

namespace PicoView.Wpf.Dispatchers;

public sealed class ViewsDispatcher : IViewsDispatcher
{
    public void Invoke(Action action)
    {
        Helper.CheckApplicationDispatcher(out var dispatcher);
        dispatcher.Invoke(action);
    }

    public static readonly ViewsDispatcher Instance = new(); // TODO delete

    public IReadOnlyDictionary<object, IWindow> OpenedWindow => _openWindows;

    public void RegisterViewType<TVm, TWin>(bool rootWindow = false) where TWin : IView where TVm : IViewModel
    {
        var vmType = typeof(TVm);

        if (vmType.IsInterface)
        {
            throw new ArgumentException("Cannot register interfaces");
        }

        if (vmType.IsAbstract)
        {
            throw new ArgumentException("Cannot register abstract");
        }

        if (_vmToWindowMapping.ContainsKey(vmType))
        {
            throw new InvalidOperationException($"Type {vmType.FullName} is already registered");
        }

        _vmToWindowMapping[vmType] = typeof(TWin);
        _rootedWindows[typeof(TWin)] = rootWindow;
    }
        
    public void UnregisterWindowType<TVm>()
    {
        var vmType = typeof(TVm);

        if (vmType.IsInterface)
        {
            throw new ArgumentException("Cannot register interfaces");
        }

        if (!_vmToWindowMapping.ContainsKey(vmType))
        {
            throw new InvalidOperationException($"Type {vmType.FullName} is not registered");
        }

        _vmToWindowMapping.Remove(vmType);
        _rootedWindows.Remove(vmType);
    }

    public void ShowView([NotNull] IViewModel vm, [CanBeNull] IOwner owner = null, [CanBeNull] EventHandler closedEvent = null)
    {
        if (vm == null)
        {
            throw new ArgumentNullException(nameof(vm));
        }

        if (_openWindows.ContainsKey(vm))
        {
            throw new InvalidOperationException("UI for this VM is already displayed");
        }

        var window = CreateWindowInstanceWithVm(vm, owner);
        if (closedEvent != null)
        {
            window.Closed += closedEvent.Invoke;
        }
        window.Show();

        _openWindows[vm] = window;
    }

    public void HidePresentation([NotNull] IViewModel vm)
    {
        if (!_openWindows.TryGetValue(vm, out var window))
        {
            throw new InvalidOperationException("UI for this VM is not displayed");
        }

        window.Close();
        _openWindows.Remove(vm);
    }

    public bool? ShowModalView([NotNull] IViewModel vm, [NotNull] IOwner owner)
    {
        var window = CreateWindowInstanceWithVm(vm, owner);
        _openWindows[vm] = window;

        var result = window.ShowDialog();
        _openWindows.Remove(vm);
        _openWindows[owner].Activate();

        if (vm is IExecuted executed)
        {
            return executed.Executed;
        }
        return result;
    }

    public void SetPresentationResult([NotNull] IViewModel vm, bool? dialogResult)
    {
        _openWindows[vm].DialogResult = dialogResult;
    }

    [NotNull]
    private IWindow CreateWindowInstanceWithVm([NotNull] IViewModel vm, [CanBeNull] IOwner owner = null)
    {
        if (vm == null)
        {
            throw new ArgumentNullException(nameof(vm));
        }

        Type windowType = null;

        var vmType = vm.GetType();
        while (vmType != null && !_vmToWindowMapping.TryGetValue(vmType, out windowType))
        {
            vmType = vmType.BaseType;
        }

        if (windowType == null)
        {
            throw new ArgumentException($"No registered window type for argument type {vm.GetType().FullName}");
        }

        var window = (IWindow) Activator.CreateInstance(windowType);
        window.DataContext = vm;

        if (owner != null)
        {
            if (_openWindows.TryGetValue(owner, out var ownerWindow))
            {
                if (_rootedWindows[window.GetType()])
                {
                    Application.Current.MainWindow = window as Window;
                    window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                }
                else
                {
                    window.Owner = ownerWindow as Window;
                    window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    window.Icon = ownerWindow.Icon;
                }
            }
            else
            {
                window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            }
        }

        return window;
    }

    private readonly Dictionary<Type, Type> _vmToWindowMapping = new Dictionary<Type, Type>();
    private readonly Dictionary<object, IWindow> _openWindows = new Dictionary<object, IWindow>();
    private readonly Dictionary<Type, bool> _rootedWindows = new Dictionary<Type, bool>();
}