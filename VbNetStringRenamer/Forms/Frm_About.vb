Friend NotInheritable Class Frm_About

    Private Sub Frm_About_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Définissez le titre du formulaire.
        Dim ApplicationTitle As String
        If My.Application.Info.Title <> "" Then
            ApplicationTitle = My.Application.Info.Title
        Else
            ApplicationTitle = System.IO.Path.GetFileNameWithoutExtension(My.Application.Info.AssemblyName)
        End If
        Me.Text = String.Format("À propos de {0}", ApplicationTitle)
        ' Initialisez tout le texte affiché dans la boîte de dialogue À propos de.
        ' TODO: personnalisez les informations d'assembly de l'application dans le volet "Application" de la 
        '    boîte de dialogue Propriétés du projet (sous le menu "Projet").
        Me.LabelProductName.Text = My.Application.Info.ProductName
        Me.LabelVersion.Text = String.Format("Version {0}", My.Application.Info.Version.ToString)
        Me.LabelCopyright.Text = My.Application.Info.Copyright
        'Me.LabelCompanyName.Text = ""
        Me.TextBoxDescription.Text = "Cet utilitaire permet de charger un projet de type VbNet et de crypter les chaines de caractères (type 'String') présentent dans l'ensemble du projet. Enfin lorsque vous compilerez votre binaire ce dernier découragera les moins aguérrits du reverse engineering lorsqu'ils décideront d'ouvrir votre programme avec Relector ou autre..."
    End Sub

    Private Sub BtnAboutClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAboutClose.Click
        Me.Close()
    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        Try
            Process.Start(LinkLabel2.Text)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Try
            Process.Start(LinkLabel1.Text)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub LogoPictureBox_Click(sender As System.Object, e As System.EventArgs) Handles LogoPictureBox.Click
        Try
            Process.Start(LinkLabel2.Text)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub PictureBox1_Click(sender As System.Object, e As System.EventArgs) Handles PictureBox1.Click
        Try
            Process.Start("http://creativecommons.org/licenses/by-nc-nd/3.0/fr/")
        Catch ex As Exception
        End Try
    End Sub


End Class
