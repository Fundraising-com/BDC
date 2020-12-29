USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[crm_FEDEX_shipping_address]    Script Date: 02/14/2014 13:01:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--select * from dbo.crm_all_sales
CREATE    VIEW [dbo].[crm_FEDEX_shipping_address]
AS


SELECT --DISTINCT 
	s.lead_id
	, s.sales_id 
	, attention_of
	, ca.street_address
	, state_code
	, country_code
	, city
	, zip_code
FROM dbo.Sale s with(nolock)
	INNER JOIN dbo.Client c with(nolock)
		ON s.client_id = c.client_id AND s.client_sequence_code = c.client_sequence_code 
	INNER JOIN dbo.client_address ca with(nolock)
		ON ca.client_id = c.client_id AND ca.client_sequence_code = c.client_sequence_code 
	INNER JOIN dbo.sales_item si  with(nolock)
		ON si.sales_id = s.sales_id
	INNER JOIN dbo.scratch_book sb  with(nolock)
		ON sb.scratch_book_id = si.scratch_book_id
where 
	s.production_status_id = 13 -- In Process
	and s.sales_status_id = 2 -- confirmed
	and s.is_validated = 1 -- validated
	and sb.product_class_id in (1, 2, 3, 8, 10, 39 )--Scratchcard, Samples, Agent Products, Brochures, Restaurant Cards, E-Fund Cards
	and ca.address_type = 'ST'
GO
