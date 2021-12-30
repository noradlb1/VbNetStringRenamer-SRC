Imports VbNetStringRenamer.Components

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Main
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_Main))
        Me.TsMenu = New System.Windows.Forms.ToolStrip()
        Me.TsBtnOpenProject = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.TsCbxEncryptMethod = New System.Windows.Forms.ToolStripComboBox()
        Me.toolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.TsBtnApply = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.TsBtnAbout = New System.Windows.Forms.ToolStripButton()
        Me.BgwLoadVbNetProject = New System.ComponentModel.BackgroundWorker()
        Me.BgwLoadVbFileCodeAndStrings = New System.ComponentModel.BackgroundWorker()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.LvProjectFiles = New VbNetStringRenamer.Components.Cls_ListViewEx()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.TbcSelectedVbFileInfo = New System.Windows.Forms.TabControl()
        Me.TpSelectedVbFileCode = New System.Windows.Forms.TabPage()
        Me.RtbSelectedVbFileCode = New System.Windows.Forms.RichTextBox()
        Me.TpSelectedVbFileStrings = New System.Windows.Forms.TabPage()
        Me.LvSelectedVbFileStrings = New VbNetStringRenamer.Components.Cls_ListViewEx()
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.BgwEncryptVbNetProject = New System.ComponentModel.BackgroundWorker()
        Me.TsMenu.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.TbcSelectedVbFileInfo.SuspendLayout()
        Me.TpSelectedVbFileCode.SuspendLayout()
        Me.TpSelectedVbFileStrings.SuspendLayout()
        Me.SuspendLayout()
        '
        'TsMenu
        '
        Me.TsMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.TsMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TsBtnOpenProject, Me.toolStripSeparator, Me.TsCbxEncryptMethod, Me.toolStripSeparator2, Me.TsBtnApply, Me.ToolStripSeparator1, Me.TsBtnAbout})
        Me.TsMenu.Location = New System.Drawing.Point(0, 0)
        Me.TsMenu.Name = "TsMenu"
        Me.TsMenu.Size = New System.Drawing.Size(1076, 25)
        Me.TsMenu.TabIndex = 0
        Me.TsMenu.Text = "ToolStrip1"
        '
        'TsBtnOpenProject
        '
        Me.TsBtnOpenProject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TsBtnOpenProject.Image = CType(resources.GetObject("TsBtnOpenProject.Image"), System.Drawing.Image)
        Me.TsBtnOpenProject.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TsBtnOpenProject.Name = "TsBtnOpenProject"
        Me.TsBtnOpenProject.Size = New System.Drawing.Size(23, 22)
        Me.TsBtnOpenProject.Text = "&Ouvrir"
        Me.TsBtnOpenProject.ToolTipText = "Sélectionnez votre fichier de projet VBNET (.vbproj)"
        '
        'toolStripSeparator
        '
        Me.toolStripSeparator.Name = "toolStripSeparator"
        Me.toolStripSeparator.Size = New System.Drawing.Size(6, 25)
        '
        'TsCbxEncryptMethod
        '
        Me.TsCbxEncryptMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.TsCbxEncryptMethod.Enabled = False
        Me.TsCbxEncryptMethod.Items.AddRange(New Object() {"Xor+Base64+Ascii", "Xor+Ascii", "Base64+Ascii", "Ascii"})
        Me.TsCbxEncryptMethod.Name = "TsCbxEncryptMethod"
        Me.TsCbxEncryptMethod.Size = New System.Drawing.Size(120, 25)
        Me.TsCbxEncryptMethod.ToolTipText = "Méthode d'encryption"
        '
        'toolStripSeparator2
        '
        Me.toolStripSeparator2.Name = "toolStripSeparator2"
        Me.toolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'TsBtnApply
        '
        Me.TsBtnApply.Enabled = False
        Me.TsBtnApply.Image = Global.VbNetStringRenamer.My.Resources.Resources.Apply
        Me.TsBtnApply.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TsBtnApply.Name = "TsBtnApply"
        Me.TsBtnApply.Size = New System.Drawing.Size(79, 22)
        Me.TsBtnApply.Text = "Appliquer"
        Me.TsBtnApply.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        Me.TsBtnApply.ToolTipText = "Lancez l'encryption du projet VBNET sélectionné"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'TsBtnAbout
        '
        Me.TsBtnAbout.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.TsBtnAbout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TsBtnAbout.Image = CType(resources.GetObject("TsBtnAbout.Image"), System.Drawing.Image)
        Me.TsBtnAbout.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TsBtnAbout.Name = "TsBtnAbout"
        Me.TsBtnAbout.Size = New System.Drawing.Size(23, 22)
        Me.TsBtnAbout.Text = "&?"
        Me.TsBtnAbout.ToolTipText = "À propos"
        '
        'BgwLoadVbNetProject
        '
        Me.BgwLoadVbNetProject.WorkerReportsProgress = True
        '
        'BgwLoadVbFileCodeAndStrings
        '
        Me.BgwLoadVbFileCodeAndStrings.WorkerReportsProgress = True
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 25)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.LvProjectFiles)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.TbcSelectedVbFileInfo)
        Me.SplitContainer1.Size = New System.Drawing.Size(1076, 468)
        Me.SplitContainer1.SplitterDistance = 220
        Me.SplitContainer1.TabIndex = 1
        '
        'LvProjectFiles
        '
        Me.LvProjectFiles.CheckBoxes = True
        Me.LvProjectFiles.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1})
        Me.LvProjectFiles.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LvProjectFiles.FullRowSelect = True
        Me.LvProjectFiles.LargeImageList = Me.ImageList1
        Me.LvProjectFiles.Location = New System.Drawing.Point(0, 0)
        Me.LvProjectFiles.MultiSelect = False
        Me.LvProjectFiles.Name = "LvProjectFiles"
        Me.LvProjectFiles.ShowItemToolTips = True
        Me.LvProjectFiles.Size = New System.Drawing.Size(220, 468)
        Me.LvProjectFiles.SmallImageList = Me.ImageList1
        Me.LvProjectFiles.TabIndex = 0
        Me.LvProjectFiles.UseCompatibleStateImageBehavior = False
        Me.LvProjectFiles.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Fichier(s)"
        Me.ColumnHeader1.Width = 215
        '
        'ImageList1
        '
        Me.ImageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit
        Me.ImageList1.ImageSize = New System.Drawing.Size(20, 20)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        '
        'TbcSelectedVbFileInfo
        '
        Me.TbcSelectedVbFileInfo.Controls.Add(Me.TpSelectedVbFileCode)
        Me.TbcSelectedVbFileInfo.Controls.Add(Me.TpSelectedVbFileStrings)
        Me.TbcSelectedVbFileInfo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TbcSelectedVbFileInfo.Location = New System.Drawing.Point(0, 0)
        Me.TbcSelectedVbFileInfo.Name = "TbcSelectedVbFileInfo"
        Me.TbcSelectedVbFileInfo.SelectedIndex = 0
        Me.TbcSelectedVbFileInfo.Size = New System.Drawing.Size(852, 468)
        Me.TbcSelectedVbFileInfo.TabIndex = 0
        '
        'TpSelectedVbFileCode
        '
        Me.TpSelectedVbFileCode.Controls.Add(Me.RtbSelectedVbFileCode)
        Me.TpSelectedVbFileCode.Location = New System.Drawing.Point(4, 22)
        Me.TpSelectedVbFileCode.Name = "TpSelectedVbFileCode"
        Me.TpSelectedVbFileCode.Padding = New System.Windows.Forms.Padding(3)
        Me.TpSelectedVbFileCode.Size = New System.Drawing.Size(844, 442)
        Me.TpSelectedVbFileCode.TabIndex = 0
        Me.TpSelectedVbFileCode.Text = "Code"
        Me.TpSelectedVbFileCode.UseVisualStyleBackColor = True
        '
        'RtbSelectedVbFileCode
        '
        Me.RtbSelectedVbFileCode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RtbSelectedVbFileCode.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RtbSelectedVbFileCode.Location = New System.Drawing.Point(3, 3)
        Me.RtbSelectedVbFileCode.Name = "RtbSelectedVbFileCode"
        Me.RtbSelectedVbFileCode.ReadOnly = True
        Me.RtbSelectedVbFileCode.Size = New System.Drawing.Size(838, 436)
        Me.RtbSelectedVbFileCode.TabIndex = 0
        Me.RtbSelectedVbFileCode.Text = ""
        Me.RtbSelectedVbFileCode.WordWrap = False
        '
        'TpSelectedVbFileStrings
        '
        Me.TpSelectedVbFileStrings.Controls.Add(Me.LvSelectedVbFileStrings)
        Me.TpSelectedVbFileStrings.Location = New System.Drawing.Point(4, 22)
        Me.TpSelectedVbFileStrings.Name = "TpSelectedVbFileStrings"
        Me.TpSelectedVbFileStrings.Padding = New System.Windows.Forms.Padding(3)
        Me.TpSelectedVbFileStrings.Size = New System.Drawing.Size(844, 442)
        Me.TpSelectedVbFileStrings.TabIndex = 1
        Me.TpSelectedVbFileStrings.Text = "Chaines"
        Me.TpSelectedVbFileStrings.UseVisualStyleBackColor = True
        '
        'LvSelectedVbFileStrings
        '
        Me.LvSelectedVbFileStrings.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader3, Me.ColumnHeader2})
        Me.LvSelectedVbFileStrings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LvSelectedVbFileStrings.FullRowSelect = True
        Me.LvSelectedVbFileStrings.Location = New System.Drawing.Point(3, 3)
        Me.LvSelectedVbFileStrings.Name = "LvSelectedVbFileStrings"
        Me.LvSelectedVbFileStrings.ShowItemToolTips = True
        Me.LvSelectedVbFileStrings.Size = New System.Drawing.Size(838, 436)
        Me.LvSelectedVbFileStrings.TabIndex = 0
        Me.LvSelectedVbFileStrings.UseCompatibleStateImageBehavior = False
        Me.LvSelectedVbFileStrings.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "N° de ligne"
        Me.ColumnHeader3.Width = 65
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Chaine de caractères"
        Me.ColumnHeader2.Width = 765
        '
        'BgwEncryptVbNetProject
        '
        Me.BgwEncryptVbNetProject.WorkerReportsProgress = True
        '
        'Frm_Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1076, 493)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.TsMenu)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Frm_Main"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "VbNet String Renamer"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.TsMenu.ResumeLayout(False)
        Me.TsMenu.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.TbcSelectedVbFileInfo.ResumeLayout(False)
        Me.TpSelectedVbFileCode.ResumeLayout(False)
        Me.TpSelectedVbFileStrings.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TsMenu As System.Windows.Forms.ToolStrip
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents TsBtnOpenProject As System.Windows.Forms.ToolStripButton
    Friend WithEvents TsBtnAbout As System.Windows.Forms.ToolStripButton
    Friend WithEvents TbcSelectedVbFileInfo As System.Windows.Forms.TabControl
    Friend WithEvents TpSelectedVbFileCode As System.Windows.Forms.TabPage
    Friend WithEvents TpSelectedVbFileStrings As System.Windows.Forms.TabPage
    Friend WithEvents LvProjectFiles As Cls_ListViewEx
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents BgwLoadVbNetProject As System.ComponentModel.BackgroundWorker
    Friend WithEvents toolStripSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents LvSelectedVbFileStrings As Cls_ListViewEx
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents BgwLoadVbFileCodeAndStrings As System.ComponentModel.BackgroundWorker
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents BgwEncryptVbNetProject As System.ComponentModel.BackgroundWorker
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents RtbSelectedVbFileCode As System.Windows.Forms.RichTextBox
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents TsCbxEncryptMethod As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents TsBtnApply As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator2 As System.Windows.Forms.ToolStripSeparator

End Class
