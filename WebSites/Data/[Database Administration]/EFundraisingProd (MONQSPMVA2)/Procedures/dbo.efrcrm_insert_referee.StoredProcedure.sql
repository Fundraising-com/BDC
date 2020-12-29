USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_referee]    Script Date: 02/14/2014 13:07:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Referee
CREATE PROCEDURE [dbo].[efrcrm_insert_referee] @Referee_Id int OUTPUT, @Lead_Id int, @Entry_Date smalldatetime, @First_Name varchar(25), @Last_Name varchar(25), @Email varchar(50), @Phone_Number varchar(25), @Is_Entered bit AS
begin

insert into Referee(Lead_Id, Entry_Date, First_Name, Last_Name, Email, Phone_Number, Is_Entered) values(@Lead_Id, @Entry_Date, @First_Name, @Last_Name, @Email, @Phone_Number, @Is_Entered)

select @Referee_Id = SCOPE_IDENTITY()

end
GO
