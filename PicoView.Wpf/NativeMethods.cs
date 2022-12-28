// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System.Runtime.InteropServices;
using System.Text;
using PicoView.Core.Properties;

namespace PicoView.Wpf;

internal static class NativeMethods
{
    public enum AssocStr
    {
        Command = 1,
        Executable,
        FriendlyDocName,
        FriendlyAppName,
        NoOpen,
        ShellNewValue,
        DDECommand,
        DDEIfExec,
        DDEApplication,
        DDETopic,
        InfoTip,
        QuickTip,
        TileInfo,
        ContentType,
        DefaultIcon,
        ShellExtension,
        DropTarget,
        DelegateExecute,
        SupportedUriProtocols,
        Max
    }

    [NotNull]
    public static string AssocQueryString(AssocStr association, string extension)
    {
        const int sOk = 0;
        const int sFalse = 1;

        uint length = 0;
        uint ret = AssocQueryString(AssocF.None, association, extension, null, null, ref length);
        if (ret != sFalse)
        {
            throw new InvalidOperationException("Could not determine associated string");
        }

        var sb = new StringBuilder((int)length);
        ret = AssocQueryString(AssocF.None, association, extension, null, sb, ref length);
        if (ret != sOk)
        {
            throw new InvalidOperationException("Could not determine associated string");
        }

        return sb.ToString();
    }

    [Flags]
    private enum AssocF : uint
    {
        None = 0
    }

    [DllImport("Shlwapi.dll", CharSet = CharSet.Unicode)]
    private static extern uint AssocQueryString(AssocF flags, AssocStr str, string pszAssoc, string pszExtra, [Out] StringBuilder pszOut, ref uint pcchOut);
}