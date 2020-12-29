USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[get_cart_items]    Script Date: 02/14/2014 13:06:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Created By:	JF Lavigne
Created On:	May 18, 2004
Description:	This stored procedure get the information of every product in the cart
*/
CREATE PROCEDURE [dbo].[get_cart_items] 
	@intCartID INT
	, @strCultureName VARCHAR(10)
AS
SELECT
	i.shopping_cart_id
	, i.scratch_book_id
	, CASE 
		WHEN pc.product_class_id = 1 THEN pcd.product_class_desc + ' - ' + pd.product_name
		WHEN pc.product_class_id = 14 THEN pd.product_name + ' - ' + pcd.product_class_desc
		ELSE pd.product_name 
	  END AS product_name 
	, i.quantity
	, i.carrier_id
	, i.shipping_option_id
	, pp.Unit_Price
	, pp.Unit_Price * i.quantity AS SubTotal
	, ac.delivery_days
	, ac.accounting_class_id
FROM
	dbo.scratch_book p
	INNER JOIN dbo.product_desc pd
		ON p.scratch_book_id = pd.product_id 
	INNER JOIN dbo.product_class pc 
		ON p.product_class_id = pc.product_class_id 
	INNER JOIN dbo.product_class_desc pcd 
		ON p.product_class_id = pcd.product_class_id 
	INNER JOIN dbo.accounting_class ac 
		ON pc.accounting_class_id = ac.accounting_class_id
	INNER JOIN dbo.shopping_cart_items i 
		ON p.scratch_book_id = i.scratch_book_id 
	INNER JOIN languages l
		ON pd.language_id = l.language_id
		 AND pcd.language_id = l.language_id
	INNER JOIN cultures c
		ON l.language_id = c.language_id
	INNER JOIN (
		SELECT 
			sbpi.Country_Code
			, sbpi.Scratch_Book_ID
			, sbpi.Unit_Price
			, sbpi.Product_Class_ID
		FROM 	efundstore..Scratch_Book_Price_Info sbpi
			INNER JOIN (
				SELECT
					Country_Code
					, Scratch_Book_ID
					, MAX( Effective_Date ) AS Effective_Date
				FROM	dbo.Scratch_Book_Price_Info
				GROUP BY
					Country_Code
					, Scratch_Book_ID
			) p
				ON sbpi.Country_Code = p.Country_Code
				 AND sbpi.Scratch_Book_ID = p.Scratch_Book_ID
				 AND sbpi.Effective_Date = p.Effective_Date
	) pp 
		ON p.Scratch_Book_ID = pp.Scratch_Book_ID 
		 AND pc.product_class_id = pp.Product_Class_ID 
		 AND c.Country_Code = pp.Country_Code 
WHERE 
	shopping_cart_id = @intCartID
 AND	LOWER( c.culture_name ) = LTRIM( RTRIM( LOWER( @strCultureName ) ) )

/*
SELECT
	i.shopping_cart_id
	, i.scratch_book_id
	, CASE 
		WHEN pc.product_class_id = 1 THEN pc.[description] + ' - ' + pd.product_name
		WHEN pc.product_class_id = 14 THEN pd.product_name + ' - ' + pc.[description]
		ELSE pd.product_name 
	  END AS product_name 
	, i.quantity
	, i.carrier_id
	, i.shipping_option_id
	, pp.Unit_Price
	, pp.Unit_Price * i.quantity AS SubTotal
	, ac.delivery_days
	, ac.accounting_class_id
FROM
	dbo.scratch_book p
	INNER JOIN dbo.product_desc pd
		ON p.scratch_book_id = pd.product_id 
	INNER JOIN dbo.shopping_cart_items i 
		ON p.scratch_book_id = i.scratch_book_id 
	INNER JOIN languages l
		ON pd.language_id = l.language_id
	INNER JOIN cultures c
		ON l.language_id = c.language_id
	INNER JOIN (
		SELECT 
			sbpi.Country_Code
			, sbpi.Scratch_Book_ID
			, sbpi.Unit_Price
		FROM 	dbo.Scratch_Book_Price_Info sbpi
			INNER JOIN cultures c
				ON sbpi.country_code = c.country_code 
			INNER JOIN (
				SELECT
					Country_Code
					, Scratch_Book_ID
					, MAX( Effective_Date ) AS Effective_Date
				FROM	dbo.Scratch_Book_Price_Info
				GROUP BY
					Country_Code
					, Scratch_Book_ID
		) p
				ON sbpi.Country_Code = p.Country_Code
				 AND sbpi.Scratch_Book_ID = p.Scratch_Book_ID
				 AND sbpi.Effective_Date = p.Effective_Date
		WHERE 
			LOWER( c.culture_name ) = LTRIM( RTRIM( LOWER( @strCultureName ) ) )
	) pp 
		ON p.Scratch_Book_ID = pp.Scratch_Book_ID 
	INNER JOIN dbo.product_class pc 
		ON p.product_class_id = pc.product_class_id 
	INNER JOIN dbo.accounting_class ac 
		ON pc.accounting_class_id = ac.accounting_class_id
WHERE 
	shopping_cart_id = @intCartID
 AND	LOWER( c.culture_name ) = LTRIM( RTRIM( LOWER( @strCultureName ) ) )
*/
GO
