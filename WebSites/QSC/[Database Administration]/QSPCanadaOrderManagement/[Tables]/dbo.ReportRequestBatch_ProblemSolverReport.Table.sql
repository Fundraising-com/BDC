USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[ReportRequestBatch_ProblemSolverReport]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ReportRequestBatch_ProblemSolverReport](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ReportRequestBatchID] [int] NOT NULL,
	[Lang] [varchar](2) NOT NULL,
	[pCampaignID] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[QueueDate] [datetime] NULL,
	[RunDateStart] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[FileName] [varchar](256) NULL,
 CONSTRAINT [rrb_probsolv_pk] PRIMARY KEY CLUSTERED 
(
	[id] ASC,
	[ReportRequestBatchID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
