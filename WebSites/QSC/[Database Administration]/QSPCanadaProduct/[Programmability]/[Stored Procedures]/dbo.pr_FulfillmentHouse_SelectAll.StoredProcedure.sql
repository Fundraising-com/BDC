USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_FulfillmentHouse_SelectAll]    Script Date: 06/07/2017 09:17:52 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_FulfillmentHouse_SelectAll] 

AS

SELECT 	ful_nbr, 
		ful_name 
FROM 		Fulfillment_House 
WHERE 	ful_status='ACTIVE' 
ORDER BY 	ful_name
GO
