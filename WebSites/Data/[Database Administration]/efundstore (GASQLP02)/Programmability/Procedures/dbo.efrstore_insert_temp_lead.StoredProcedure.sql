USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_insert_temp_lead]    Script Date: 02/14/2014 13:06:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Temp_lead
CREATE PROCEDURE [dbo].[efrstore_insert_temp_lead] @Temp_lead_id int OUTPUT, @Division_id int, @Promotion_id int, @Channel_code varchar(4), @Lead_status_id int, @Organization_type_id tinyint, @Campaign_reason_id tinyint, @Web_site_id smallint, @Group_type_id tinyint, @Salutation varchar(10), @Title_id tinyint, @Hear_id tinyint, @Lead_entry_date datetime, @First_name varchar(50), @Last_name varchar(50), @Organization varchar(100), @Street_address varchar(100), @City varchar(50), @State_code varchar(10), @Country_code char(2), @Zip_code varchar(10), @Day_phone varchar(20), @Day_time_call varchar(20), @Evening_phone varchar(20), @Fax varchar(20), @Email varchar(50), @Participant_count int, @Fund_raising_goal int, @Decision_date datetime, @Decision_maker bit, @Fund_raiser_start_date datetime, @Onemaillist bit, @Comments varchar(2000), @Day_phone_ext varchar(10), @Evening_phone_ext varchar(10), @Other_phone varchar(20), @Cookie_content varchar(255), @Group_web_site varchar(50), @Other_phone_ext varchar(10), @Isnew bit, @Opt_in_sweepstakes bit AS
begin

insert into Temp_lead(Division_id, Promotion_id, Channel_code, Lead_status_id, Organization_type_id, Campaign_reason_id, Web_site_id, Group_type_id, Salutation, Title_id, Hear_id, Lead_entry_date, First_name, Last_name, Organization, Street_address, City, State_code, Country_code, Zip_code, Day_phone, Day_time_call, Evening_phone, Fax, Email, Participant_count, Fund_raising_goal, Decision_date, Decision_maker, Fund_raiser_start_date, Onemaillist, Comments, Day_phone_ext, Evening_phone_ext, Other_phone, Cookie_content, Group_web_site, Other_phone_ext, Isnew, Opt_in_sweepstakes) values(@Division_id, @Promotion_id, @Channel_code, @Lead_status_id, @Organization_type_id, @Campaign_reason_id, @Web_site_id, @Group_type_id, @Salutation, @Title_id, @Hear_id, @Lead_entry_date, @First_name, @Last_name, @Organization, @Street_address, @City, @State_code, @Country_code, @Zip_code, @Day_phone, @Day_time_call, @Evening_phone, @Fax, @Email, @Participant_count, @Fund_raising_goal, @Decision_date, @Decision_maker, @Fund_raiser_start_date, @Onemaillist, @Comments, @Day_phone_ext, @Evening_phone_ext, @Other_phone, @Cookie_content, @Group_web_site, @Other_phone_ext, @Isnew, @Opt_in_sweepstakes)

select @Temp_lead_id = SCOPE_IDENTITY()

end
GO
