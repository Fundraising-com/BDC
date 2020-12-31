USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[PR_CALC_ORDER_ITEM_AMOUNTS]    Script Date: 06/07/2017 09:33:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE      PROCEDURE [dbo].[PR_CALC_ORDER_ITEM_AMOUNTS]

	@p_date                     	VARCHAR(10),
     	@p_amount 		 numeric(10,2),--float,
       	@p_section_id		INT,
             @p_product_code           VARCHAR(10)  , 
	@p_is_straight_order	VARCHAR(1),
	@p_campaign_id	INT,
	@MagPrice_Instance      INT,
	@p_ProvinceCode	VARCHAR(2),
	@CustomerOrderHeaderInstance INT
--        @p_tax_1        	float OUTPUT, --GST/hst
--	@p_tax_2        	float OUTPUT,-- PST 
--	@p_gross		float OUTPUT,-- item amount + taxes amount
--	@p_net			float OUTPUT-- item amount without any taxes amount
AS
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--    Saqib Shah -  April 2004
--     CALCULATES ALL APPLICABLE TAXES, GROSS AMOUNT AND NET AMOUNT FOR EACH ORDER ITEM
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  set nocount on
  DECLARE  @V_TAX_1 	             numeric(14,6) --gst/hst
  DECLARE  @V_TAX_2 		 numeric(14,6) --pst
  DECLARE  @V_GROSS		 numeric(14,6)
  DECLARE  @V_NET			 numeric(14,6)
  DECLARE  @V_UNIT_PRICE		 numeric(10,2)
  DECLARE  @V_IS_PRICE_WITH_TAX 	VARCHAR(1)
  DECLARE  @V_IS_TAX_INCLUDED	VARCHAR(1)
  DECLARE  @V_STAFF_DISCOUNT	 numeric(10,2)
  DECLARE  @V_MAJOR_PRODUCT	INT
  DECLARE  @V_GROUP_PROFIT	 numeric(10,2)
  DECLARE  @V_GROUP_PROFIT_AMOUNT   numeric(10,2)
  DECLARE  @V_PRODUCT_TYPE             INT
  DECLARE  @V_TITLE_CODE               VARCHAR(10)
  DECLARE  @V_PROVINCE                   VARCHAR(2)
  DECLARE  @V_COUNTRY                    VARCHAR(10)
  DECLARE  @V_SECTION_TYPE           INT

   

  declare       	@p_tax_1        	numeric(14,6)--	float  --GST
  declare	@p_tax_2       	numeric(14,6)--float -- PST or HST
  declare	@p_gross	numeric(14,6)--float -- item amount + taxes amount
  declare	@p_net		numeric(14,6)--float --item amount without any taxes amount
  declare	@p_group_profit_amount numeric(14,6)
  declare 	@Season_Year	int
  declare 	@Month	int
  declare 	@Season	varchar(1)
  declare	@OriginalGross numeric(14,6)

  SET @V_TAX_1   =  0.00
  SET @V_TAX_2   =  0
  SET @V_GROSS =  0
  SET @V_NET      =  0
  SET @V_STAFF_DISCOUNT = 0
  SET @V_GROUP_PROFIT = 0
  set @p_tax_1 = 0.00
  set @p_tax_2 = 0
  set @p_gross = 0
  set @p_net = 0
  set @p_group_profit_amount = 0
  set @OriginalGross = 0

/*
SELECT @V_COUNTRY= ac.Country, @V_PROVINCE= Ac.State
	From QspCanadaCommon..CAccount Ac, QspCanadaCommon..Campaign Camp
	Where Camp.Id = @p_campaign_id
	And Ac.Id=Camp.ShipToAccountId;
*/

 IF @p_section_id is null or @p_section_id = 0 or @MagPrice_Instance is null or @MagPrice_Instance = 0 or  -- if input is null then dont calculate at all
     @p_ProvinceCode is null or @p_ProvinceCode = '' or @p_campaign_id is null or @p_campaign_id = 0 or
     @p_product_code is null or @p_product_code = '0'  
    BEGIN

      SELECT @P_TAX_1 as tax1, @P_TAX_2 as tax2,@P_GROSS as gross ,@P_NET as net, @P_GROUP_PROFIT_AMOUNT as GroupProfitAmount
   END


