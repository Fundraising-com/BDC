USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[get_activity_category]    Script Date: 02/14/2014 13:06:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[get_activity_category]
	
AS
SELECT 
	Country_Code
	, Country_Name 
FROM 	Country
ORDER BY 
	Country_Name  ASC
GO
