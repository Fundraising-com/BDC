USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[ShipmentOrder]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShipmentOrder](
	[ShipmentID] [int] NOT NULL,
	[OrderID] [int] NOT NULL,
	[DistributionCenterID] [int] NULL,
	[ShipmentBatchID] [int] NULL,
	[IsShipmentBatchCreated] [bit] NULL,
 CONSTRAINT [PK_ShipmentOrder] PRIMARY KEY CLUSTERED 
(
	[ShipmentID] ASC,
	[OrderID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
