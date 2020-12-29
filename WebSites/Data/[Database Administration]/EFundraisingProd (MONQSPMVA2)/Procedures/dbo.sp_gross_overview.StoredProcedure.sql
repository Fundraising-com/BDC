USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[sp_gross_overview]    Script Date: 02/14/2014 13:08:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- sp_ code
CREATE     PROCEDURE [dbo].[sp_gross_overview] (@date_from VARCHAR(20) = '', @date_to VARCHAR(20) = '', @Partner_ID INT = 0) AS

	IF @Partner_ID = -1
		EXEC sp_gross_overview_without_partner @date_from, @date_to
	ELSE
		EXEC sp_gross_overview_with_partner @date_from, @date_to, @Partner_ID
GO
