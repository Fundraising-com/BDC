USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[get_partner_payment_info]    Script Date: 02/14/2014 13:08:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[get_partner_payment_info]
	 @dteBeginDate as datetime
	, @dteEndDate as datetime
		--@VarBeginDate as varchar(100) = null
		--, @VarEndDate as varchar(100) = null
as 
/*
declare @dteBeginDate as datetime
declare @dteEndDate as datetime

set @dteBeginDate = cast(@VarBeginDate as datetime)
set @dteEndDate = cast (@VarEndDate as datetime)*/


SET @dteEndDate = CONVERT( DATETIME, CONVERT( CHAR(10),  @dteEndDate, 110 ) + ' 23:59:59' )

SELECT 
	p.Partner_Name
	, Sum( pay.Payment_Amount ) AS Total_Paid
	, s.Sales_ID
	, l.Lead_ID
	, c.Organization
	, pcs.[Description]
	, c.Salutation
	, c.First_Name
	, c.Last_name
	, Payment_Entry_Date
FROM 
	dbo.Promotion 
	INNER JOIN dbo.Partner p
		ON dbo.Promotion.Partner_ID = p.Partner_ID
	INNER JOIN dbo.Client c 
		INNER JOIN dbo.Payment pay 
			INNER JOIN dbo.Sale s 
				ON pay.Sales_ID = s.Sales_ID
			ON c.Client_Sequence_Code = s.Client_Sequence_Code 
			AND c.Client_ID = s.Client_ID
		INNER JOIN dbo.Lead l
			 ON c.Lead_ID = l.Lead_ID 
		ON dbo.Promotion.Promotion_ID = l.Promotion_ID
	INNER JOIN (
		SELECT 
			s.Sales_ID 
			, pc.product_class_ID
			, pc.[Description]
		FROM 
			dbo.Sale s 
			INNER JOIN dbo.Sales_Item si
				ON s.Sales_ID = si.Sales_ID 
			INNER JOIN dbo.Scratch_Book sb 
				ON si.Scratch_Book_ID = sb.Scratch_Book_ID
			INNER JOIN Product_class pc 
				ON sb.product_class_ID = pc.product_class_Id
		GROUP BY 
			pc.product_class_id
			, s.Sales_ID
			, pc.[Description]
	) pcs 
		ON s.Sales_ID = pcs.Sales_ID
WHERE 
	dbo.Promotion.Partner_ID =40
	AND pay.Payment_Entry_Date BETWEEN @dteBeginDate AND @dteEndDate
GROUP BY 
	p.Partner_Name
	, s.sales_id
	, l.Lead_ID
	, Payment_Entry_Date
	, s.Total_Amount
	, c.Salutation
	, c.First_Name
	, c.Last_name
	, c.Organization
	, pcs.[Description]
GO
