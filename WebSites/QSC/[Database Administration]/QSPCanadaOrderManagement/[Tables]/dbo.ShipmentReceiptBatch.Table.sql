USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[ShipmentReceiptBatch]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShipmentReceiptBatch](
	[ShipmentReceiptBatchID] [int] IDENTITY(1,1) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[FileName] [nvarchar](137) NULL,
 CONSTRAINT [PK_ShipmentReceiptBatch] PRIMARY KEY CLUSTERED 
(
	[ShipmentReceiptBatchID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ShipmentReceiptBatch] ADD  CONSTRAINT [DF_ShipmentReceiptBatch_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
