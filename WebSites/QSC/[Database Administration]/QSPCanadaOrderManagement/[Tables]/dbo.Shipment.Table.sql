USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[Shipment]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Shipment](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CarrierID] [int] NULL,
	[ShipmentDate] [datetime] NULL,
	[CountryCode] [varchar](10) NULL,
	[ExpectedDeliveryDate] [datetime] NULL,
	[NumberBoxes] [int] NULL,
	[Weight] [numeric](10, 2) NULL,
	[DateModified] [datetime] NULL,
	[UserIDModified] [dbo].[UserID_UDDT] NULL,
	[OperatorName] [varchar](50) NULL,
	[NumberSkids] [int] NULL,
	[WeightUnitOfMeasure] [varchar](20) NULL,
	[Comment] [varchar](256) NULL,
	[FMEmailNotificationSent] [datetime] NULL,
	[SentToSAP] [datetime] NULL,
	[ShipmentGroupID] [int] NULL,
 CONSTRAINT [PK_Shipment] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Shipment]  WITH CHECK ADD  CONSTRAINT [FK_Shipment_ShipmentGroup] FOREIGN KEY([ShipmentGroupID])
REFERENCES [dbo].[ShipmentGroup] ([ShipmentGroupID])
GO
ALTER TABLE [dbo].[Shipment] CHECK CONSTRAINT [FK_Shipment_ShipmentGroup]
GO
ALTER TABLE [dbo].[Shipment] ADD  CONSTRAINT [DF_Shipment_FMEmailNotificationSent]  DEFAULT ('1/1/95') FOR [FMEmailNotificationSent]
GO
