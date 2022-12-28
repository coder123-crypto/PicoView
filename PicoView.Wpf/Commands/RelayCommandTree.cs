// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System.Collections.ObjectModel;
using PicoView.Core;
using PicoView.Core.Properties;

namespace PicoView.Wpf.Commands;

public sealed class RelayCommandTree : RelayCommand, IRelayCommandTree
{
    public ObservableCollection<IRelayCommandTree> Items { get; private set; } = new ObservableCollection<IRelayCommandTree>();
    ICollection<IRelayCommandTree> IRelayCommandTree.Items
    {
        get => Items;
        set => Items = new ObservableCollection<IRelayCommandTree>(value);
    }

    [NotNull]
    public IRelayCommandTree Add(Action action, string text)
    {
        var item = new RelayCommandTree(action, text);
        Items.Add(item);
        return item;
    }

    [NotNull]
    public IRelayCommandTree Add(string text)
    {
        return Add(null, text);
    }

    public bool IsRoot { get; set; }

    public RelayCommandTree(string text) : this(null, text)
    {
    }

    public RelayCommandTree(Action action, [CanBeNull] Func<bool> canExecute = null) : base(action, canExecute)
    {
    }

    public RelayCommandTree(Action action, string text) : base(action, text)
    {
    }

    public RelayCommandTree(Action action, string text, [CanBeNull] Func<bool> canExecute = null) : base(action, text, canExecute)
    {
    }
}