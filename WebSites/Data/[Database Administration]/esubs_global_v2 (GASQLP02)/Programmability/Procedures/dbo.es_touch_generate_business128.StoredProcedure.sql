USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_touch_generate_business128]    Script Date: 02/14/2014 13:07:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Philippe Girard
-- Create date: 	2007/08/15
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[es_touch_generate_business128]
AS
BEGIN

	DECLARE @food_account_id int
	DECLARE @fsm_id int
	DECLARE @touch_info_id int
	DECLARE @touch_id int	

	BEGIN TRAN

	-- List all the accounts we need to send email to
	CREATE TABLE #account (
		food_account_id int
		, fsm_id int
		, processed bit
	)
	
	INSERT INTO #account (
		food_account_id
		, fsm_id
		, processed
	)
	select A.account_id as food_account_id
		--	, A.fulf_account_id
		--	, A.account_name
		, A.fm_id as fsm_id
		, 0 as processed
	from QSPFulfillment.dbo.Organization O with(nolock)
	inner join QSPFulfillment.dbo.Account A  with(nolock)
		on O.organization_id = A.organization_id
	inner join QSPFulfillment.dbo.field_sales_manager fsm with(nolock)
		on fsm.fm_id = A.fm_id
		and fsm.[deleted] = 0
	inner join QSPFulfillment.dbo.campaign c with(nolock)
		on a.account_id = c.account_id
	inner join QSPFulfillment.dbo.[order] ord with(nolock)	on c.campaign_id = ord.campaign_id 
	left outer join external_account ea  with(nolock)
		on a.account_id = ea.food_account_id
	where
		ea.food_account_id is null
		and 
		ord.create_date >= '2008-02-01'
		--AND O.organization_id = @organizationID
		AND O.business_division_id = 1
		AND O.[deleted] = 0
		AND A.[deleted] = 0 
		AND 
		(
			A.fulf_account_id between  30000000 and  30999999 --WFC 
			-- OR A.fulf_account_id between 425000000 and 425099999 --MMB
			-- OR A.fulf_account_id between 425200000 and 425299999 --MMB
			-- OR A.fulf_account_id between 425400000 and 425499999 --MMB
			-- OR A.fulf_account_id between 425900000 and 425999999 --MMB
		)
		AND A.fulf_account_id NOT IN
		(
		425888812 --D2C MAGAZINE ORDERS (rolls up to same flagpole as 428999999)
		, 425888813 --RD AGENCY TEST - ATTACHED MAIL -AmericanMagazineOutlet.com
		, 425888814 --QSP RETENTION BLITZ FREE OFFER
		, 428999999 --QDS MAGAZINE VOUCHER ORDERS
		, 710111111  --Gift Spree: Early Signing 2007
		)
		-- test FSM
		--AND A.fm_id in (0332,0345,0401,0413,0576,0745,0758,0769,0801,0831,0841,0940,0951,1372,1675,4444,4575,4774,5299,5584,6167,7614,7985,8365,8795,9478,9804,9844)
	    AND a.fm_id not in (0120)
	GROUP BY A.account_id, A.fm_id

	IF @@ROWCOUNT > 0
	BEGIN

		-- Create touch_info
		
		INSERT INTO touch_info (
			business_rule_id
			, launch_date
			, create_date
		) VALUES (
			128
			, getdate()
			, getdate()
		)
		
		IF @@error <> 0
		BEGIN
			ROLLBACK TRAN
			RETURN -1
		END
		ELSE
		BEGIN
	    	
			SET @touch_info_id = SCOPE_IDENTITY()
			
			-- Loop to insert into touch and external_account
			WHILE EXISTS(SELECT * FROM #account WHERE processed = 0)
			BEGIN

				INSERT INTO touch (
					event_participation_id
					,touch_info_id
					,processed
					,create_date
				) 
				SELECT NULL
					  ,@touch_info_id
					  ,0
					  ,getdate()

				IF @@error <> 0
				BEGIN
					ROLLBACK TRAN
					RETURN -1
				END
				ELSE
				BEGIN
					SET @touch_id = SCOPE_IDENTITY()

					SELECT TOP 1 @food_account_id = food_account_id
							, @fsm_id = fsm_id
					FROM #account
					WHERE processed = 0
					
					UPDATE #account
					SET processed = 1
					WHERE food_account_id = @food_account_id

					-- insert into external_account
					
					INSERT INTO external_account (
						food_account_id
						, fsm_id
						, touch_id
					) VALUES (
						@food_account_id
						, @fsm_id
						, @touch_id
					)

					IF @@error <> 0
					BEGIN
						ROLLBACK TRAN
						RETURN -1
					END

				END
			END
			
		END
	END
	
	COMMIT TRANSACTION
	RETURN 0	
END
GO
