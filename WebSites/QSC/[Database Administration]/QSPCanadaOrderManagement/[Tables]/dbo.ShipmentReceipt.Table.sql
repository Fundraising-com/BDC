USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[ShipmentReceipt]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShipmentReceipt](
	[ShipmentReceiptID] [int] IDENTITY(1,1) NOT NULL,
	[ShipmentReceiptBatchID] [int] NOT NULL,
	[StatusID] [int] NOT NULL,
	[BatchOrderID] [int] NOT NULL,
	[CustomerOrderHeaderInstance] [int] NOT NULL,
	[TransID] [int] NOT NULL,
	[ProductCode] [nvarchar](255) NOT NULL,
	[QtyShipped] [int] NOT NULL,
	[Courier] [nvarchar](255) NULL,
	[NumBoxes] [int] NOT NULL,
	[Weight] [decimal](16, 6) NULL,
	[TrackingNumber] [nvarchar](255) NULL,
	[ProcessDate] [datetime] NULL,
	[ShipDate] [datetime] NULL,
	[CreateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_ShipmentReceipt] PRIMARY KEY CLUSTERED 
(
	[ShipmentReceiptID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ShipmentReceipt]  WITH CHECK ADD  CONSTRAINT [FK_ShipmentReceipt_ShipmentReceiptBatch] FOREIGN KEY([ShipmentReceiptBatchID])
REFERENCES [dbo].[ShipmentReceiptBatch] ([ShipmentReceiptBatchID])
GO
ALTER TABLE [dbo].[ShipmentReceipt] CHECK CONSTRAINT [FK_ShipmentReceipt_ShipmentReceiptBatch]
GO
ALTER TABLE [dbo].[ShipmentReceipt]  WITH CHECK ADD  CONSTRAINT [FK_ShipmentReceipt_ShipmentReceiptStatus] FOREIGN KEY([StatusID])
REFERENCES [dbo].[ShipmentReceiptStatus] ([ShipmentReceiptStatusID])
GO
ALTER TABLE [dbo].[ShipmentReceipt] CHECK CONSTRAINT [FK_ShipmentReceipt_ShipmentReceiptStatus]
GO
ALTER TABLE [dbo].[ShipmentReceipt] ADD  CONSTRAINT [DF_ShipmentReceipt_StatusID]  DEFAULT ((1)) FOR [StatusID]
GO
ALTER TABLE [dbo].[ShipmentReceipt] ADD  CONSTRAINT [DF_ShipmentReceipt_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
