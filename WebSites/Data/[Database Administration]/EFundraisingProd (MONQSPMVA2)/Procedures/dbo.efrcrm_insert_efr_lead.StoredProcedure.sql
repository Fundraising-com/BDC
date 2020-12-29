USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_efr_lead]    Script Date: 02/14/2014 13:06:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for EFR_Lead
CREATE PROCEDURE [dbo].[efrcrm_insert_efr_lead] 
	@Lead_ID int OUTPUT, 
	@First_Name varchar(50), 
	@Last_Name varchar(50), 
	@Organization_Name varchar(100), 
	@Promotion_Description varchar(60), 
	@Lead_Activity_Detail varchar(255), 
	@Lead_Comment varchar(255), 
	@Activity_Scheduled_Date datetime, 
	@Consultant_ID int, 
	@Consultant_Ext int, 
	@Is_Done int, 
	@Phone_Number varchar(20), 
	@Phone_extension varchar(15), 
	@Promotion_Type varchar(50), 
	@2ndPhone_Number varchar(20), 
	@2ndPhone_Extension varchar(15) AS
begin

insert into EFR_Lead(
	First_Name, 
	Last_Name, 
	Organization_Name, 
	Promotion_Description, 
	Lead_Activity_Detail, 
	Lead_Comment, 
	Activity_Scheduled_Date, 
	Consultant_ID, 
	Consultant_Ext, 
	Is_Done, 
	Phone_Number, 
	Phone_extension, 
	Promotion_Type, 
	[2ndPhone_Number], 
	[2ndPhone_Extension]) 
values(@First_Name, 
	@Last_Name, 
	@Organization_Name, 
	@Promotion_Description, 
	@Lead_Activity_Detail, 
	@Lead_Comment, 
	@Activity_Scheduled_Date, 
	@Consultant_ID, 
	@Consultant_Ext, 
	@Is_Done, 
	@Phone_Number, 
	@Phone_extension, 
	@Promotion_Type, 
	@2ndPhone_Number, 
	@2ndPhone_Extension)

select @Lead_ID = SCOPE_IDENTITY()

end
GO
