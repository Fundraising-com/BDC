USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_administrative_email]    Script Date: 02/14/2014 13:07:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Administrative_Email
CREATE PROCEDURE [dbo].[efrcrm_update_administrative_email] @Administrative_ID int, @Email varchar(255), @First_Name varchar(50), @Last_Name varchar(50) AS
begin

update Administrative_Email set Email=@Email, First_Name=@First_Name, Last_Name=@Last_Name where Administrative_ID=@Administrative_ID

end
GO
