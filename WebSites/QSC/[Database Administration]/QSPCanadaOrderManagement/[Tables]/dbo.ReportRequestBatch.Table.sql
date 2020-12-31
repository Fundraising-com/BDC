USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[ReportRequestBatch]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReportRequestBatch](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[BatchOrderId] [int] NOT NULL,
	[TypeId] [int] NULL,
	[CreateDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[IsPrinted] [int] NULL,
	[DatePrinted] [datetime] NULL,
	[IsMerged] [bit] NOT NULL,
	[IsQSPPrint] [bit] NULL,
	[ShipmentGroupID] [int] NULL,
 CONSTRAINT [rrbpk] PRIMARY KEY CLUSTERED 
(
	[id] ASC,
	[BatchOrderId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ReportRequestBatch]  WITH CHECK ADD  CONSTRAINT [FK_ReportRequestBatch_ShipmentGroup] FOREIGN KEY([ShipmentGroupID])
REFERENCES [dbo].[ShipmentGroup] ([ShipmentGroupID])
GO
ALTER TABLE [dbo].[ReportRequestBatch] CHECK CONSTRAINT [FK_ReportRequestBatch_ShipmentGroup]
GO
ALTER TABLE [dbo].[ReportRequestBatch] ADD  CONSTRAINT [DF__ReportReq__IsMer__1C13D14E]  DEFAULT (0) FOR [IsMerged]
GO