ELSE -- if input values are valid then start calculating


   BEGIN

	Select top 1 @Season_Year = pricing_year,@Season = pricing_season
	from qspcanadaproduct..pricing_details
	where magPrice_instance = @MagPrice_Instance 



 SET @V_COUNTRY = 'CA'
 SET @V_PROVINCE = @p_ProvinceCode

 Select @V_SECTION_TYPE = Type from 
   QspCanadaProduct..programsection
   where ID = @p_section_id; 


 SELECT @V_STAFF_DISCOUNT = (      SELECT  StaffOrderDiscount
					FROM QSPCanadaCommon..CAMPAIGN
					WHERE IsStaffOrder = 1
					and ID = @p_campaign_id )

 SELECT   @V_IS_PRICE_WITH_TAX =  ISPRICEWITHTAX, @V_IS_TAX_INCLUDED = ISTAXINCLUDED
								    FROM   QSPCANADAPRODUCT.DBO.PROGRAMSECTIONTYPE
								    WHERE ID = @V_SECTION_TYPE   

 SELECT   @V_PRODUCT_TYPE = (SELECT top 1 TYPE
				       FROM QSPCANADAPRODUCT.DBO.PRODUCT
			                     WHERE PRODUCT_CODE = @P_PRODUCT_CODE and TYPE IS NOT NULL 				        and      product_year	 = @Season_Year       AND product_season = @Season  )

/*   BEGIN
	IF (@V_SECTION_TYPE  = 1 OR @V_SECTION_TYPE  = 11 OR @V_SECTION_TYPE  = 13 OR @V_SECTION_TYPE  = 15 OR @V_SECTION_TYPE  = 10 OR @V_SECTION_TYPE = 17) -- Gifts / Jewellery / Candles / Entertainment / Popcorn / Pretzel Rods
                BEGIN
                  SET  @V_MAJOR_PRODUCT = 2 
	  END
            ELSE IF  (@V_SECTION_TYPE  = 2 OR @V_SECTION_TYPE  = 14) --Magazines / TRT
                BEGIN
                  SET @V_MAJOR_PRODUCT = 1
	  END 
            ELSE
                BEGIN
                  SET @V_MAJOR_PRODUCT = -1
	  END 
   END*/

   BEGIN
    
     IF @V_PRODUCT_TYPE = 46001 --if magzine then fetch its title code
        BEGIN 
          SET @V_TITLE_CODE = @P_PRODUCT_CODE
        END

     ELSE
         BEGIN
            SET @V_TITLE_CODE = NULL
         END
   END 


/* SELECT @V_GROUP_PROFIT =  (	Select max(Isnull(Camp.GroupProfit,0))
				       	From  	QspCanadaCommon.Dbo.Campaign Cam,
						QspCanadaCommon.Dbo.CampaignProgram Camp,
						QspCanadaCommon.Dbo.Program prog
					Where Cam.Id        =  Camp.Campaignid
						and camp.ProgramID = prog.id
						And Camp.Campaignid = @P_Campaign_Id
						And Prog.Majorproductlineid = @V_Major_Product
						And Camp.DeletedTF = 0
						/*And Exists ( select 1 from QspCanadaProduct..Pricing_Details
							      where magprice_instance = @MagPrice_Instance
							             and FSIsbrochure = 1  ) */ 
							    ) */  
DECLARE	@OrderQualifierID INT,
		@CAccountCodeClass VARCHAR(10),
		@TRTGenerationCode VARCHAR(4),
		@IsStaffOrder BIT
SELECT	@OrderQualifierID  = b.OrderQualifierID,
		@CAccountCodeClass = acc.CAccountCodeClass,
		@TRTGenerationCode = coh.TRTGenerationCode,
		@IsStaffOrder = camp.IsStaffOrder
