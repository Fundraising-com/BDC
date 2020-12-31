USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[ShipmentRequestOrder]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ShipmentRequestOrder](
	[ShipmentRequestOrderID] [int] IDENTITY(1,1) NOT NULL,
	[ShipmentRequestBatchID] [int] NOT NULL,
	[OrderID] [int] NOT NULL,
	[OrderType] [varchar](200) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[PDFFilename] [varchar](200) NULL,
	[CourierRequest] [varchar](200) NOT NULL,
	[ServiceRequest] [varchar](200) NOT NULL,
	[RequestedShipDate] [datetime] NOT NULL,
 CONSTRAINT [PK_ShipmentRequestOrder] PRIMARY KEY CLUSTERED 
(
	[ShipmentRequestOrderID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[ShipmentRequestOrder]  WITH CHECK ADD  CONSTRAINT [FK_ShipmentRequestOrder_ShipmentRequestBatch] FOREIGN KEY([ShipmentRequestBatchID])
REFERENCES [dbo].[ShipmentRequestBatch] ([ShipmentRequestBatchID])
GO
ALTER TABLE [dbo].[ShipmentRequestOrder] CHECK CONSTRAINT [FK_ShipmentRequestOrder_ShipmentRequestBatch]
GO
ALTER TABLE [dbo].[ShipmentRequestOrder] ADD  CONSTRAINT [DF_ShipmentRequest_Batch_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
