USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[Leads_Not_Completed_Activity_View]    Script Date: 02/14/2014 13:02:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  View dbo.Leads_Not_Completed_Activity_View    Script Date: 2003-02-22 20:34:17 ******/

CREATE VIEW [dbo].[Leads_Not_Completed_Activity_View]
AS
SELECT DISTINCT Lead_ID FROM Lead_Activity WHERE Completed_Date is null
GO
