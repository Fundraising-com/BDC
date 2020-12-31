USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_Get_Order_ReadyForShipment]    Script Date: 06/07/2017 09:19:58 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_Get_Order_ReadyForShipment]

@OrderId int

AS


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[#TempShip]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[#TempShip]

CREATE TABLE [dbo].[#TempShip] (
	[OrderId] [int] NOT NULL ,
	[ShipToGroupId] [int] NULL ,
	[CampaignId] [int] NOT NULL ,
	[For] [varchar] (500) NULL ,
	[ShipToFMId] [varchar](4) NULL ,
	[IsSplit] [bit] NULL
) ON [PRIMARY]



INSERT INTO
	#TempShip
SELECT
	A.OrderId
	, A.ShipToAccountId
	, A.CampaignId
	, ''
	, A.ShipToFMID
	, 0
FROM
	Batch A
WHERE
	A.OrderId = @OrderId



--- UPDATE THE For Field with FMs Information
UPDATE
	#TempShip
SET
	[For] = 'FM: ' + A.ShipToFMId + ' ' + B.FirstName + ' ' + B.LastName
FROM
	#TempShip A
	INNER JOIN QSPCanadaCommon..FieldManager B ON B.FMID = A.ShipToFMID
WHERE
	A.ShipToFMId IS NOT NULL


--- UPDATE THE For Field with Account Information
UPDATE
	#TempShip
SET
	[For] = 'GROUP: ' + B.Name
FROM
	#TempShip A
	INNER JOIN QSPCanadaOrderManagement..Account B ON B.ID = A.ShipToGroupID
WHERE
	A.ShipToGroupId IS NOT NULL


SELECT
	*	
FROM	
	#TempShip
GO
