USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_leads]    Script Date: 02/14/2014 13:05:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Lead
CREATE PROCEDURE [dbo].[efrcrm_get_leads] AS
begin

select Lead_id, Lead_status_id, Lead_qualification_type_id, Lead_priority_id, Temp_lead_id, Division_id, Promotion_id, Channel_code, Consultant_id, Group_type_id, 
	Organization_type_id, Hear_id, Fk_kit_type_id, Old_lead_id, Assigner_id, Referee_id, Title_id, Campaign_reason_id, Web_site_id, Promotion_code_id, 
	Activity_closing_reason_id, Ext_consultant_id, Salutation, First_name, Last_name, Organization, Street_address, City, State_code, Country_code, Zip_code, 
	Day_phone, Day_time_call, Evening_phone, Evening_time_call, Fax, Email, Lead_entry_date, Member_count, Participant_count, Fund_raising_goal, 
	Decision_date, Decision_maker, Committee_meeting_required, Committee_meeting_date, Fund_raiser_start_date, Onemaillist, Faxkit, Emailkit, Comments, 
	Kit_to_send, Kit_sent, Kit_sent_date, Lead_assignment_date, Interests, Has_been_contacted, Day_phone_ext, Evening_phone_ext, Other_phone, 
	Group_web_site, Nb_queries, Submit_date, Cookie_content, First_contact_date, Day_phone_is_good, Evening_phone_is_good, Account_number, 
	Valid_email, Other_closing_activity_reason, Transfered_date, Matching_code, Phone_number_tracking_id, Customer_status_id, address_zone_id, fundraisers_per_year
from Lead

end
GO
