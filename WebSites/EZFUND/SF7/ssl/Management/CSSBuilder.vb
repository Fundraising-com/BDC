Imports StoreFront.BusinessRule
Imports StoreFront.SystemBase

'------------------------------------------------------------------------
'Class Summary
'------------------------------------------------------------------------
'Creates a new css file from the LayoutPreview table and saves it using the
'filename specified.  The main method of this class was taken straight from
'the client tools code.
'------------------------------------------------------------------------
'------------------------------------------------------------------------
Public Class CSSBuilder

    Private mCssFileName As String = String.Empty

    Private Property CssFileName() As String
        Get
            Return mCssFileName
        End Get
        Set(ByVal Value As String)
            mCssFileName = Value
        End Set
    End Property

    Public Sub WriteCss(ByVal filename As String, Optional ByVal flag As Boolean = False)
        Me.CssFileName = filename
        'Dim CssFile As System.IO.File
        Dim myDesignManager As New DesignManager
        Dim dsLayout As DataSet = myDesignManager.GetAllLayoutPreview()
        Dim Css As String
        Dim fileStream As System.IO.StreamWriter
        Css = getCSSString(dsLayout.Tables(0).Rows, flag)
        fileStream = IO.File.CreateText(filename)
        fileStream.Write(Css)
        fileStream.Close()
    End Sub

    Public Function getCss(Optional ByVal flag As Boolean = False) As String
        Dim myDesignManager As New DesignManager
        Dim dsLayout As DataSet = myDesignManager.GetAllLayoutPreview()
        Return getCSSString(dsLayout.Tables(0).Rows, flag)
    End Function

    Public Function getPreviewCss(Optional ByVal flag As Boolean = False) As String
        Dim myDesignManager As New DesignManager
        Dim dsLayout As DataSet = myDesignManager.GetAllLayoutPreview()
        Return getPreviewCSSString(dsLayout.Tables(0).Rows, flag)
    End Function

