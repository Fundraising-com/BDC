USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_kit_type_by_lead_information]    Script Date: 02/14/2014 13:04:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- select * from promotion where description like '%model%'
-- exec efrcrm_get_kit_type_by_lead_information NULL, 'CI', NULL, 839, 'QC', 'CA'

CREATE   procedure [dbo].[efrcrm_get_kit_type_by_lead_information]
	@kit_type_id int
	, @consultant_id int
	, @channel_code varchar(4)
	, @promotion_id int
	, @partner_id int
	, @state_code varchar(10)
	, @country_code varchar(10)
as

if @kit_type_id < 0
	set @kit_type_id = 42;

begin 
	if @country_code = 'CA'	-- canada
		set @kit_type_id = 34 -- EFR - CANADA --removed by mel	set @kit_type_id = 30 -- SC - CA
	else if @promotion_id = 5861 or @promotion_id = 5932 or @promotion_id = 5933 or @promotion_id = 4088
		set @kit_type_id = 32
	else if @promotion_id = 6185 -- Arena Model
		set @kit_type_id = 10 -- No kit to send
	else if @promotion_id = 11877 or @partner_id = 839 -- PT: we exclude Humeur from kit assignment
		set @kit_type_id = 10 -- No kit to send
	else if @kit_type_id = 0
		select @kit_type_id = kit_type_id from kit_type where is_default = 1 and is_active = 1
end

select kit_type_id, description, delivery_time, comments, is_default, is_active, create_date from kit_type where kit_type_id = @kit_type_id
GO
