USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_relaunch_campaign]    Script Date: 02/14/2014 13:06:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[es_relaunch_campaign]
	@event_id int 
	,@culture_code nvarchar(5)
as

declare @group_id int
declare @sponsor_id int 
declare @start_date datetime
declare @new_event_id int
declare @return int
declare @member_hierarchy_id int
declare @event_participation_id int
declare @event_name varchar(100)
declare @redirect varchar(255)
declare @salutation varchar(300)
declare @payment_name varchar(100)
declare @on_behalf_of_name varchar(50)
declare @ship_to_name varchar(100)
declare @phone_number varchar(50)
declare @ssn varchar(50)
declare @address_1 varchar(100)
declare @address_2 varchar(100)
declare @city varchar(100)
declare @zip_code varchar(10)
declare @country_code nvarchar(2)
declare @subdivision_code nvarchar(7)
declare @address_is_validated tinyint
declare @postal_address_id int
declare @phone_number_id int
declare @payment_info_id int
declare @group_type_id int
declare @profit_group_id int
declare @profit_calculated float
declare @partner_profit_group_id int
declare @partner_profit_calculated float

set @start_date = getdate()


--if exists (select * from event where event_id = @event_id and active =0 )
--return -6  -- la campagne a déjà été désactivée.

begin transaction

select
	  @group_id = g.group_id
	, @member_hierarchy_id = sponsor_id
	, @event_name = event_name
	, @redirect = e.redirect
	, @salutation = ep.salutation
    , @group_type_id = e.group_type_id
    , @profit_group_id = e.profit_group_id
    , @profit_calculated = e.profit_calculated
from
	event_group eg
	inner join [group]g
		on g.group_id = eg.group_id
	inner join event e
		on e.event_id = eg.event_id
	inner join event_participation ep
		on ep.event_id = e.event_id
		and ep.participation_channel_id = 3
where
	  eg.event_id = @event_id
	  
-- on va chercher les infos de l'ancienne campagne
IF EXISTS(
			SELECT payment_info_id 
			FROM payment_info 
			WHERE group_id = @group_id 
			  and event_id = @event_id
			  and active = 1
		) 
BEGIN
    select
	  @payment_name = pi.payment_name
	, @on_behalf_of_name = pi.on_behalf_of_name
	, @ship_to_name = pi.ship_to_name
	, @phone_number = pn.phone_number
	, @ssn = pi.ssn
	, @address_1 = pa.address_1
	, @address_2 = pa.address_2
	, @city = pa.city
	, @zip_code = pa.zip_code
	, @country_code = pa.country_code
	, @subdivision_code = pa.subdivision_code
	, @address_is_validated = pa.is_validated
from
	event_group eg
	inner join [group]g
		on g.group_id = eg.group_id
	inner join event e
		on e.event_id = eg.event_id
	inner join event_participation ep
		on ep.event_id = e.event_id
		and ep.participation_channel_id = 3
	inner join payment_info pi
		on pi.group_id = eg.group_id
	   and pi.event_id = eg.event_id
	left join postal_address pa
		on pa.postal_address_id = pi.postal_address_id
	left join phone_number pn
		on pn.phone_number_id = pi.phone_number_id
where
	  eg.event_id = @event_id
	  and pi.active = 1
