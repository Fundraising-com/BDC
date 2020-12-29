USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_efo_campaign]    Script Date: 02/14/2014 13:06:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for EFO_Campaign
CREATE PROCEDURE [dbo].[efrcrm_insert_efo_campaign] @Campaign_ID int OUTPUT, @Group_Type_ID int, @QSP_Program_ID int, @Campaign_Image_ID int, @Organizer_ID int, @Group_Name varchar(50), @Creation_Date smalldatetime, @Financial_Goal decimal, @Fund_Raising_Reason varchar(200), @Background_Info varchar(200), @Comments varchar(150), @Is_Launched bit, @Is_Over bit, @Account_Number varchar(15) AS
begin

insert into EFO_Campaign(Group_Type_ID, QSP_Program_ID, Campaign_Image_ID, Organizer_ID, Group_Name, Creation_Date, Financial_Goal, Fund_Raising_Reason, Background_Info, Comments, Is_Launched, Is_Over, Account_Number) values(@Group_Type_ID, @QSP_Program_ID, @Campaign_Image_ID, @Organizer_ID, @Group_Name, @Creation_Date, @Financial_Goal, @Fund_Raising_Reason, @Background_Info, @Comments, @Is_Launched, @Is_Over, @Account_Number)

select @Campaign_ID = SCOPE_IDENTITY()

end
GO
