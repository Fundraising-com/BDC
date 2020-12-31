USE [QSPCanadaCommon]
GO
/****** Object:  UserDefinedFunction [dbo].[FNC_GetCampaignShipToProvince]    Script Date: 06/07/2017 09:33:36 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE FUNCTION [dbo].[FNC_GetCampaignShipToProvince] (@CampaignID int)  
RETURNS VarChar(2) AS  
BEGIN 


Declare @p varchar(2)

SELECT
	@p=C.StateProvince
	
FROM
	QSPCanadaCommon..Campaign CA,
	QSPCanadaCommon..CAccount A
	INNER JOIN QSPCanadaCommon..AddressList B ON A.AddressListId = B.Id
	INNER JOIN QSPCanadaCommon..Address C ON C.AddressListId = B.Id
WHERE
	C.Address_Type = 54001 		--- SHIP TO Address Type
	AND CA.ShipToAccountID=A.id
	AND CA.Id = @CampaignID
 return @p
END
GO
