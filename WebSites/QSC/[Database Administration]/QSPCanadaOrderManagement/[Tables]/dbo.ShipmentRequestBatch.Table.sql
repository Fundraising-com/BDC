USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[ShipmentRequestBatch]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShipmentRequestBatch](
	[ShipmentRequestBatchID] [int] IDENTITY(1,1) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[Filename] [nvarchar](100) NULL,
 CONSTRAINT [PK_ShipmentRequestBatch] PRIMARY KEY CLUSTERED 
(
	[ShipmentRequestBatchID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ShipmentRequestBatch] ADD  CONSTRAINT [DF_ShipmentRequestBatch_CreationDate]  DEFAULT (getdate()) FOR [CreationDate]
GO
