Imports System.Text
Imports System.Text.RegularExpressions
Imports System.IO
Imports System.Collections.ObjectModel
Imports System.Xml
Imports Microsoft.Win32
Imports System.Globalization
Imports VbNetStringRenamer.Core.Encryption
Imports VbNetStringRenamer.Core.Randomize
Imports VbNetStringRenamer.Core.Project
Imports VbNetStringRenamer.Core.Native

Friend Class Frm_Main

#Region "PRIVATE DECLARATIONS"
    Private _GenNames As Cls_Randomize
    Private _EncodedFile As Cls_EncryptedFileWriter
    Private _ApplyInProgress As Boolean = False
    Private _AppPath As String = String.Empty
    Private _GeneratedPath As String = String.Empty
    Private _AlreadyEncryptedProj As Boolean = False

    'Displays a form when a listviewItem is selected.
    Private strLoad As New Frm_LoadStrings()
    'Displays a form when the task of renaming string begins.
    Private StrObfuscation As New Frm_LoadStrings()

    'Occurrences of strings found.
    Private totalOccur As Integer = 0
    'RegEx String Search Pattern
    Private StrPattern As String = "(""[^""]*"")+" & "(c?)"

    Private l As Integer = 0
    Private Delegate Sub UpdateProgessCallback(ByVal txt$)

    Private _EncodeType As Integer
    Private _RelatedFunctions As Dictionary(Of String, String)
#End Region