FROM	QSPCanadaOrderManagement..CustomerOrderHeader coh
JOIN	QSPCanadaOrderManagement..Batch b ON b.ID = coh.OrderBatchID AND b.Date = coh.OrderBatchDate
JOIN	QSPCanadaCommon..Campaign camp ON camp.ID = b.CampaignID
JOIN	QSPCanadaCommon..CAccount acc ON acc.ID = camp.BillToAccountID
WHERE	coh.Instance = @CustomerOrderHeaderInstance

DECLARE @ProgramType INT,
		@PostageAmount NUMERIC(10, 6)
SELECT	@ProgramType = pm.SubType,
		@PostageAmount = ISNULL(pd.PostageAmount,0) * ISNULL(pd.PostageRemitRate,0) * ISNULL(pd.ConversionRate,0)
FROM	QSPCanadaProduct..PRICING_DETAILS pd
JOIN	QSPCanadaProduct..ProgramSection ps ON ps.ID = pd.ProgramSectionID
JOIN	QSPCanadaProduct..PROGRAM_MASTER pm ON pm.Program_ID = ps.Program_ID
WHERE	pd.MagPrice_Instance = @MagPrice_Instance

DECLARE @PopcornGroupProfit NUMERIC(10,2)
IF (@V_SECTION_TYPE = 10)
BEGIN
	SELECT	@PopcornGroupProfit = convert(numeric(10,2), (convert(numeric(10,2), CP.GroupProfit ) / convert(numeric(10,2), 100 ))  )
	FROM	QSPCanadaCommon..CampaignProgram cp
	WHERE	cp.CampaignID = @p_campaign_id
	AND		cp.ProgramID IN (61, 66)

	SET @PopcornGroupProfit = ISNULL(@PopcornGroupProfit, 0.45)
END

DECLARE @SavingsPassGroupProfit NUMERIC(10,2)
IF (@V_SECTION_TYPE = 15)
BEGIN
	SELECT	@SavingsPassGroupProfit = convert(numeric(10,2), (convert(numeric(10,2), CP.GroupProfit ) / convert(numeric(10,2), 100 ))  )
	FROM	QSPCanadaCommon..CampaignProgram cp
	WHERE	cp.CampaignID = @p_campaign_id
	AND		cp.ProgramID = 64 --Savings Pass

	SET @SavingsPassGroupProfit = ISNULL(@SavingsPassGroupProfit, 0.40)
END

