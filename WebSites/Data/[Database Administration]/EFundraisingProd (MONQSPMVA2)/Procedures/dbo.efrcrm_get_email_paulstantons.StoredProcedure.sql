USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_email_paulstantons]    Script Date: 02/14/2014 13:04:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for EMail_PaulStanton
CREATE PROCEDURE [dbo].[efrcrm_get_email_paulstantons] AS
begin

select ID, GoodEmail from EMail_PaulStanton

end
GO
