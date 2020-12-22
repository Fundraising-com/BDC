Imports StoreFront.Systembase
Imports StoreFront.BusinessRule
Imports StoreFront.BusinessRule.Management
Partial  Class ProductImagesControl
    Inherits CWebControl


    Private m_uid As Long
    Private objProdManagement As CProductManagement
    
#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Tee 8/7/2007 product configurator
        m_uid = Request.QueryString("ID")
        If m_uid = 0 Then
            m_uid = Session("ProductId")
        Else
            Session("ProductId") = m_uid
        End If
        If IsNothing(m_uid) Then
            Exit Sub
        End If
        objProdManagement = New Management.CProductManagement(m_uid)
        MakeCommonVisible(objProdManagement.ProductType, False)
        'end Tee

        cmdApply.Attributes.Add("onclick", "javascript: return confirm('Changing the Attributes with Images selection will reset all product swatches related to attributes for this product.  Do you want to continue with the change?')")
        If IsPostBack Then
            m_uid = ProdUID.Value
        Else

            ProdUID.Value = m_uid
            'BEGIN CUSTOM CODE 3/3/04
            SwatchesPerRow.Text = objProdManagement.SwatchesPerRow
            Swatches.DataSource = objProdManagement.Swatch
            Swatches.DataBind()
            'END CUSTOM CODE 3/3/04
            'BEGIN CUSTOM CODE Oct '04
            CloseUpLinkText.Text = objProdManagement.CloseUpLinkText
            LinkBigImage.Checked = objProdManagement.LinkBigImage
            ShowCloseUpLink.Checked = objProdManagement.ShowCloseUpLink
            ChangeOnClick.Checked = objProdManagement.ChangeOnClick
            ChangeOnMouseover.Checked = objProdManagement.ChangeOnMouseover
            If ChangeOnMouseover.Checked = False And ChangeOnClick.Checked = False Then
                ChangeOnMouseover.Checked = True
            End If
            SwatchAllignment.SelectedIndex = objProdManagement.SwatchAllignment
            DescriptionAllignment.SelectedIndex = objProdManagement.DescriptionAllignment
            'END CUSTOM CODE Oct '04

            Dim oAttributeManager As New CAttributeManagement
            'Tee 14/2/2008 bug 1134 fix
            oAttributeManager.ExcludeNoChildAttribute = True
            oAttributeManager.ProductID = m_uid
            Me.cblAttributes.DataSource = oAttributeManager.Attributes
            oAttributeManager.ExcludeNoChildAttribute = False
            'end Tee
            Me.cblAttributes.DataValueField = "uid"
            Me.cblAttributes.DataTextField = "Name"
            Me.cblAttributes.DataBind()

            If objProdManagement.Swatch.Count > 0 Then
                Dim asAttributeNames() As String = CType(objProdManagement.Swatch(0), Swatch).AttributeNames.Split(",")
                For Each oListItem As ListItem In Me.cblAttributes.Items
                    For Each sAttributeName As String In asAttributeNames
                        If sAttributeName = oListItem.Text Then
                            oListItem.Selected = True
                            Exit For
                        End If
                    Next
                Next
            End If
        End If
    End Sub

    Public Sub DeleteSwatch(ByVal sender As System.Object, ByVal e As System.EventArgs)
        objProdManagement = New CProductManagement(m_uid)
        objProdManagement.DeleteSwatch(sender.commandname)
        objProdManagement.ResetSwatches()
        Swatches.DataSource = objProdManagement.Swatch
        Swatches.DataBind()
    End Sub

    Public Sub MoveUp(ByVal sender As System.Object, ByVal e As System.EventArgs)
        objProdManagement = New CProductManagement(m_uid)
        objProdManagement.MoveSwatch(sender.commandname, -1)
        Swatches.DataSource = objProdManagement.Swatch
        Swatches.DataBind()

    End Sub

    Public Sub MoveDown(ByVal sender As System.Object, ByVal e As System.EventArgs)
        objProdManagement = New CProductManagement(m_uid)
        objProdManagement.MoveSwatch(sender.commandname, 1)
        Swatches.DataSource = objProdManagement.Swatch
        Swatches.DataBind()
    End Sub

    Private Sub swatches_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles Swatches.ItemCreated
        If e.Item.ItemIndex > -1 And IsNothing(Swatches.DataSource) = False Then
            If Swatches.DataSource.Count = 1 Then
                e.Item.FindControl("lnkMoveUp").Visible = False
                e.Item.FindControl("lnkMoveDown").Visible = False
            ElseIf e.Item.ItemIndex = 0 Then
                e.Item.FindControl("lnkMoveUp").Visible = False
                e.Item.FindControl("lnkMoveDown").Visible = True
            ElseIf e.Item.ItemIndex = Swatches.DataSource.Count - 1 Then
                e.Item.FindControl("lnkMoveUp").Visible = True
                e.Item.FindControl("lnkMoveDown").Visible = False
            Else
                e.Item.FindControl("lnkMoveUp").Visible = True
                e.Item.FindControl("lnkMoveDown").Visible = True
            End If
            If CType(e.Item.DataItem, Swatch).AttributeNames.Length > 0 Then
                e.Item.FindControl("cmdDelete").Visible = False
            End If
        End If
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Dim objError As Label
        objError = CType(Me.Parent.FindControl("ErrorMessage"), Label)
        objError.Visible = False

        objProdManagement = New CProductManagement(m_uid)
        ProdUID.Value = m_uid
        'BEGIN CUSTOM CODE Oct '04
        objProdManagement.CloseUpLinkText = CloseUpLinkText.Text
        objProdManagement.LinkBigImage = LinkBigImage.Checked
        objProdManagement.ShowCloseUpLink = ShowCloseUpLink.Checked
        objProdManagement.ChangeOnClick = ChangeOnClick.Checked
        objProdManagement.ChangeOnMouseover = ChangeOnMouseover.Checked
        objProdManagement.SwatchAllignment = SwatchAllignment.SelectedIndex
        objProdManagement.DescriptionAllignment = DescriptionAllignment.SelectedIndex
        'END CUSTOM CODE Oct '04
        'BEGIN CUSTOM CODE 3/3/04
        objProdManagement.SwatchesPerRow = SwatchesPerRow.Text
        Dim x As Integer

        For x = 0 To Me.Swatches.Items.Count - 1
            objProdManagement.Swatch(x).Description = CType(Swatches.Items(x).FindControl("SwatchDescription"), TextBox).Text
            objProdManagement.Swatch(x).showDescription = CType(Swatches.Items(x).FindControl("ShowDescription"), CheckBox).Checked
            objProdManagement.Swatch(x).ShowImage = CType(Swatches.Items(x).FindControl("ShowImage"), CheckBox).Checked

            Dim uc As UploadControl
            uc = CType(Swatches.Items(x).FindControl("LittleImage"), UploadControl)
            'Tee 2/11/2008 bug 1116 fix
            If Not (uc.FileText.ToLower.StartsWith("http://") OrElse uc.FileText.ToLower.StartsWith("https://")) Then
                If uc.FileText.ToLower.StartsWith("images/") = False Then
                    If uc.FileText <> "" Then
                        uc.FileText = "images/" & uc.FileText
                    End If
                End If
            End If

            objProdManagement.Swatch(x).littleimage = uc.FileText

            uc = CType(Swatches.Items(x).FindControl("ThumbnailImage"), UploadControl)
            If Not (uc.FileText.ToLower.StartsWith("http://") OrElse uc.FileText.ToLower.StartsWith("https://")) Then
                If uc.FileText.ToLower.StartsWith("images/") = False Then
                    If uc.FileText <> "" Then
                        uc.FileText = "images/" & uc.FileText
                    End If
                End If
            End If

            objProdManagement.Swatch(x).thumbnailimage = uc.FileText

            uc = CType(Swatches.Items(x).FindControl("BigImage"), UploadControl)

            If Not (uc.FileText.ToLower.StartsWith("http://") OrElse uc.FileText.ToLower.StartsWith("https://")) Then
                If uc.FileText.ToLower.StartsWith("images/") = False Then
                    If uc.FileText <> "" Then
                        uc.FileText = "images/" & uc.FileText
                    End If
                End If
            End If

            objProdManagement.Swatch(x).LargeImage = uc.FileText
            'BEGIN CUSTOM CODE Oct '04
            uc = CType(Swatches.Items(x).FindControl("CloseUpImage"), UploadControl)
            If Not (uc.FileText.ToLower.StartsWith("http://") OrElse uc.FileText.ToLower.StartsWith("https://")) Then
                If uc.FileText.ToLower.StartsWith("images/") = False Then
                    If uc.FileText <> "" Then
                        uc.FileText = "images/" & uc.FileText
                    End If
                End If
            End If
            'end Tee
            objProdManagement.Swatch(x).CloseUpImage = uc.FileText
            'END CUSTOM CODE Sept '04
        Next
        'END CUSTOM CODE 3/3/04
        objProdManagement.update()
        Swatches.DataSource = objProdManagement.Swatch
        Swatches.DataBind()
    End Sub
    'END CUSTOM CODE 3/3/04

    Private Sub cmdApply_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdApply.Click
        Me.objProdManagement = New CProductManagement(Me.m_uid)
        Me.objProdManagement.DeleteAllSwatchWithAttributes(Me.m_uid)

        Dim aoSelectedAttributes() As ArrayList = {}
        Dim sAttributeNames As String = ""
        Dim oAttributeManager As New CAttributeManagement
        oAttributeManager.ProductID = m_uid
        For Each oAttribute As CAttribute In oAttributeManager.Attributes
            For Each oListItem As ListItem In Me.cblAttributes.Items
                If oListItem.Value = oAttribute.UID AndAlso oListItem.Selected Then
                    ReDim Preserve aoSelectedAttributes(aoSelectedAttributes.Length)
                    aoSelectedAttributes(aoSelectedAttributes.Length - 1) = oAttribute.AttributeDetails
                    If sAttributeNames.Length > 0 Then
                        sAttributeNames += ","
                    End If
                    sAttributeNames += oAttribute.Name
                End If
            Next
        Next
        If aoSelectedAttributes.Length > 0 Then
            Dim asCombinations() As String = Me.GetAttributeCombinations(aoSelectedAttributes)

            For Each sCombination As String In asCombinations
                Dim sw As New SystemBase.Swatch
                sw.ProductID = m_uid
                sw.Name = sCombination
                sw.AttributeNames = sAttributeNames
                sw.ShowImage = True
                objProdManagement.AddSwatch(sw, objProdManagement.Swatch.Count)
            Next
        End If
        objProdManagement.ResetSwatches()

        Swatches.DataSource = objProdManagement.Swatch
        Swatches.DataBind()
    End Sub

    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        objProdManagement = New CProductManagement(m_uid)
        Dim sw As New SystemBase.Swatch
        sw.ProductID = m_uid
        sw.Name = Me.NewName.Text
        sw.Description = Me.NewDescription.Text
        sw.ShowDescription = Me.NewDisplayDescription.Checked()
        sw.ShowImage = Me.NewDisplayImage.Checked()
        'Tee 2/11/2008 bug 1116 fix
        If Not (NewLittleImage.FileText.ToLower.StartsWith("http://") OrElse NewLittleImage.FileText.ToLower.StartsWith("https://")) Then
            If Me.NewLittleImage.FileText.ToLower.StartsWith("images/") = False Then
                If Me.NewLittleImage.FileText <> "" Then
                    Me.NewLittleImage.FileText = "images/" & Me.NewLittleImage.FileText
                End If
            End If
        End If

        sw.LittleImage = Me.NewLittleImage.FileText

        If Not (NewThumbnailImage.FileText.ToLower.StartsWith("http://") OrElse NewThumbnailImage.FileText.ToLower.StartsWith("https://")) Then
            If Me.NewThumbnailImage.FileText.ToLower.StartsWith("images/") = False Then
                If Me.NewThumbnailImage.FileText <> "" Then
                    Me.NewThumbnailImage.FileText = "images/" & Me.NewThumbnailImage.FileText
                End If
            End If
        End If

        sw.ThumbnailImage = Me.NewThumbnailImage.FileText

        If Not (NewBigImage.FileText.ToLower.StartsWith("http://") OrElse NewBigImage.FileText.ToLower.StartsWith("https://")) Then
            If Me.NewBigImage.FileText.ToLower.StartsWith("images/") = False Then
                If Me.NewBigImage.FileText <> "" Then
                    Me.NewBigImage.FileText = "images/" & Me.NewBigImage.FileText
                End If
            End If
        End If

        sw.LargeImage = Me.NewBigImage.FileText

        'BEGIN CUSTOM CODE Sept '04
        If Not (NewCloseUpImage.FileText.ToLower.StartsWith("http://") OrElse NewCloseUpImage.FileText.ToLower.StartsWith("https://")) Then
            If Me.NewCloseUpImage.FileText.ToLower.StartsWith("images/") = False Then
                If Me.NewCloseUpImage.FileText <> "" Then
                    Me.NewCloseUpImage.FileText = "images/" & Me.NewCloseUpImage.FileText
                End If
            End If
        End If

        sw.CloseUpImage = Me.NewCloseUpImage.FileText

        'END CUSTOM CODE Sept '04
        objProdManagement.AddSwatch(sw, objProdManagement.Swatch.Count)
        Me.NewBigImage.FileText = ""
        Me.NewLittleImage.FileText = ""
        Me.NewThumbnailImage.FileText = ""
        Me.NewDescription.Text = ""
        Me.NewName.Text = ""
        Me.NewCloseUpImage.FileText = ""
        Me.NewDisplayDescription.Checked = False
        Me.NewDisplayImage.Checked = True
        objProdManagement.ResetSwatches()
        Swatches.DataSource = objProdManagement.Swatch
        Swatches.DataBind()
    End Sub

    Private Function GetAttributeCombinations(ByVal aoSelectedAttributes() As ArrayList) As String()
        Dim aiRepeatFactor(aoSelectedAttributes.Length - 1) As Integer
        Dim nTotalCombinationCount As Integer = 1
        Dim aoCombinations() As String


        For iAttributeIndex As Integer = 0 To aoSelectedAttributes.Length - 1
            nTotalCombinationCount = nTotalCombinationCount * aoSelectedAttributes(iAttributeIndex).Count
        Next
        Dim nMultiple As Integer
        For iAttributeIndex As Integer = aoSelectedAttributes.Length - 1 To 0 Step -1
            If iAttributeIndex = aoSelectedAttributes.Length - 1 Then
                nMultiple = aoSelectedAttributes(iAttributeIndex).Count
                aiRepeatFactor(iAttributeIndex) = CInt(nMultiple / aoSelectedAttributes(iAttributeIndex).Count)
            Else
                nMultiple = nMultiple * aoSelectedAttributes(iAttributeIndex).Count
                aiRepeatFactor(iAttributeIndex) = CInt(nMultiple / aoSelectedAttributes(iAttributeIndex).Count)
            End If
        Next

        ReDim aoCombinations(nTotalCombinationCount - 1)
        For iCombinationIndex As Integer = 0 To nTotalCombinationCount - 1
            aoCombinations(iCombinationIndex) = MakeCombination(iCombinationIndex, aoSelectedAttributes, aiRepeatFactor)
        Next
        Return aoCombinations
    End Function

    Private Function MakeCombination(ByVal n As Integer, ByVal aoSelectedAttributes() As ArrayList, ByVal aiRepeatFactor() As Integer) As String
        Dim sReturn As String = ""
        For iAttributeIndex As Integer = 0 To aoSelectedAttributes.Length - 1
            Dim attIndex As Integer = CInt(Int(n / aiRepeatFactor(iAttributeIndex))) Mod aoSelectedAttributes(iAttributeIndex).Count
            If sReturn.Length > 0 Then
                sReturn += ","
            End If
            sReturn += CType(aoSelectedAttributes(iAttributeIndex)(attIndex), CAttributeDetail).Name
        Next
        Return sReturn
    End Function
End Class