SET @V_GROUP_PROFIT = CASE WHEN @V_SECTION_TYPE = 1 THEN CASE WHEN @CAccountCodeClass = 'FM' THEN 0.00
														WHEN @OrderQualifierID = 39022 THEN 0.00
														WHEN @ProgramType IN (30327) THEN 0.75
														WHEN @ProgramType IN (30323, 30329) THEN 0.45
														WHEN @ProgramType IN (30337) THEN 0.30
														WHEN @ProgramType IN (30344) THEN 0.35
														ELSE 0.40
													END
					  WHEN @V_SECTION_TYPE = 2 THEN CASE @IsStaffOrder WHEN 1 THEN 0.00 ELSE 0.37 END
					  WHEN @V_SECTION_TYPE = 9 THEN 0.00 --Will be calculated during order close as it depends on quantity of items in shipment
					  WHEN @V_SECTION_TYPE = 10 THEN CASE WHEN @OrderQualifierID = 39022 THEN 0.00 
														WHEN @CAccountCodeClass = 'FM' THEN 0.00
														ELSE @PopcornGroupProfit
													END
					  WHEN @V_SECTION_TYPE = 11 THEN CASE WHEN @OrderQualifierID = 39022 THEN 0.00 
														WHEN @CAccountCodeClass = 'FM' THEN 0.00
														ELSE 0.35
													END
					  WHEN @V_SECTION_TYPE = 14 THEN CASE WHEN @OrderQualifierID = 39022 THEN 0.00 
														WHEN @CAccountCodeClass = 'FM' THEN 0.00
														WHEN @TRTGenerationCode IN ('2') THEN 0.20
														WHEN @TRTGenerationCode IN ('0', 'N') THEN 0.00
														ELSE 0.37
													END
					  WHEN @V_SECTION_TYPE = 15 THEN CASE WHEN @OrderQualifierID = 39022 THEN 0.00 
														WHEN @CAccountCodeClass = 'FM' THEN 0.00
														WHEN @ProgramType IN (30342) THEN 0.50
														ELSE @SavingsPassGroupProfit
													END
					  WHEN @V_SECTION_TYPE = 16 THEN CASE WHEN @OrderQualifierID = 39022 THEN 0.00 
														WHEN @CAccountCodeClass = 'FM' THEN 0.00
														ELSE 0.37
													END
					  WHEN @V_SECTION_TYPE = 17 THEN CASE WHEN @OrderQualifierID = 39022 THEN 0.00 
														WHEN @CAccountCodeClass = 'FM' THEN 0.00
														ELSE 0.40
													END
					  WHEN @V_SECTION_TYPE = 18 THEN CASE WHEN @OrderQualifierID = 39022 THEN 0.00 
														WHEN @CAccountCodeClass = 'FM' THEN 0.00
														ELSE 0.30
													END
					  ELSE 0
			END

         IF (@V_SECTION_TYPE = 4 OR @V_SECTION_TYPE = 9 OR @V_SECTION_TYPE = 10 OR @V_SECTION_TYPE = 15 OR (@V_SECTION_TYPE = 1 AND SUBSTRING(@p_product_code, 1, 2) IN ('DO', 'D1'))) -- if incentive or Cookie dough or Popcorn or Discount Card or Donation then no tax should be calculated, no gst no pst
               
               BEGIN
                 SET @V_TAX_1 = 0
				SET @V_TAX_2 = 0
				SET @V_GROSS = @p_amount
				SET @V_GROUP_PROFIT_AMOUNT= @V_GROSS * @V_GROUP_PROFIT
				SET @V_NET = @p_amount - @V_GROUP_PROFIT_AMOUNT
    	  END
         ELSE
               
            BEGIN --# 2
                     
	        SET @V_UNIT_PRICE = @P_AMOUNT

                      IF ISNULL(@V_STAFF_DISCOUNT,0) > 0 -- if there is a staff discount exist for campaign then reduce the amount
	         BEGIN
	            SET @V_UNIT_PRICE = @V_UNIT_PRICE - (@V_UNIT_PRICE * (@V_STAFF_DISCOUNT/100))
 	         END

	          IF ISNULL(@V_IS_PRICE_WITH_TAX,'N') = 'Y' -- if item price includes the tax then reduce the tax in order to calculate the NET value
              BEGIN

	                SET @V_GROSS = @V_UNIT_PRICE
					SET @OriginalGross = @V_GROSS

					--Pretzel Rods 30% needs tax calculated on wholesale not retail
					IF @V_SECTION_TYPE = 18
						SET @V_GROSS = @V_UNIT_PRICE * 0.70

					EXEC QspCanadaCommon.dbo.PR_CAN_TAX_CALC_TAXES_AMOUNT    @p_prov_code 		= @V_PROVINCE,
						    					@p_country_code 	= @V_COUNTRY,
						    					@p_date 		= @p_date, 
						    					@p_amount		= @V_GROSS,
						    					@P_TRANSAC 		= 'DEL',
						    					@p_section_type	= @V_SECTION_TYPE,
						    					@p_return_amount 	= @V_NET OUTPUT
              END
	          ELSE  -- if item doesnt include tax then calculate the tax inorder to calculate the GROSS amount
			  BEGIN
	                SET @V_GROSS = @V_UNIT_PRICE
					SET @OriginalGross = @V_GROSS

					--Pretzel Rods 40% needs tax calculated on wholesale not retail
					IF @V_SECTION_TYPE = 17
						SET @V_GROSS = @V_UNIT_PRICE * 0.60

					EXEC QspCanadaCommon.dbo.PR_CAN_TAX_CALC_TAXES_AMOUNT @p_prov_code 		= @V_PROVINCE,
						    					@p_country_code 	= @V_COUNTRY,
						    					@p_date 		= @p_date, 
						    					@p_amount		= @V_GROSS,
						    					@P_TRANSAC 		= 'ADD',
						    					@p_section_type	= @V_SECTION_TYPE,
						    					@p_return_amount 	= @V_GROSS OUTPUT
					SET @V_NET = @V_UNIT_PRICE;

	          END

		      IF @V_IS_TAX_INCLUDED = 1 AND @V_GROUP_PROFIT > 0  -- if tax is included in group profit

              BEGIN

		           SET @V_GROUP_PROFIT_AMOUNT= @V_GROSS * @V_GROUP_PROFIT
		           --SET @V_GROSS = @V_GROSS - @V_GROUP_PROFIT_AMOUNT

				   --Pretzel Rods 30% 
				   IF @V_SECTION_TYPE = 18
					  SET @V_GROUP_PROFIT_AMOUNT= @OriginalGross * @V_GROUP_PROFIT

		           EXEC QspCanadaCommon.dbo.PR_CAN_TAX_CALC_TAXES_AMOUNT  	@p_prov_code 		= @V_PROVINCE,
						    		         				@p_country_code 	= @V_COUNTRY,
						    		         				@p_date 		= @p_date, 
						    		         				@p_amount		= @V_GROSS,
						    		         				@P_TRANSAC 		= 'DEL',
						    		         				@p_section_type	= @V_SECTION_TYPE,
						    		         				@p_return_amount 	= @V_NET OUTPUT
              END 

	    	  ELSE IF @V_IS_TAX_INCLUDED = 0 AND @V_GROUP_PROFIT > 0
              BEGIN

				IF @V_SECTION_TYPE = 17
					SET @V_GROUP_PROFIT_AMOUNT= (@V_NET - @PostageAmount) * @V_GROUP_PROFIT

				EXEC QspCanadaCommon.dbo.PR_CAN_TAX_CALC_TAXES_AMOUNT  	@p_prov_code 	= @V_PROVINCE,
						    		         			@p_country_code 	= @V_COUNTRY,
						    		         			@p_date 		= @p_date, 
						    		         			@p_amount	= @V_GROSS,
						    		         			@P_TRANSAC 	= 'DEL',
						    		         			@p_section_type	= @V_SECTION_TYPE,
						    		         			@p_return_amount = @V_NET OUTPUT

 				IF (@V_SECTION_TYPE <> 17)
					SET @V_GROUP_PROFIT_AMOUNT= (@V_NET - @PostageAmount) * @V_GROUP_PROFIT
		        --SET @V_GROSS = @V_GROSS - @V_GROUP_PROFIT_AMOUNT
            END

			ELSE
			BEGIN
				SET @V_GROUP_PROFIT_AMOUNT = 0.00
			END
                    -- now calculate Gst and pst based on the derived NET amount
            BEGIN

				EXEC QspCanadaCommon..PR_CAN_TAX_CALC_TAX         @p_prov_code 	= @V_PROVINCE,
						    		         @p_country_code 	= @V_COUNTRY,
						    		         @p_date 		= @p_date, 
						    		         @p_amount	= @V_NET,
								         @p_section_type   = @V_SECTION_TYPE,
						    		         @p_title_code	= null, -- sending null so it can calc tax whether or not magazine has tax reg#, cos we have to charge  tax to customer, only we dont have to pay taxes tu fulf house for remitting if title has no tax reg#
								         @P_TAX_1        	= @V_TAX_1 OUTPUT,
						    		         @P_TAX_2 	= @V_TAX_2 OUTPUT

				SET @V_NET = @V_GROSS - @V_GROUP_PROFIT_AMOUNT - ISNULL(@V_TAX_1,0) - ISNULL(@V_TAX_2,0) - @PostageAmount
	

            END
	 	
         END --#2
     

----------------------RETURN  CLAUSES---------------------------------

     SET @P_GROSS =  ISNULL(@V_GROSS,0)
     SET @P_NET      =  ISNULL(@V_NET,0)
     SET @P_TAX_1  =  ISNULL(@V_TAX_1,0)
     SET @P_TAX_2  =  ISNULL(@V_TAX_2,0)
	 SET @P_GROUP_PROFIT_AMOUNT = ISNULL(@V_GROUP_PROFIT_AMOUNT,0)
    
 SELECT @P_TAX_1 as tax1, @P_TAX_2 as tax2,@P_GROSS as gross ,@P_NET as net, @P_GROUP_PROFIT_AMOUNT

END
GO
