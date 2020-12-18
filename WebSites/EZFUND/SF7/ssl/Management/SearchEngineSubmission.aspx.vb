'BEGINVERSIONINFO

'APPVERSION: 7.0.0

'STARTCOPYRIGHT
'The contents of this file are protected under the United States
'copyright laws and is confidential and proprietary to
'LaGarde, Incorporated.  Its use or disclosure in whole or in part without the
'expressed written permission of LaGarde, Incorporated is expressly prohibited.
'
'(c) Copyright 2002 by LaGarde, Incorporated.  All rights reserved.
'ENDCOPYRIGHT

'ENDVERSIONINFO

Imports System.Data.OleDb
Imports StoreFront.SystemBase

Partial Class SearchEngineSubmission
    Inherits CWebPage
    Protected WithEvents PageTable As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents PageCell As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents PageSubTable As System.Web.UI.HtmlControls.HtmlTable

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
        'chcek if user has permission to view this page
        If MyBase.RestrictedPages(Tasks.SearchEngines) = True Then
            Response.Redirect("Accessdenied.aspx")
        End If
        Try
            'Put user code to initialize the page here

            CType(Me.FindControl("LeftColumnNav2").FindControl("CMenuBar1"), CMenubar1).IsAdminArea = True

            If (Not IsPostBack) Then
                Dim strSQL As String
                strSQL = "SELECT SiteKeywords, SiteDescription FROM Admin"

                Trace.Write(strSQL)
                Dim conn As New OleDbConnection(SystemBase.StoreFrontConfiguration.ConnectionString)
                Dim cmd As New OleDbDataAdapter(strSQL, conn)

                Dim ds As DataSet = New DataSet
                cmd.Fill(ds, "Admin")

                If (Not (IsDBNull(ds.Tables("Admin").Rows(0)("SiteKeywords")))) Then
                    Me.txtKeywords.Text = ds.Tables("Admin").Rows(0)("SiteKeywords")
                End If
                If (Not (IsDBNull(ds.Tables("Admin").Rows(0)("SiteKeywords")))) Then
                    Me.txtDescription.Text = ds.Tables("Admin").Rows(0)("SiteDescription")
                End If
                conn.Close()
            End If

        Catch ex As Exception
            Session("DetailError") = "Class SearchEngineSubmission Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
    End Sub

    '## Information Links
    '## Google
    Private Sub Linkbutton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Linkbutton1.Click
        Me.lblInfo.Text = "<b>Google</b> is a search engine that makes heavy use of link popularity as a primary way to rank web sites. This can be especially helpful in finding good sites in response to general searches such as <i>cars</i> and <i>travel</i>, because users across the web have in essence voted for good sites by linking to them. The system works so well that Google has gained wide-spread praise for its high relevancy. Google also has a huge index of the web and provides some results to Yahoo and Netscape Search."
        Me.lblInfo.Visible = True
    End Sub

    '## Lycos
    Private Sub Linkbutton2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Linkbutton2.Click
        Me.lblInfo.Text = "<b>Lycos</b> first started in early 1994 as a University project which later became a commercial operation and has expanded to a large scale search engine. Only this year did Lycos make the shift from being an exclusive search engine to also offering directory-based listings. Last year, Lycos acquired Wired Digital's Hotbot and continues to operate it under the same name. "
        Me.lblInfo.Visible = True
    End Sub

    '## AllTheWeb
    Private Sub Linkbutton3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Linkbutton3.Click
        Me.lblInfo.Text = "<none>"
        Me.lblInfo.Visible = True
    End Sub

    '## AltaVista
    Private Sub Linkbutton4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Linkbutton4.Click
        Me.lblInfo.Text = "<b>AltaVista</b> is consistently one of the largest search engines on the web, in terms of pages indexed. Its comprehensive coverage and wide range of power searching commands makes it a particular favorite among researchers. It also offers a number of features designed to appeal to basic users, such as <b>Ask AltaVista</b> results, which come from Ask Jeeves (see below), and directory listings from the Open Directory and LookSmart. AltaVista opened in December 1995. It was owned by Digital, then run by Compaq (which purchased Digital in 1998), then spun off into a separate company which is now controlled by CMGI."
        Me.lblInfo.Visible = True
    End Sub

    '## The Open Directory
    Private Sub Linkbutton5_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Linkbutton5.Click
        Me.lblInfo.Text = "<b>The Open Directory</b> uses volunteer editors to catalog the web. Formerly known as NewHoo, it was launched in June 1998. It was acquired by Netscape in November 1998, and the company pledged that anyone would be able to use information from the directory through an open license arrangement. Netscape itself was the first licensee. Lycos and AOL Search also make heavy use of Open Directory data. "
        Me.lblInfo.Visible = True
    End Sub

    '## Yahoo
    Private Sub Linkbutton6_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Linkbutton6.Click
        Me.lblInfo.Text = "<b>Yahoo</b> is the web's most popular search service and has a well-deserved reputation for helping people find information easily. The secret to Yahoo's success is human beings. It is the largest human-compiled guide to the web, employing about 150 editors in an effort to categorize the web. Yahoo has over 1 million sites listed. Yahoo also supplements its results with those from Google (beginning in July 2000, when Google takes over from Inktomi). If a search fails to find a match within Yahoo's own listings, then matches from Google are displayed. Google matches also appear after all Yahoo matches have first been shown. Yahoo is the oldest major web site directory, having launched in late 1994."
        Me.lblInfo.Visible = True
    End Sub

    '##  Ask Jeeves
    Private Sub Linkbutton7_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Linkbutton7.Click
        Me.lblInfo.Text = "<b>Ask Jeeves</b> is a human-powered search service that aims to direct you to the exact page that answers your question. If it fails to find a match within its own database, then it will provide matching web pages from various search engines. The service went into beta in mid-April 1997 and opened fully on June 1, 1997. Some results from Ask Jeeves also appear within AltaVista. "
        Me.lblInfo.Visible = True
    End Sub

    '## Looksmart
    Private Sub Linkbutton8_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Linkbutton8.Click
        Me.lblInfo.Text = "<b>LookSmart</b> is a human-compiled directory of web sites. In addition to being a stand-alone service, LookSmart provides directory results to MSN Search, Excite and many other partners. Inktomi provides LookSmart with search results when a search fails to find a match from among LookSmart's reviews. LookSmart launched independently in October 1996, was backed by Reader's Digest for about a year, and then company executives bought back control of the service. LookSmart cange now to new keyword pay for placement system. "
        Me.lblInfo.Visible = True
    End Sub

    '## Overture
    Private Sub Linkbutton9_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Linkbutton9.Click
        Me.lblInfo.Text = "<b>Overture / Goto</b> Unlike the other major search engines, GoTo sells its main listings. Companies can pay money to be placed higher in the search results, which GoTo feels improves relevancy. Non-paid results come from Inktomi. GoTo launched in 1997 and incorporated the former University of Colorado-based World Wide Web Worm. In February 1998, it shifted to its current pay-for-placement model and soon after replaced the WWW Worm with Inktomi for its non-paid listings. GoTo is not related to Go (Infoseek). Paid listing from GoTo also appear on other major search engines, including AltaVista, Lycos, HotBot, Direct Hit, MSN, Yahoo,Excite and Web Crawler. "
        Me.lblInfo.Visible = True
    End Sub

    '## Google Adwords
    Private Sub Linkbutton10_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Linkbutton10.Click
        Me.lblInfo.Text = "<none>"
        Me.lblInfo.Visible = True
    End Sub

    Private Sub btnSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        If Page.IsValid Then
            Dim strSQL As String
            strSQL = "UPDATE Admin SET SiteKeywords = '" & Me.txtKeywords.Text & "',SiteDescription = '" & Me.txtDescription.Text & "'"

            Trace.Write(strSQL)
            Dim conn As New OleDbConnection(SystemBase.StoreFrontConfiguration.ConnectionString)
            Dim cmd As New OleDbCommand(strSQL, conn)

            conn.Open()
            cmd.ExecuteNonQuery()
            conn.Close()

            Me.lblConfirmation.Text = "Information Saved."
            Me.lblConfirmation.Visible = True
        End If
    End Sub

End Class
