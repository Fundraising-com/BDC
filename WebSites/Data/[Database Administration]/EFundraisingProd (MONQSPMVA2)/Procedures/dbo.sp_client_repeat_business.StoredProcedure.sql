USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[sp_client_repeat_business]    Script Date: 02/14/2014 13:08:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*SET @date_from1 = '2001-07-01'
SET @date_to1 = '2002-06-30'
SET @date_from2 = '2002-07-01'
SET @date_to2 = '2003-06-30'*/



CREATE  PROCEDURE [dbo].[sp_client_repeat_business](@date_from1 DATETIME ,@date_to1 DATETIME ,@date_from2 DATETIME ,@date_to2 DATETIME ) 
AS


SELECT Sum(first_year) as inYear, SUM(second_year) as repeat, SUM(CONVERT(FLOAT,second_year))/SUM(CONVERT(FLOAT,first_year)) *100 as ratio
FROM  
(
SELECT COUNT(*) as first_year, 0 as second_year FROM 
(
SELECT DISTINCT c.client_id, c.client_sequence_code
FROM client c, sale s
WHERE c.client_id = s.client_id
AND c.client_sequence_code = s.client_sequence_code
AND s.sales_date BETWEEN @date_from1 AND @date_to1
GROUP BY c.client_id, c.client_sequence_code
) as first_year

UNION

SELECT 0, COUNT(*) FROM 
(
SELECT DISTINCT c.client_id, c.client_sequence_code
FROM client c, sale s, 
(SELECT c.client_id, c.client_sequence_code
FROM client c, sale s
WHERE c.client_id = s.client_id
AND c.client_sequence_code = s.client_sequence_code
AND s.sales_date BETWEEN @date_from1 AND @date_to1) as cfy

WHERE c.client_id = s.client_id
AND c.client_sequence_code = s.client_sequence_code
AND c.client_id = cfy.client_id
AND c.client_sequence_code = cfy.client_sequence_code
AND s.sales_date BETWEEN @date_from2 AND @date_to2
) as second_year
) as ratio
GO
