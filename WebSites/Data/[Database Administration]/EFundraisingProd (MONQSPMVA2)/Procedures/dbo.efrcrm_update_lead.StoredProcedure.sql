USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_lead]    Script Date: 02/14/2014 13:08:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Lead
CREATE PROCEDURE [dbo].[efrcrm_update_lead] 
@Lead_id int, @Lead_status_id int, @Lead_qualification_type_id int, @Lead_priority_id int, @Temp_lead_id int, @Division_id tinyint, @Promotion_id int, @Channel_code varchar(4), 
@Consultant_id int, @Group_type_id tinyint, @Organization_type_id tinyint, @Hear_id tinyint, @Fk_kit_type_id int, @Old_lead_id int, @Assigner_id int, @Referee_id int, 
@Title_id tinyint, @Campaign_reason_id tinyint, @Web_site_id int, @Promotion_code_id int, @Activity_closing_reason_id tinyint, @Ext_consultant_id int, @Salutation varchar(10), 
@First_name varchar(50), @Last_name varchar(50), @Organization varchar(100), @Street_address varchar(100), @City varchar(50), @State_code varchar(10), @Country_code varchar(10), 
@Zip_code varchar(10), @Day_phone varchar(20), @Day_time_call varchar(20), @Evening_phone varchar(20), @Evening_time_call varchar(20), @Fax varchar(20), @Email varchar(50), 
@Lead_entry_date datetime, @Member_count int, @Participant_count int, @Fund_raising_goal int, @Decision_date datetime, @Decision_maker bit, @Committee_meeting_required bit, 
@Committee_meeting_date datetime, @Fund_raiser_start_date datetime, @Onemaillist bit, @Faxkit bit, @Emailkit bit, @Comments varchar(1750), @Kit_to_send bit, @Kit_sent bit, 
@Kit_sent_date datetime, @Lead_assignment_date datetime, @Interests varchar(2800), @Has_been_contacted bit, @Day_phone_ext varchar(10), @Evening_phone_ext varchar(10), 
@Other_phone varchar(20), @Group_web_site varchar(50), @Nb_queries int, @Submit_date datetime, @Cookie_content varchar(255), @First_contact_date datetime, @Day_phone_is_good bit, 
@Evening_phone_is_good bit, @Account_number int, @Valid_email bit, @Other_closing_activity_reason varchar(50), @Transfered_date datetime, @Matching_code varchar(10), 
@Phone_number_tracking_id int, @Customer_status_id int, @address_zone_id int, @fundraisers_per_year tinyint,
@Client_status_id int = NULL 
AS
begin

if @Channel_code IS NULL
	select @Channel_code = Channel_code from Lead where Lead_id = @Lead_id
if @Fk_kit_type_id IS NULL
	select @Fk_kit_type_id = Fk_kit_type_id from Lead where Lead_id = @Lead_id
if @Web_site_id IS NULL
	select @Web_site_id = Web_site_id from Lead where Lead_id = @Lead_id	
	
update Lead 
set Lead_status_id=@Lead_status_id, Lead_qualification_type_id=@Lead_qualification_type_id, Lead_priority_id=@Lead_priority_id, Temp_lead_id=@Temp_lead_id, 
	Division_id=@Division_id, Channel_code=@Channel_code, Consultant_id=@Consultant_id, Group_type_id=@Group_type_id, 
	Organization_type_id=@Organization_type_id, Hear_id=@Hear_id, Fk_kit_type_id=@Fk_kit_type_id, Old_lead_id=@Old_lead_id, Assigner_id=@Assigner_id, 
	Referee_id=@Referee_id, Title_id=@Title_id, Campaign_reason_id=@Campaign_reason_id, Web_site_id=@Web_site_id, Promotion_code_id=@Promotion_code_id, 
	Activity_closing_reason_id=@Activity_closing_reason_id, Ext_consultant_id=@Ext_consultant_id, Salutation=@Salutation, First_name=@First_name, 
	Last_name=@Last_name, Organization=@Organization, Street_address=@Street_address, City=@City, State_code=@State_code, Country_code=@Country_code, 
	Zip_code=@Zip_code, Day_phone=@Day_phone, Day_time_call=@Day_time_call, Evening_phone=@Evening_phone, Evening_time_call=@Evening_time_call, 
	Fax=@Fax, Email=@Email, Member_count=@Member_count, Participant_count=@Participant_count, 
	Fund_raising_goal=@Fund_raising_goal, Decision_date=@Decision_date, Decision_maker=@Decision_maker, Committee_meeting_required=@Committee_meeting_required, 
	Committee_meeting_date=@Committee_meeting_date, Fund_raiser_start_date=@Fund_raiser_start_date, Onemaillist=@Onemaillist, Faxkit=@Faxkit, 
	Emailkit=@Emailkit, Comments=@Comments, Kit_to_send=@Kit_to_send, Kit_sent=@Kit_sent, Kit_sent_date=@Kit_sent_date, Lead_assignment_date=@Lead_assignment_date, 
	Interests=@Interests, Has_been_contacted=@Has_been_contacted, Day_phone_ext=@Day_phone_ext, Evening_phone_ext=@Evening_phone_ext, Other_phone=@Other_phone, 
	Group_web_site=@Group_web_site, Nb_queries=@Nb_queries, Submit_date=@Submit_date, Cookie_content=@Cookie_content, First_contact_date=@First_contact_date, 
	Day_phone_is_good=@Day_phone_is_good, Evening_phone_is_good=@Evening_phone_is_good, Account_number=@Account_number, Valid_email=@Valid_email, 
	Other_closing_activity_reason=@Other_closing_activity_reason, Transfered_date=@Transfered_date, Matching_code=@Matching_code, Phone_number_tracking_id=@Phone_number_tracking_id, 
	Customer_status_id=@Customer_status_id, address_zone_id = @address_zone_id, fundraisers_per_year = @fundraisers_per_year
where Lead_id=@Lead_id

end
GO
