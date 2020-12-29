USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[sp_repeat_business_leads_without_im_af]    Script Date: 02/14/2014 13:09:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
--SELECT * FROM Promotion_Type


CREATE      PROCEDURE [dbo].[sp_repeat_business_leads_without_im_af](@date_from1 DATETIME ,@date_to1 DATETIME ,@date_from2 DATETIME ,@date_to2 DATETIME ) 
AS


SELECT Sum(first_year) as inYear, SUM(second_year) as repeat, Sum(total_amount) as total_amount, SUM(CONVERT(FLOAT,second_year))/SUM(CONVERT(FLOAT,first_year)) *100 as ratio
FROM  
(
SELECT COUNT(*) as first_year, 0 as second_year, 0.00 as total_amount FROM 
(
SELECT lead_id FROM lead l, promotion p 
WHERE lead_entry_date BETWEEN @date_from1 AND @date_to1
AND l.promotion_id = p.promotion_id
AND promotion_type_code not in ('IM', 'AF')
AND l.channel_code <> 'LIST'
) as first_year

UNION

SELECT 0, COUNT(*), Sum(total_amount)  FROM 
(
SELECT DISTINCT c.client_id, c.client_sequence_code, SUM(s.total_amount) as total_amount 
FROM client c, sale s, 
(SELECT lead_id FROM lead l, promotion p 
WHERE lead_entry_date BETWEEN @date_from1 AND @date_to1
AND l.promotion_id = p.promotion_id
AND promotion_type_code not in ('IM', 'AF')
AND l.channel_code <> 'LIST'
) as cfy

WHERE c.client_id = s.client_id
AND c.client_sequence_code = s.client_sequence_code
AND c.lead_id = cfy.lead_id
AND s.sales_date BETWEEN @date_from2 AND @date_to2
AND s.Actual_ship_date is not null 
AND s.sales_status_ID = 2
GROUP BY c.client_id, c.client_sequence_code

) as second_year
) as ratio
GO
