USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[sp_repeat_business_leads_not_clients_usable]    Script Date: 02/14/2014 13:09:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE           PROCEDURE [dbo].[sp_repeat_business_leads_not_clients_usable](@date_from1 DATETIME ,@date_to1 DATETIME ,@date_from2 DATETIME ,@date_to2 DATETIME ) 
AS


SELECT Sum(first_year) as inYear, SUM(second_year) as repeat, Sum(total_amount) as total_amount, SUM(CONVERT(FLOAT,second_year))/SUM(CONVERT(FLOAT,first_year)) *100 as ratio
FROM  
(
SELECT COUNT(*) as first_year, 0 as second_year, 0.00 as total_amount FROM 
(
SELECT DISTINCT l.lead_id FROM lead l
	LEFT OUTER JOIN (SELECT DISTINCT c.lead_id FROM client c, sale s 
	where c.client_id = s.client_id 
	and c.client_sequence_code = s.client_sequence_code
	and s.sales_date  < @date_to1 
	AND s.Actual_ship_date is not null 
	AND s.sales_status_ID = 2) c On l.lead_id = c.lead_id
	INNER JOIN promotion p ON l.promotion_id = p.promotion_id
	INNER JOIN consultant co ON co.consultant_id = l.consultant_id
WHERE l.lead_entry_date BETWEEN @date_from1 AND @date_to1
AND p.promotion_type_code not in ('IM', 'AF')
AND l.channel_code <> 'LIST'
AND co.department_id = 7
AND l.Country_code = 'US'
AND c.lead_id is null
) as first_year

UNION

SELECT 0, COUNT(*), Sum(total_amount)  FROM 
(
SELECT DISTINCT c.client_id, c.client_sequence_code, SUM(s.total_amount) as total_amount 
FROM client c, sale s, 
(SELECT DISTINCT l.lead_id FROM lead l
	LEFT OUTER JOIN (SELECT DISTINCT c.lead_id FROM client c, sale s 
	where c.client_id = s.client_id 
	and c.client_sequence_code = s.client_sequence_code
	and s.sales_date  < @date_to1 
	AND s.Actual_ship_date is not null 
	AND s.sales_status_ID = 2) c On l.lead_id = c.lead_id
	INNER JOIN promotion p ON l.promotion_id = p.promotion_id
	INNER JOIN consultant co ON co.consultant_id = l.consultant_id
WHERE l.lead_entry_date BETWEEN @date_from1 AND @date_to1
AND p.promotion_type_code not in ('IM', 'AF')
AND l.channel_code <> 'LIST'
AND co.department_id = 7
AND l.Country_code = 'US'
AND c.lead_id is null
) as cfy

WHERE c.client_id = s.client_id
AND c.client_sequence_code = s.client_sequence_code
AND c.lead_id = cfy.lead_id
AND s.sales_date BETWEEN @date_from2 AND @date_to2
AND Actual_ship_date is not null 
AND sales_status_ID = 2
GROUP BY c.client_id, c.client_sequence_code

) as second_year
) as ratio
GO
