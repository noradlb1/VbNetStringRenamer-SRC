Namespace Core.Project

    NotInheritable Class Cls_SelectedProject

        Private Shared _FilePath$
        Public Shared Property FilePath() As String
            Get
                Return _FilePath
            End Get
            Set(ByVal value$)
                _FilePath = value
            End Set
        End Property

        Private Shared _FileName$
        Public Shared Property FileName() As String
            Get
                Return _FileName
            End Get
            Set(ByVal value$)
                _FileName = value
            End Set
        End Property

        Private Shared _FolderPath$
        Public Shared Property FolderPath() As String
            Get
                Return _FolderPath
            End Get
            Set(ByVal value$)
                _FolderPath = value
            End Set
        End Property

        Private Shared _ParentFolder$
        Public Shared Property ParentFolder() As String
            Get
                Return _ParentFolder
            End Get
            Set(ByVal value$)
                _ParentFolder = value
            End Set
        End Property
    End Class
End Namespace

