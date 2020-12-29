USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[crm_all_sales_bak]    Script Date: 02/14/2014 13:01:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   VIEW [dbo].[crm_all_sales_bak]
AS

SELECT
sub.Product_Class, 
sub.lead_id,
sub.sales_id, 
sub.supplier_name,
pg.Description AS [Product Type],
sub.Production_Status, 
sub.sales_date, 
sub.scheduled_ship_date, 
sub.scheduled_delivery_date, 
sub.Consultant, 
sub.Sale_Status, 
sub.[Sponsor Required], 
sub.Carrier, 
sub.Shipping_Option, 
sub.Method, 
sub.Term,
sub.Client_Seq, 
sub.Id, 
sub.Coupons, 
sub.Local_Sponsor_Found, 
sub.division_name, 
sub.po_number, 
sub.PO_Status, 
sub.consultant_id, 
sub.sales_status_id, 
sub.production_status_id, 
sub.payment_term_id, 
sub.po_status_id, 
sub.carrier_id, 
sub.shipping_option_id, 
sub.division_id, 
sub.Is_Validated, 
sub.waybill_no

FROM (

SELECT --DISTINCT 
pc.description AS Product_Class, 
s.lead_id,
s.sales_id, 
su.supplier_name,
max(sb.package_id) as package_id,
ps.Description AS Production_Status, 
s.sales_date, 
s.scheduled_ship_date, 
s.scheduled_delivery_date, 
con.name AS Consultant, 
ss.Description AS Sale_Status, 
(CASE WHEN Sponsor_Required = 0 THEN 'No' ELSE 'Yes'END ) AS [Sponsor Required], 
ca.description AS Carrier, 
cso.description AS Shipping_Option, 
pm.description AS Method, 
pt.description AS Term, 
c.client_sequence_code AS Client_Seq, 
c.client_id AS Id, 
(CASE WHEN sics.Sales_ID = null THEN 'no' ELSE 'yes' END) AS Coupons, 
(CASE WHEN s.local_Sponsor_Found = 1 THEN 'True' ELSE 'False' END) AS Local_Sponsor_Found, 
d.division_name, 
s.po_number, 
pos.description AS PO_Status, 
con.consultant_id, 
s.sales_status_id, 
s.production_status_id, 
s.payment_term_id, 
s.po_status_id, 
s.carrier_id, 
s.shipping_option_id, 
d.division_id, 
(CASE WHEN s.Is_Validated = 1 THEN 'True' ELSE 'False' END) AS Is_Validated, 
s.waybill_no
FROM dbo.Sale s
	INNER JOIN dbo.Client c
		ON s.client_id = c.client_id AND s.client_sequence_code = c.client_sequence_code 
	INNER JOIN dbo.Division d
		ON d.division_id = c.division_id
	INNER JOIN dbo.Consultant con
		ON s.consultant_id = con.consultant_id 
	INNER JOIN dbo.Production_Status ps
		ON s.production_status_id = ps.Production_Status_ID 
	INNER JOIN dbo.Sales_Status ss 
		ON s.sales_status_id = ss.Sales_Status_ID 
	INNER JOIN dbo.Payment_Method pm
		ON s.payment_method_id = pm.payment_method_id
	INNER JOIN dbo.Payment_Term pt 
		ON s.payment_term_id = pt.payment_term_id
	LEFT JOIN dbo.PO_Status pos 
		ON s.po_status_id = pos.po_status_id
	LEFT JOIN dbo.Carrier ca 
		ON s.carrier_id = ca.carrier_id
	LEFT JOIN dbo.Carrier_Shipping_Option cso
		ON s.shipping_option_id = cso.shipping_option_id
	LEFT JOIN dbo.Sales_Item_Coupon_Sheet sics
		ON s.sales_id = sics.Sales_ID
	INNER JOIN dbo.Sales_Item si 
		ON s.sales_id = si.sales_id
	INNER JOIN dbo.Scratch_Book sb 
		ON si.scratch_book_id = sb.scratch_book_id
	INNER JOIN dbo.Product_Class pc 
		ON sb.product_class_id = pc.product_class_id
	LEFT JOIN dbo.supplier su
		ON su.supplier_id = sb.supplier_id
GROUP BY
pc.description, 
s.lead_id,
s.sales_id, 
su.supplier_name,
ps.Description, 
s.sales_date, 
s.scheduled_ship_date, 
s.scheduled_delivery_date, 
con.name, 
ss.Description, 
Sponsor_Required, 
ca.description, 
cso.description, 
pm.description, 
pt.description, 
c.client_sequence_code, 
c.client_id, 
sics.Sales_ID, 
s.local_Sponsor_Found, 
d.division_name, 
s.po_number, 
pos.description, 
con.consultant_id, 
s.sales_status_id, 
s.production_status_id, 
s.payment_term_id, 
s.po_status_id, 
s.carrier_id, 
s.shipping_option_id, 
d.division_id, 
s.Is_Validated, 
s.waybill_no

) AS sub

LEFT JOIN dbo.Package pg
ON pg.Package_Id = sub.package_id
GO
