Imports System.IO
Imports VbNetStringRenamer.Core.Randomize

Namespace Core.Encryption
    Friend Class Cls_EncryptedFileWriter

        Private _EncodedFilePath$ = String.Empty
        Public Property EncodedFilePath() As String
            Get
                Return _EncodedFilePath
            End Get
            Set(ByVal value As String)
                _EncodedFilePath = value
            End Set
        End Property

        Public Sub New(ByVal FilePath$)
            _EncodedFilePath = FilePath
        End Sub

        Public Sub OpenWriter()
            _s = New FileStream(EncodedFilePath, FileMode.Create, FileAccess.ReadWrite)
            w = New StreamWriter(s, System.Text.Encoding.Unicode)
        End Sub

        Private _s As Stream
        Private Property s() As Stream
            Get
                Return _s
            End Get
            Set(ByVal value As Stream)
                _s = value
            End Set
        End Property

        Private _w As StreamWriter
        Private Property w() As StreamWriter
            Get
                Return _w
            End Get
            Set(ByVal value As StreamWriter)
                _w = value
            End Set
        End Property

        Public Sub WriteLine(ByVal Line$)
            w.WriteLine(Line)
        End Sub

        Public Sub CreateXorDecoderFunction()
            Dim _Function As String = "Public Shared Function " & Cls_Randomize.XorFunctionDecoderName & "(text1 As String, num1 As Integer) As String" & vbNewLine _
            & "    Dim builder As New StringBuilder(text1)" & vbNewLine _
            & "    Dim builder2 As New StringBuilder(text1.Length)" & vbNewLine _
            & "    Dim num2 As Integer = (text1.Length - 1)" & vbNewLine _
            & "    Dim i As Integer = 0" & vbNewLine _
            & "    Do While (i <= num2)" & vbNewLine _
            & "        Dim ch As Char = builder.Chars(i)" & vbNewLine _
            & "        ch = Strings.Chr((Strings.Asc(ch) Xor num1))" & vbNewLine _
            & "        builder2.Append(ch)" & vbNewLine _
            & "        i += 1" & vbNewLine _
            & "    Loop" & vbNewLine _
            & "    Return builder2.ToString"
            w.WriteLine(_Function)
            w.WriteLine("End Function")
            w.Flush()
        End Sub

        Public Sub CreateUnicodeGetStringFunction()
            Dim _Function As String = "Public Shared Function " & Cls_Randomize.UnicodeGetStringFunction & "(v As Byte()) As String" & vbNewLine _
          & "    Return Encoding.Unicode.GetString(v)"
            w.WriteLine(_Function)
            w.WriteLine("End Function" & vbNewLine)
            w.Flush()
        End Sub

        Public Sub CreateFromBase64Function()
            Dim _Function$ = "Public Shared Function " & Cls_Randomize.FromBase64Function & "(v As String) As Byte()" & vbNewLine _
          & "    Return Convert.FromBase64String(v)"
            w.WriteLine(_Function)
            w.WriteLine("End Function" & vbNewLine)
            w.Flush()
        End Sub

        Public Sub CreateEncodingFunction(ByVal Encode As Integer, ByVal FuncName As String, ByVal StrName As String, ByVal Salt As Integer)
            Dim _Function$ = String.Empty
            Select Case Encode
                Case 0
                    _Function = "Public Shared Function " & FuncName & "() As String" & vbNewLine _
                    & "    Return " & Cls_Randomize.EncodedClassName & "." & Cls_Randomize.UnicodeGetStringFunction & "(" & Cls_Randomize.EncodedClassName & "." & Cls_Randomize.FromBase64Function & "(" & Cls_Randomize.EncodedClassName & "." & Cls_Randomize.XorFunctionDecoderName & "(" & Cls_EncryptionType.ToAscii(StrName) & "," & Salt & ")))"
                Case 1
                    _Function = "Public Shared Function " & FuncName & "() As String" & vbNewLine _
                    & "    Return " & Cls_Randomize.EncodedClassName & "." & Cls_Randomize.XorFunctionDecoderName & "(" & Cls_EncryptionType.ToAscii(StrName) & "," & Salt & ")"
                Case 2
                    _Function = "Public Shared Function " & FuncName & "() As String" & vbNewLine _
                    & "    Return " & Cls_Randomize.EncodedClassName & "." & Cls_Randomize.UnicodeGetStringFunction & "(" & Cls_Randomize.EncodedClassName & "." & Cls_Randomize.FromBase64Function & "(" & Cls_EncryptionType.ToAscii(StrName) & "))"
                Case 3
                    _Function = "Public Shared Function " & FuncName & "() As String" & vbNewLine _
                    & "    Return " & Cls_EncryptionType.ToAscii(StrName)
            End Select
            w.WriteLine(_Function)
            w.WriteLine("End Function" & vbNewLine)
            w.Flush()
        End Sub

        Public Sub CloseStream()
            w.Flush()
            s.Close()
        End Sub
    End Class

End Namespace