#Region "Function getCSSString(ByVal drRows As DataRowCollection, Optional ByVal flag As Boolean = False) As String"
    Private Function getCSSString(ByVal drRows As DataRowCollection, Optional ByVal flag As Boolean = False) As String
        Dim cssString As String = ""
        Dim dr As DataRow

        cssString = cssString & "<Style>"
        cssString = cssString & "/* Default CSS Stylesheet for a new Web Application project */{}" & vbCrLf & vbCrLf

        For Each dr In drRows
            If dr("Name").ToString() = "BodyTable" Then
                cssString = cssString & ".GeneralPage" & vbCrLf
                cssString = cssString & "{" & vbCrLf
                cssString = cssString & "background-color: " & dr("BackgroundColor").ToString & "; /* Background Color */" & vbCrLf
                If (dr("BackgroundImageURL").ToString().Length <> 0) Then
                    If Me.CssFileName.IndexOf("PreviewStyles.css") > 0 Then
                        cssString = cssString & "background-image: url(" & StoreFrontConfiguration.SSLPath & "images/" & dr("BackgroundImageURL").ToString & "); /* Background Image */" & vbCrLf
                    Else
                        If flag = False Then
                            cssString = cssString & "background-image: url(images/" & dr("BackgroundImageURL").ToString & "); /* Background Image */" & vbCrLf
                        ElseIf flag = True Then
                            cssString = cssString & "background-image: url(" & dr("BackgroundImageURL").ToString & "); /* Background Image */" & vbCrLf
                        End If
                    End If
                End If
                cssString = cssString & "margin-bottom: " & dr("BottomMargin").ToString & "px; /* Margins */" & vbCrLf
                cssString = cssString & "margin-left: " & dr("LeftMargin").ToString & "px; /* Margins */" & vbCrLf
                cssString = cssString & "margin-right: " & dr("RightMargin").ToString & "px; /* Margins */" & vbCrLf
                cssString = cssString & "margin-top: " & dr("TopMargin").ToString & "px; /* Margins */" & vbCrLf
                cssString = cssString & "}" & vbCrLf & vbCrLf

                cssString = cssString & ".GeneralTable" & vbCrLf
                cssString = cssString & "{" & vbCrLf
                cssString = cssString & "background-color: " & dr("BorderColor").ToString & "; /* Background Color */" & vbCrLf
                cssString = cssString & "}" & vbCrLf & vbCrLf
            End If
        Next

        For Each dr In drRows
            If dr("Name").ToString() = "TopBanner" Then
                cssString = cssString & ".TopBanner" & vbCrLf
                cssString = cssString & "{" & vbCrLf
                cssString = cssString & "font-family: " & dr("FontFace").ToString & "; /* Font Face */" & vbCrLf
                cssString = cssString & "font-size: " & dr("FontSize").ToString & "pt; /* Font Size */" & vbCrLf
                cssString = cssString & "color: " & dr("FontColor").ToString & "; /* Font Color */" & vbCrLf
                If dr("FontStyle").ToString.ToLower = "normal" OrElse dr("FontStyle").ToString.ToLower = "bold" Then
                    cssString = cssString & "font-weight: " & dr("FontStyle").ToString & "; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: normal; /* FontStyle */" & vbCrLf
                ElseIf dr("FontStyle").ToString.ToLower = "italic" Then
                    cssString = cssString & "font-weight: normal; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: italic; /* FontStyle */" & vbCrLf
                Else
                    cssString = cssString & "font-weight: bold; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: italic; /* FontStyle */" & vbCrLf
                End If
                cssString = cssString & "text-decoration: none;" & vbCrLf
                cssString = cssString & "background-color: " & dr("BackgroundColor").ToString & "; /* Background color */" & vbCrLf
                If (dr("BackgroundImageURL").ToString().Length <> 0) Then
                    If Me.CssFileName.IndexOf("PreviewStyles.css") > 0 Then
                        cssString = cssString & "background-image: url(" & StoreFrontConfiguration.SSLPath & "images/" & dr("BackgroundImageURL").ToString & "); /* Background Image */" & vbCrLf
                    Else
                        If flag = False Then
                            cssString = cssString & "background-image: url(images/" & dr("BackgroundImageURL").ToString & "); /* Background Image */" & vbCrLf
                        ElseIf flag = True Then
                            cssString = cssString & "background-image: url(" & dr("BackgroundImageURL").ToString & "); /* Background Image */" & vbCrLf
                        End If
                    End If
                End If
                cssString = cssString & "text-align: " & dr("HorizontalAlignment").ToString & "; /* Align Text */" & vbCrLf
                cssString = cssString & "}" & vbCrLf & vbCrLf
            End If
        Next

        For Each dr In drRows
            If dr("Name").ToString() = "TopSubBanner" Then
                cssString = cssString & ".TopSubBanner" & vbCrLf
                cssString = cssString & "{" & vbCrLf
                cssString = cssString & "font-family: " & dr("FontFace").ToString & "; /* Font Face */" & vbCrLf
                cssString = cssString & "font-size: " & dr("FontSize").ToString & "pt; /* Font Size */" & vbCrLf
                cssString = cssString & "color: " & dr("FontColor").ToString & "; /* Font Color */" & vbCrLf
                If dr("FontStyle").ToString.ToLower = "normal" OrElse dr("FontStyle").ToString.ToLower = "bold" Then
                    cssString = cssString & "font-weight: " & dr("FontStyle").ToString & "; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: normal; /* FontStyle */" & vbCrLf
                ElseIf dr("FontStyle").ToString.ToLower = "italic" Then
                    cssString = cssString & "font-weight: normal; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: italic; /* FontStyle */" & vbCrLf
                Else
                    cssString = cssString & "font-weight: bold; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: italic; /* FontStyle */" & vbCrLf
                End If
                cssString = cssString & "text-decoration: none;" & vbCrLf
                cssString = cssString & "background-color: " & dr("BackgroundColor").ToString & "; /* Background color */" & vbCrLf
                If (dr("BackgroundImageURL").ToString().Length <> 0) Then
                    If Me.CssFileName.IndexOf("PreviewStyles.css") > 0 Then
                        cssString = cssString & "background-image: url(" & StoreFrontConfiguration.SSLPath & "images/" & dr("BackgroundImageURL").ToString & "); /* Background Image */" & vbCrLf
                    Else
                        If flag = False Then
                            cssString = cssString & "background-image: url(images/" & dr("BackgroundImageURL").ToString & "); /* Background Image */" & vbCrLf
                        ElseIf flag = True Then
                            cssString = cssString & "background-image: url(" & dr("BackgroundImageURL").ToString & "); /* Background Image */" & vbCrLf
                        End If
                    End If
                End If
                cssString = cssString & "text-align: " & dr("HorizontalAlignment").ToString & "; /* Align Text */" & vbCrLf
                cssString = cssString & "}" & vbCrLf & vbCrLf
            End If
        Next

        'update #2319
        For Each dr In drRows
            If dr("Name").ToString() = "TopSubBanner" Then
                cssString = cssString & ".TopSubBannerText" & vbCrLf
                cssString = cssString & "{" & vbCrLf
                cssString = cssString & "font-family: " & dr("FontFace").ToString & "; /* Font Face */" & vbCrLf
                cssString = cssString & "font-size: " & dr("FontSize").ToString & "pt; /* Font Size */" & vbCrLf
                cssString = cssString & "color: " & dr("FontColor").ToString & "; /* Font Color */" & vbCrLf
                If dr("FontStyle").ToString.ToLower = "normal" OrElse dr("FontStyle").ToString.ToLower = "bold" Then
                    cssString = cssString & "font-weight: " & dr("FontStyle").ToString & "; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: normal; /* FontStyle */" & vbCrLf
                ElseIf dr("FontStyle").ToString.ToLower = "italic" Then
                    cssString = cssString & "font-weight: normal; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: italic; /* FontStyle */" & vbCrLf
                Else
                    cssString = cssString & "font-weight: bold; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: italic; /* FontStyle */" & vbCrLf
                End If
                cssString = cssString & "text-decoration: none;" & vbCrLf
                cssString = cssString & "text-align: " & dr("HorizontalAlignment").ToString & "; /* Align Text */" & vbCrLf
                cssString = cssString & "}" & vbCrLf & vbCrLf
            End If
        Next

        For Each dr In drRows
            If dr("Name").ToString() = "LeftColumn" Then
                cssString = cssString & ".LeftColumn" & vbCrLf
                cssString = cssString & "{" & vbCrLf
                cssString = cssString & "font-family: " & dr("FontFace").ToString & "; /* Font Face */" & vbCrLf
                cssString = cssString & "font-size: " & dr("FontSize").ToString & "pt; /* Font Size */" & vbCrLf
                cssString = cssString & "color: " & dr("FontColor").ToString & "; /* Font Color */" & vbCrLf
                If dr("FontStyle").ToString.ToLower = "normal" OrElse dr("FontStyle").ToString.ToLower = "bold" Then
                    cssString = cssString & "font-weight: " & dr("FontStyle").ToString & "; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: normal; /* FontStyle */" & vbCrLf
                ElseIf dr("FontStyle").ToString.ToLower = "italic" Then
                    cssString = cssString & "font-weight: normal; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: italic; /* FontStyle */" & vbCrLf
                Else
                    cssString = cssString & "font-weight: bold; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: italic; /* FontStyle */" & vbCrLf
                End If

                cssString = cssString & "text-decoration: none;" & vbCrLf
                cssString = cssString & "background-color: " & dr("BackgroundColor").ToString & "; /* Background color */" & vbCrLf
                If (dr("BackgroundImageURL").ToString().Length <> 0) Then
                    If Me.CssFileName.IndexOf("PreviewStyles.css") > 0 Then
                        cssString = cssString & "background-image: url(" & StoreFrontConfiguration.SSLPath & "images/" & dr("BackgroundImageURL").ToString & "); /* Background Image */" & vbCrLf
                    Else
                        If flag = False Then
                            cssString = cssString & "background-image: url(images/" & dr("BackgroundImageURL").ToString & "); /* Background Image */" & vbCrLf
                        ElseIf flag = True Then
                            cssString = cssString & "background-image: url(" & dr("BackgroundImageURL").ToString & "); /* Background Image */" & vbCrLf
                        End If
                    End If
                End If
                cssString = cssString & "text-align: " & dr("HorizontalAlignment").ToString & "; /* Align Text */" & vbCrLf
                cssString = cssString & "vertical-align: " & dr("VerticalAlignment").ToString & "; /* Align Text */" & vbCrLf
                If (dr("TableWidth").ToString().Trim.IndexOf("%") <> -1) Then
                    cssString = cssString & "width: " & dr("TableWidth").ToString() & ";"
                Else
                    cssString = cssString & "width: " & dr("TableWidth").ToString() & "px;"
                End If
                cssString = cssString & "}" & vbCrLf & vbCrLf
            End If
        Next

        'update #2319
        For Each dr In drRows
            If dr("Name").ToString() = "LeftColumn" Then
                cssString = cssString & ".LeftColumnText" & vbCrLf
                cssString = cssString & "{" & vbCrLf
                cssString = cssString & "font-family: " & dr("FontFace").ToString & "; /* Font Face */" & vbCrLf
                cssString = cssString & "font-size: " & dr("FontSize").ToString & "pt; /* Font Size */" & vbCrLf
                cssString = cssString & "color: " & dr("FontColor").ToString & "; /* Font Color */" & vbCrLf
                If dr("FontStyle").ToString.ToLower = "normal" OrElse dr("FontStyle").ToString.ToLower = "bold" Then
                    cssString = cssString & "font-weight: " & dr("FontStyle").ToString & "; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: normal; /* FontStyle */" & vbCrLf
                ElseIf dr("FontStyle").ToString.ToLower = "italic" Then
                    cssString = cssString & "font-weight: normal; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: italic; /* FontStyle */" & vbCrLf
                Else
                    cssString = cssString & "font-weight: bold; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: italic; /* FontStyle */" & vbCrLf
                End If
                cssString = cssString & "text-decoration: none;" & vbCrLf
                cssString = cssString & "text-align: " & dr("HorizontalAlignment").ToString & "; /* Align Text */" & vbCrLf
                cssString = cssString & "vertical-align: " & dr("VerticalAlignment").ToString & "; /* Align Text */" & vbCrLf
                cssString = cssString & "}" & vbCrLf & vbCrLf
            End If
        Next


        For Each dr In drRows
            If dr("Name").ToString() = "RightColumn" Then
                cssString = cssString & ".RightColumn" & vbCrLf
                cssString = cssString & "{" & vbCrLf
                cssString = cssString & "font-family: " & dr("FontFace").ToString & "; /* Font Face */" & vbCrLf
                cssString = cssString & "font-size: " & dr("FontSize").ToString & "pt; /* Font Size */" & vbCrLf
                cssString = cssString & "color: " & dr("FontColor").ToString & "; /* Font Color */" & vbCrLf
                If dr("FontStyle").ToString.ToLower = "normal" OrElse dr("FontStyle").ToString.ToLower = "bold" Then
                    cssString = cssString & "font-weight: " & dr("FontStyle").ToString & "; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: normal; /* FontStyle */" & vbCrLf
                ElseIf dr("FontStyle").ToString.ToLower = "italic" Then
                    cssString = cssString & "font-weight: normal; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: italic; /* FontStyle */" & vbCrLf
                Else
                    cssString = cssString & "font-weight: bold; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: italic; /* FontStyle */" & vbCrLf
                End If
                cssString = cssString & "text-decoration: none;" & vbCrLf
                cssString = cssString & "background-color: " & dr("BackgroundColor").ToString & "; /* Background color */" & vbCrLf
                If (dr("BackgroundImageURL").ToString().Length <> 0) Then
                    If Me.CssFileName.IndexOf("PreviewStyles.css") > 0 Then
                        cssString = cssString & "background-image: url(" & StoreFrontConfiguration.SSLPath & "images/" & dr("BackgroundImageURL").ToString & "); /* Background Image */" & vbCrLf
                    Else
                        If flag = False Then
                            cssString = cssString & "background-image: url(images/" & dr("BackgroundImageURL").ToString & "); /* Background Image */" & vbCrLf
                        ElseIf flag = True Then
                            cssString = cssString & "background-image: url(" & dr("BackgroundImageURL").ToString & "); /* Background Image */" & vbCrLf
                        End If
                    End If
                End If
                cssString = cssString & "text-align: " & dr("HorizontalAlignment").ToString & "; /* Align Text */" & vbCrLf
                cssString = cssString & "vertical-align: " & dr("VerticalAlignment").ToString & "; /* Align Text */" & vbCrLf
                If (dr("TableWidth").ToString().Trim.IndexOf("%") <> -1) Then
                    cssString = cssString & "width: " & dr("TableWidth").ToString() & ";"
                Else
                    cssString = cssString & "width: " & dr("TableWidth").ToString() & "px;"
                End If
                cssString = cssString & "}" & vbCrLf & vbCrLf
            End If
        Next

        'update #2319
        For Each dr In drRows
            If dr("Name").ToString() = "RightColumn" Then
                cssString = cssString & ".RightColumnText" & vbCrLf
                cssString = cssString & "{" & vbCrLf
                cssString = cssString & "font-family: " & dr("FontFace").ToString & "; /* Font Face */" & vbCrLf
                cssString = cssString & "font-size: " & dr("FontSize").ToString & "pt; /* Font Size */" & vbCrLf
                cssString = cssString & "color: " & dr("FontColor").ToString & "; /* Font Color */" & vbCrLf
                If dr("FontStyle").ToString.ToLower = "normal" OrElse dr("FontStyle").ToString.ToLower = "bold" Then
                    cssString = cssString & "font-weight: " & dr("FontStyle").ToString & "; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: normal; /* FontStyle */" & vbCrLf
                ElseIf dr("FontStyle").ToString.ToLower = "italic" Then
                    cssString = cssString & "font-weight: normal; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: italic; /* FontStyle */" & vbCrLf
                Else
                    cssString = cssString & "font-weight: bold; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: italic; /* FontStyle */" & vbCrLf
                End If
                cssString = cssString & "text-decoration: none;" & vbCrLf
                cssString = cssString & "text-align: " & dr("HorizontalAlignment").ToString & "; /* Align Text */" & vbCrLf
                cssString = cssString & "vertical-align: " & dr("VerticalAlignment").ToString & "; /* Align Text */" & vbCrLf
                cssString = cssString & "}" & vbCrLf & vbCrLf
            End If
        Next

        For Each dr In drRows
            If dr("Name").ToString() = "Footer" Then
                cssString = cssString & ".Footer" & vbCrLf
                cssString = cssString & "{" & vbCrLf
                cssString = cssString & "font-family: " & dr("FontFace").ToString & "; /* Font Face */" & vbCrLf
                cssString = cssString & "font-size: " & dr("FontSize").ToString & "pt; /* Font Size */" & vbCrLf
                cssString = cssString & "color: " & dr("FontColor").ToString & "; /* Font Color */" & vbCrLf
                If dr("FontStyle").ToString.ToLower = "normal" OrElse dr("FontStyle").ToString.ToLower = "bold" Then
                    cssString = cssString & "font-weight: " & dr("FontStyle").ToString & "; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: normal; /* FontStyle */" & vbCrLf
                ElseIf dr("FontStyle").ToString.ToLower = "italic" Then
                    cssString = cssString & "font-weight: normal; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: italic; /* FontStyle */" & vbCrLf
                Else
                    cssString = cssString & "font-weight: bold; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: italic; /* FontStyle */" & vbCrLf
                End If
                cssString = cssString & "text-decoration: none;" & vbCrLf
                cssString = cssString & "background-color: " & dr("BackgroundColor").ToString & "; /* Background color */" & vbCrLf
                If (dr("BackgroundImageURL").ToString().Length <> 0) Then
                    If Me.CssFileName.IndexOf("PreviewStyles.css") > 0 Then
                        cssString = cssString & "background-image: url(" & StoreFrontConfiguration.SSLPath & "images/" & dr("BackgroundImageURL").ToString & "); /* Background Image */" & vbCrLf
                    Else
                        If flag = False Then
                            cssString = cssString & "background-image: url(images/" & dr("BackgroundImageURL").ToString & "); /* Background Image */" & vbCrLf
                        ElseIf flag = True Then
                            cssString = cssString & "background-image: url(" & dr("BackgroundImageURL").ToString & "); /* Background Image */" & vbCrLf
                        End If
                    End If
                End If
                cssString = cssString & "text-align: " & dr("HorizontalAlignment").ToString & "; /* Align Text */" & vbCrLf
                cssString = cssString & "vertical-align: " & dr("VerticalAlignment").ToString & "; /* Align Text */" & vbCrLf
                cssString = cssString & "}" & vbCrLf & vbCrLf
            End If
        Next

        'update #2319
        For Each dr In drRows
            If dr("Name").ToString() = "Footer" Then
                cssString = cssString & ".FooterText" & vbCrLf
                cssString = cssString & "{" & vbCrLf
                cssString = cssString & "font-family: " & dr("FontFace").ToString & "; /* Font Face */" & vbCrLf
                cssString = cssString & "font-size: " & dr("FontSize").ToString & "pt; /* Font Size */" & vbCrLf
                cssString = cssString & "color: " & dr("FontColor").ToString & "; /* Font Color */" & vbCrLf
                If dr("FontStyle").ToString.ToLower = "normal" OrElse dr("FontStyle").ToString.ToLower = "bold" Then
                    cssString = cssString & "font-weight: " & dr("FontStyle").ToString & "; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: normal; /* FontStyle */" & vbCrLf
                ElseIf dr("FontStyle").ToString.ToLower = "italic" Then
                    cssString = cssString & "font-weight: normal; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: italic; /* FontStyle */" & vbCrLf
                Else
                    cssString = cssString & "font-weight: bold; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: italic; /* FontStyle */" & vbCrLf
                End If
                cssString = cssString & "text-decoration: none;" & vbCrLf
                cssString = cssString & "text-align: " & dr("HorizontalAlignment").ToString & "; /* Align Text */" & vbCrLf
                cssString = cssString & "vertical-align: " & dr("VerticalAlignment").ToString & "; /* Align Text */" & vbCrLf
                cssString = cssString & "}" & vbCrLf & vbCrLf
            End If
        Next

        For Each dr In drRows
            If dr("Name").ToString() = "Instruction" Then
                cssString = cssString & ".Instructions" & vbCrLf
                cssString = cssString & "{" & vbCrLf
                cssString = cssString & "font-family: " & dr("FontFace").ToString & "; /* Font Face */" & vbCrLf
                cssString = cssString & "font-size: " & dr("FontSize").ToString & "pt; /* Font Size */" & vbCrLf
                cssString = cssString & "color: " & dr("FontColor").ToString & "; /* Font Color */" & vbCrLf
                If dr("FontStyle").ToString.ToLower = "normal" OrElse dr("FontStyle").ToString.ToLower = "bold" Then
                    cssString = cssString & "font-weight: " & dr("FontStyle").ToString & "; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: normal; /* FontStyle */" & vbCrLf
                ElseIf dr("FontStyle").ToString.ToLower = "italic" Then
                    cssString = cssString & "font-weight: normal; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: italic; /* FontStyle */" & vbCrLf
                Else
                    cssString = cssString & "font-weight: bold; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: italic; /* FontStyle */" & vbCrLf
                End If
                cssString = cssString & "text-decoration: none; /* Font Style */" & vbCrLf
                cssString = cssString & "background-color: " & dr("BackgroundColor").ToString & "; /* Background color */" & vbCrLf
                If (dr("BackgroundImageURL").ToString().Length <> 0) Then
                    If Me.CssFileName.IndexOf("PreviewStyles.css") > 0 Then
                        cssString = cssString & "background-image: url(" & StoreFrontConfiguration.SSLPath & "images/" & dr("BackgroundImageURL").ToString & "); /* Background Image */" & vbCrLf
                    Else
                        If flag = False Then
                            cssString = cssString & "background-image: url(images/" & dr("BackgroundImageURL").ToString & "); /* Background Image */" & vbCrLf
                        ElseIf flag = True Then
                            cssString = cssString & "background-image: url(" & dr("BackgroundImageURL").ToString & "); /* Background Image */" & vbCrLf
                        End If
                    End If
                End If
                cssString = cssString & "text-align: " & dr("HorizontalAlignment").ToString & "; /* Align Text */" & vbCrLf
                cssString = cssString & "}" & vbCrLf & vbCrLf
            End If
        Next

        For Each dr In drRows
            If dr("Name").ToString() = "ErrorMessages" Then
                cssString = cssString & ".ErrorMessages" & vbCrLf
                cssString = cssString & "{" & vbCrLf
                cssString = cssString & "font-family: " & dr("FontFace").ToString & "; /* Font Face */" & vbCrLf
                cssString = cssString & "font-size: " & dr("FontSize").ToString & "pt; /* Font Size */" & vbCrLf
                cssString = cssString & "color: " & dr("FontColor").ToString & "; /* Font Color */" & vbCrLf
                If dr("FontStyle").ToString.ToLower = "normal" OrElse dr("FontStyle").ToString.ToLower = "bold" Then
                    cssString = cssString & "font-weight: " & dr("FontStyle").ToString & "; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: normal; /* FontStyle */" & vbCrLf
                ElseIf dr("FontStyle").ToString.ToLower = "italic" Then
                    cssString = cssString & "font-weight: normal; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: italic; /* FontStyle */" & vbCrLf
                Else
                    cssString = cssString & "font-weight: bold; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: italic; /* FontStyle */" & vbCrLf
                End If
                cssString = cssString & "text-decoration: none; /* Font Style */" & vbCrLf
                cssString = cssString & "background-color: " & dr("BackgroundColor").ToString & "; /* Background color */" & vbCrLf
                If (dr("BackgroundImageURL").ToString().Length <> 0) Then
                    If Me.CssFileName.IndexOf("PreviewStyles.css") > 0 Then
                        cssString = cssString & "background-image: url(" & StoreFrontConfiguration.SSLPath & "images/" & dr("BackgroundImageURL").ToString & "); /* Background Image */" & vbCrLf
                    Else
                        If flag = False Then
                            cssString = cssString & "background-image: url(images/" & dr("BackgroundImageURL").ToString & "); /* Background Image */" & vbCrLf
                        ElseIf flag = True Then
                            cssString = cssString & "background-image: url(" & dr("BackgroundImageURL").ToString & "); /* Background Image */" & vbCrLf
                        End If
                    End If
                End If
                cssString = cssString & "text-align: " & dr("HorizontalAlignment").ToString & "; /* Align Text */" & vbCrLf
                cssString = cssString & "}" & vbCrLf & vbCrLf
            End If
        Next

        For Each dr In drRows
            If dr("Name").ToString() = "Messages" Then
                cssString = cssString & ".Messages" & vbCrLf
                cssString = cssString & "{" & vbCrLf
                cssString = cssString & "font-family: " & dr("FontFace").ToString & "; /* Font Face */" & vbCrLf
                cssString = cssString & "font-size: " & dr("FontSize").ToString & "pt; /* Font Size */" & vbCrLf
                cssString = cssString & "color: " & dr("FontColor").ToString & "; /* Font Color */" & vbCrLf
                If dr("FontStyle").ToString.ToLower = "normal" OrElse dr("FontStyle").ToString.ToLower = "bold" Then
                    cssString = cssString & "font-weight: " & dr("FontStyle").ToString & "; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: normal; /* FontStyle */" & vbCrLf
                ElseIf dr("FontStyle").ToString.ToLower = "italic" Then
                    cssString = cssString & "font-weight: normal; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: italic; /* FontStyle */" & vbCrLf
                Else
                    cssString = cssString & "font-weight: bold; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: italic; /* FontStyle */" & vbCrLf
                End If
                cssString = cssString & "text-decoration: none; /* Font Style */" & vbCrLf
                cssString = cssString & "background-color: " & dr("BackgroundColor").ToString & "; /* Background color */" & vbCrLf
                If (dr("BackgroundImageURL").ToString().Length <> 0) Then
                    If Me.CssFileName.IndexOf("PreviewStyles.css") > 0 Then
                        cssString = cssString & "background-image: url(" & StoreFrontConfiguration.SSLPath & "images/" & dr("BackgroundImageURL").ToString & "); /* Background Image */" & vbCrLf
                    Else
                        If flag = False Then
                            cssString = cssString & "background-image: url(images/" & dr("BackgroundImageURL").ToString & "); /* Background Image */" & vbCrLf
                        ElseIf flag = True Then
                            cssString = cssString & "background-image: url(" & dr("BackgroundImageURL").ToString & "); /* Background Image */" & vbCrLf
                        End If
                    End If
                End If
                cssString = cssString & "text-align: " & dr("HorizontalAlignment").ToString & "; /* Align Text */" & vbCrLf
                cssString = cssString & "}" & vbCrLf & vbCrLf
            End If
        Next

        For Each dr In drRows
            If dr("Name").ToString() = "Headings" Then
                cssString = cssString & ".Headings" & vbCrLf
                cssString = cssString & "{" & vbCrLf
                cssString = cssString & "font-family: " & dr("FontFace").ToString & "; /* Font Face */" & vbCrLf
                cssString = cssString & "font-size: " & dr("FontSize").ToString & "pt; /* Font Size */" & vbCrLf
                cssString = cssString & "color: " & dr("FontColor").ToString & "; /* Font Color */" & vbCrLf
                If dr("FontStyle").ToString.ToLower = "normal" OrElse dr("FontStyle").ToString.ToLower = "bold" Then
                    cssString = cssString & "font-weight: " & dr("FontStyle").ToString & "; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: normal; /* FontStyle */" & vbCrLf
                ElseIf dr("FontStyle").ToString.ToLower = "italic" Then
                    cssString = cssString & "font-weight: normal; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: italic; /* FontStyle */" & vbCrLf
                Else
                    cssString = cssString & "font-weight: bold; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: italic; /* FontStyle */" & vbCrLf
                End If
                cssString = cssString & "text-decoration: none; /* Font Style */" & vbCrLf
                cssString = cssString & "background-color: " & dr("BackgroundColor").ToString & "; /* Background color */" & vbCrLf
                If (dr("BackgroundImageURL").ToString().Length <> 0) Then
                    If Me.CssFileName.IndexOf("PreviewStyles.css") > 0 Then
                        cssString = cssString & "background-image: url(" & StoreFrontConfiguration.SSLPath & "images/" & dr("BackgroundImageURL").ToString & "); /* Background Image */" & vbCrLf
                    Else
                        If flag = False Then
                            cssString = cssString & "background-image: url(images/" & dr("BackgroundImageURL").ToString & "); /* Background Image */" & vbCrLf
                        ElseIf flag = True Then
                            cssString = cssString & "background-image: url(" & dr("BackgroundImageURL").ToString & "); /* Background Image */" & vbCrLf
                        End If
                    End If
                End If
                cssString = cssString & "text-align: " & dr("HorizontalAlignment").ToString & "; /* Align Text */" & vbCrLf
                cssString = cssString & "}" & vbCrLf & vbCrLf
            End If
        Next



        For Each dr In drRows
            If dr("Name").ToString() = "Content" Then
                cssString = cssString & ".Content" & vbCrLf
                cssString = cssString & "{" & vbCrLf
                cssString = cssString & "font-family: " & dr("FontFace").ToString & "; /* Font Face */" & vbCrLf
                cssString = cssString & "font-size: " & dr("FontSize").ToString & "pt; /* Font Size */" & vbCrLf
                cssString = cssString & "color: " & dr("FontColor").ToString & "; /* Font Color */" & vbCrLf
                If dr("FontStyle").ToString.ToLower = "normal" OrElse dr("FontStyle").ToString.ToLower = "bold" Then
                    cssString = cssString & "font-weight: " & dr("FontStyle").ToString & "; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: normal; /* FontStyle */" & vbCrLf
                ElseIf dr("FontStyle").ToString.ToLower = "italic" Then
                    cssString = cssString & "font-weight: normal; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: italic; /* FontStyle */" & vbCrLf
                Else
                    cssString = cssString & "font-weight: bold; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: italic; /* FontStyle */" & vbCrLf
                End If
                cssString = cssString & "text-decoration: none; /* Font Style */" & vbCrLf
                cssString = cssString & "background-color: " & dr("BackgroundColor").ToString & "; /* Background color */" & vbCrLf
                If (dr("BackgroundImageURL").ToString().Length <> 0) Then
                    If Me.CssFileName.IndexOf("PreviewStyles.css") > 0 Then
                        cssString = cssString & "background-image: url(" & StoreFrontConfiguration.SSLPath & "images/" & dr("BackgroundImageURL").ToString & "); /* Background Image */" & vbCrLf
                    Else
                        If flag = False Then
                            cssString = cssString & "background-image: url(images/" & dr("BackgroundImageURL").ToString & "); /* Background Image */" & vbCrLf
                        ElseIf flag = True Then
                            cssString = cssString & "background-image: url(" & dr("BackgroundImageURL").ToString & "); /* Background Image */" & vbCrLf
                        End If
                    End If
                End If
                cssString = cssString & "text-align: " & dr("HorizontalAlignment").ToString & "; /* Align Text */" & vbCrLf
                cssString = cssString & "}" & vbCrLf & vbCrLf
            End If
        Next

        For Each dr In drRows
            If dr("Name").ToString() = "ContentTableHeader" Then
                cssString = cssString & ".ContentTableHeader" & vbCrLf
                cssString = cssString & "{" & vbCrLf
                cssString = cssString & "font-family: " & dr("FontFace").ToString & "; /* Font Face */" & vbCrLf
                cssString = cssString & "font-size: " & dr("FontSize").ToString & "pt; /* Heading Font Size */" & vbCrLf
                cssString = cssString & "color: " & dr("FontColor").ToString & "; /* Heading Font Color */" & vbCrLf
                If dr("FontStyle").ToString.ToLower = "normal" OrElse dr("FontStyle").ToString.ToLower = "bold" Then
                    cssString = cssString & "font-weight: " & dr("FontStyle").ToString & "; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: normal; /* FontStyle */" & vbCrLf
                ElseIf dr("FontStyle").ToString.ToLower = "italic" Then
                    cssString = cssString & "font-weight: normal; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: italic; /* FontStyle */" & vbCrLf
                Else
                    cssString = cssString & "font-weight: bold; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: italic; /* FontStyle */" & vbCrLf
                End If
                cssString = cssString & "text-decoration: none; /* Heading Font Style */" & vbCrLf
                cssString = cssString & "background-color: " & dr("BackgroundColor").ToString & "; /* Background color */" & vbCrLf
                If (dr("BackgroundImageURL").ToString().Length <> 0) Then
                    If Me.CssFileName.IndexOf("PreviewStyles.css") > 0 Then
                        cssString = cssString & "background-image: url(" & StoreFrontConfiguration.SSLPath & "images/" & dr("BackgroundImageURL").ToString & "); /* Background Image */" & vbCrLf
                    Else
                        If flag = False Then
                            cssString = cssString & "background-image: url(images/" & dr("BackgroundImageURL").ToString & "); /* Background Image */" & vbCrLf
                        ElseIf flag = True Then
                            cssString = cssString & "background-image: url(" & dr("BackgroundImageURL").ToString & "); /* Background Image */" & vbCrLf
                        End If
                    End If
                End If
                cssString = cssString & "text-align: " & dr("HorizontalAlignment").ToString & "; /* Align Text */" & vbCrLf
                cssString = cssString & "}" & vbCrLf & vbCrLf

                cssString = cssString & ".ContentTable" & vbCrLf
                cssString = cssString & "{" & vbCrLf
                cssString = cssString & "background-color: " & dr("BorderColor").ToString & ";" & vbCrLf
                cssString = cssString & "}" & vbCrLf & vbCrLf

                cssString = cssString & ".ContentTableHorizontal" & vbCrLf
                cssString = cssString & "{" & vbCrLf
                cssString = cssString & "background-color: " & dr("BorderColor").ToString & ";" & vbCrLf
                cssString = cssString & "}" & vbCrLf & vbCrLf
            End If
        Next
        cssString = cssString & "</Style>"
        Return cssString
    End Function
