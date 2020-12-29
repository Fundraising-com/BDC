USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[rs_invoice_header]    Script Date: 02/14/2014 13:08:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE    PROCEDURE [dbo].[rs_invoice_header] @sale_id int AS

select
	 (bc.street_address + ' ' + bc.city_name + ', ' + bc.state_code + ' ' + bc.zip_code + ' | T:' 
		+ bc.telephone_number + ' | F:' + bc.fax_number) as billing_compagny_address 
	, bc.web as billing_compagny_website
	, bc.logo_path as billing_compagny_logo
from sale s
left join billing_company bc on bc.billing_company_id = s.billing_company_id
where s.sales_id = @sale_id
GO
