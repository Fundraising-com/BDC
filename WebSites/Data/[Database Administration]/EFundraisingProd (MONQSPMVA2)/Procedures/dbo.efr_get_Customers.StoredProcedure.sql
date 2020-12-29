USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efr_get_Customers]    Script Date: 02/14/2014 13:03:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================

-- Author: <Jason Farrell>

-- Create date: <December 21 2009>

-- Description: <Generate eFundraising.com Mailing List>

-- =============================================

CREATE PROCEDURE [dbo].[efr_get_Customers] 

-- Add the parameters for the stored procedure here

@partner_id int = null , @date_from char(15), @date_to char(15), @country_Selected char (10)

AS

BEGIN

-- SET NOCOUNT ON added to prevent extra result sets from

-- interfering with SELECT statements.

SET NOCOUNT ON;

SELECT

l.lead_entry_date
, z_mkt_one_sale.NumberOfSales
, z_mkt_one_sale.TotalSales
, z_mkt_one_sale.LastSaleID
, z_mkt_one_sale.LastSalesDate
, l.lead_id
, l.first_name
, l.last_name
, REPLACE ( l.street_address , ',', ' ')
, REPLACE ( l.city, ',', ' ')
, l.state_code
, l.country_code
, l.zip_code
, l.day_phone
, l.evening_phone
, l.email
, con.name
, l.organization
, gt.description
, ot.organization_type_desc
, p.description
, pt.Description
, p.partner_id 
, pa.partner_name 
FROM Lead l with(nolock)
INNER JOIN Consultant con with(nolock) ON l.consultant_id = con.consultant_id
INNER JOIN Group_Type gt with(nolock) ON l.group_type_id = gt.group_type_id
INNER JOIN Organization_Type ot with(nolock) ON l.organization_type_id = ot.organization_type_id
INNER JOIN Promotion p with(nolock) ON l.promotion_id = p.promotion_id
inner join Partner pa with(nolock) on pa.partner_id = p.partner_id
INNER JOIN Promotion_Type pt with(nolock) ON p.promotion_type_code = pt.Promotion_Type_Code
left outer join dbo.crm_static_past3seasons_new ps with(nolock) on ps.qsp_account_matching_code = l.matching_code
and fm_id not in(1234,1366,1386,1555,1556,1557,1558,1559,1560,1561,1562,1563,1564,1565,1566,1567,1568,1569,1570,
1571,1572,1573,1574,1575,1576,1577,1578,1579,1580,1581,1582,1583,1683,1684,5728,5729)
INNER JOIN 

(

SELECT 

c.lead_id

, Count(s.sales_id) AS CountOfsales_id

, Min(s.sales_id) AS FirstOfsales_id

, Min(s.sales_date) AS FirstOfsales_date

, count(*) as NumberOfSales

, sum(isnull(s.total_amount,0)) as TotalSales

, max(s.sales_id) as LastSaleID

, max(s.sales_date) as LastSalesDate

FROM Client c with(nolock)

INNER JOIN Sale s with(nolock) ON c.client_sequence_code = s.client_sequence_code AND c.client_id = s.client_id

GROUP BY c.lead_id

HAVING sum(isnull(s.total_amount,0)) > 0 

) z_mkt_one_sale on l.lead_id = z_mkt_one_sale.lead_id

WHERE 

l.lead_entry_date Between @date_from And @date_to

AND l.country_code = @country_Selected 

AND (p.partner_id = CAST(@partner_id as varchar(10)) or @partner_id is null) 


AND l.transfered_date Is Null 

AND l.ext_consultant_id Is Null

AND con.is_agent = 0

AND con.is_fm = 0 

AND p.promotion_type_code Not In ('AG')

and ps.qsp_account_matching_code is null

 END
GO