#End Region

#Region "Function getPreviewCSSString(ByVal drRows As DataRowCollection, Optional ByVal flag As Boolean = False) As String"
    Private Function getPreviewCSSString(ByVal drRows As DataRowCollection, Optional ByVal flag As Boolean = False) As String
        Dim cssString As String = ""
        Dim dr As DataRow

        cssString = cssString & "<Style>"
        cssString = cssString & "/* Default CSS Stylesheet for a new Web Application project */{}" & vbCrLf & vbCrLf

        For Each dr In drRows
            If dr("Name").ToString() = "BodyTable" Then
                cssString = cssString & ".GeneralPage" & vbCrLf
                cssString = cssString & "{" & vbCrLf
                cssString = cssString & "background-color: " & dr("BackgroundColor").ToString & "; /* Background Color */" & vbCrLf
                If (dr("BackgroundImageURL").ToString().Length <> 0) Then
                    If flag = True Then
                        cssString = cssString & "background-image: url(" & dr("BackgroundImageURL").ToString & "); /* Background Image */" & vbCrLf
                    Else
                        cssString = cssString & "background-image: url(" & StoreFrontConfiguration.SSLPath & "images/" & dr("BackgroundImageURL").ToString & "); /* Background Image */" & vbCrLf
                    End If
                End If
                    cssString = cssString & "margin-bottom: " & dr("BottomMargin").ToString & "px; /* Margins */" & vbCrLf
                    cssString = cssString & "margin-left: " & dr("LeftMargin").ToString & "px; /* Margins */" & vbCrLf
                    cssString = cssString & "margin-right: " & dr("RightMargin").ToString & "px; /* Margins */" & vbCrLf
                    cssString = cssString & "margin-top: " & dr("TopMargin").ToString & "px; /* Margins */" & vbCrLf
                    cssString = cssString & "}" & vbCrLf & vbCrLf

                    cssString = cssString & ".GeneralTable" & vbCrLf
                    cssString = cssString & "{" & vbCrLf
                    cssString = cssString & "background-color: " & dr("BorderColor").ToString & "; /* Background Color */" & vbCrLf
                    cssString = cssString & "}" & vbCrLf & vbCrLf
                End If
        Next

        For Each dr In drRows
            If dr("Name").ToString() = "TopBanner" Then
                cssString = cssString & ".TopBanner" & vbCrLf
                cssString = cssString & "{" & vbCrLf
                cssString = cssString & "font-family: " & dr("FontFace").ToString & "; /* Font Face */" & vbCrLf
                cssString = cssString & "font-size: " & dr("FontSize").ToString & "pt; /* Font Size */" & vbCrLf
                cssString = cssString & "color: " & dr("FontColor").ToString & "; /* Font Color */" & vbCrLf
                If dr("FontStyle").ToString.ToLower = "normal" OrElse dr("FontStyle").ToString.ToLower = "bold" Then
                    cssString = cssString & "font-weight: " & dr("FontStyle").ToString & "; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: normal; /* FontStyle */" & vbCrLf
                ElseIf dr("FontStyle").ToString.ToLower = "italic" Then
                    cssString = cssString & "font-weight: normal; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: italic; /* FontStyle */" & vbCrLf
                Else
                    cssString = cssString & "font-weight: bold; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: italic; /* FontStyle */" & vbCrLf
                End If
                cssString = cssString & "text-decoration: none;" & vbCrLf
                cssString = cssString & "background-color: " & dr("BackgroundColor").ToString & "; /* Background color */" & vbCrLf
                If (dr("BackgroundImageURL").ToString().Length <> 0) Then
                    If flag = True Then
                        cssString = cssString & "background-image: url(" & dr("BackgroundImageURL").ToString & "); /* Background Image */" & vbCrLf
                    Else
                        cssString = cssString & "background-image: url(" & StoreFrontConfiguration.SSLPath & "images/" & dr("BackgroundImageURL").ToString & "); /* Background Image */" & vbCrLf
                    End If
                End If
                cssString = cssString & "text-align: " & dr("HorizontalAlignment").ToString & "; /* Align Text */" & vbCrLf
                cssString = cssString & "}" & vbCrLf & vbCrLf
            End If
        Next

        For Each dr In drRows
            If dr("Name").ToString() = "TopSubBanner" Then
                cssString = cssString & ".TopSubBanner" & vbCrLf
                cssString = cssString & "{" & vbCrLf
                cssString = cssString & "font-family: " & dr("FontFace").ToString & "; /* Font Face */" & vbCrLf
                cssString = cssString & "font-size: " & dr("FontSize").ToString & "pt; /* Font Size */" & vbCrLf
                cssString = cssString & "color: " & dr("FontColor").ToString & "; /* Font Color */" & vbCrLf
                If dr("FontStyle").ToString.ToLower = "normal" OrElse dr("FontStyle").ToString.ToLower = "bold" Then
                    cssString = cssString & "font-weight: " & dr("FontStyle").ToString & "; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: normal; /* FontStyle */" & vbCrLf
                ElseIf dr("FontStyle").ToString.ToLower = "italic" Then
                    cssString = cssString & "font-weight: normal; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: italic; /* FontStyle */" & vbCrLf
                Else
                    cssString = cssString & "font-weight: bold; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: italic; /* FontStyle */" & vbCrLf
                End If
                cssString = cssString & "text-decoration: none;" & vbCrLf
                cssString = cssString & "background-color: " & dr("BackgroundColor").ToString & "; /* Background color */" & vbCrLf
                If (dr("BackgroundImageURL").ToString().Length <> 0) Then
                    If flag = True Then
                        cssString = cssString & "background-image: url(" & dr("BackgroundImageURL").ToString & "); /* Background Image */" & vbCrLf
                    Else
                        cssString = cssString & "background-image: url(" & StoreFrontConfiguration.SSLPath & "images/" & dr("BackgroundImageURL").ToString & "); /* Background Image */" & vbCrLf
                    End If
                End If
                cssString = cssString & "text-align: " & dr("HorizontalAlignment").ToString & "; /* Align Text */" & vbCrLf
                cssString = cssString & "}" & vbCrLf & vbCrLf
            End If
        Next

        'update #2319
        For Each dr In drRows
            If dr("Name").ToString() = "TopSubBanner" Then
                cssString = cssString & ".TopSubBannerText" & vbCrLf
                cssString = cssString & "{" & vbCrLf
                cssString = cssString & "font-family: " & dr("FontFace").ToString & "; /* Font Face */" & vbCrLf
                cssString = cssString & "font-size: " & dr("FontSize").ToString & "pt; /* Font Size */" & vbCrLf
                cssString = cssString & "color: " & dr("FontColor").ToString & "; /* Font Color */" & vbCrLf
                If dr("FontStyle").ToString.ToLower = "normal" OrElse dr("FontStyle").ToString.ToLower = "bold" Then
                    cssString = cssString & "font-weight: " & dr("FontStyle").ToString & "; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: normal; /* FontStyle */" & vbCrLf
                ElseIf dr("FontStyle").ToString.ToLower = "italic" Then
                    cssString = cssString & "font-weight: normal; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: italic; /* FontStyle */" & vbCrLf
                Else
                    cssString = cssString & "font-weight: bold; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: italic; /* FontStyle */" & vbCrLf
                End If
                cssString = cssString & "text-decoration: none;" & vbCrLf
                cssString = cssString & "text-align: " & dr("HorizontalAlignment").ToString & "; /* Align Text */" & vbCrLf
                cssString = cssString & "}" & vbCrLf & vbCrLf
            End If
        Next

        For Each dr In drRows
            If dr("Name").ToString() = "LeftColumn" Then
                cssString = cssString & ".LeftColumn" & vbCrLf
                cssString = cssString & "{" & vbCrLf
                cssString = cssString & "font-family: " & dr("FontFace").ToString & "; /* Font Face */" & vbCrLf
                cssString = cssString & "font-size: " & dr("FontSize").ToString & "pt; /* Font Size */" & vbCrLf
                cssString = cssString & "color: " & dr("FontColor").ToString & "; /* Font Color */" & vbCrLf
                If dr("FontStyle").ToString.ToLower = "normal" OrElse dr("FontStyle").ToString.ToLower = "bold" Then
                    cssString = cssString & "font-weight: " & dr("FontStyle").ToString & "; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: normal; /* FontStyle */" & vbCrLf
                ElseIf dr("FontStyle").ToString.ToLower = "italic" Then
                    cssString = cssString & "font-weight: normal; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: italic; /* FontStyle */" & vbCrLf
                Else
                    cssString = cssString & "font-weight: bold; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: italic; /* FontStyle */" & vbCrLf
                End If

                cssString = cssString & "text-decoration: none;" & vbCrLf
                cssString = cssString & "background-color: " & dr("BackgroundColor").ToString & "; /* Background color */" & vbCrLf
                If (dr("BackgroundImageURL").ToString().Length <> 0) Then
                    If flag = True Then
                        cssString = cssString & "background-image: url(" & dr("BackgroundImageURL").ToString & "); /* Background Image */" & vbCrLf
                    Else
                        cssString = cssString & "background-image: url(" & StoreFrontConfiguration.SSLPath & "images/" & dr("BackgroundImageURL").ToString & "); /* Background Image */" & vbCrLf
                    End If
                End If
                cssString = cssString & "text-align: " & dr("HorizontalAlignment").ToString & "; /* Align Text */" & vbCrLf
                cssString = cssString & "vertical-align: " & dr("VerticalAlignment").ToString & "; /* Align Text */" & vbCrLf
                If (dr("TableWidth").ToString().Trim.IndexOf("%") <> -1) Then
                    cssString = cssString & "width: " & dr("TableWidth").ToString() & ";"
                Else
                    cssString = cssString & "width: " & dr("TableWidth").ToString() & "px;"
                End If
                cssString = cssString & "}" & vbCrLf & vbCrLf
            End If
        Next

        'update #2319
        For Each dr In drRows
            If dr("Name").ToString() = "LeftColumn" Then
                cssString = cssString & ".LeftColumnText" & vbCrLf
                cssString = cssString & "{" & vbCrLf
                cssString = cssString & "font-family: " & dr("FontFace").ToString & "; /* Font Face */" & vbCrLf
                cssString = cssString & "font-size: " & dr("FontSize").ToString & "pt; /* Font Size */" & vbCrLf
                cssString = cssString & "color: " & dr("FontColor").ToString & "; /* Font Color */" & vbCrLf
                If dr("FontStyle").ToString.ToLower = "normal" OrElse dr("FontStyle").ToString.ToLower = "bold" Then
                    cssString = cssString & "font-weight: " & dr("FontStyle").ToString & "; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: normal; /* FontStyle */" & vbCrLf
                ElseIf dr("FontStyle").ToString.ToLower = "italic" Then
                    cssString = cssString & "font-weight: normal; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: italic; /* FontStyle */" & vbCrLf
                Else
                    cssString = cssString & "font-weight: bold; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: italic; /* FontStyle */" & vbCrLf
                End If
                cssString = cssString & "text-decoration: none;" & vbCrLf
                cssString = cssString & "text-align: " & dr("HorizontalAlignment").ToString & "; /* Align Text */" & vbCrLf
                cssString = cssString & "vertical-align: " & dr("VerticalAlignment").ToString & "; /* Align Text */" & vbCrLf
                cssString = cssString & "}" & vbCrLf & vbCrLf
            End If
        Next


        For Each dr In drRows
            If dr("Name").ToString() = "RightColumn" Then
                cssString = cssString & ".RightColumn" & vbCrLf
                cssString = cssString & "{" & vbCrLf
                cssString = cssString & "font-family: " & dr("FontFace").ToString & "; /* Font Face */" & vbCrLf
                cssString = cssString & "font-size: " & dr("FontSize").ToString & "pt; /* Font Size */" & vbCrLf
                cssString = cssString & "color: " & dr("FontColor").ToString & "; /* Font Color */" & vbCrLf
                If dr("FontStyle").ToString.ToLower = "normal" OrElse dr("FontStyle").ToString.ToLower = "bold" Then
                    cssString = cssString & "font-weight: " & dr("FontStyle").ToString & "; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: normal; /* FontStyle */" & vbCrLf
                ElseIf dr("FontStyle").ToString.ToLower = "italic" Then
                    cssString = cssString & "font-weight: normal; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: italic; /* FontStyle */" & vbCrLf
                Else
                    cssString = cssString & "font-weight: bold; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: italic; /* FontStyle */" & vbCrLf
                End If
                cssString = cssString & "text-decoration: none;" & vbCrLf
                cssString = cssString & "background-color: " & dr("BackgroundColor").ToString & "; /* Background color */" & vbCrLf
                If (dr("BackgroundImageURL").ToString().Length <> 0) Then
                    If flag = True Then
                        cssString = cssString & "background-image: url(" & dr("BackgroundImageURL").ToString & "); /* Background Image */" & vbCrLf
                    Else
                        cssString = cssString & "background-image: url(" & StoreFrontConfiguration.SSLPath & "images/" & dr("BackgroundImageURL").ToString & "); /* Background Image */" & vbCrLf
                    End If
                End If
                cssString = cssString & "text-align: " & dr("HorizontalAlignment").ToString & "; /* Align Text */" & vbCrLf
                cssString = cssString & "vertical-align: " & dr("VerticalAlignment").ToString & "; /* Align Text */" & vbCrLf
                If (dr("TableWidth").ToString().Trim.IndexOf("%") <> -1) Then
                    cssString = cssString & "width: " & dr("TableWidth").ToString() & ";"
                Else
                    cssString = cssString & "width: " & dr("TableWidth").ToString() & "px;"
                End If
                cssString = cssString & "}" & vbCrLf & vbCrLf
            End If
        Next

        'update #2319
        For Each dr In drRows
            If dr("Name").ToString() = "RightColumn" Then
                cssString = cssString & ".RightColumnText" & vbCrLf
                cssString = cssString & "{" & vbCrLf
                cssString = cssString & "font-family: " & dr("FontFace").ToString & "; /* Font Face */" & vbCrLf
                cssString = cssString & "font-size: " & dr("FontSize").ToString & "pt; /* Font Size */" & vbCrLf
                cssString = cssString & "color: " & dr("FontColor").ToString & "; /* Font Color */" & vbCrLf
                If dr("FontStyle").ToString.ToLower = "normal" OrElse dr("FontStyle").ToString.ToLower = "bold" Then
                    cssString = cssString & "font-weight: " & dr("FontStyle").ToString & "; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: normal; /* FontStyle */" & vbCrLf
                ElseIf dr("FontStyle").ToString.ToLower = "italic" Then
                    cssString = cssString & "font-weight: normal; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: italic; /* FontStyle */" & vbCrLf
                Else
                    cssString = cssString & "font-weight: bold; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: italic; /* FontStyle */" & vbCrLf
                End If
                cssString = cssString & "text-decoration: none;" & vbCrLf
                cssString = cssString & "text-align: " & dr("HorizontalAlignment").ToString & "; /* Align Text */" & vbCrLf
                cssString = cssString & "vertical-align: " & dr("VerticalAlignment").ToString & "; /* Align Text */" & vbCrLf
                cssString = cssString & "}" & vbCrLf & vbCrLf
            End If
        Next

        For Each dr In drRows
            If dr("Name").ToString() = "Footer" Then
                cssString = cssString & ".Footer" & vbCrLf
                cssString = cssString & "{" & vbCrLf
                cssString = cssString & "font-family: " & dr("FontFace").ToString & "; /* Font Face */" & vbCrLf
                cssString = cssString & "font-size: " & dr("FontSize").ToString & "pt; /* Font Size */" & vbCrLf
                cssString = cssString & "color: " & dr("FontColor").ToString & "; /* Font Color */" & vbCrLf
                If dr("FontStyle").ToString.ToLower = "normal" OrElse dr("FontStyle").ToString.ToLower = "bold" Then
                    cssString = cssString & "font-weight: " & dr("FontStyle").ToString & "; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: normal; /* FontStyle */" & vbCrLf
                ElseIf dr("FontStyle").ToString.ToLower = "italic" Then
                    cssString = cssString & "font-weight: normal; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: italic; /* FontStyle */" & vbCrLf
                Else
                    cssString = cssString & "font-weight: bold; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: italic; /* FontStyle */" & vbCrLf
                End If
                cssString = cssString & "text-decoration: none;" & vbCrLf
                cssString = cssString & "background-color: " & dr("BackgroundColor").ToString & "; /* Background color */" & vbCrLf
                If (dr("BackgroundImageURL").ToString().Length <> 0) Then
                    If flag = True Then
                        cssString = cssString & "background-image: url(" & dr("BackgroundImageURL").ToString & "); /* Background Image */" & vbCrLf
                    Else
                        cssString = cssString & "background-image: url(" & StoreFrontConfiguration.SSLPath & "images/" & dr("BackgroundImageURL").ToString & "); /* Background Image */" & vbCrLf
                    End If
                End If
                cssString = cssString & "text-align: " & dr("HorizontalAlignment").ToString & "; /* Align Text */" & vbCrLf
                cssString = cssString & "vertical-align: " & dr("VerticalAlignment").ToString & "; /* Align Text */" & vbCrLf
                cssString = cssString & "}" & vbCrLf & vbCrLf
            End If
        Next

        'update #2319
        For Each dr In drRows
            If dr("Name").ToString() = "Footer" Then
                cssString = cssString & ".FooterText" & vbCrLf
                cssString = cssString & "{" & vbCrLf
                cssString = cssString & "font-family: " & dr("FontFace").ToString & "; /* Font Face */" & vbCrLf
                cssString = cssString & "font-size: " & dr("FontSize").ToString & "pt; /* Font Size */" & vbCrLf
                cssString = cssString & "color: " & dr("FontColor").ToString & "; /* Font Color */" & vbCrLf
                If dr("FontStyle").ToString.ToLower = "normal" OrElse dr("FontStyle").ToString.ToLower = "bold" Then
                    cssString = cssString & "font-weight: " & dr("FontStyle").ToString & "; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: normal; /* FontStyle */" & vbCrLf
                ElseIf dr("FontStyle").ToString.ToLower = "italic" Then
                    cssString = cssString & "font-weight: normal; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: italic; /* FontStyle */" & vbCrLf
                Else
                    cssString = cssString & "font-weight: bold; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: italic; /* FontStyle */" & vbCrLf
                End If
                cssString = cssString & "text-decoration: none;" & vbCrLf
                cssString = cssString & "text-align: " & dr("HorizontalAlignment").ToString & "; /* Align Text */" & vbCrLf
                cssString = cssString & "vertical-align: " & dr("VerticalAlignment").ToString & "; /* Align Text */" & vbCrLf
                cssString = cssString & "}" & vbCrLf & vbCrLf
            End If
        Next

        For Each dr In drRows
            If dr("Name").ToString() = "Instruction" Then
                cssString = cssString & ".Instructions" & vbCrLf
                cssString = cssString & "{" & vbCrLf
                cssString = cssString & "font-family: " & dr("FontFace").ToString & "; /* Font Face */" & vbCrLf
                cssString = cssString & "font-size: " & dr("FontSize").ToString & "pt; /* Font Size */" & vbCrLf
                cssString = cssString & "color: " & dr("FontColor").ToString & "; /* Font Color */" & vbCrLf
                If dr("FontStyle").ToString.ToLower = "normal" OrElse dr("FontStyle").ToString.ToLower = "bold" Then
                    cssString = cssString & "font-weight: " & dr("FontStyle").ToString & "; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: normal; /* FontStyle */" & vbCrLf
                ElseIf dr("FontStyle").ToString.ToLower = "italic" Then
                    cssString = cssString & "font-weight: normal; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: italic; /* FontStyle */" & vbCrLf
                Else
                    cssString = cssString & "font-weight: bold; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: italic; /* FontStyle */" & vbCrLf
                End If
                cssString = cssString & "text-decoration: none; /* Font Style */" & vbCrLf
                cssString = cssString & "background-color: " & dr("BackgroundColor").ToString & "; /* Background color */" & vbCrLf
                If (dr("BackgroundImageURL").ToString().Length <> 0) Then
                    If flag = True Then
                        cssString = cssString & "background-image: url(" & dr("BackgroundImageURL").ToString & "); /* Background Image */" & vbCrLf
                    Else
                        cssString = cssString & "background-image: url(" & StoreFrontConfiguration.SSLPath & "images/" & dr("BackgroundImageURL").ToString & "); /* Background Image */" & vbCrLf
                    End If
                End If
                cssString = cssString & "text-align: " & dr("HorizontalAlignment").ToString & "; /* Align Text */" & vbCrLf
                cssString = cssString & "}" & vbCrLf & vbCrLf
            End If
        Next

        For Each dr In drRows
            If dr("Name").ToString() = "ErrorMessages" Then
                cssString = cssString & ".ErrorMessages" & vbCrLf
                cssString = cssString & "{" & vbCrLf
                cssString = cssString & "font-family: " & dr("FontFace").ToString & "; /* Font Face */" & vbCrLf
                cssString = cssString & "font-size: " & dr("FontSize").ToString & "pt; /* Font Size */" & vbCrLf
                cssString = cssString & "color: " & dr("FontColor").ToString & "; /* Font Color */" & vbCrLf
                If dr("FontStyle").ToString.ToLower = "normal" OrElse dr("FontStyle").ToString.ToLower = "bold" Then
                    cssString = cssString & "font-weight: " & dr("FontStyle").ToString & "; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: normal; /* FontStyle */" & vbCrLf
                ElseIf dr("FontStyle").ToString.ToLower = "italic" Then
                    cssString = cssString & "font-weight: normal; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: italic; /* FontStyle */" & vbCrLf
                Else
                    cssString = cssString & "font-weight: bold; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: italic; /* FontStyle */" & vbCrLf
                End If
                cssString = cssString & "text-decoration: none; /* Font Style */" & vbCrLf
                cssString = cssString & "background-color: " & dr("BackgroundColor").ToString & "; /* Background color */" & vbCrLf
                If (dr("BackgroundImageURL").ToString().Length <> 0) Then
                    If flag = True Then
                        cssString = cssString & "background-image: url(" & dr("BackgroundImageURL").ToString & "); /* Background Image */" & vbCrLf
                    Else
                        cssString = cssString & "background-image: url(" & StoreFrontConfiguration.SSLPath & "images/" & dr("BackgroundImageURL").ToString & "); /* Background Image */" & vbCrLf
                    End If
                End If
                cssString = cssString & "text-align: " & dr("HorizontalAlignment").ToString & "; /* Align Text */" & vbCrLf
                cssString = cssString & "}" & vbCrLf & vbCrLf
            End If
        Next

        For Each dr In drRows
            If dr("Name").ToString() = "Messages" Then
                cssString = cssString & ".Messages" & vbCrLf
                cssString = cssString & "{" & vbCrLf
                cssString = cssString & "font-family: " & dr("FontFace").ToString & "; /* Font Face */" & vbCrLf
                cssString = cssString & "font-size: " & dr("FontSize").ToString & "pt; /* Font Size */" & vbCrLf
                cssString = cssString & "color: " & dr("FontColor").ToString & "; /* Font Color */" & vbCrLf
                If dr("FontStyle").ToString.ToLower = "normal" OrElse dr("FontStyle").ToString.ToLower = "bold" Then
                    cssString = cssString & "font-weight: " & dr("FontStyle").ToString & "; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: normal; /* FontStyle */" & vbCrLf
                ElseIf dr("FontStyle").ToString.ToLower = "italic" Then
                    cssString = cssString & "font-weight: normal; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: italic; /* FontStyle */" & vbCrLf
                Else
                    cssString = cssString & "font-weight: bold; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: italic; /* FontStyle */" & vbCrLf
                End If
                cssString = cssString & "text-decoration: none; /* Font Style */" & vbCrLf
                cssString = cssString & "background-color: " & dr("BackgroundColor").ToString & "; /* Background color */" & vbCrLf
                If (dr("BackgroundImageURL").ToString().Length <> 0) Then
                    If flag = True Then
                        cssString = cssString & "background-image: url(" & dr("BackgroundImageURL").ToString & "); /* Background Image */" & vbCrLf
                    Else
                        cssString = cssString & "background-image: url(" & StoreFrontConfiguration.SSLPath & "images/" & dr("BackgroundImageURL").ToString & "); /* Background Image */" & vbCrLf
                    End If
                End If
                cssString = cssString & "text-align: " & dr("HorizontalAlignment").ToString & "; /* Align Text */" & vbCrLf
                cssString = cssString & "}" & vbCrLf & vbCrLf
            End If
        Next

        For Each dr In drRows
            If dr("Name").ToString() = "Headings" Then
                cssString = cssString & ".Headings" & vbCrLf
                cssString = cssString & "{" & vbCrLf
                cssString = cssString & "font-family: " & dr("FontFace").ToString & "; /* Font Face */" & vbCrLf
                cssString = cssString & "font-size: " & dr("FontSize").ToString & "pt; /* Font Size */" & vbCrLf
                cssString = cssString & "color: " & dr("FontColor").ToString & "; /* Font Color */" & vbCrLf
                If dr("FontStyle").ToString.ToLower = "normal" OrElse dr("FontStyle").ToString.ToLower = "bold" Then
                    cssString = cssString & "font-weight: " & dr("FontStyle").ToString & "; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: normal; /* FontStyle */" & vbCrLf
                ElseIf dr("FontStyle").ToString.ToLower = "italic" Then
                    cssString = cssString & "font-weight: normal; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: italic; /* FontStyle */" & vbCrLf
                Else
                    cssString = cssString & "font-weight: bold; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: italic; /* FontStyle */" & vbCrLf
                End If
                cssString = cssString & "text-decoration: none; /* Font Style */" & vbCrLf
                cssString = cssString & "background-color: " & dr("BackgroundColor").ToString & "; /* Background color */" & vbCrLf
                If (dr("BackgroundImageURL").ToString().Length <> 0) Then
                    cssString = cssString & "background-image: url(" & StoreFrontConfiguration.SSLPath & "images/" & dr("BackgroundImageURL").ToString & "); /* Background Image */" & vbCrLf
                End If
                cssString = cssString & "text-align: " & dr("HorizontalAlignment").ToString & "; /* Align Text */" & vbCrLf
                cssString = cssString & "}" & vbCrLf & vbCrLf
            End If
        Next



        For Each dr In drRows
            If dr("Name").ToString() = "Content" Then
                cssString = cssString & ".Content" & vbCrLf
                cssString = cssString & "{" & vbCrLf
                cssString = cssString & "font-family: " & dr("FontFace").ToString & "; /* Font Face */" & vbCrLf
                cssString = cssString & "font-size: " & dr("FontSize").ToString & "pt; /* Font Size */" & vbCrLf
                cssString = cssString & "color: " & dr("FontColor").ToString & "; /* Font Color */" & vbCrLf
                If dr("FontStyle").ToString.ToLower = "normal" OrElse dr("FontStyle").ToString.ToLower = "bold" Then
                    cssString = cssString & "font-weight: " & dr("FontStyle").ToString & "; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: normal; /* FontStyle */" & vbCrLf
                ElseIf dr("FontStyle").ToString.ToLower = "italic" Then
                    cssString = cssString & "font-weight: normal; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: italic; /* FontStyle */" & vbCrLf
                Else
                    cssString = cssString & "font-weight: bold; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: italic; /* FontStyle */" & vbCrLf
                End If
                cssString = cssString & "text-decoration: none; /* Font Style */" & vbCrLf
                cssString = cssString & "background-color: " & dr("BackgroundColor").ToString & "; /* Background color */" & vbCrLf
                If (dr("BackgroundImageURL").ToString().Length <> 0) Then
                    If flag = True Then
                        cssString = cssString & "background-image: url(" & dr("BackgroundImageURL").ToString & "); /* Background Image */" & vbCrLf
                    Else
                        cssString = cssString & "background-image: url(" & StoreFrontConfiguration.SSLPath & "images/" & dr("BackgroundImageURL").ToString & "); /* Background Image */" & vbCrLf
                    End If
                End If
                cssString = cssString & "text-align: " & dr("HorizontalAlignment").ToString & "; /* Align Text */" & vbCrLf
                cssString = cssString & "}" & vbCrLf & vbCrLf
            End If
        Next

        For Each dr In drRows
            If dr("Name").ToString() = "ContentTableHeader" Then
                cssString = cssString & ".ContentTableHeader" & vbCrLf
                cssString = cssString & "{" & vbCrLf
                cssString = cssString & "font-family: " & dr("FontFace").ToString & "; /* Font Face */" & vbCrLf
                cssString = cssString & "font-size: " & dr("FontSize").ToString & "pt; /* Heading Font Size */" & vbCrLf
                cssString = cssString & "color: " & dr("FontColor").ToString & "; /* Heading Font Color */" & vbCrLf
                If dr("FontStyle").ToString.ToLower = "normal" OrElse dr("FontStyle").ToString.ToLower = "bold" Then
                    cssString = cssString & "font-weight: " & dr("FontStyle").ToString & "; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: normal; /* FontStyle */" & vbCrLf
                ElseIf dr("FontStyle").ToString.ToLower = "italic" Then
                    cssString = cssString & "font-weight: normal; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: italic; /* FontStyle */" & vbCrLf
                Else
                    cssString = cssString & "font-weight: bold; /* Font Style */" & vbCrLf
                    cssString = cssString & "font-style: italic; /* FontStyle */" & vbCrLf
                End If
                cssString = cssString & "text-decoration: none; /* Heading Font Style */" & vbCrLf
                cssString = cssString & "background-color: " & dr("BackgroundColor").ToString & "; /* Background color */" & vbCrLf
                If (dr("BackgroundImageURL").ToString().Length <> 0) Then
                    If flag = True Then
                        cssString = cssString & "background-image: url(" & dr("BackgroundImageURL").ToString & "); /* Background Image */" & vbCrLf
                    Else
                        cssString = cssString & "background-image: url(" & StoreFrontConfiguration.SSLPath & "images/" & dr("BackgroundImageURL").ToString & "); /* Background Image */" & vbCrLf
                    End If
                End If
                cssString = cssString & "text-align: " & dr("HorizontalAlignment").ToString & "; /* Align Text */" & vbCrLf
                cssString = cssString & "}" & vbCrLf & vbCrLf

                cssString = cssString & ".ContentTable" & vbCrLf
                cssString = cssString & "{" & vbCrLf
                cssString = cssString & "background-color: " & dr("BorderColor").ToString & ";" & vbCrLf
                cssString = cssString & "}" & vbCrLf & vbCrLf

                cssString = cssString & ".ContentTableHorizontal" & vbCrLf
                cssString = cssString & "{" & vbCrLf
                cssString = cssString & "background-color: " & dr("BorderColor").ToString & ";" & vbCrLf
                cssString = cssString & "}" & vbCrLf & vbCrLf
            End If
        Next
        cssString = cssString & "</Style>"
        Return cssString
    End Function
#End Region

End Class
