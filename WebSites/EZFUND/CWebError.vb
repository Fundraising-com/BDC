'BEGINVERSIONINFO

'APPVERSION: 6.0.0.0

'STARTCOPYRIGHT
'The contents of this file is protected under the United States
'copyright laws and is confidential and proprietary to
'LaGarde, Incorporated.  Its use or disclosure in whole or in part without the
'expressed written permission of LaGarde, Incorporated is expressly prohibited.
'
'(c) Copyright 2002 by LaGarde, Incorporated.  All rights reserved.
'ENDCOPYRIGHT

'ENDVERSIONINFO

Namespace FrameworkExceptions

    Public Interface IStoreFrontError
        Property Name() As String

        Sub Process()
        Sub Track(ByVal str As String)
        Sub Debug(ByVal str As String)
    End Interface

    Public Class CStoreFrontWebError
        Inherits CStoreFrontError

        Private m_objError As ErrObject

        Sub New(ByVal err As ErrObject)
            MyBase.New()
            m_objError = err
        End Sub

        Public Overrides Sub Process()
            m_objtrace.Write(m_objError.Description)
        End Sub

        Public Overrides Sub TrackInfo(ByVal strName As String, Optional ByVal strMessage As String = "")
            Name = strName
            If (strMessage <> "") Then
                Track(strMessage)
            Else
                Track(m_objError.Description)
            End If
        End Sub
    End Class

    Public Class CStoreFrontExceptionError
        Inherits CStoreFrontError

        Private m_objError As Exception

        Sub New(ByVal err As Exception)
            MyBase.New()
            m_objError = err
        End Sub

        Public Overrides Sub Process()
            m_objtrace.Write(m_objError.Message)
        End Sub

        Public Overrides Sub TrackInfo(ByVal strName As String, Optional ByVal strMessage As String = "")
            Name = strName
            If (strMessage <> "") Then
                Track(strMessage)
            Else
                Track(m_objError.Message)
            End If
        End Sub
    End Class

    Public Class CStoreFrontError
        Implements IStoreFrontError

        Protected m_objTrace As CStoreFrontTrace
        Private m_strName As String

        Sub New()
            m_objTrace = New CStoreFrontTrace()
        End Sub

        Public Property Name() As String Implements IStoreFrontError.Name
            Get
                Return m_strName
            End Get
            Set(ByVal Value As String)
                m_strName = Value
            End Set
        End Property

        Public Overridable Sub Process() Implements IStoreFrontError.Process
        End Sub

        Public Overridable Sub TrackInfo(ByVal strName As String, Optional ByVal strMessage As String = "")
        End Sub

        Protected Friend Sub Track(ByVal strMessage As String) Implements IStoreFrontError.Track
            If (m_strName <> "") Then
                m_objTrace.Warn(m_strName, strMessage)
            Else
                m_objTrace.Warn(strMessage)
            End If
        End Sub

        Protected Friend Sub Debug(ByVal strMessage As String) Implements IStoreFrontError.Debug
            If (m_strName <> "") Then
                m_objTrace.Warn(m_strName, "Debug Statement")
            Else
                m_objTrace.Warn(strMessage)
            End If
            If (m_strName <> "") Then
                m_objTrace.Write(m_strName, strMessage)
            Else
                m_objTrace.Warn(strMessage)
            End If
        End Sub

        Protected Class CStoreFrontTrace

            Private m_objContext As HttpContext
            Private m_objTraceContext As TraceContext

            Sub New()
                m_objContext = HttpContext.Current
                m_objContext.Trace.IsEnabled = True
                m_objTraceContext = m_objContext.Trace
            End Sub

            Public Sub Write(ByVal strMessage As String)
                m_objTraceContext.Write(strMessage)
            End Sub

            Public Sub Write(ByVal strName As String, ByVal strMessage As String)
                m_objTraceContext.Write(strMessage)
            End Sub

            Public Sub Warn(ByVal strMessage As String)
                m_objTraceContext.Warn(strMessage)
            End Sub

            Public Sub Warn(ByVal strName As String, ByVal strMessage As String)
                m_objTraceContext.Warn(strName, strMessage)
            End Sub
        End Class

    End Class

End Namespace
