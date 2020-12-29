USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_efo_email_type_by_id]    Script Date: 02/14/2014 13:04:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for EFO_Email_Type
CREATE PROCEDURE [dbo].[efrcrm_get_efo_email_type_by_id] @Email_Type_ID int AS
begin

select Email_Type_ID, Body, Description from EFO_Email_Type where Email_Type_ID=@Email_Type_ID

end
GO
