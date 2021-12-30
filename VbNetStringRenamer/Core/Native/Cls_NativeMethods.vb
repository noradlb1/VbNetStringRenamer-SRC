Imports System.Runtime.InteropServices

Namespace Core.Native
    NotInheritable Class Cls_NativeMethods
        <DllImport("user32.dll", CharSet:=CharSet.Auto)> _
        Public Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal msg%, ByVal wParam%, ByVal lParam%) As IntPtr
        End Function
        <DllImport("uxtheme.dll", CharSet:=CharSet.Unicode)> _
        Public Shared Function SetWindowTheme(ByVal hWnd As IntPtr, ByVal pszSubAppName$, ByVal pszSubIdList$) As Integer
        End Function
        <DllImport("shell32.dll")> _
        Public Shared Function ExtractIcon(ByVal hInst As IntPtr, ByVal lpszExeFileName$, ByVal nIconIndex%) As IntPtr
        End Function

        Public Const LVM_FIRST% = 4096
        Public Const LVM_SETEXTENDEDLISTVIEWSTYLE% = LVM_FIRST + 54
        Public Const LVS_EX_FULLROWSELECT% = 32
        Public Const LVS_EX_DOUBLEBUFFER% = 65536
    End Class

End Namespace