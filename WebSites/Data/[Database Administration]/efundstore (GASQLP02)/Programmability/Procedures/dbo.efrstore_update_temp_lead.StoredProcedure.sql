USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_update_temp_lead]    Script Date: 02/14/2014 13:06:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Temp_lead
CREATE PROCEDURE [dbo].[efrstore_update_temp_lead] @Temp_lead_id int, @Division_id int, @Promotion_id int, @Channel_code varchar(4), @Lead_status_id int, @Organization_type_id tinyint, @Campaign_reason_id tinyint, @Web_site_id smallint, @Group_type_id tinyint, @Salutation varchar(10), @Title_id tinyint, @Hear_id tinyint, @Lead_entry_date datetime, @First_name varchar(50), @Last_name varchar(50), @Organization varchar(100), @Street_address varchar(100), @City varchar(50), @State_code varchar(10), @Country_code char(2), @Zip_code varchar(10), @Day_phone varchar(20), @Day_time_call varchar(20), @Evening_phone varchar(20), @Fax varchar(20), @Email varchar(50), @Participant_count int, @Fund_raising_goal int, @Decision_date datetime, @Decision_maker bit, @Fund_raiser_start_date datetime, @Onemaillist bit, @Comments varchar(2000), @Day_phone_ext varchar(10), @Evening_phone_ext varchar(10), @Other_phone varchar(20), @Cookie_content varchar(255), @Group_web_site varchar(50), @Other_phone_ext varchar(10), @Isnew bit, @Opt_in_sweepstakes bit AS
begin

update Temp_lead set Division_id=@Division_id, Promotion_id=@Promotion_id, Channel_code=@Channel_code, Lead_status_id=@Lead_status_id, Organization_type_id=@Organization_type_id, Campaign_reason_id=@Campaign_reason_id, Web_site_id=@Web_site_id, Group_type_id=@Group_type_id, Salutation=@Salutation, Title_id=@Title_id, Hear_id=@Hear_id, Lead_entry_date=@Lead_entry_date, First_name=@First_name, Last_name=@Last_name, Organization=@Organization, Street_address=@Street_address, City=@City, State_code=@State_code, Country_code=@Country_code, Zip_code=@Zip_code, Day_phone=@Day_phone, Day_time_call=@Day_time_call, Evening_phone=@Evening_phone, Fax=@Fax, Email=@Email, Participant_count=@Participant_count, Fund_raising_goal=@Fund_raising_goal, Decision_date=@Decision_date, Decision_maker=@Decision_maker, Fund_raiser_start_date=@Fund_raiser_start_date, Onemaillist=@Onemaillist, Comments=@Comments, Day_phone_ext=@Day_phone_ext, Evening_phone_ext=@Evening_phone_ext, Other_phone=@Other_phone, Cookie_content=@Cookie_content, Group_web_site=@Group_web_site, Other_phone_ext=@Other_phone_ext, Isnew=@Isnew, Opt_in_sweepstakes=@Opt_in_sweepstakes where Temp_lead_id=@Temp_lead_id

end
GO
