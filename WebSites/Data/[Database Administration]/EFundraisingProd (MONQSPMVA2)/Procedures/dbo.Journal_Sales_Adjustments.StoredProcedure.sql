USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[Journal_Sales_Adjustments]    Script Date: 02/14/2014 13:08:42 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
-- Used as source for the "Sales and adjustment journal", a report found in the Reporting module. 
-- Open the Reporting module, 
-- choose "Accounting", 
-- then "Sales and Deposits", 
-- select the radio button "Sales and adjustments"
-- choose your dates and click on "Detail report"
-- Actual name of the report object is "journal_sales_adjustments"

-- Nicolas Désy, dec 3rd 2003

CREATE  PROCEDURE [dbo].[Journal_Sales_Adjustments] (@StartDate AS DateTime, @EndDate As Datetime)
AS
(
SELECT 
	s.Sales_ID
	, s.Actual_Ship_Date AS Date
	, CONVERT(CHAR(2),s.client_sequence_code) + '-' + CONVERT(CHAR(7),s.client_id) AS Client_No
	, co.Currency_Code
	, cl.Organization
	, 'Invoice' AS type
	, cs.Description AS Sequence
	, CONVERT(MONEY,tbs.Original_Amount) AS Original_Amount
	, CONVERT(MONEY,tbs.Discount_Amount) AS Discount_Amount
	, CONVERT(MONEY,tbs.sales_amount) AS Sales_Amount
	, s.Shipping_Fees
	, s.Shipping_Fees_Discount
	, jsg.Tax_Amount AS gst
	, jsq.Tax_Amount AS qst
	, s.Total_Amount
FROM 
	client cl 
	LEFT OUTER JOIN Client_Address ca
		ON cl.client_sequence_code = ca.client_sequence_code
		AND cl.client_id = ca.client_id
	LEFT OUTER JOIN Country co
		ON ca.Country_Code = co.Country_Code
	LEFT OUTER JOIN Sale s
		ON cl.client_sequence_code = s.client_sequence_code
		AND cl.client_Id = s.client_Id
	LEFT OUTER JOIN Client_Sequence cs
		ON s.client_sequence_code = cs.client_sequence_code
	LEFT OUTER JOIN (
				SELECT 
					Sales_Item.Sales_ID
					, SUM(sales_item.quantity_sold * sales_item.unit_price_sold) AS Original_Amount
					, SUM(Sales_Item.Discount_Amount) AS Discount_Amount
					, SUM(Sales_Item.Sales_Amount) AS sales_amount
				FROM 
					Sales_Item
				GROUP BY 
					Sales_Item.Sales_ID
			) tbs
		ON s.sales_id = tbs.sales_ID
	LEFT OUTER JOIN (
				SELECT 
					Applicable_Tax.Sales_ID
					, Applicable_Tax.Tax_Amount
				FROM 
					Applicable_Tax
				WHERE 
					Applicable_Tax.Tax_Code ='gst'
			)jsg
		ON s.sales_ID = jsg.sales_id
	LEFT OUTER JOIN (
				SELECT 
					Applicable_Tax.Sales_ID
					, Applicable_Tax.Tax_Amount
				FROM 
					Applicable_Tax
				WHERE 
					Applicable_Tax.Tax_Code ='qst'
			)jsq
		ON s.sales_ID = jsq.Sales_ID


WHERE 
	s.Actual_Ship_Date BETWEEN @StartDate AND @EndDate
 AND	ca.Address_Type ='bt'
)

UNION

(
SELECT 
	a.Sales_ID
	, a.Adjustment_Date AS date
	, CONVERT(CHAR(2),cl.client_sequence_code) + '-' + CONVERT(CHAR(7),cl.client_id) AS client_no
	, co.Currency_Code
	, cl.Organization
	, 'Adjustment' AS type
	, r.Description AS adj_reason
	, CONVERT(MONEY,a.Adjustment_On_Sale_Amount) AS Original_Adjustment_Amount
	, CONVERT(MONEY,0) AS Expr1
	, CONVERT(MONEY,a.Adjustment_On_Sale_Amount) AS sales_amount
	, a.Adjustment_On_Shipping AS Adj_shipping
	, 0 AS Expr4
	, jag.Tax_Amount AS adj_gst
	, jaq.Tax_Amount AS adj_qst
	, a.Adjustment_Amount
FROM 
	Client cl
	INNER JOIN Client_Address ca
		ON cl.client_sequence_code = ca.client_sequence_code
		AND cl.client_ID = ca.client_ID
	INNER JOIN country co
		ON ca.country_code = co.country_code
	INNER JOIN sale s
		ON cl.client_sequence_code = s.client_sequence_code
		AND cl.client_ID = s.client_ID
	INNER JOIN adjustment a
		ON s.sales_ID = a.sales_Id
	INNER JOIN reason r
		ON a.reason_ID = r.reason_ID
	LEFT OUTER JOIN 	(
				SELECT 
					Applicable_Adjustment_Tax.Sales_Id
					, Applicable_Adjustment_Tax.Adjustement_No
					, Applicable_Adjustment_Tax.Tax_Amount
				FROM 
					Applicable_Adjustment_Tax
				WHERE 
					Applicable_Adjustment_Tax.Tax_Code='gst'
			)jag
		ON a.sales_ID = jag.sales_ID
		AND a.Adjustment_no = jag.Adjustement_No
	LEFT OUTER JOIN	(
				SELECT 
					Applicable_Adjustment_Tax.Sales_Id
					, Applicable_Adjustment_Tax.Adjustement_No
					, Applicable_Adjustment_Tax.Tax_Amount
				FROM 
					Applicable_Adjustment_Tax
				WHERE 
					Applicable_Adjustment_Tax.Tax_Code='qst'
			)jaq
		ON a.sales_ID = jaq.sales_ID
		AND a.Adjustment_no = jaq.Adjustement_No
WHERE 
	a.Adjustment_Date BETWEEN @StartDate And @EndDate 
 AND 	ca.Address_Type = 'bt'
)

