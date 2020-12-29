-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jason F
-- Create date: 2018-01-16
-- Description:	EFR - Partner Traditional Confirmed Sales by Product Class automation
-- =============================================
alter PROCEDURE [dbo].[report_fr_partner_traditional_confirmed_sales_product_class] 
	
	   @dteToday as datetime
	, @firstDayOfYear as datetime
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	
	SET NOCOUNT ON;
	 -- Insert statements for procedure here
	select sum(s.total_amount) AS sum_payment1
FROM    lead l 
	INNER JOIN client c ON l.lead_id = c.lead_id 
	INNER JOIN sale s ON c.client_sequence_code = s.client_sequence_code AND c.client_id = s.client_id 
	INNER JOIN consultant co ON co.consultant_id = s.consultant_id
	INNER JOIN promotion p ON l.promotion_id = p.promotion_id 
	INNER JOIN partner pa ON p.partner_id = pa.partner_id 
	INNER JOIN sales_item si ON s.sales_id = si.sales_id 
	INNER JOIN scratch_book sb ON si.scratch_book_id = sb.scratch_book_id 
	INNER JOIN product_class pc ON sb.product_class_id = pc.product_class_id
	INNER JOIN client_address ca ON ca.client_id = c.client_id and ca.client_sequence_code= c.client_sequence_code and ca.address_type = 'BT'
	INNER JOIN countries on countries.country_code = ca.country_code
where s.confirmed_date between @firstDayOfYear and @dteToday
and (si.sales_item_no = 1)
	
END
GO
