USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_administrative_email_by_id]    Script Date: 02/14/2014 13:03:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Administrative_Email
CREATE PROCEDURE [dbo].[efrcrm_get_administrative_email_by_id] @Administrative_ID int AS
begin

select Administrative_ID, Email, First_Name, Last_Name from Administrative_Email where Administrative_ID=@Administrative_ID

end
GO
