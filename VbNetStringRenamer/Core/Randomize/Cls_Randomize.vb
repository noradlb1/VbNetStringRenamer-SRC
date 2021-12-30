Imports System.IO
Imports System.Text

Namespace Core.Randomize
    Friend Class Cls_Randomize

        Private Shared letters$ = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz"
        Private Shared rnd As Random

        Public Sub New()
            rnd = New Random()
            Me.GenerateFuncNames()
        End Sub

        Private Sub GenerateFuncNames()
            _XorNamespace = GenerateKey(8)
            _XorClassDecoderName = GenerateKey(8)
            _XorFunctionDecoderName = GenerateKey(8)
            _EncodedNamespace = GenerateKey(8)
            _EncodedClassName = GenerateKey(8)
            _UnicodeGetStringFunction = GenerateKey(8)
            _FromBase64Function = GenerateKey(8)
        End Sub

        Public Function GenerateKey(ByVal length%) As String
            Dim buffer As Char() = New Char(length - 1) {}
            For i As Integer = 0 To length - 1
                buffer(i) = letters(rnd.Next(letters.Length))
            Next
            Return New String(buffer)
        End Function

        Private Shared _XorNamespace$ = String.Empty
        Public Shared Property XorNamespace() As String
            Get
                Return _XorNamespace
            End Get
            Set(ByVal value$)
                _XorNamespace = value
            End Set
        End Property

        Private Shared _XorClassDecoderName$ = String.Empty
        Public Shared Property XorClassDecoderName() As String
            Get
                Return _XorClassDecoderName
            End Get
            Set(ByVal value$)
                _XorClassDecoderName = value
            End Set
        End Property

        Private Shared _XorFunctionDecoderName$ = String.Empty
        Public Shared Property XorFunctionDecoderName() As String
            Get
                Return _XorFunctionDecoderName
            End Get
            Set(ByVal value$)
                _XorFunctionDecoderName = value
            End Set
        End Property

        Private Shared _EncodedNamespace$ = String.Empty
        Public Shared Property EncodedNamespace() As String
            Get
                Return _EncodedNamespace
            End Get
            Set(ByVal value$)
                _EncodedNamespace = value
            End Set
        End Property

        Private Shared _EncodedClassName$ = String.Empty
        Public Shared Property EncodedClassName() As String
            Get
                Return _EncodedClassName
            End Get
            Set(ByVal value$)
                _EncodedClassName = value
            End Set
        End Property

        Private Shared _UnicodeGetStringFunction$ = String.Empty
        Public Shared Property UnicodeGetStringFunction() As String
            Get
                Return _UnicodeGetStringFunction
            End Get
            Set(ByVal value$)
                _UnicodeGetStringFunction = value
            End Set
        End Property

        Private Shared _FromBase64Function$ = String.Empty
        Public Shared Property FromBase64Function() As String
            Get
                Return _FromBase64Function
            End Get
            Set(ByVal value$)
                _FromBase64Function = value
            End Set
        End Property
    End Class
End Namespace