UNION

(

SELECT 
	s.Sales_ID
	, s.Box_Return_Date AS date
	, CONVERT(CHAR(2),s.client_sequence_code) + '-' + CONVERT(CHAR(7),s.client_id) AS Client_No
	, co.Currency_Code
	, cl.Organization
	, 'Box Return' AS type
	, cs.description AS Sequence
	, CONVERT(MONEY,tbs.Original_Amount)
	, CONVERT(MONEY,tbs.Discount_Amount)
	, CONVERT(MONEY,tbs.sales_amount)
	, s.Shipping_Fees
	, s.Shipping_Fees_Discount
	, jsg.Tax_Amount AS gst
	, jsq.Tax_Amount AS qst
	, s.Total_Amount
FROM 
	client cl
	LEFT OUTER JOIN sale s
		ON cl.client_sequence_code = s.client_sequence_code
		AND cl.client_ID = s.client_ID
	LEFT OUTER JOIN client_address ca
		ON cl.client_sequence_code = ca.client_sequence_code
		AND cl.client_ID = ca.client_ID
	LEFT OUTER JOIN country co
		ON ca.country_code = co.country_code
	LEFT OUTER JOIN Client_Sequence cs
		ON s.Client_Sequence_Code = cs.Client_Sequence_Code
	LEFT OUTER JOIN	(
				SELECT 
					Applicable_Tax.Sales_ID
					, Applicable_Tax.Tax_Amount
				FROM 
					Applicable_Tax
				WHERE 
					Applicable_Tax.Tax_Code ='qst'
			)jsq
		ON s.Sales_ID = jsq.Sales_ID
	LEFT OUTER JOIN (
				SELECT 
					Applicable_Tax.Sales_ID
					, Applicable_Tax.Tax_Amount
				FROM 
					Applicable_Tax
				WHERE 
					Applicable_Tax.Tax_Code ='gst'
			)jsg
		ON s.Sales_ID = jsg.Sales_ID
	LEFT OUTER JOIN (
				SELECT 
					Sales_Item.Sales_ID
					, SUM(sales_item.quantity_sold * sales_item.unit_price_sold) AS Original_Amount
					, SUM(Sales_Item.Discount_Amount) AS Discount_Amount
					, SUM(Sales_Item.Sales_Amount) AS sales_amount
				FROM 
					Sales_Item
				GROUP BY 
					Sales_Item.Sales_ID
			) tbs
		ON s.Sales_Id = tbs.Sales_ID
	

WHERE 
	s.Box_Return_Date BETWEEN @StartDate AND @EndDate 
	AND ca.Address_Type ='bt'
)

UNION

(

SELECT 
	s.Sales_ID
	, s.Reship_Date AS date
	, CONVERT(CHAR(2),s.client_sequence_code) + '-' + CONVERT(CHAR(7),s.client_id) AS Client_No
	, co.Currency_Code
	, cl.Organization
	, 'Box Reship' AS type
	, cs.description AS Sequence
	, CONVERT(MONEY,tbs.Original_Amount)
	, CONVERT(MONEY,tbs.Discount_Amount)
	, CONVERT(MONEY,tbs.sales_amount)
	, s.Shipping_Fees
	, s.Shipping_Fees_Discount
	, jsg.Tax_Amount AS gst
	, jsq.Tax_Amount AS qst
	, s.Total_Amount
FROM 
	client cl
	LEFT OUTER JOIN sale s
		ON cl.client_sequence_code = s.client_sequence_code
		AND cl.client_ID = s.client_ID
	LEFT OUTER JOIN client_address ca
		ON cl.client_sequence_code = ca.client_sequence_code
		AND cl.client_ID = ca.client_ID
	LEFT OUTER JOIN country co
		ON ca.country_code = co.country_code
	LEFT OUTER JOIN Client_Sequence cs
		ON s.Client_Sequence_Code = cs.Client_Sequence_Code
	LEFT OUTER JOIN	(
				SELECT 
					Applicable_Tax.Sales_ID
					, Applicable_Tax.Tax_Amount
				FROM 
					Applicable_Tax
				WHERE 
					Applicable_Tax.Tax_Code = 'qst'
			)jsq
		ON s.Sales_ID = jsq.Sales_ID
	LEFT OUTER JOIN (
				SELECT 
					Applicable_Tax.Sales_ID
					, Applicable_Tax.Tax_Amount
				FROM 
					Applicable_Tax
				WHERE 
					Applicable_Tax.Tax_Code = 'gst'
			)jsg
		ON s.Sales_ID = jsg.Sales_ID
	LEFT OUTER JOIN (
				SELECT 
					Sales_Item.Sales_ID
					, SUM(sales_item.quantity_sold * sales_item.unit_price_sold) AS Original_Amount
					, SUM(Sales_Item.Discount_Amount) AS Discount_Amount
					, SUM(Sales_Item.Sales_Amount) AS sales_amount
				FROM 
					Sales_Item
				GROUP BY 
					Sales_Item.Sales_ID
			) tbs
		ON s.Sales_Id = tbs.Sales_ID
	

WHERE 
	s.Reship_Date BETWEEN @StartDate AND @EndDate
	AND ca.Address_Type ='bt'

)
GO
