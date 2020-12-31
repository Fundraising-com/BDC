USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_Publishers_SelectAll]    Script Date: 06/07/2017 09:17:59 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_Publishers_SelectAll] 

AS

SELECT 	pub_nbr, 
		pub_name 
FROM 		Publishers 
WHERE 	pub_status='ACTIVE' 
ORDER BY 	pub_name
GO