END
ELSE
BEGIN
    select @sponsor_id = sponsor_id from [group] where group_id = @group_id;
    IF EXISTS(
			SELECT payment_info_id 
			FROM payment_info pinfo
			INNER JOIN event_group as eg
					ON eg.group_id = pinfo.group_id
					AND eg.event_id = pinfo.event_id
			INNER JOIN [group] as g
					ON g.group_id = pinfo.group_id
			WHERE g.sponsor_id = @sponsor_id and pinfo.active = 1
		) 
	BEGIN
		select
		  @payment_name = pinfo.payment_name
		, @on_behalf_of_name = pinfo.on_behalf_of_name
		, @ship_to_name = pinfo.ship_to_name
		, @phone_number = pn.phone_number
		, @ssn = pinfo.ssn
		, @address_1 = pa.address_1
		, @address_2 = pa.address_2
		, @city = pa.city
		, @zip_code = pa.zip_code
		, @country_code = pa.country_code
		, @subdivision_code = pa.subdivision_code
		, @address_is_validated = pa.is_validated
		  FROM payment_info as pinfo
				LEFT JOIN postal_address as pa
					ON pa.postal_address_id = pinfo.postal_address_id
				LEFT JOIN phone_number as pn
					ON pn.phone_number_id = pinfo.phone_number_id
				INNER JOIN event_group as eg
					ON eg.group_id = pinfo.group_id
					AND eg.event_id = pinfo.event_id
				INNER JOIN [group] as g
					ON g.group_id = pinfo.group_id
		  WHERE g.sponsor_id = @sponsor_id and pinfo.active = 1
	END
	ELSE
	BEGIN
		select TOP 1
		  @payment_name = pinfo.payment_name
		, @on_behalf_of_name = pinfo.on_behalf_of_name
		, @ship_to_name = pinfo.ship_to_name
		, @phone_number = pn.phone_number
		, @ssn = pinfo.ssn
		, @address_1 = pa.address_1
		, @address_2 = pa.address_2
		, @city = pa.city
		, @zip_code = pa.zip_code
		, @country_code = pa.country_code
		, @subdivision_code = pa.subdivision_code
		, @address_is_validated = pa.is_validated
		  FROM payment_info as pinfo
				LEFT JOIN postal_address as pa
					ON pa.postal_address_id = pinfo.postal_address_id
				LEFT JOIN phone_number as pn
					ON pn.phone_number_id = pinfo.phone_number_id
				INNER JOIN event_group as eg
					ON eg.group_id = pinfo.group_id
					AND eg.event_id = pinfo.event_id
				INNER JOIN [group] as g
					ON g.group_id = pinfo.group_id
		  WHERE g.sponsor_id = @sponsor_id
		  ORDER BY pinfo.payment_info_id DESC
	END
END  

if @@error <> 0 
begin 
	rollback transaction
	return -1
end

-- on annulle l'ancienne campagne
update event
set active = 0
, end_date = getdate()
where event_id = @event_id

if @@error <> 0 
begin 
	rollback transaction
	return -2
end

-- Check if the partner's profit was changed
select @partner_profit_group_id = p.profit_group_id, @partner_profit_calculated = p.profit_percentage 
from efrcommon.dbo.partner_profit pp with (nolock) inner join efrcommon.dbo.profit p with (nolock) 
on pp.profit_group_id = p.profit_group_id and qsp_catalog_type_id IS NULL inner join [group] g with (nolock)
on pp.partner_id = g.partner_id
where g.group_id = @group_id and end_date IS NULL 

if @profit_group_id <> @partner_profit_group_id AND @partner_profit_calculated >= @profit_calculated
BEGIN
	SET @profit_group_id = @partner_profit_group_id;
	SET @profit_calculated = @partner_profit_calculated;
END

-- on cré la nouvelle campagne
exec @return = es_create_event
	@group_id = @group_id
	, @event_status_id = 3 -- Campaign relaunch
	, @culture_code = @culture_code 
	, @event_name = @event_name 
	, @start_date = @start_date
	, @end_date = NULL
	, @active = 1
	, @comments = NULL
	, @redirect = @redirect
	, @relaunch = 1
	, @event_id = @new_event_id OUTPUT
    , @group_type_id = @group_type_id
    , @profit_group_id = @profit_group_id
    , @profit_calculated = @profit_calculated

if @return <> 0 
begin 
	rollback transaction
	return -3
end

-- on crée le payment_info
/*
exec @return = es_create_payment_info
	@group_id = @group_id
	, @event_id = @new_event_id
	, @payment_name = @payment_name
	, @on_behalf_of_name = @on_behalf_of_name
	, @ship_to_name = @ship_to_name
	, @phone_number = @phone_number
	, @ssn = @ssn
	, @address_1 = @address_1
	, @address_2 = @address_2
	, @city = @city
	, @zip_code = @zip_code
	, @country_code = @country_code
	, @subdivision_code = @subdivision_code
	, @address_is_validated = @address_is_validated
	, @postal_address_id = @postal_address_id OUTPUT
	, @phone_number_id = @phone_number_id OUTPUT
	, @payment_info_id = @payment_info_id OUTPUT
*/

