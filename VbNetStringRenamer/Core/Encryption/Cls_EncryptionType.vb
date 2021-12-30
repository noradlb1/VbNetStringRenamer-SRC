Imports System.Text
Imports System.IO

Namespace Core.Encryption

    NotInheritable Class Cls_EncryptionType

        Public Shared Function XorEncoder(ByVal textToEncrypt$, ByVal key%) As String
            Dim inSb As New StringBuilder(textToEncrypt)
            Dim outSb As New StringBuilder(textToEncrypt.Length)
            Dim c As Char
            For i As Integer = 0 To textToEncrypt.Length - 1
                c = inSb(i)
                c = Chr(Asc(c) Xor key)
                outSb.Append(c)
            Next
            Return outSb.ToString()
        End Function

        Public Shared Function EncodeTo_64(ByVal toEncode$) As String
            Return Convert.ToBase64String(System.Text.Encoding.Unicode.GetBytes(toEncode))
        End Function

        Public Shared Function ToAscii(ByVal hash$) As String
            Dim sString$ = hash
            Dim nLen% = sString.Length
            Dim msg$ = ""
            For a As Integer = 1 To nLen
                msg &= " Chr" & "(" & Asc(sString.Chars(a - 1)) & ") &"
            Next a
            msg = msg.Substring(0, msg.Length - 1)
            Return msg
        End Function
    End Class

End Namespace