USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[sp_shipped_overview]    Script Date: 02/14/2014 13:09:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[sp_shipped_overview] (@date_from VARCHAR(20) = '', @date_to VARCHAR(20) = '', @partner_ID INT = 0) AS

	If @Partner_ID = -1
		EXEC dbo.sp_shipped_overview_without_partner @date_from, @date_to
	Else
		EXEC dbo.sp_shipped_overview_with_partner @date_from, @date_to, @partner_ID
GO
