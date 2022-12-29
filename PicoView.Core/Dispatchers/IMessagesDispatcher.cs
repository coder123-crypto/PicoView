// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

namespace PicoView.Core.Dispatchers;

public interface IMessagesDispatcher
{
    void ShowInfo(string mainInstruction);
    void ShowInfo(string mainInstruction, string content);

    void ShowWarning(string mainInstruction, Exception exception);
    void ShowWarning(string mainInstruction);
    void ShowWarning(string mainInstruction, string content);

    void ShowError(string mainInstruction, Exception exception);
    void ShowError(string mainInstruction);
    void ShowError(string mainInstruction, string content);

    bool AskQuestion(string question);
    bool AskQuestion(string mainInstruction, string content);
    bool AskQuestion(string mainInstruction, string content, string verificationText, out bool isVerificationChecked);
    bool AskQuestion(string question, out bool cancelled);

    bool? AskNullableQuestion(string question);

    string GetString(string content, bool usePasswordMasking = false);
    string GetString(string content, string value, bool usePasswordMasking = false);
}