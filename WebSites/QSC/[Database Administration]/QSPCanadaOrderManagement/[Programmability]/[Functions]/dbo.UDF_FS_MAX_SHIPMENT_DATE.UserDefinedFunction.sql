USE [QSPCanadaOrderManagement]
GO
/****** Object:  UserDefinedFunction [dbo].[UDF_FS_MAX_SHIPMENT_DATE]    Script Date: 06/07/2017 09:21:02 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE FUNCTION [dbo].[UDF_FS_MAX_SHIPMENT_DATE] (@CampaignId int )

RETURNS DATETIME  AS  
BEGIN 

  DECLARE  @ShipDate DATETIME

	SELECT @ShipDate = MAX(S.ShipmentDate)
	FROM QSPCanadaOrderManagement.dbo.Batch B INNER JOIN
	           QSPCanadaOrderManagement.dbo.CustomerOrderHeader H ON B.ID = H.OrderBatchID AND B.[Date] = H.OrderBatchDate INNER JOIN
	           QSPCanadaOrderManagement.dbo.CustomerOrderDetail D ON H.Instance = D.CustomerOrderHeaderInstance LEFT OUTER JOIN
	           QSPCanadaOrderManagement.dbo.Shipment S ON D.ShipmentID = S.ID
	WHERE     (B.OrderQualifierID = 39007)   -- Field Supplies
	AND B.CampaignId = @CampaignId
	AND ISNULL(S.ShipmentDate,'01/01/1995') > CAST('01/01/1995' As DATETIME)  


  RETURN @ShipDate
  
END
GO
