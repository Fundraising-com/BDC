USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_temp_leads]    Script Date: 02/14/2014 13:05:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Temp_lead
CREATE PROCEDURE [dbo].[efrstore_get_temp_leads] AS
begin

select Temp_lead_id, Division_id, Promotion_id, Channel_code, Lead_status_id, Organization_type_id, Campaign_reason_id, Web_site_id, Group_type_id, Salutation, Title_id, Hear_id, Lead_entry_date, First_name, Last_name, Organization, Street_address, City, State_code, Country_code, Zip_code, Day_phone, Day_time_call, Evening_phone, Fax, Email, Participant_count, Fund_raising_goal, Decision_date, Decision_maker, Fund_raiser_start_date, Onemaillist, Comments, Day_phone_ext, Evening_phone_ext, Other_phone, Cookie_content, Group_web_site, Other_phone_ext, Isnew, Opt_in_sweepstakes from Temp_lead

end
GO
