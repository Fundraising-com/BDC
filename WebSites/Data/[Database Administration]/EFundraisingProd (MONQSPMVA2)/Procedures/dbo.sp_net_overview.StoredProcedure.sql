USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[sp_net_overview]    Script Date: 02/14/2014 13:09:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[sp_net_overview] (@date_from VARCHAR(20) = '', @date_to VARCHAR(20) = '', @Partner_ID INT = 0) AS

	IF @Partner_ID = -1
		EXEC dbo.sp_net_overview_without_partner @date_from, @date_to
	ELSE
		EXEC dbo.sp_net_overview_with_partner @date_from, @date_to, @partner_ID
GO
