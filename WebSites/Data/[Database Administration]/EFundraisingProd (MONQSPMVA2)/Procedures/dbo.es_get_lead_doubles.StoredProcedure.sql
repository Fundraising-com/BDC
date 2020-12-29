USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[es_get_lead_doubles]    Script Date: 02/14/2014 13:08:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   procedure [dbo].[es_get_lead_doubles]
		@first_name as varchar(50),
		@last_name as varchar(50),
		@street_address as varchar(100),
		@zip_code as varchar(10),
		@day_phone as varchar(20),
		@evening_phone as varchar(20),
		@email as varchar (50)
as

declare @lead_id as int
declare @consultant_id as int
declare @consultant_active as tinyint
declare @organization as varchar(100)
declare @city as varchar(50)
declare @dayPhone as varchar(20)
declare @Participant_count as int
declare @partner_id as int
declare @promotion_id int
declare @email_address varchar(100)
declare @country_code varchar(50)
declare @state_code varchar(50)
declare @lead_status_id int


select  
	l.consultant_id as consultant_id
	, l.lead_id as lead_id
	, p.partner_id as partner_id
	, c.is_active as consultant_active
	, first_name
	, last_name
	, organization
	, street_address
	, city
	, day_phone
	, participant_count
	, l.promotion_id as promotion_id
	, l.email as email_address
	, l.country_code as country_code
	, l.state_code as state_code
	, zip_code
	, evening_phone 
	, lead_status_id as lead_status_id
from 
	lead l
	inner join consultant c
	on l.consultant_id = c.consultant_id
	inner join promotion p
	on p.promotion_id = l.promotion_id
where 
	l.first_name = @first_name AND l.last_name = @last_name
AND     (
	   email = @email
	   OR  (REPLACE(@evening_phone,'-','') <> '' AND evening_phone LIKE (REPLACE(@evening_phone,'-','')  + '%'))
	   OR  (REPLACE(@day_phone,'-','')  <> '' AND evening_phone LIKE (REPLACE(@day_phone,'-','') + '%'))
	   OR  (REPLACE(@day_phone,'-','') <> '' AND day_phone LIKE (REPLACE(@day_phone,'-','') + '%'))
	   OR  (REPLACE(@evening_phone,'-','')  <> '' AND day_phone LIKE (REPLACE(@evening_phone,'-','')  + '%'))
              )
GO
