USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[vw_BeFree]    Script Date: 02/14/2014 13:02:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_BeFree]
AS
SELECT
	RTRIM( Merchant_ID ) + 
	CHAR( 9 ) + 
	Record_Type + 
	CHAR( 9 ) + 
	REPLACE( STR( DATEPART( month, Date_Insert ), 2 ), ' ', '0' ) + 
	REPLACE( STR( DATEPART( day, Date_Insert ), 2 ), ' ', '0' ) + 
	STR( DATEPART( year, Date_Insert ), 4 ) + 
	' ' + 
	REPLACE( STR( DATEPART( hour, Date_Insert ), 2 ), ' ', '0') + 
	':' + 
	REPLACE( STR( DATEPART( minute, Date_Insert ), 2 ), ' ', '0' ) + 
	':' + 
	REPLACE( STR( DATEPART( second, Date_Insert ), 2 ), ' ', '0' ) + 
	CHAR( 9 ) + 
	RTRIM( Source_ID ) + 
	CHAR( 9 ) + 
	RTRIM( Transaction_ID ) + 
	CHAR( 9 ) + 
	RTRIM( Product_Key ) + 
	CHAR( 9 ) + 
	LTRIM( STR( Qty_Product, 5, 0 ) ) + 
	CHAR( 9 ) + 
	LTRIM( STR( Unit_Price, 11, 2 ) ) + 
	CHAR( 9 ) + 
	Currency_Type + 
	CHAR( 9 ) + 
	RTRIM( Merchandise_Type ) +
	CHAR( 13 ) AS BeFree 
FROM 	
	dbo.BeFree
GO
