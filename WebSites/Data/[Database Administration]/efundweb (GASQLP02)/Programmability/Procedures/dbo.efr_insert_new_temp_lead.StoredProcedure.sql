USE [eFundweb]
GO
/****** Object:  StoredProcedure [dbo].[efr_insert_new_temp_lead]    Script Date: 02/14/2014 13:04:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[efr_insert_new_temp_lead]
	@Division_ID int, 
	@Promotion_ID int, 
	@Channel_Code varchar(4), 
	@Lead_Status_ID int, 
	@Consultant_ID int, 
	@Lead_Entry_Date datetime, 
	@Salutation varchar(10), 
	@First_Name varchar(50), 
	@Last_Name varchar(50), 
	@Organization varchar(100), 
	@Street_Address varchar(100), 
	@City varchar(50), 
	@State_Code varchar(10), 
	@Country_Code varchar(10), 
	@Zip_Code varchar(10), 
	@Day_Phone varchar(20), 
	@Day_Time_Call varchar(20), 
	@Evening_Phone varchar(20), 
	@Fax varchar(20), 
	@Email varchar(50), 
	@Group_Type_ID int, 	
	@Participant_Count int, 
	@Fund_Raising_Goal int, 
	@Decision_Date datetime, 
	@Decision_Maker bit, 
	@Fund_Raiser_Start_Date datetime, 
	@OnEmailList bit, 
	@Comments varchar(2000), 
	@Hear_ID int, 
	@Kit_to_send bit, 
	@Kit_sent bit, 
	@Kit_sent_date datetime, 
	@Day_Phone_Ext varchar(10), 
	@Evening_Phone_Ext varchar(10), 
	@Rejection_reason varchar(2000), 
	@Other_Phone varchar(20), 
	@Cookie_Content varchar(255), 
	@Group_Web_Site varchar(50), 
	@Organization_Type_ID int, 
	@Title_ID int, 
	@Campaign_Reason_ID int, 
	@Web_Site_ID int, 
	@Other_Phone_Ext varchar(10), 	
	@IsNew bit, 
	@Opt_In_Sweepstakes bit,
	@Group_ID int 

AS
declare @intErrorCode INT

BEGIN TRANSACTION

 SET @intErrorCode = @@ERROR

 INSERT INTO temp_Lead
  (
	Division_ID,
	Promotion_ID, 
	Channel_Code, 
	Lead_Status_ID, 
	Consultant_ID, 
	Lead_Entry_Date, 
	Salutation, 
	First_Name, 
	Last_Name, 
	Organization, 
	Street_Address, 
	City, 
	State_Code, 
	Country_Code, 
	Zip_Code, 	
	Day_Phone, 
	Day_Time_Call, 
	Evening_Phone, 
	Fax, 
	Email, 
	Group_Type_ID, 
	Participant_Count, 
	Fund_Raising_Goal, 
	Decision_Date, 
	Decision_Maker, 	
	Fund_Raiser_Start_Date, 
	OnEmailList, 
	Comments, 
	Hear_ID, 
	Kit_to_send, 	
	Kit_sent, 
	Kit_sent_date, 
	Day_Phone_Ext, 
	Evening_Phone_Ext, 
	Rejection_reason, 
	Other_Phone, 
	Cookie_Content, 
	Group_Web_Site, 
	Organization_Type_ID, 
	Title_ID, 
	Campaign_Reason_ID, 
	Web_Site_ID, 
	Other_Phone_Ext, 
	IsNew, 
	Opt_In_Sweepstakes,
	Group_ID
  ) 

values
  (
	@Division_ID,
	@Promotion_ID, 	
	@Channel_Code, 
	@Lead_Status_ID, 
	@Consultant_ID, 
	@Lead_Entry_Date, 
	@Salutation, 
	@First_Name, 
	@Last_Name, 
	@Organization, 
	@Street_Address, 
	@City, 
	@State_Code, 
	@Country_Code, 
	@Zip_Code, 
	@Day_Phone, 
	@Day_Time_Call, 
	@Evening_Phone, 
	@Fax, 
	@Email, 
	@Group_Type_ID, 
	@Participant_Count, 
	@Fund_Raising_Goal, 
	@Decision_Date, 
	@Decision_Maker, 
	@Fund_Raiser_Start_Date, 
	@OnEmailList, 
	@Comments, 
	@Hear_ID, 
	@Kit_to_send, 	
	@Kit_sent, 	
	@Kit_sent_date, 	
	@Day_Phone_Ext, 
	@Evening_Phone_Ext, 
	@Rejection_reason, 
	@Other_Phone, 
	@Cookie_Content, 
	@Group_Web_Site, 
	@Organization_Type_ID, 
	@Title_ID, 
	@Campaign_Reason_ID, 
	@Web_Site_ID, 	
	@Other_Phone_Ext, 
	@IsNew, 
	@Opt_In_Sweepstakes,
	@Group_ID
  )  

 SET @intErrorCode = @@ERROR
 IF @intErrorCode = 0 
 begin
  	SET @intErrorCode = 0
	commit transaction
	return @intErrorCode	
 end
 ELSE 
 begin
  	SET @intErrorCode = -1
	rollback transaction
	return @intErrorCode
 end
GO
