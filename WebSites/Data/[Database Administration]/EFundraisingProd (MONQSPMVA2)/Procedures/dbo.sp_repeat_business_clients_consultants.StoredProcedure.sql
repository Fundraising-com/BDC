USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[sp_repeat_business_clients_consultants]    Script Date: 02/14/2014 13:09:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE       PROCEDURE [dbo].[sp_repeat_business_clients_consultants](@date_from1 DATETIME ,@date_to1 DATETIME ,@date_from2 DATETIME ,@date_to2 DATETIME ) 
AS


SELECT con.name, Sum(first_year) as inYear, SUM(second_year) as repeat, SUM(total_amount) as total_amount, SUM(CONVERT(FLOAT,second_year))/SUM(CONVERT(FLOAT,first_year)) *100 as ratio
FROM  
(
SELECT first_year.consultant_id, COUNT(*) as first_year, 0 as second_year, 0 as total_amount FROM 
(
SELECT DISTINCT c.client_id, c.client_sequence_code, l.consultant_id
FROM client c, sale s, lead l
WHERE c.client_id = s.client_id
AND c.client_sequence_code = 'UI'
AND c.client_sequence_code = s.client_sequence_code
AND l.lead_id = c.lead_id
AND s.sales_date BETWEEN @date_from1 AND @date_to1
AND s.Actual_ship_date is not null 
AND s.sales_status_ID = 2
AND s.total_amount >0
GROUP BY l.consultant_id, c.client_id, c.client_sequence_code
) as first_year
group by first_year.consultant_id

UNION

SELECT second_year.consultant_id, 0, COUNT(*), SUM(second_year.total_amount) as total_amount FROM 
(
SELECT DISTINCT l.consultant_id, c.client_id, c.client_sequence_code, SUM(s.total_amount) as total_amount
FROM client c, sale s, lead l,
(SELECT DISTINCT c.client_id, c.client_sequence_code
FROM client c, sale s
WHERE c.client_id = s.client_id
AND c.client_sequence_code = 'UI'
AND c.client_sequence_code = s.client_sequence_code
AND s.sales_date BETWEEN @date_from1 AND @date_to1
AND s.Actual_ship_date is not null 
AND s.sales_status_ID = 2
AND s.total_amount >0
) as cfy

WHERE c.client_id = s.client_id
AND c.client_sequence_code = 'UI'
AND c.client_sequence_code = s.client_sequence_code
AND l.lead_id = c.lead_id
AND c.client_id = cfy.client_id
AND c.client_sequence_code = cfy.client_sequence_code
AND s.sales_date BETWEEN @date_from2 AND @date_to2
AND s.Actual_ship_date is not null 
AND s.sales_status_ID = 2
AND s.total_amount >0
GROUP BY l.consultant_id, c.client_id, c.client_sequence_code
) as second_year
group by second_year.consultant_id
) as ratio
inner join consultant con on con.consultant_id = ratio.consultant_id
group by con.name
GO
