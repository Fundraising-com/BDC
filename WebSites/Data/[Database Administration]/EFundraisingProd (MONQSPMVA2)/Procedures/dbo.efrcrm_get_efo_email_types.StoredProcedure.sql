USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_efo_email_types]    Script Date: 02/14/2014 13:04:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for EFO_Email_Type
CREATE PROCEDURE [dbo].[efrcrm_get_efo_email_types] AS
begin

select Email_Type_ID, Body, Description from EFO_Email_Type

end
GO
