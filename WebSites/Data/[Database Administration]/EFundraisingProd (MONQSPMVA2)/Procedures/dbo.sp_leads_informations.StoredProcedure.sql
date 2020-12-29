USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[sp_leads_informations]    Script Date: 02/14/2014 13:08:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE      PROCEDURE [dbo].[sp_leads_informations](@date_from VARCHAR(10) = NULL, @date_to VARCHAR(10) = NULL, @partner_id INTEGER = 0) AS

	If @partner_ID = -1
		EXEC dbo.sp_leads_informations_without_partner @date_from, @date_to
	Else
		exec dbo.sp_leads_informations_with_partner @date_from, @date_to, @partner_id
GO
