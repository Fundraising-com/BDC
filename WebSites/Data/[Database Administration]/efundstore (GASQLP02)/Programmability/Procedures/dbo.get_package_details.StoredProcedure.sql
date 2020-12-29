USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[get_package_details]    Script Date: 02/14/2014 13:06:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Created By:	Paolo De Rosa
Created On:	June 9, 2004
Description:	This stored procedure returns the package information for a package.
		Since a package can contain other packages, the passed package_id has 
		to be tested to verify if it is a parent or child package.
*/
CREATE PROCEDURE [dbo].[get_package_details]
	@intPackageID TINYINT
	, @strCultureName VARCHAR(10)
AS
IF (
	SELECT 
		SUM( cp.package_id )
	FROM 	packages p
		LEFT OUTER JOIN packages cp
			ON p.package_id = cp.parent_package_id 
	WHERE
		p.package_id = @intPackageID
) IS NULL
	SELECT
		 pk.package_id
		, pkd.package_name
		, pkd.package_short_desc
		, pkd.package_long_desc
		, pkd.package_extra_desc
		, pkd.package_small_img
		, pkd.package_large_img
		, pkd.page_url
		, pk.contains_products
		, pk.accounting_class_id
		, pk.nb_participants_per_case
		, MAX( 
			CASE 
				WHEN pp.unit_price = 0 THEN 0 
				WHEN p.raising_potential = 0 THEN 0 
				ELSE ( p.raising_potential - pp.unit_price ) / p.raising_potential * 100
			END 
		) AS profit_percent
	FROM	packages pk
		INNER JOIN package_desc pkd
			ON pk.package_id = pkd.package_id 
		INNER JOIN products_packages ppk
			ON pk.package_id = ppk.package_id
		INNER JOIN scratch_book p
			ON ppk.product_id = p.scratch_book_id
		INNER JOIN product_class pc
			ON p.product_class_id = pc.product_class_id 
		INNER JOIN languages l
			ON pkd.language_id = l.language_id
		INNER JOIN cultures c
			ON l.language_id = c.language_id
		LEFT OUTER JOIN (
			SELECT 
				sbpi.Country_Code
				, sbpi.Scratch_Book_ID
				, sbpi.Unit_Price
				, sbpi.Product_Class_ID
			FROM 	Scratch_Book_Price_Info sbpi
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
		pk.package_id = @intPackageID
	 AND	LOWER( c.culture_name ) = LTRIM( RTRIM( LOWER( @strCultureName ) ) )
	GROUP BY
		pk.package_id
		, pkd.package_name
		, pkd.package_short_desc
		, pkd.package_long_desc
		, pkd.package_extra_desc
		, pkd.package_small_img
		, pkd.package_large_img
		, pkd.page_url
		, pk.contains_products
		, pk.accounting_class_id
		, pk.nb_participants_per_case
ELSE
	IF (
		SELECT 
			SUM( cp.package_id )
		FROM 	packages gp
			INNER JOIN packages p
				ON gp.package_id = p.parent_package_id 
			LEFT OUTER JOIN packages cp
				ON p.package_id = cp.parent_package_id 
		WHERE
			gp.package_id = @intPackageID
	) IS NULL
		SELECT
			pk.package_id
			, pkd.package_name
			, pkd.package_short_desc
			, pkd.package_long_desc
			, pkd.package_extra_desc
			, pkd.package_small_img
			, pkd.package_large_img
			, pkd.page_url
			, pk.contains_products
			, pk.accounting_class_id
			, pk.nb_participants_per_case
			, MAX( 
				CASE 
					WHEN pp.unit_price = 0 THEN 0 
					WHEN p.raising_potential = 0 THEN 0 
					ELSE ( p.raising_potential - pp.unit_price ) / p.raising_potential * 100
				END 
			) AS profit_percent
		FROM	packages pk
			INNER JOIN package_desc pkd
				ON pk.package_id = pkd.package_id 
			INNER JOIN packages cpk
				ON pk.package_id = cpk.parent_package_id 
			INNER JOIN products_packages ppk
				ON cpk.package_id = ppk.package_id
			INNER JOIN scratch_book p
				ON ppk.product_id = p.scratch_book_id
			INNER JOIN product_class pc
				ON p.product_class_id = pc.product_class_id 
			INNER JOIN languages l
				ON pkd.language_id = l.language_id
			INNER JOIN cultures c
				ON l.language_id = c.language_id
			LEFT OUTER JOIN (
				SELECT 
					sbpi.Country_Code
					, sbpi.Scratch_Book_ID
					, sbpi.Unit_Price
					, sbpi.Product_Class_ID
				FROM 	Scratch_Book_Price_Info sbpi
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
			pk.package_id = @intPackageID
		 AND	LOWER( c.culture_name ) = LTRIM( RTRIM( LOWER( @strCultureName ) ) )
		GROUP BY
			pk.package_id
			, pkd.package_name
			, pkd.package_short_desc
			, pkd.package_long_desc
			, pkd.package_extra_desc
			, pkd.package_small_img
			, pkd.package_large_img
			, pkd.page_url
			, pk.contains_products
			, pk.accounting_class_id
			, pk.nb_participants_per_case
	ELSE
		SELECT
			gpk.package_id
			, gpkd.package_name
			, gpkd.package_short_desc
			, gpkd.package_long_desc
			, gpkd.package_extra_desc
			, gpkd.package_small_img
			, gpkd.package_large_img
			, gpk.contains_products
			, gpk.accounting_class_id
			, gpk.nb_participants_per_case
			, MAX( 
				CASE 
					WHEN pp.unit_price = 0 THEN 0 
					WHEN p.raising_potential = 0 THEN 0 
					ELSE ( p.raising_potential - pp.unit_price ) / p.raising_potential * 100
				END 
			) AS profit_percent
		FROM	packages gpk
			INNER JOIN package_desc gpkd
				ON gpk.package_id = gpkd.package_id 
			INNER JOIN packages pk
				ON gpk.package_id = pk.parent_package_id 
			INNER JOIN packages cpk
				ON pk.package_id = cpk.parent_package_id 
			INNER JOIN products_packages ppk
				ON cpk.package_id = ppk.package_id
			INNER JOIN scratch_book p
				ON ppk.product_id = p.scratch_book_id
			INNER JOIN product_class pc
				ON p.product_class_id = pc.product_class_id 
			INNER JOIN languages l
				ON gpkd.language_id = l.language_id
			INNER JOIN cultures c
				ON l.language_id = c.language_id
			LEFT OUTER JOIN (
				SELECT 
					sbpi.Country_Code
					, sbpi.Scratch_Book_ID
					, sbpi.Unit_Price
					, sbpi.Product_Class_ID
				FROM 	Scratch_Book_Price_Info sbpi
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
			gpk.package_id = @intPackageID
		 AND	LOWER( c.culture_name ) = LTRIM( RTRIM( LOWER( @strCultureName ) ) )
		GROUP BY
			gpk.package_id
			, gpkd.package_name
			, gpkd.package_short_desc
			, gpkd.package_long_desc
			, gpkd.package_extra_desc
			, gpkd.package_small_img
			, gpkd.package_large_img
			, gpk.contains_products
			, gpk.accounting_class_id
			, gpk.nb_participants_per_case
GO
