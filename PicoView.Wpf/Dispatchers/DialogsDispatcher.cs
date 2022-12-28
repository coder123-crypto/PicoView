// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System.IO;
using Microsoft.Win32;
using Ookii.Dialogs.Wpf;
using PicoView.Core.Dispatchers;
using PicoView.Core.Properties;
using PicoView.Wpf.Properties;
using PicoView.Wpf.Ui;

namespace PicoView.Wpf.Dispatchers;

public sealed class DialogsDispatcher : IDialogsDispatcher
{
    public string GetDirectoryName(string initialDirectory)
    {
        var dialog = new VistaFolderBrowserDialog
        {
            SelectedPath = initialDirectory,
            ShowNewFolderButton = false
        };

        Helper.CheckMainWindow(out var window);
        return dialog.ShowDialog(window) == true ? dialog.SelectedPath : string.Empty;
    }

    [CanBeNull]
    public string GetOpenFileName(string initialDirectory, [NotNull] params string[] filters)
    {
        string filter = string.Join("|", filters.Select(t => $"{Resources.Files} {t}|*{t}"));

        var dialog = new OpenFileDialog
        {
            Filter = filter,
            FilterIndex =  0,
            InitialDirectory = initialDirectory,
            RestoreDirectory = true
        };

        Helper.CheckMainWindow(out var window);
        return dialog.ShowDialog(window) == true ? dialog.FileName : string.Empty;
    }

    public IReadOnlyList<string> GetOpenFileNames(string initialDirectory, params string[] filters)
    {
        string filter = string.Join("|", filters.Select(t => $"{Resources.Files} {t}|*{t}"));

        var dialog = new OpenFileDialog
        {
            Filter = filter,
            Multiselect = true,
            FilterIndex = 0,
            InitialDirectory = initialDirectory,
            RestoreDirectory = true
        };

        Helper.CheckMainWindow(out var window);
        return dialog.ShowDialog(window) == true ? dialog.FileNames : new string[] { };
    }

    [CanBeNull]
    public string GetSaveFileName(string initialDirectory, [NotNull] params string[] filters)
    {
        string filter = string.Join("|", filters.Select(t => $"{Resources.Files} {t}|*{t}"));

        var dialog = new SaveFileDialog
        {
            Filter = filter,
            FilterIndex = 0,
            InitialDirectory = initialDirectory,
            RestoreDirectory = true
        };

        Helper.CheckMainWindow(out var window);
        return dialog.ShowDialog(window) == true ? dialog.FileName : string.Empty;
    }

    public void SaveOrOpen(string path)
    {
        var window = new DownloadFileWindow(path, Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), Path.GetFileNameWithoutExtension(path));
        window.ShowDialog();
    }
}