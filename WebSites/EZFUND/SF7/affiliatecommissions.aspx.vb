'BEGINVERSIONINFO

'APPVERSION: 7.0.0

'STARTCOPYRIGHT
'The contents of this file are protected under the United States
'copyright laws and is confidential and proprietary to
'LaGarde, Incorporated.  Its use or disclosure in whole or in part without the
'expressed written permission of LaGarde, Incorporated is expressly prohibited.
'
'(c) Copyright 2002 by LaGarde, Incorporated.  All rights reserved.
'@ENDCOPYRIGHT

'ENDVERSIONINFO

Imports StoreFront.SystemBase
Partial Class affiliatecommissions
    Inherits CWebPage
    Protected WithEvents link As System.Web.UI.WebControls.Label
    Protected WithEvents PageCell As System.Web.UI.HtmlControls.HtmlTableCell

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


        SetPageTitle = m_objMessages.GetXMLMessage("affiliateaccount.aspx", "PageTitle", "Title")
        SetDesign(PageTable, PageSubTable, PageCell)
        m_Affiliate = Session("Affiliate")
        If IsNothing(m_Affiliate) Then
            Response.Redirect("affsignIn.aspx?ReturnPage=affiliateaccount.aspx")
            Exit Sub
        ElseIf m_Affiliate.IsSignedIn = False Then
            Response.Redirect("affsignIn.aspx?ReturnPage=affiliateaccount.aspx")
            Exit Sub
        End If

        Try
            If m_Affiliate.CurrentEarnings = 0 And m_Affiliate.LastPaymentAmount = 0 Then
                Commissions.Visible = False
                Terms.Visible = True
                Me.lblTerms.Text = m_Affiliate.Settings.Terms
            Else
                Commissions.Visible = True
                Terms.Visible = False

                Select Case m_Affiliate.CommissionType
                    Case SystemBase.Commissions.FlatFee
                        If m_Affiliate.PayOutRule = AffiliatePayOut.Origanating Then
                            lblCommisionType.Text = Format(m_Affiliate.PayOut, "c") & " Per Referral"
                        Else
                            lblCommisionType.Text = Format(m_Affiliate.PayOut, "c") & " Per Sale"
                        End If

                    Case SystemBase.Commissions.Percent
                        If m_Affiliate.PayOut <> 0 Then
                            If m_Affiliate.PayOutRule = AffiliatePayOut.Origanating Then
                                lblCommisionType.Text = m_Affiliate.PayOut & "% Per Referral"
                            Else
                                lblCommisionType.Text = m_Affiliate.PayOut & "% Per Sale"
                            End If
                        Else
                            lblCommisionType.Text = Format(m_Affiliate.MinumimPayOut, "c") & " Per Sale"
                        End If

                End Select

                Me.lblCurrentEarings.Text = Format(m_Affiliate.CurrentEarnings, "c")
                If m_Affiliate.LastPaymentAmount > 0 Then
                    Me.lblLastPayment.Text = Format(m_Affiliate.LastPaymentAmount, "c") & " on " & m_Affiliate.LastPaymentDate
                    LastPayment.Visible = True
                Else
                    LastPayment.Visible = False
                End If

            End If


        Catch ex As Exception
            Session("DetailError") = "Class AffiliateCommissions Error=" & ex.Message
            Response.Redirect(StoreFrontConfiguration.SiteURL & "errors.aspx")
        End Try
    End Sub

End Class
