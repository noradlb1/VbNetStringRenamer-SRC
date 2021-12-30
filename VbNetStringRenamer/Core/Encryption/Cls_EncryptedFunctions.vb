Namespace Core.Encryption
    NotInheritable Class Cls_EncryptedFunctions

        Private _OriginalString$
        Public Property OriginalString() As String
            Get
                Return _OriginalString
            End Get
            Set(ByVal value$)
                _OriginalString = value
            End Set
        End Property

        Private _CalledFunction$
        Public Property CalledFunction() As String
            Get
                Return _CalledFunction
            End Get
            Set(ByVal value$)
                _CalledFunction = value
            End Set
        End Property

        Public Sub New(ByVal Original$, ByVal Called$)
            _OriginalString = Original
            _CalledFunction = Called
        End Sub

    End Class
End Namespace