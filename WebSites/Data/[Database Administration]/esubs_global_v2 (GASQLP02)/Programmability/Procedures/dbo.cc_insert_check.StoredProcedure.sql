USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[cc_insert_check]    Script Date: 02/14/2014 13:04:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Created By:	jf lavigne
Created On:	sept 20, 2004
Modified By:	
Modified On:	
Description:      adds one entry into the check table
*/


--SELECT p.cheque_number, p.paid_amount, p.name FROM   dbo.payment p INNER JOIN dbo.payment_info pi ON p.payment_info_id = pi.payment_info_id inner join event_group eg on pi.group_id = eg.group_id WHERE event_id = 35282 and p.payment_period_id = 21
	
--select * from payment order by create_date desc
--em_insert_check 21,246.53,1999, 35282

CREATE     PROCEDURE [dbo].[cc_insert_check]
	@intCheckPeriodID INT
	, @numCheckAmt DECIMAL(9,2)
	, @intCheckNo INT
	, @intEventID INT
AS
DECLARE @intErrorCode INT

/*
declare @intCheckPeriodID INT
declare @numCheckAmt DECIMAL(9,2)
declare @intCheckNo INT
declare @intCampaignID INT

set @intCheckPeriodID = 21
set @numCheckAmt = 26
set @intCheckNo = 999
set @intCampaignID = 35282

*/

SET NOCOUNT ON
SET @intErrorCode = @@ERROR

BEGIN TRANSACTION

IF EXISTS(
	SELECT TOP 1 1
	FROM	dbo.payment p INNER JOIN
                dbo.payment_info pi ON p.payment_info_id = pi.payment_info_id and pi.active = 1 
	WHERE
		p.payment_period_id = @intCheckPeriodID
	 AND	pi.event_id = @intEventID
)
BEGIN
	UPDATE 
		payment
	SET	paid_amount = @numCheckAmt
		,cheque_number = @intCheckNo
	FROM	dbo.payment p INNER JOIN 
                dbo.payment_info pi ON p.payment_info_id = pi.payment_info_id and pi.active = 1 
        where
		payment_period_id = @intCheckPeriodID
	 AND	pi.event_id = @intEventID
	SET @intErrorCode = @@ERROR
END
ELSE
BEGIN
	INSERT INTO [payment] (
                payment_type_id
		, payment_info_id
		, payment_period_id
                , cheque_number
                , cheque_date 
                , paid_amount
		, [name]
                , address_1
           	, city
		, zip_code
		, country_code
		, subdivision_code

	)
	SELECT DISTINCT
		1
                , pi.payment_info_id
                , @intCheckPeriodID
		, @intCheckNo 
                , getdate()
                , @numCheckAmt
		, payment_name
		, pa.address_1
		, pa.city
                , pa.zip_code
		, pa.country_code
                , pa.subdivision_code
		
		
		
	FROM	payment_info pi
              inner join postal_address pa
	      on pa.postal_address_id = pi.postal_address_id


	WHERE
		pi.event_id = @intEventID and pi.active = 1 



	SET @intErrorCode = @@ERROR






declare @checkPeriod int
declare @end_date datetime
SET @checkPeriod = (select max(payment_period_id) from payment_period) + 1


select @end_date = max(end_date), @checkPeriod = max(payment_period_id) from payment_period

--update les vente de la periode passe avec le payment de la periode courante
update
order_profit
set payment_id = pa.payment_id
,update_date = getdate()
from order_profit op
inner join payment_info py
on py.event_id = op.event_id
inner join payment pa
on pa.payment_info_id = py.payment_info_id
and payment_period_id = @checkPeriod
where 
op.payment_id is null
and op.order_date <= @end_date 
and op.event_id = @intEventID


END



IF @intErrorCode = 0
	COMMIT TRANSACTION
ELSE
	ROLLBACK TRANSACTION
GO
