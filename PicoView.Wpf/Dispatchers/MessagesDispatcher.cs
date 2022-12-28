// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System.Drawing;
using System.Windows;
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;
using TaskDialog = Ookii.Dialogs.Wpf.TaskDialog;
using TaskDialogButton = Ookii.Dialogs.Wpf.TaskDialogButton;
using TaskDialogIcon = Ookii.Dialogs.Wpf.TaskDialogIcon;
using Ookii.Dialogs.WinForms;
using ButtonType = Ookii.Dialogs.Wpf.ButtonType;
using System.Reflection;
using PicoView.Core.Dispatchers;
using PicoView.Core.Properties;

namespace PicoView.Wpf.Dispatchers;

public sealed class MessagesDispatcher : IMessagesDispatcher
{
    public static readonly MessagesDispatcher Dispatcher = new();

    #region Info
    public void ShowInfo(string mainInstruction)
    {
        Show(mainInstruction, string.Empty, TaskDialogIcon.Information);
    }

    public void ShowInfo(string mainInstruction, string content)
    {
        Show(mainInstruction, content, TaskDialogIcon.Information);
    }
    #endregion

    #region Warning
    public void ShowWarning(string mainInstruction, [NotNull] Exception exception)
    {
        Show(mainInstruction, exception.Message, TaskDialogIcon.Warning);
    }

    public void ShowWarning(string mainInstruction)
    {
        MessageBox.Show(mainInstruction, Title, MessageBoxButton.OK, MessageBoxImage.Warning);
    }

    public void ShowWarning(string mainInstruction, string content)
    {
        Show(mainInstruction, content, TaskDialogIcon.Warning);
    }
    #endregion

    #region Error
    public void ShowError(string mainInstruction, [NotNull] Exception exception)
    {
        Show(mainInstruction, exception.Message, TaskDialogIcon.Error);
    }

    public void ShowError(string mainInstruction)
    {
        MessageBox.Show(mainInstruction, Title, MessageBoxButton.OK, MessageBoxImage.Error);
    }

    public void ShowError(string mainInstruction, string content)
    {
        Show(mainInstruction, content, TaskDialogIcon.Error);
    }
    #endregion

    #region Question
    public bool AskQuestion(string question)
    {
        return MessageBox.Show(question, Title, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
    }

    public bool AskQuestion(string question, out bool cancelled)
    {
        var answer = MessageBox.Show(question, Title, MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
        cancelled = answer == MessageBoxResult.Cancel;
        return answer == MessageBoxResult.Yes;
    }

    public bool AskQuestion(string mainInstruction, string content)
    {
        using (var messageBox = new TaskDialog
               {
                   WindowTitle = Title,
                   MainInstruction = mainInstruction,
                   Content = content,
                   CustomMainIcon = SystemIcons.Question,
                   Buttons = {new TaskDialogButton(ButtonType.Yes), new TaskDialogButton(ButtonType.No)}
               })
        {
            return messageBox.ShowDialog().ButtonType == ButtonType.Yes;
        }
    }

    public bool? AskNullableQuestion(string question)
    {
        switch (MessageBox.Show(question, Title, MessageBoxButton.YesNoCancel, MessageBoxImage.Question))
        {
            case MessageBoxResult.Yes:
                return true;

            case MessageBoxResult.No:
                return false;

            default:
                return null;
        }
    }
    #endregion

    #region Input
    [NotNull]
    public string GetString(string content, bool usePasswordMasking)
    {
        Helper.CheckApplicationDispatcher(out var dispatcher);
        var callback = new Func<string, string, bool, string>(GetStringInternal);
        return (string) dispatcher.Invoke(callback, content, string.Empty, usePasswordMasking);
    }

    [NotNull]
    public string GetString(string content, string value, bool usePasswordMasking)
    {
        Helper.CheckApplicationDispatcher(out var dispatcher);
        var callback = new Func<string, string, bool, string>(GetStringInternal);
        return (string)dispatcher.Invoke(callback, content, value, usePasswordMasking);
    }
    #endregion

    [NotNull]
    private static string Title => Assembly.GetEntryAssembly()?.GetName().Name ?? string.Empty;

    private static void Show(string mainInstruction, string content, TaskDialogIcon icon)
    {
        Helper.CheckApplicationDispatcher(out var dispatcher);
        var callback = new Action<string, string, TaskDialogIcon>(ShowDialogInternal);
        dispatcher.Invoke(callback, mainInstruction, content, icon);
    }

    private static void ShowDialogInternal(string mainInstruction, string content, TaskDialogIcon icon)
    {
        var messageBox = new TaskDialog
        {
            WindowTitle = Title,
            MainInstruction = mainInstruction,
            Content = content,
            MainIcon = icon,
            Buttons = {new TaskDialogButton(ButtonType.Ok)}
        };

        messageBox.ShowDialog();
    }

    private static string GetStringInternal(string content, string value, bool usePasswordMasking)
    {
        var dialog = new InputDialog
        {
            Input = value,
            WindowTitle = Title,
            MainInstruction = content,
            UsePasswordMasking = usePasswordMasking
        };

        return dialog.ShowDialog() == DialogResult.OK ? dialog.Input : string.Empty;
    }
}