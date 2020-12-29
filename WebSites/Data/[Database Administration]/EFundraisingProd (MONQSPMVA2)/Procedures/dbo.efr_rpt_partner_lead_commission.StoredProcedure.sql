USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efr_rpt_partner_lead_commission]    Script Date: 02/14/2014 13:03:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[efr_rpt_partner_lead_commission] @start_date as datetime 
  , @end_date as datetime
  , @partner_id as int
AS
BEGIN

    declare @end_date2 varchar(30)
    set @end_date2 = convert(varchar(30), @end_date, 101) + ' 23:59:59'
    set @end_date = convert(datetime, @end_date2)

    -- header du rapport
    select 'Year'
	    , 'Month#'
	    , 'Month'
	    , 'Channel'
	    , 'Min Range'
	    , 'Max Range'
	    , 'Amount Per Lead'
	    , 'Qualifying Leads'
	    , '$Commission Amount'

    IF @start_date < '2011-01-01' AND @end_date < '2011-01-01'
    BEGIN
		SELECT 
    		year(l.lead_entry_date) as year,
			month(l.lead_entry_date) as monthnum,
			DateName(month,l.lead_entry_date) as month,
    		lc.description as lead_channel_descr, 
    		pcr.MinThresholdValue as MinThresholdValue, 
    		pcr.MaxThresholdValue as MaxThresholdValue, 
    		plc.fixed_amount, 
		CASE
			  WHEN count(l.lead_id) between MinThresholdValue and MaxThresholdValue THEN
						  (count(l.lead_id) - MinThresholdValue+1) 
					WHEN count(l.lead_id) > MaxThresholdValue THEN
						   (MaxThresholdValue - MinThresholdValue+1)
					ELSE
						  0
			  END as QualifyingLeads,
		CASE
			  WHEN count(l.lead_id) between MinThresholdValue and MaxThresholdValue THEN
						  (count(l.lead_id) - MinThresholdValue+1) * fixed_amount
					WHEN count(l.lead_id) > MaxThresholdValue THEN
						   (MaxThresholdValue - MinThresholdValue+1)  * fixed_amount
					ELSE
						  0
			  END as CommissionPaymentDue
		 FROM partner p
		INNER JOIN promotion pr on p.partner_id = pr.partner_id
		INNER JOIN lead l on l.promotion_id = pr.promotion_id
		INNER JOIN partner_lead_commission plc ON p.partner_id = plc.partner_id
		INNER JOIN partner_commission_range pcr ON plc.partner_commission_range_id = pcr.partner_commission_range_id
		INNER JOIN lead_channel lc ON plc.channel_code = lc.channel_code AND lc.channel_code = l.channel_code
		WHERE p.partner_id = @partner_id
		  AND l.lead_entry_date between @start_date and @end_date
		  AND p.partner_id = @partner_id 
		  AND (day_phone is not null OR evening_phone is not null)
		  AND country_code in ('US','CA')
		  AND plc.effective_date = (SELECT MAX(plc2.Effective_Date)
    					  FROM partner_lead_commission plc2
    					 WHERE plc.partner_id = plc2.partner_id  
    					   AND plc2.Effective_Date <= l.lead_entry_date
    					   AND plc2.active = 1)
		  AND plc.active = 1
		GROUP BY 
			year(l.lead_entry_date),
			month(l.lead_entry_date),
			DateName(month,l.lead_entry_date),
    		lc.description, 
    		pcr.MinThresholdValue, 
    		pcr.MaxThresholdValue, 
    		plc.fixed_amount
		HAVING CASE
    			WHEN count(l.lead_id) between MinThresholdValue and MaxThresholdValue THEN
    				(MaxThresholdValue - count(l.lead_id)+1) * fixed_amount
    			WHEN count(l.lead_id) > MaxThresholdValue THEN
    				 (MaxThresholdValue+1) * fixed_amount
    			ELSE
    				0
    		END > 0
		order by 1,2,4,5
	END
	ELSE IF @start_date >= '2011-01-01' AND @end_date >= '2011-01-01'
	BEGIN
		--
		-- New rule to check for Has_Been_Contacted after January 1, 2011
		-- 
		SELECT 
    		year(l.lead_entry_date) as year,
			month(l.lead_entry_date) as monthnum,
			DateName(month,l.lead_entry_date) as month,
    		lc.description as lead_channel_descr, 
    		pcr.MinThresholdValue as MinThresholdValue, 
    		pcr.MaxThresholdValue as MaxThresholdValue, 
    		plc.fixed_amount, 
		CASE
			  WHEN count(l.lead_id) between MinThresholdValue and MaxThresholdValue THEN
						  (count(l.lead_id) - MinThresholdValue+1) 
					WHEN count(l.lead_id) > MaxThresholdValue THEN
						   (MaxThresholdValue - MinThresholdValue+1)
					ELSE
						  0
			  END as QualifyingLeads,
		CASE
			  WHEN count(l.lead_id) between MinThresholdValue and MaxThresholdValue THEN
						  (count(l.lead_id) - MinThresholdValue+1) * fixed_amount
					WHEN count(l.lead_id) > MaxThresholdValue THEN
						   (MaxThresholdValue - MinThresholdValue+1)  * fixed_amount
					ELSE
						  0
			  END as CommissionPaymentDue
		 FROM partner p
		INNER JOIN promotion pr on p.partner_id = pr.partner_id
		INNER JOIN lead l on l.promotion_id = pr.promotion_id
		INNER JOIN partner_lead_commission plc ON p.partner_id = plc.partner_id
		INNER JOIN partner_commission_range pcr ON plc.partner_commission_range_id = pcr.partner_commission_range_id
		INNER JOIN lead_channel lc ON plc.channel_code = lc.channel_code AND lc.channel_code = l.channel_code
		WHERE p.partner_id = @partner_id
		  AND l.lead_entry_date between @start_date and @end_date
		  AND p.partner_id = @partner_id 
		  AND (day_phone is not null OR evening_phone is not null)
		  AND country_code in ('US','CA')
		  AND plc.effective_date = (SELECT MAX(plc2.Effective_Date)
    					  FROM partner_lead_commission plc2
    					 WHERE plc.partner_id = plc2.partner_id  
    					   AND plc2.Effective_Date <= l.lead_entry_date
    					   AND plc2.active = 1)
		  AND plc.active = 1
		  AND l.has_been_contacted = 1
		GROUP BY 
			year(l.lead_entry_date),
			month(l.lead_entry_date),
			DateName(month,l.lead_entry_date),
    		lc.description, 
    		pcr.MinThresholdValue, 
    		pcr.MaxThresholdValue, 
    		plc.fixed_amount
		HAVING CASE
    			WHEN count(l.lead_id) between MinThresholdValue and MaxThresholdValue THEN
    				(MaxThresholdValue - count(l.lead_id)+1) * fixed_amount
    			WHEN count(l.lead_id) > MaxThresholdValue THEN
    				 (MaxThresholdValue+1) * fixed_amount
    			ELSE
    				0
    		END > 0
		order by 1,2,4,5
	END
	ELSE
	BEGIN
		-- Return no row
		SELECT 
    		'' as year,
			'' as monthnum,
			'' as month,
    		'' as lead_channel_descr, 
    		0 as MinThresholdValue, 
    		0 as MaxThresholdValue, 
    		0 as fixed_amount,
    		0 as QualifyingLeads,
    		0 as CommissionPaymentDue
		WHERE 1 = 2
	END

END
GO
