USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[crm_get_all_similar_leads]    Script Date: 02/14/2014 13:03:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create   procedure [dbo].[crm_get_all_similar_leads] @lead_id as int
           
as


--declare @lead_id int
--set @lead_id = 667

declare @fraud_lead_id int
declare @message varchar(100)
declare @found_lead_id int
declare @zip varchar(10)
declare @street varchar(50)
declare @city varchar(50)
declare @state varchar(5)
declare @dayPhone varchar(15)
declare @nightPhone varchar(15)
declare @email varchar(30)
select @zip = zip_code, @street = street_address, @city = city, @state = state_code ,
      @dayPhone = day_phone, @nightPhone = evening_phone, @email = email from lead where lead_id = @lead_id
set @zip = substring(@zip,0,5)


set @message = 'No fraud found'
select @fraud_lead_id = lead_id from lead where consultant_id in (3530,3675) and (
                                       (substring(zip_code,0,5) = @zip)
                                       or (street_address = @street and city = @city and state_code = @state))
                                     
if (@fraud_lead_id > 0)
begin
   set @message = 'Warning: Found similar fraudulant lead (' + convert(varchar(100),@fraud_lead_id)+ ') by zip code or address. Please Investigate' 
end
else
begin

   select @fraud_lead_id = lead_id from lead 
   where lead_id <> @lead_id and
       ((day_phone = @dayPhone or
         day_phone = @nightPhone or
         evening_phone = @nightPhone  or
         evening_phone = @dayPhone) OR (email = @email))
                                     
   if (@fraud_lead_id > 0)
   begin
      set @message = 'Warning: Found similar fraudulant lead (' + convert(varchar(100),@fraud_lead_id)+ ') by email or phone number. Please Investigate' 
   end
end

IF @@ERROR <> 0 
begin
      set @message = 'An error cccured in the procedure'
end

select @message
GO
