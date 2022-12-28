// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System.Diagnostics;
using System.Drawing;
using System.IO;
using Microsoft.Win32;
using Ookii.Dialogs.Wpf;
using PicoView.Wpf.Properties;

namespace PicoView.Wpf.Ui;

internal sealed class DownloadFileWindow
{
    public DownloadFileWindow(string fileName, string savingDir, string savingFileName)
    {
        _defaultSavingDir = savingDir;
        _defaultSavingFileName = savingFileName;
        _fileName = fileName;
    }

    public void ShowDialog()
    {
        var openButton = new TaskDialogButton(Resources.ToOpen);
        var saveButton = new TaskDialogButton(Resources.ToSaveInFile) {Default = true};

        var taskDialog = new TaskDialog
        {
            WindowTitle = Resources.FileOpenning,
            MainInstruction = _fileName,
            Content = Resources.HowShouldToProccessThisFile,
            MinimizeBox = false,
            EnableHyperlinks = true,
            Buttons = {new TaskDialogButton(ButtonType.Cancel), openButton, saveButton},
            AllowDialogCancellation = true,
            VerificationText = Resources.ToOpenFileAfterSaveing,
            IsVerificationChecked = true,
            ButtonStyle = TaskDialogButtonStyle.CommandLinks,
            CustomMainIcon = Icon.ExtractAssociatedIcon(_fileName)
        };

        var selectedButton = taskDialog.ShowDialog();

        if (selectedButton == openButton)
        {
            OpenFile();
        }
        else if (selectedButton == saveButton)
        {
            SaveFile(taskDialog.IsVerificationChecked);
        }
    }

    private void SaveFile(bool openAfter)
    {
        string extension = Path.GetExtension(_fileName);
        string friendlyDocName = NativeMethods.AssocQueryString(NativeMethods.AssocStr.FriendlyDocName, extension);

        var d = new SaveFileDialog
        {
            InitialDirectory = _defaultSavingDir,
            Filter = $"{friendlyDocName}|*{extension}|{Resources.AllFiles}|*.*",
            FileName = $"{_defaultSavingFileName}{extension}",
            RestoreDirectory = true
        };

        if (d.ShowDialog() == true)
        {
            File.Copy(_fileName, d.FileName, true);

            if (openAfter)
            {
                OpenFile(d.FileName);
            }
        }
    }

    private void OpenFile(string fileName)
    {
        string extension = Path.GetExtension(fileName);
        string executable = NativeMethods.AssocQueryString(NativeMethods.AssocStr.Executable, extension);

        string arguments = $"\"{fileName}\"";

        Process.Start(executable, arguments);
    }

    private void OpenFile()
    {
        OpenFile(_fileName);
    }

    private readonly string _defaultSavingDir;
    private readonly string _defaultSavingFileName;
    private readonly string _fileName;
}