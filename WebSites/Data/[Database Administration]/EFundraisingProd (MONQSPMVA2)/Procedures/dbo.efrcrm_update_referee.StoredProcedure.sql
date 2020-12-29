USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_referee]    Script Date: 02/14/2014 13:08:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Referee
CREATE PROCEDURE [dbo].[efrcrm_update_referee] @Referee_Id int, @Lead_Id int, @Entry_Date smalldatetime, @First_Name varchar(25), @Last_Name varchar(25), @Email varchar(50), @Phone_Number varchar(25), @Is_Entered bit AS
begin

update Referee set Lead_Id=@Lead_Id, Entry_Date=@Entry_Date, First_Name=@First_Name, Last_Name=@Last_Name, Email=@Email, Phone_Number=@Phone_Number, Is_Entered=@Is_Entered where Referee_Id=@Referee_Id

end
GO
