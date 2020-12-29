USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_client_from_lead_wfc]    Script Date: 02/14/2014 13:06:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--exec efrcrm_insert_client_from_lead  475055
-- Create Client From Lead Info
-- Doit pas etre utiliser dans EFUND 2.0 Patch Temporaire
-- Create By mcote March 13, 2007
CREATE PROCEDURE [dbo].[efrcrm_insert_client_from_lead_wfc]  
	 
	 
	 @lead_id int,
     @client_id int output,
     @client_seq char(2) output
AS
begin

--Param
--declare @lead_id int
--set @lead_id = 475055

--Output
--declare @client_id int 
--declare @client_seq char(2) 

--Input
declare	@group_type_id int
declare	@channel_code varchar(4)
declare	@promotion_id int
declare	@division_id int
declare	@title_id int
declare	@salutation varchar(10)
declare	@first_name varchar(50)
declare	@last_name varchar(50)
declare	@title varchar(50)
declare	@organization varchar(100)
declare	@day_phone varchar(20)
declare	@day_time_call varchar(45)
declare	@evening_phone varchar(20)
declare	@evening_time_call varchar(20)
declare	@fax varchar(20)
declare	@email varchar(50)
declare	@day_phone_ext varchar(10)
declare	@evening_phone_ext varchar(10)
declare	@other_phone varchar(20)
declare	@other_phone_ext varchar(10)

declare @Address_id int
declare @Address_type char(2)
declare @Street_address varchar(100)
declare @State_code varchar(10)
declare @Country_code varchar(10)
declare @City varchar(50)
declare @Zip_code varchar(10)
declare @Attention_of varchar(100)



select @group_type_id = group_type_id
	, @channel_code = channel_code
	, @promotion_id = promotion_id
	, @division_id = division_id
	, @title_id = title_id
	, @salutation = salutation
	, @first_name = first_name
	, @last_name = last_name
	--, @title = title
	, @organization = organization
	, @day_phone = day_phone
	, @day_time_call = day_time_call
	, @evening_phone = evening_phone
	, @evening_time_call = evening_time_call
	, @fax = fax
	, @email = email
	, @day_phone_ext = day_phone_ext
	, @evening_phone_ext = evening_phone_ext
	, @other_phone = other_phone
	--, @other_phone_ext = other_phone_ext
	, @Street_address = street_address
	, @State_code = state_code
	, @Country_code = country_code
	, @City = city
	, @Zip_code = zip_code
	, @Attention_of = first_name + ' ' + last_name
	from lead where lead_id = @lead_id


--select * from client_address
declare @partner_id int

select @partner_id = partner_id from promotion where promotion_id = @promotion_id

set @client_seq = 'UI'

if @country_code = 'CA'
begin
	if @partner_id = 686
		set @client_seq = 'CF' 	-- CF	Canada Fundraising.com
	else 
		set @client_seq = 'CI'		-- CI	Canada Direct Client
--CA	Agent Canada
--CD	Distributor Canada
--CG	Client of Agent Canada
--CS	Canada Samples
--CU	Canadian Unofficial Agent
end
else 
begin 
	if @partner_id = 686
		set @client_seq = 'IF' 	-- IF	Internet Fundraising.com
	else 
		set @client_seq = 'UI'	-- UI	USA Direct Client
end
--OF	Online FastFundraising.com
--UA	Agent USA
--UD	Distributor USA
--UG	Client of Agent USA
--UN	Unoffical Agents USA
--US	USA Samples

IF NOT EXISTS (select * from client where lead_id = @lead_id)
BEGIN


exec efrcrm_insert_client 
	@client_id output
	, @client_seq 
	, 'OTH'
	, @group_type_id
	, @channel_code
	, @promotion_id
	, @lead_id
	, @division_id
	, NULL
	, @title_id
	, @salutation
	, @first_name
	, @last_name
	, NULL--@title
	, @organization
	, @day_phone
	, @day_time_call
	, @evening_phone
	, @evening_time_call
	, @fax
	, @email
	, NULL
	, 0
	, 0
	, @day_phone_ext
	, @evening_phone_ext
	, @other_phone
	, NULL--@other_phone_ext

set @Address_type = 'BT'
exec dbo.efrcrm_insert_client_address
	@Address_id output
	, @Client_seq
	, @Client_id
	, @Address_type
	, @Street_address
	, @State_code
	, @Country_code
	, @City
	, @Zip_code
	, @Attention_of
	, NULL

set @Address_type = 'ST'
exec dbo.efrcrm_insert_client_address
	@Address_id output
	, @Client_seq
	, @Client_id
	, @Address_type
	, @Street_address
	, @State_code
	, @Country_code
	, @City
	, @Zip_code
	, @Attention_of
	, NULL
END
ELSE
BEGIN
   select @client_id = client_id, @client_seq = client_sequence_code from client where lead_id = @lead_id
END

select @client_id, @client_seq

end
GO
