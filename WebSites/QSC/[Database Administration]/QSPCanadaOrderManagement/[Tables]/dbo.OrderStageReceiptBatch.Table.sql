USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[OrderStageReceiptBatch]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderStageReceiptBatch](
	[OrderStageReceiptBatchID] [int] IDENTITY(1,1) NOT NULL,
	[SnapshotDate] [datetime] NULL,
	[TransmissionSequenceID] [int] NULL,
	[TotalReceived] [int] NULL,
	[Filename] [nvarchar](255) NULL,
	[CreateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_OrderStageReceiptBatch] PRIMARY KEY CLUSTERED 
(
	[OrderStageReceiptBatchID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[OrderStageReceiptBatch] ADD  CONSTRAINT [DF_OrderStageReceiptBatch_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
