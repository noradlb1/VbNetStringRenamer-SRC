Friend Class Frm_LoadStrings
    Public Sub New()
        InitializeComponent()
    End Sub

    Private _title$
    Public Property title() As String
        Get
            Return _title
        End Get
        Set(value$)
            _title = value
        End Set
    End Property

    Private Sub Frm_LoadStrings_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.Text = title
    End Sub
End Class