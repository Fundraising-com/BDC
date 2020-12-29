USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_payment_by_Partner_id_between]    Script Date: 02/14/2014 13:06:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[es_get_payment_by_Partner_id_between] @Partner_id int , @Start_Date datetime, @End_Date datetime As
begin

declare @PaidToPartner int


set @PaidToPartner = 0
select @PaidToPartner = payment_to from partner_payment_config where partner_id = @Partner_id
IF (@PaidToPartner = 1)
BEGIN

	select p.Payment_id
		, p.Payment_type_id
		, p.Payment_info_id
		, p.Payment_period_id
		, p.Cheque_number
		, p.Cheque_date
		, p.Paid_amount
		, p.[Name]
		, p.Phone_number
		, p.Address_1
		, p.Address_2
		, p.City
		, p.Zip_code
		, p.Country_code
		, p.Subdivision_code
		, p.Create_date 
	from Payment p
		inner join partner_payment_info ppInfo
			on (ppInfo.payment_info_id = p.payment_info_id and ppinfo.active = 1)
		left join 
			(
			select pps.payment_id, pps.payment_status_id
			from payment_payment_status pps
			inner join (
				select payment_id, max(create_date) as create_date 
				from payment_payment_status 
				group by payment_id
				) pps2 on pps.payment_id = pps2.payment_id and pps.create_date = pps2.create_date
			)  pps
			on pps.payment_id = p.payment_id
	where ppInfo.partner_id = @Partner_id
	and ppInfo.active = 1
	and (pps.payment_status_id in (2,6,10) OR pps.payment_id is null) -- Cashed or before implementing payment_payment_statu
	and p.create_date between @Start_Date and @End_Date

END
ELSE
BEGIN

	select p.Payment_id
		, p.Payment_type_id
		, p.Payment_info_id
		, p.Payment_period_id
		, p.Cheque_number
		, p.Cheque_date
		, p.Paid_amount
		, p.[Name]
		, p.Phone_number
		, p.Address_1
		, p.Address_2
		, p.City
		, p.Zip_code
		, p.Country_code
		, p.Subdivision_code
		, p.Create_date
	from Payment p
		inner join payment_info pi
			on p.payment_info_id = pi.payment_info_id
		inner join [group] g
			on g.group_id = pi.group_id
		left join 
			(
			select pps.payment_id, pps.payment_status_id
			from payment_payment_status pps
			inner join (
				select payment_id, max(create_date) as create_date 
				from payment_payment_status 
				group by payment_id
				) pps2 on pps.payment_id = pps2.payment_id and pps.create_date = pps2.create_date
			)  pps
			on pps.payment_id = p.payment_id
	where g.partner_id = @Partner_id
	and (pps.payment_status_id in (2,6,10) OR pps.payment_id is null) -- Cashed or before implementing payment_payment_statu
	and p.create_date between @Start_Date and @End_Date
END

end
GO
