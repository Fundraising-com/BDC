USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_administrative_emails]    Script Date: 02/14/2014 13:03:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Administrative_Email
CREATE PROCEDURE [dbo].[efrcrm_get_administrative_emails] AS
begin

select Administrative_ID, Email, First_Name, Last_Name from Administrative_Email

end
GO
