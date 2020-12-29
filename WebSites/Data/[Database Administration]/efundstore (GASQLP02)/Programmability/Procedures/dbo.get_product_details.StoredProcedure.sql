USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[get_product_details]    Script Date: 02/14/2014 13:06:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Created By:	Paolo De Rosa
Created On:	June 9, 2004
Description:	This stored procedure returns the information for a passed product.
*/
CREATE PROCEDURE [dbo].[get_product_details]
	@intProductID INT
	, @strCultureName VARCHAR(10)
AS
SELECT
	p.scratch_book_id AS product_id
	, pc.product_class_id
	, pc.accounting_class_id
	, pc.minimum_order_qty
	, pd.product_name
	, pd.product_short_desc
	, pd.product_long_desc
	, pd.product_small_img
	, pd.product_large_img
	, pk.nb_participants_per_case
	, pp.unit_price 
	, p.raising_potential 
	, p.total_qty
	, CASE 
		WHEN pp.unit_price = 0 THEN 0 
		WHEN p.raising_potential = 0 THEN 0 
		ELSE ( p.raising_potential - pp.unit_price ) / p.raising_potential * 100
	  END AS profit_percent
FROM	scratch_book p
	INNER JOIN product_desc pd
		ON p.scratch_book_id = pd.product_id
	INNER JOIN product_class pc
		ON p.product_class_id = pc.product_class_id
	INNER JOIN products_packages ppa
		ON p.scratch_book_id = ppa.product_id
	INNER JOIN packages pk
		ON ppa.package_id = pk.package_id
	INNER JOIN (
		SELECT 
			sbpi.Country_Code
			, sbpi.Scratch_Book_ID
			, sbpi.Unit_Price
		FROM 	Scratch_Book_Price_Info sbpi
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
	INNER JOIN languages l
		ON pd.language_id = l.language_id
	INNER JOIN cultures c
		ON l.language_id = c.language_id
WHERE
	pd.product_id = @intProductID
 AND	LOWER( c.culture_name ) = LTRIM( RTRIM( LOWER( @strCultureName ) ) )
GO
