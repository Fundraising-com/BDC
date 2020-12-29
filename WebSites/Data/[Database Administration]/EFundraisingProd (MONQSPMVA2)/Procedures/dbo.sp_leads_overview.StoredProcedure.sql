USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[sp_leads_overview]    Script Date: 02/14/2014 13:09:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- sp_ code
CREATE  PROCEDURE [dbo].[sp_leads_overview] (@date_from VARCHAR(20) = '', @date_to VARCHAR(20) = '', @Partner_ID INT) AS

If @Partner_ID = -1
	EXEC sp_leads_overview_without_Partner @date_from, @date_to
Else
	EXEC sp_leads_overview_with_Partner @date_from, @date_to, @Partner_ID
GO
