// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

namespace PicoView.Core.Dispatchers;

public interface IDialogsDispatcher
{
    string GetDirectoryName(string initialDirectory);

    string GetSaveFileName(string initialDirectory, params string[] filters);
        
    string GetOpenFileName(string initialDirectory, params string[] filters);

    IReadOnlyList<string> GetOpenFileNames(string initialDirectory, params string[] filters);

    void SaveOrOpen(string path);
}