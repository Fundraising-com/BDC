USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efr_rpt_partner_sales_commission]    Script Date: 02/14/2014 13:03:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
-- =============================================
-- Author: Stephen Lim
-- Create Date: 2009-11-30
-- Description:	Partner sales commission report
--
-- Variable rate commission is calculated against the *received* payment and payment date 
-- including refunds (negative commission) that may occur inside the payment. 
-- 
-- A note about EFR payment system. Payments include shipping fees, taxes and refunds. Historically, 
-- commission has always been calculated against payment received instead of individual sales before taxes & shipping.
-- This is true for FC commission and partner commission calculation.
-- 
-- Unfortunately, EFR does not keep track of the type of refund (shipping refund, ad-hoc refund or product refund)
-- and as such, we're not able to calculate precise negative commission due to refunds. However, since refunds seldom occur, 
-- we can choose to approximate or ignore it completely.
-- 
-- =============================================
CREATE PROCEDURE [dbo].[efr_rpt_partner_sales_commission] 
	@partner_id int,
	@start_date datetime,
	@end_date datetime
AS
BEGIN

	-- Force clock to end of day
	SET @end_date = convert(varchar(30), @end_date, 101) + ' 23:59:59'

	-- Print header
	SELECT 
		'YEAR',
		'MONTH',
		'PRODUCT',
		'+UNITS',
		'$SALES AMOUNT',
		'CURRENCY',
		'$TOTAL COMMISSION'

	-- Print data
	SELECT 
		YEAR(pay.payment_entry_date) AS payment_year, 
		DATENAME(MONTH, pay.payment_entry_date) AS payment_month,
		pc.description,
		SUM(si.quantity_sold) AS qty,
		SUM(pay.payment_amount) AS sum_payment,
		countries.currency_code,
		SUM(pay.payment_amount * psc.variable_rate) as commission
	FROM lead l 
		INNER JOIN client c ON l.lead_id = c.lead_id 
		INNER JOIN sale s ON c.client_sequence_code = s.client_sequence_code AND c.client_id = s.client_id 
		INNER JOIN payment pay ON s.sales_id = pay.sales_id AND pay.payment_entry_date BETWEEN @start_date AND @end_date
		INNER JOIN promotion p ON l.promotion_id = p.promotion_id 
		INNER JOIN partner pa ON p.partner_id = pa.partner_id AND 
					-- HACK: Merge Fundsnet (147) and fundsnet (107) in condition until data is merged.
					(p.partner_id = @partner_id OR p.partner_id = CASE WHEN @partner_id = 147 THEN 107 ELSE NULL END)

		-- In EFR, every sale order can only belong to one product class. Product classes cannot be mixed per sale.
		-- The following join logic is taken from efr_rpt_partner_payment_report proc to get a single product_class for the sale.
		INNER JOIN (
						SELECT DISTINCT sales_id, MIN(sales_item_no) AS sales_item_no 
						FROM sales_item 
						GROUP BY sales_id
					) si1 ON s.sales_id = si1.sales_id 
		INNER JOIN sales_item si ON si1.sales_id = si.sales_id AND si1.sales_item_no = si.sales_item_no

		INNER JOIN scratch_book sb ON si.scratch_book_id = sb.scratch_book_id 
		INNER JOIN product_class pc ON sb.product_class_id = pc.product_class_id
		INNER JOIN client_address ca ON ca.client_id = c.client_id AND ca.client_sequence_code= c.client_sequence_code AND ca.address_type = 'BT'
		INNER JOIN countries on countries.country_code = ca.country_code

		-- Get the earliest matching commission rate
		INNER JOIN Partner_Sales_Commission psc ON psc.Partner_Sales_Commission_ID = 
			(
				SELECT TOP 1 Partner_Sales_Commission_ID 
				FROM Partner_Sales_Commission 
				WHERE Partner_ID = @partner_id AND Product_Class_ID = pc.product_class_id AND 
					Effective_Date <= pay.payment_entry_date AND Active = 1 
				ORDER BY Effective_Date DESC
			)
	GROUP BY 
		YEAR(pay.payment_entry_date),
		MONTH(pay.payment_entry_date),
		DATENAME(MONTH, pay.payment_entry_date),
		pc.description,
		countries.currency_code
	ORDER BY YEAR(pay.payment_entry_date), MONTH(pay.payment_entry_date), pc.description

END
GO
