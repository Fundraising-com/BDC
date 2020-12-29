USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_administrative_email]    Script Date: 02/14/2014 13:06:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Administrative_Email
CREATE PROCEDURE [dbo].[efrcrm_insert_administrative_email] @Administrative_ID int OUTPUT, @Email varchar(255), @First_Name varchar(50), @Last_Name varchar(50) AS
begin

insert into Administrative_Email(Email, First_Name, Last_Name) values(@Email, @First_Name, @Last_Name)

select @Administrative_ID = SCOPE_IDENTITY()

end
GO
