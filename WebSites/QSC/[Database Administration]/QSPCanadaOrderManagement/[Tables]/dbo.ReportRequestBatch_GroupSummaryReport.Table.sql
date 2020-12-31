USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[ReportRequestBatch_GroupSummaryReport]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ReportRequestBatch_GroupSummaryReport](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ReportRequestBatchID] [int] NOT NULL,
	[pBatchId] [int] NOT NULL,
	[pBatchDate] [datetime] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[QueueDate] [datetime] NULL,
	[RunDateStart] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[FileName] [varchar](256) NULL,
 CONSTRAINT [rrb_group_pk] PRIMARY KEY CLUSTERED 
(
	[id] ASC,
	[ReportRequestBatchID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