IF EXISTS(
			SELECT payment_info_id 
			FROM payment_info 
			WHERE group_id = @group_id 
			  and event_id = @event_id
		) 
BEGIN
    UPDATE payment_info
    SET active = 0
    WHERE group_id = @group_id
      and event_id = @event_id

    IF @@error <> 0
    BEGIN
        ROLLBACK TRAN
        RETURN -3
    END
END

        IF (@address_1 IS NOT NULL) OR (@address_2 IS NOT NULL) OR (@city IS NOT NULL) OR (@zip_code IS NOT NULL) OR 
           (@country_code IS NOT NULL) OR (@subdivision_code IS NOT NULL)
        BEGIN
			INSERT INTO postal_address (
					address_1
					, address_2
					, city
					, zip_code
					, country_code
					, subdivision_code
					, create_date
					, matching_code
					, is_validated
			) VALUES (
					@address_1
					, @address_2
					, @city
					, @zip_code
					, @country_code
					, @subdivision_code
					, GETDATE()
					, dbo.es_generate_matching_code(@address_1, @zip_code)
					, @address_is_validated
			)

			IF @@error <> 0
			BEGIN
				ROLLBACK TRAN
				RETURN -4
			END

			SET @postal_address_id = SCOPE_IDENTITY()

			IF @@error <> 0
			BEGIN
				ROLLBACK TRAN
				RETURN -5
			END
       END
    
IF @phone_number IS NOT NULL
BEGIN

    INSERT INTO phone_number (
        phone_number
        , create_date
    ) VALUES (
        @phone_number
        , GETDATE()
    )

    IF @@error <> 0
    BEGIN
        ROLLBACK TRAN
        RETURN -6
    END

    SET @phone_number_id = SCOPE_IDENTITY()

    IF @@error <> 0
    BEGIN
        ROLLBACK TRAN
        RETURN -7
    END
END

INSERT INTO payment_info (
    group_id
    , event_id
    , postal_address_id
    , phone_number_id
    , payment_name
    , on_behalf_of_name
    , ship_to_name
    , ssn
    , active 
    , create_date
) VALUES (
    @group_id
    , @new_event_id
    , @postal_address_id
    , @phone_number_id
    , @payment_name
    , @on_behalf_of_name
    , @ship_to_name
    , @ssn
    , 1
    , GETDATE()
)

IF @@error <> 0
BEGIN
    ROLLBACK TRAN
    RETURN -8
END

SET @payment_info_id = SCOPE_IDENTITY()

IF @@error <> 0
BEGIN
    ROLLBACK TRAN
    RETURN -9
END


-- on cré le event_participation 
exec @return = es_create_event_participation
	@event_id = @new_event_id, 
	@member_hierarchy_id = @member_hierarchy_id ,
	@participation_channel_id = 3,
	@salutation = @salutation,
	@event_participation_id = @event_participation_id OUTPUT

if @return <> 0 
begin 
	rollback transaction
	return -10
end

-- on cré la personnalisation
-- on copie l'ancienne
insert into personalization (
 	event_participation_id
	, header_title1
	, header_title2
	--, body
	, fundraising_goal
	, site_bgcolor
	, header_bgcolor
	, header_color
	, group_url
	, image_url
	, create_date
	, image_motivator
	, skip
	, redirect
)
select 
	 @event_participation_id
	, header_title1
	, header_title2
	--, body
	, 0
	, site_bgcolor
	, header_bgcolor
	, header_color
	, group_url
	, image_url
	, getdate()
	, image_motivator
	, 0
	, p.redirect
from 
	event e
	inner join event_participation ep
	on e.event_id = ep.event_id
	inner join personalization p
	on p.event_participation_id = ep.event_participation_id
where 
	ep.participation_channel_id = 3
and 	e.event_id = @event_id 

if @@error <> 0 
begin 
	rollback transaction
	return -11
end
	
commit transaction
return @new_event_id
GO
