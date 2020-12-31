USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_publisher_SelectAll]    Script Date: 06/07/2017 09:20:21 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_publisher_SelectAll]
	@iFulfillmentHouseID int = 0
 AS


if(@iFulfillmentHouseID <> 0)
BEGIN

SELECT distinct pub.*  FROM QSPCanadaProduct..PUBLISHERS pub,QSPCanadaProduct..product p

	where
	
		p.Fulfill_House_Nbr = @iFulfillmentHouseID
	and	p.pub_nbr = pub.pub_nbr
	ORDER BY pub.Pub_Name
	
END
else
BEGIN

	
	SELECT *  FROM QSPCanadaProduct.dbo.PUBLISHERS pub
	ORDER BY pub.Pub_Name
	
END
GO
