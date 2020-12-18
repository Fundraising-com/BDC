Imports System.Diagnostics
Imports System.Diagnostics.FileVersionInfo

Public Class VersionInfo
    Inherits System.Web.UI.UserControl

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dgVersionInfo As System.Web.UI.WebControls.DataGrid

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here

        If Not Page.IsPostBack Then

            Dim strBinDirectory As String = Server.MapPath("~/bin/")

            Dim fvi As FileVersionInfo() = {GetVersionInfo(String.Format("{0}StoreFront.dll", strBinDirectory)), _
                                            GetVersionInfo(String.Format("{0}BusinessRule.dll", strBinDirectory)), _
                                            GetVersionInfo(String.Format("{0}DataAccess.dll", strBinDirectory)), _
                                            GetVersionInfo(String.Format("{0}SystemBase.dll", strBinDirectory)), _
                                            GetVersionInfo(String.Format("{0}UITools.dll", strBinDirectory)), _
                                            GetVersionInfo(String.Format("{0}CSRBusinessRule.dll", strBinDirectory)), _
                                            GetVersionInfo(String.Format("{0}CSRDataAccess.dll", strBinDirectory)), _
                                            GetVersionInfo(String.Format("{0}CSRSystemBase.dll", strBinDirectory)), _
                                            GetVersionInfo(String.Format("{0}RMSBusinessRule.dll", strBinDirectory)), _
                                            GetVersionInfo(String.Format("{0}StoreFront.Integration.dll", strBinDirectory)) _
                                            }

            dgVersionInfo.DataSource = fvi
            dgVersionInfo.DataBind()

        End If

    End Sub

End Class