#Region "MAIN FORM SUBS"
    Public Sub New()
        InitializeComponent()
        _AppPath = Application.StartupPath & "\VbNetStringRenamer"
        Me.TsCbxEncryptMethod.SelectedIndex = 0
    End Sub

    Private Sub Frm_Main_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Directory.Exists(_AppPath) Then
            Directory.CreateDirectory(_AppPath)
        End If
    End Sub

    Private Sub Frm_Main_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If _ApplyInProgress Then
            MessageBox.Show("Veuillez attendre la fin du processus d'encryption !", "Modification en cours", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            e.Cancel = True
        End If
    End Sub

    Private Sub Frm_Main_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SizeChanged
        Me.ColumnHeader1.Width = SplitContainer1.Panel1.Width
    End Sub

    Private Sub SplitContainer1_Panel1_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles SplitContainer1.Panel1.SizeChanged
        Me.ColumnHeader1.Width = SplitContainer1.Panel1.Width
    End Sub
#End Region
  
#Region "LOAD VBPROJ FILE"
    Private Sub TsBtnOpenProject_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TsBtnOpenProject.Click
        Dim opn As New OpenFileDialog
        With opn
            .Title = "Sélectionnez votre fichier de projet VBNET (*.vbproj)"
            .FileName = ""
            .Filter = "VB.NET-Projet |*.vbproj"
            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                Me.TsBtnApply.Enabled = True
                Me.TsCbxEncryptMethod.Enabled = True
                _AlreadyEncryptedProj = False
                LoadVbProjFile(.FileName)
            End If
        End With
    End Sub

    Private Sub LoadVbProjFile(ByVal FileName As String)
        If Not Me.BgwLoadVbNetProject.IsBusy Then
            Me.LvProjectFiles.Items.Clear()
            Me.LvSelectedVbFileStrings.Items.Clear()
            Me.RtbSelectedVbFileCode.Text = String.Empty
            _GenNames = New Cls_Randomize
            Cls_SelectedProject.FilePath = FileName
            Cls_SelectedProject.FileName = FileName.Substring(FileName.LastIndexOf("\") + 1)
            Cls_SelectedProject.FolderPath = FileName.Replace(Cls_SelectedProject.FileName, String.Empty)
            Cls_SelectedProject.ParentFolder = New FileInfo(Cls_SelectedProject.FilePath).Directory.Name.ToString
            Me.BgwLoadVbNetProject.RunWorkerAsync(FileName)
        End If
    End Sub

    Private Sub BgwLoadVbNetProject_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BgwLoadVbNetProject.DoWork
        If (New FileInfo(e.Argument.ToString).Length <> 0) Then
            Try
                Dim enumer1 As IEnumerator = Nothing
                'Retrieve Icon from .vb file Type
                Dim iconFromFileType As Icon = Me.GetIconFromFileType(".vb")
                Dim Xdoc As New XmlDocument
                Xdoc.Load(e.Argument.ToString)
                If File.Exists((New FileInfo(e.Argument.ToString).DirectoryName & "\" & New FileInfo(e.Argument.ToString).Name & ".vso")) Then
                    Me._AlreadyEncryptedProj = True
                End If
                Try
                    enumer1 = Xdoc.GetElementsByTagName("Compile").GetEnumerator
                    Do While enumer1.MoveNext
                        Dim enumer2 As IEnumerator = Nothing
                        Dim current As XmlNode = TryCast(enumer1.Current, XmlNode)
                        Try
                            enumer2 = current.Attributes.GetEnumerator
                            Do While enumer2.MoveNext
                                Dim attribute As XmlAttribute = TryCast(enumer2.Current, XmlAttribute)
                                If (Not attribute.InnerXml.ToLower.EndsWith("my project\assemblyinfo.vb".Trim) And Not attribute.InnerXml.ToLower.EndsWith(".designer.vb")) Then
                                    Dim items As String() = New String(3 - 1) {}
                                    items(0) = attribute.InnerXml
                                    items(1) = (Cls_SelectedProject.FolderPath & attribute.InnerXml)
                                    Dim item As New ListViewItem(items) With {.ToolTipText = items(1)}
                                    Dim obj As Object = New Object() {item, iconFromFileType}
                                    Me.BgwLoadVbNetProject.ReportProgress(0, obj)
                                End If
                            Loop
                            Continue Do
                        Finally
                            If TypeOf enumer2 Is IDisposable Then
                                TryCast(enumer2, IDisposable).Dispose()
                            End If
                        End Try
                    Loop
                Finally
                    If TypeOf enumer1 Is IDisposable Then
                        TryCast(enumer1, IDisposable).Dispose()
                    End If
                End Try
            Catch ex As Exception
                MsgBox("Une erreur est survenue lors du parsing du fichier .vbproj : " & ex.ToString)
            End Try
        End If

    End Sub

    Private Function GetIconFromFileType(ByVal Ext As String) As Icon
        Dim str() As String = GetFileTypeAndIcon(Ext).ToString().Split(",")
        Return System.Drawing.Icon.FromHandle(Cls_NativeMethods.ExtractIcon(IntPtr.Zero, str(0), str(1)))
    End Function

    Private Sub BgwLoadVbNetProject_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles BgwLoadVbNetProject.ProgressChanged
        If e.ProgressPercentage = 0 Then
            Dim lvi As ListViewItem = TryCast(e.UserState(0), ListViewItem)
            Dim Icon As Icon = TryCast(e.UserState(1), Icon)
            If Not Icon Is Nothing Then
                Me.ImageList1.Images.Add(Icon)
                lvi.ImageIndex = Me.ImageList1.Images.Count - 1
            End If
            lvi.Checked = True
            Me.LvProjectFiles.Items.AddRange(New ListViewItem() {lvi})
        End If
    End Sub

    Private Function GetFileTypeAndIcon() As Hashtable
        Dim ht As Hashtable = Nothing
        Try
            Dim hkcr As RegistryKey = Registry.ClassesRoot
            Dim subKeyNames As String() = hkcr.GetSubKeyNames
            Dim ht0 As New Hashtable
            For Each str As String In subKeyNames
                If (Not String.IsNullOrEmpty(str) AndAlso (str.IndexOf(".") = 0)) Then
                    Dim k As RegistryKey = hkcr.OpenSubKey(str)
                    If (Not k Is Nothing) Then
                        Dim objVal As Object = k.GetValue("")
                        If ((Not objVal Is Nothing) AndAlso (str.ToString.ToLower = ".vb")) Then
                            Dim name As String = (objVal.ToString & "\DefaultIcon")
                            Dim k0 As RegistryKey = hkcr.OpenSubKey(name)
                            If (Not k0 Is Nothing) Then
                                Dim objVal0 As Object = k0.GetValue("")
                                If (Not objVal0 Is Nothing) Then
                                    Dim str3 As String = objVal0.ToString.Replace("""", "")
                                    ht0.Add(str, str3)
                                End If
                                k0.Close()
                            End If
                            k.Close()
                        End If
                    End If
                End If
            Next
            hkcr.Close()
            ht = ht0
        Catch ex As Exception
            MsgBox("Une erreur est survenue lors du parsing du registre : " & ex.ToString)
        End Try
        Return ht
    End Function

    Private Sub BgwLoadVbNetProject_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BgwLoadVbNetProject.RunWorkerCompleted
        If Me.LvProjectFiles.Items.Count <> 0 Then
            If _AlreadyEncryptedProj Then
                Me.TsCbxEncryptMethod.Enabled = False
                Me.TsBtnApply.Enabled = False
            Else
                Me.TsCbxEncryptMethod.Enabled = True
                Me.TsBtnApply.Enabled = True
            End If
        End If
    End Sub
#End Region

#Region "ANALYZE VB FILE"
    Private Sub LvProjectFiles_ItemSelectionChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.ListViewItemSelectionChangedEventArgs) Handles LvProjectFiles.ItemSelectionChanged
        If e.IsSelected Then
            l = 0
            TpSelectedVbFileCode.Text = "Code source"
            TpSelectedVbFileStrings.Text = "Chaines de type 'String'"
            strLoad.Label1.Text = "0 chaines trouvées ..."
            Me.LoadVbStrings(e.Item)
        Else
            Me.RtbSelectedVbFileCode.Text = Nothing
            Me.LvSelectedVbFileStrings.Items.Clear()
        End If
    End Sub

    Private Sub LoadVbStrings(ByVal lvi As ListViewItem)
        If Not Me.BgwLoadVbFileCodeAndStrings.IsBusy Then
            Me.SplitContainer1.Enabled = False
            Me.LvSelectedVbFileStrings.Items.Clear()
            Me.RtbSelectedVbFileCode.Text = String.Empty
            strLoad.title = "Recherche de chaines de type 'String' ..."
            Me.BgwLoadVbFileCodeAndStrings.RunWorkerAsync(lvi)
        End If
    End Sub

    Private Sub BgwLoadVbFileCodeAndStrings_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BgwLoadVbFileCodeAndStrings.DoWork
        Dim arg As ListViewItem = TryCast(e.Argument, ListViewItem)
        Dim read As New StreamReader(arg.SubItems.Item(1).Text)
        Dim num As Integer = 0
        Me.BgwLoadVbFileCodeAndStrings.ReportProgress(-2, Nothing)
        Me.totalOccur = 0
        Do While Not read.EndOfStream
            Dim input As String = read.ReadLine
            Me.Invoke(New UpdateProgessCallback(AddressOf Me.updateprogress), New Object() {(input & ChrW(13) & ChrW(10))})
            num += 1
            If (((Not (input.Contains("Declare") And input.Contains("Lib")) AndAlso Not input.TrimStart(New Char(0 - 1) {}).StartsWith("'")) AndAlso (Not (input.Contains("<") And input.Contains(")>")) AndAlso Not input.EndsWith(")> _"))) AndAlso (Not (input.Contains("<") And input.Contains(" _")) AndAlso Not (input.Contains("#Region") And input.EndsWith("""")))) Then
                Dim str As String = ""
                Dim matchs As MatchCollection = Regex.Matches(input, Me.StrPattern, RegexOptions.IgnoreCase)
                Dim num3 As Integer = (matchs.Count - 1)
                Dim i As Integer = 0
                Do While (i <= num3)
                    str = matchs.Item(i).Value.ToString
                    If Not str.EndsWith("""c") Then
                        Dim items As String() = New String(3 - 1) {}
                        items(0) = num.ToString
                        items(1) = str.Substring(1, (matchs.Item(i).Length - 2))
                        Me.totalOccur += 1
                        Me.BgwLoadVbFileCodeAndStrings.ReportProgress(Me.totalOccur, Nothing)
                        Dim userState As New ListViewItem(items) With {.ToolTipText = items(1)}
                        Me.BgwLoadVbFileCodeAndStrings.ReportProgress(-1, userState)
                    End If
                    i += 1
                Loop
            End If
        Loop
        read.Close()
    End Sub

    Private Sub updateprogress(ByVal txt$)
        'Fill the RichTextBox with the syntax VBNet language colorization
        Me.RtbSelectedVbFileCode.AppendText(txt)
        Dim firstCharIndexFromLine As Integer = Me.RtbSelectedVbFileCode.GetFirstCharIndexFromLine(Me.l)
        If txt.TrimStart(New Char() {" "c}).StartsWith("'") Then
            Me.RtbSelectedVbFileCode.SelectionStart = Me.RtbSelectedVbFileCode.SelectionStart
            Me.RtbSelectedVbFileCode.Select(firstCharIndexFromLine, (txt.Length + firstCharIndexFromLine))
            Me.RtbSelectedVbFileCode.SelectionColor = Color.Green
            Me.RtbSelectedVbFileCode.SelectionLength = 0
        Else
            Dim enumer1 As IEnumerator = Nothing
            Dim enumer2 As IEnumerator = Nothing
            Dim reservedString As String = "AddHandler|AddressOf|Alias|And|AndAlso|Ansi|Append|As|Assembly|Auto|Binary|Boolean|ByRef|Byte|ByVal|Call|Case|Catch|CBool|CByte|CChar|CDate|CDec|CDbl|Char|" _
                                        & "CInt|Class|CLng|CObj|Compare|Const|Continue|CByte|CShort|CSng|CStr|CType|CUInt|CULng|CUShort|Date|Decimal|Declare|Default|Delegate|" _
                                        & "Dim|DirectCast|Do|Double|Each|Else|ElseIf|End|EndIf|Enum|Erase|Error|Event|Explicit|Exit|False|Finally|For|Friend|Function|Get|GetType|" _
                                        & "Global|GoSub|GoTo|Handles|If|Implements|Imports|In|Inherits|Input|Integer|Interface|Is|IsNot|Let|Lib|Like|Lock|Long|Loop|Me|Mid|Mod|Module|" _
                                        & "MustInherit|MustOverride|MyBase|MyClass|Namespace|Narrowing|New|Next|Not|Nothing|NotInheritable|NotOverridable|Object|Of|Off|On|" _
                                        & "Operator|Option|Optional|Or|OrElse|Output|Overloads|Overridable|Overrides|ParamArray|Preserve|Partial|Private|Property|Protected|Public|RaiseEvent" _
                                        & "Random|Read|ReadOnly|ReDim|Rem|RemoveHandler|Resume|Return|SByte|Seek|Select|Set|Shadows|Shared|Short|Single|Static|Step|Stop|Strict|String|Structure|" _
                                        & "Sub|SyncLock|Then|Throw|To|True|Try|TryCast|TypeOf|Variant|Wend|UInteger|ULong|Until|UShort|Using|When|While|Widening|With|WithEvents|" _
                                        & "WriteOnly|Xor|Region"

            Dim match1 As MatchCollection = New Regex(("\b(" & reservedString & ")\b"), RegexOptions.IgnoreCase).Matches(txt)
            Dim selectionStart As Integer = Me.RtbSelectedVbFileCode.SelectionStart
            Try
                enumer1 = match1.GetEnumerator
                Do While enumer1.MoveNext
                    Dim current As Match = TryCast(enumer1.Current, Match)
                    Dim start As Integer = (current.Index + firstCharIndexFromLine)
                    Dim length As Integer = current.Length
                    Me.RtbSelectedVbFileCode.Select(start, length)
                    Me.RtbSelectedVbFileCode.SelectionColor = Color.Blue
                    Me.RtbSelectedVbFileCode.SelectionStart = selectionStart
                    Me.RtbSelectedVbFileCode.SelectionColor = Color.Black
                Loop
            Finally
                If TypeOf enumer1 Is IDisposable Then
                    TryCast(enumer1, IDisposable).Dispose()
                End If
            End Try
            Dim match2 As MatchCollection = Regex.Matches(txt, Me.StrPattern, RegexOptions.IgnoreCase)
            Try
                enumer2 = match2.GetEnumerator
                Do While enumer2.MoveNext
                    Dim match3 As Match = TryCast(enumer2.Current, Match)
                    Dim num5 As Integer = (match3.Index + firstCharIndexFromLine)
                    Dim num6 As Integer = match3.Length
                    Me.RtbSelectedVbFileCode.Select(num5, num6)
                    Me.RtbSelectedVbFileCode.SelectionColor = Color.Red
                    Me.RtbSelectedVbFileCode.SelectionStart = selectionStart
                Loop
            Finally
                If TypeOf enumer2 Is IDisposable Then
                    TryCast(enumer2, IDisposable).Dispose()
                End If
            End Try
        End If
        Me.l += 1
    End Sub

    Private Sub BgwLoadVbFileCodeAndStrings_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles BgwLoadVbFileCodeAndStrings.ProgressChanged
        If e.ProgressPercentage = -1 Then
            Dim lvi As ListViewItem = TryCast(e.UserState, ListViewItem)
            Me.LvSelectedVbFileStrings.Items.Add(lvi)
        ElseIf e.ProgressPercentage = -2 Then
            strLoad.ShowDialog()
        Else
            strLoad.Label1.Text = e.ProgressPercentage.ToString & " chaines trouvées ..."
        End If
    End Sub

    Private Sub BgwLoadVbFileCodeAndStrings_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BgwLoadVbFileCodeAndStrings.RunWorkerCompleted
        Me.SplitContainer1.Enabled = True
        strLoad.Close()
        If _AlreadyEncryptedProj Then
            Me.TsCbxEncryptMethod.Enabled = False
            Me.TsBtnApply.Enabled = False
        End If
    End Sub
#End Region
   
#Region "RENAME TASK"
    Private Sub TsBtnApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TsBtnApply.Click
        If (Not Me.BgwEncryptVbNetProject.IsBusy AndAlso (Me.LvProjectFiles.Items.Count <> 0)) Then
            If Not Me.isRunningProcess("devenv") Then
                Dim enumer1 As IEnumerator = Nothing
                Dim arg As New ObservableCollection(Of ListViewItem)
                Try
                    enumer1 = Me.LvProjectFiles.Items.GetEnumerator
                    Do While enumer1.MoveNext
                        Dim current As ListViewItem = TryCast(enumer1.Current, ListViewItem)
                        If current.Checked Then
                            arg.Add(current)
                        End If
                    Loop
                Finally
                    If TypeOf enumer1 Is IDisposable Then
                        TryCast(enumer1, IDisposable).Dispose()
                    End If
                End Try
                If (arg.Count > 0) Then
                    Me._ApplyInProgress = True
                    Me.StrObfuscation.title = "Encryption des chaines en cours ..."
                    Me._GeneratedPath = String.Format(("{0}\{1}-" & Cls_SelectedProject.ParentFolder), Me._AppPath, DateTime.Now.ToString("ddMMyyyy-HHmmss", CultureInfo.InvariantCulture))
                    Me._EncodedFile = New Cls_EncryptedFileWriter((Me._GeneratedPath & "\" & Me._GenNames.GenerateKey(8) & ".vb"))
                    My.Computer.FileSystem.CopyDirectory(Cls_SelectedProject.FolderPath, Me._GeneratedPath, True)
                    Me.BgwEncryptVbNetProject.RunWorkerAsync(arg)
                Else
                    MessageBox.Show("Veuillez cocher au minimum un fichier pour l'encryption !", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
            Else
                MessageBox.Show("Veuillez fermer Visual Studio avant de poursuivre !", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        End If

    End Sub

    Private Function isRunningProcess(ByVal Name As String) As Boolean
        For Each p As Process In Process.GetProcesses(".")
            Try
                If p.ProcessName.ToString() = Name Then
                    Return True
                End If
            Catch
                Return False
            End Try
        Next
        Return False
    End Function

    Private Function ReplaceDoubleQuotes(ByVal m As Match) As String
        Dim str As String = String.Empty
        Dim num As Integer = CInt(Math.Round(CDbl((CDbl(m.Value.Length) / 2))))
        Dim i As Integer = 1
        Do While (i <= num)
            str = (str & """")
            i += 1
        Loop
        Return str
    End Function

    Private Function IsWhitespace(ByVal S As String) As Boolean
        Dim l As Integer = S.Length
        Dim n0 As Integer = 0
        Dim n1 As Integer = (S.Length - 1)
        Dim i As Integer = 0
        Do While (i <= n1)
            If Char.IsWhiteSpace(S, i) Then
                n0 += 1
            End If
            i += 1
        Loop
        Return (n0 = l)
    End Function

    Private Function ReplaceOcc(ByVal m As Match) As String
        Dim str As String = String.Empty
        Dim inp As String = String.Empty
        If String.IsNullOrEmpty(m.Value.Substring(1, (m.Length - 2))) Then
            Return m.Value
        End If
        If ((m.Value.Substring(1, (m.Length - 2)).Length <> 0) And Me.IsWhitespace(m.Value.Substring(1, (m.Length - 2)))) Then
            Return m.Value
        End If
        If m.Value.EndsWith("c") Then
            Return m.Value
        End If
        Dim regex As New Regex("("")\1+")
        inp = m.Value.Substring(1, (m.Length - 2))
        Dim eval As MatchEvaluator = New MatchEvaluator(AddressOf Me.ReplaceDoubleQuotes)
        inp = regex.Replace(inp, eval)
        If Me._RelatedFunctions.ContainsKey(inp) Then
            Return Me._RelatedFunctions.Item(inp)
        End If
        Dim salt As Integer = New Random().Next(1, 255)
        Dim funcName As String = Me._GenNames.GenerateKey(8)
        Select Case Me._EncodeType
            Case 0
                Me._EncodedFile.CreateEncodingFunction(Me._EncodeType, funcName, Cls_EncryptionType.XorEncoder(Cls_EncryptionType.EncodeTo_64(inp), salt), salt)
                Exit Select
            Case 1
                Me._EncodedFile.CreateEncodingFunction(Me._EncodeType, funcName, Cls_EncryptionType.XorEncoder(inp, salt), salt)
                Exit Select
            Case 2
                Me._EncodedFile.CreateEncodingFunction(Me._EncodeType, funcName, Cls_EncryptionType.EncodeTo_64(inp), salt)
                Exit Select
            Case 3
                Me._EncodedFile.CreateEncodingFunction(Me._EncodeType, funcName, inp, salt)
                Exit Select
        End Select
        str = (Cls_Randomize.EncodedClassName & "." & funcName)
        Me._RelatedFunctions.Add(inp, (Cls_Randomize.EncodedClassName & "." & funcName))
        Return str
    End Function

    Private Sub BgwEncryptVbNetProject_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BgwEncryptVbNetProject.DoWork
        Try
            Me.BgwEncryptVbNetProject.ReportProgress(-2, Nothing)
            'Create the file which will contains renamed functions
            Me._EncodedFile.OpenWriter()
            Me._EncodedFile.WriteLine(("Imports System.Text" & vbNewLine & vbNewLine & "Friend Class " & Cls_Randomize.EncodedClassName & vbNewLine))
            Me._RelatedFunctions = New Dictionary(Of String, String)
            Dim item As ListViewItem
            For Each item In TryCast(e.Argument, ObservableCollection(Of ListViewItem))
                Dim reader As New StreamReader(item.SubItems.Item(1).Text)
                Dim key As Integer = 0
                Dim dictionary As New Dictionary(Of Integer, String)
                Dim txt As String = String.Empty
                Do While Not reader.EndOfStream
                    Dim str As String = reader.ReadLine
                    key += 1
                    txt = (txt & str & vbNewLine)
                    dictionary.Add(key, str)
                    'Exclude Special chars and names
                    If ((((Not (str.Contains("Declare") And str.Contains("Lib")) AndAlso Not str.TrimStart(New Char(0 - 1) {}).StartsWith("'")) AndAlso (Not (str.Contains("<") And str.Contains(")>")) AndAlso Not str.EndsWith(")> _"))) AndAlso (Not (str.Contains("<") And str.Contains(" _")) AndAlso Not (str.Contains("#Region") And str.EndsWith("""")))) AndAlso (Regex.Matches(str, Me.StrPattern, RegexOptions.IgnoreCase).Count <> 0)) Then
                        Dim input As String = str
                        Me.Invoke(New UpdateProgessCallback(AddressOf Me.obfuscationprogress), New Object() {item.Text})
                        Dim regex As New Regex(Me.StrPattern)
                        Dim evaluator As MatchEvaluator = New MatchEvaluator(AddressOf Me.ReplaceOcc)
                        input = regex.Replace(input, evaluator)
                        txt = txt.Replace(dictionary.Item(key), input)
                        dictionary.Remove(key)
                        dictionary.Add(key, input)
                    End If
                Loop
                reader.Close()
                My.Computer.FileSystem.WriteAllText((Me._GeneratedPath & "\" & item.Text), txt, False)
            Next
            Select Case Me._EncodeType
                Case 0
                    Me._EncodedFile.CreateXorDecoderFunction()
                    Me._EncodedFile.CreateUnicodeGetStringFunction()
                    Me._EncodedFile.CreateFromBase64Function()
                    Exit Select
                Case 1
                    Me._EncodedFile.CreateXorDecoderFunction()
                    Exit Select
                Case 2
                    Me._EncodedFile.CreateUnicodeGetStringFunction()
                    Me._EncodedFile.CreateFromBase64Function()
                    Exit Select
            End Select
            Me._EncodedFile.WriteLine("End Class" & vbNewLine)
            Me._EncodedFile.CloseStream()
            'Update .vbProj File
            Dim document As New XmlDocument
            document.Load((Me._GeneratedPath & "\" & Cls_SelectedProject.FileName))
            Dim node As XmlNode = document.GetElementsByTagName("Compile").ItemOf(0)
            Dim newChild As XmlElement = document.CreateElement("Compile", document.DocumentElement.NamespaceURI)
            Dim node2 As XmlNode = newChild
            newChild.SetAttribute("Include", New FileInfo(Me._EncodedFile.EncodedFilePath).Name)
            node.ParentNode.AppendChild(newChild)
            File.WriteAllText((New FileInfo(Me._EncodedFile.EncodedFilePath).DirectoryName & "\" & New FileInfo(Cls_SelectedProject.FileName).Name & ".vso"), "")
            document.Save((Me._GeneratedPath & "\" & Cls_SelectedProject.FileName))
            Me.Invoke(New UpdateProgessCallback(AddressOf Me.obfuscationprogress), New Object() {"Close"})
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

    End Sub

    Private Sub obfuscationprogress(ByVal str As String)
        If str = "Close" Then
            StrObfuscation.Close()
        Else
            StrObfuscation.Label1.Text = "Obfuscation des chaines du fichier : '" & str & "'"
        End If
    End Sub

    Private Sub BgwEncryptVbNetProject_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles BgwEncryptVbNetProject.ProgressChanged
        If e.ProgressPercentage = -2 Then
            StrObfuscation.ShowDialog()
        End If
    End Sub

    Private Sub BgwEncryptVbNetProject_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BgwEncryptVbNetProject.RunWorkerCompleted
        _ApplyInProgress = False
        If MessageBox.Show("L'Encryption s'est achevée correctement." & vbNewLine & "Voulez-vous charger le projet encrypté ?", "Fin de l'opération", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Me.LoadVbProjFile(_GeneratedPath & "\" & Cls_SelectedProject.FileName)
        End If
    End Sub

#End Region

#Region "ENCRYPTION TYPE"
    Private Sub TsCbxEncryptMethod_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TsCbxEncryptMethod.SelectedIndexChanged
        _EncodeType = Me.TsCbxEncryptMethod.SelectedIndex
    End Sub
#End Region
  
#Region "ABOUT"
    Private Sub TsBtnAbout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TsBtnAbout.Click
        Dim About As New Frm_About
        About.ShowDialog()
    End Sub
#End Region
 
End Class


