USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_efundscard_new_orders]    Script Date: 02/14/2014 13:04:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[efrcrm_get_efundscard_new_orders]

AS

SELECT		DISTINCT
			c.Lead_ID
FROM		Sale s
JOIN		Sales_Item si
				ON	si.Sales_ID = s.Sales_ID
JOIN		Scratch_book sb
				ON	sb.Scratch_book_ID = si.Scratch_book_ID
JOIN		Client c
				ON	c.Client_ID = s.Client_ID
				AND	c.Client_Sequence_Code = s.Client_Sequence_Code 
LEFT JOIN	esubs_global_v2..[Group] g
				ON	g.Lead_ID = c.Lead_ID
WHERE		sb.Product_Code = '33863' --eFunds Card
AND			g.Group_ID IS NULL
AND			s.Sales_Status_ID = 2 --2: Confirmed
GO
