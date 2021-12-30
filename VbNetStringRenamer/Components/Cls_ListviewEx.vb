Imports System.Drawing
Imports System.Windows.Forms
Imports System.Runtime.InteropServices
Imports VbNetStringRenamer.Core.Native

Namespace Components
    Public Class Cls_ListViewEx
        Inherits System.Windows.Forms.ListView
        Private elv As Boolean = False

        Protected Overrides Sub WndProc(ByRef m As Message)
            Select Case m.Msg
                Case 15
                    If Not elv Then
                        Cls_NativeMethods.SetWindowTheme(Me.Handle, "explorer", Nothing)
                        Cls_NativeMethods.SendMessage(Me.Handle, Cls_NativeMethods.LVM_SETEXTENDEDLISTVIEWSTYLE, Cls_NativeMethods.LVS_EX_DOUBLEBUFFER, Cls_NativeMethods.LVS_EX_DOUBLEBUFFER)
                        elv = True
                    End If
                    Exit Select
            End Select
            MyBase.WndProc(m)
        End Sub

    End Class


End Namespace


