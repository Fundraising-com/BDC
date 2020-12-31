USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[OrderStageReceipt]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderStageReceipt](
	[OrderStageReceiptID] [int] IDENTITY(1,1) NOT NULL,
	[OrderStageReceiptBatchID] [int] NOT NULL,
	[StatusID] [int] NOT NULL,
	[ReceiptDate] [datetime] NULL,
	[ImageDate] [datetime] NULL,
	[DataCaptureDate] [datetime] NULL,
	[VerificationDate] [datetime] NULL,
	[EditDate] [datetime] NULL,
	[TransmitDate] [datetime] NULL,
	[StageID] [int] NULL,
	[CampaignID] [int] NULL,
	[GroupID] [int] NULL,
	[GroupName] [nvarchar](255) NULL,
	[FMID] [nvarchar](255) NULL,
	[FMName] [nvarchar](255) NULL,
	[OrderID] [int] NULL,
	[ScanCount] [int] NULL,
	[ErrorCode] [int] NULL,
	[Units] [int] NULL,
	[CreateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_OrderStageReceipt] PRIMARY KEY CLUSTERED 
(
	[OrderStageReceiptID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[OrderStageReceipt]  WITH CHECK ADD  CONSTRAINT [FK_OrderStageReceipt_OrderStageReceiptBatch] FOREIGN KEY([OrderStageReceiptBatchID])
REFERENCES [dbo].[OrderStageReceiptBatch] ([OrderStageReceiptBatchID])
GO
ALTER TABLE [dbo].[OrderStageReceipt] CHECK CONSTRAINT [FK_OrderStageReceipt_OrderStageReceiptBatch]
GO
ALTER TABLE [dbo].[OrderStageReceipt]  WITH CHECK ADD  CONSTRAINT [FK_OrderStageReceipt_OrderStageReceiptStatus] FOREIGN KEY([StatusID])
REFERENCES [dbo].[OrderStageReceiptStatus] ([OrderStageReceiptStatusID])
GO
ALTER TABLE [dbo].[OrderStageReceipt] CHECK CONSTRAINT [FK_OrderStageReceipt_OrderStageReceiptStatus]
GO
ALTER TABLE [dbo].[OrderStageReceipt] ADD  CONSTRAINT [DF_OrderStageReceipt_StatusID]  DEFAULT ((1)) FOR [StatusID]
GO
ALTER TABLE [dbo].[OrderStageReceipt] ADD  CONSTRAINT [DF_OrderStageReceipt_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
