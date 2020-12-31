USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[ReportCombination]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReportCombination](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ReportRequestId] [int] NOT NULL,
	[CombinedReportRequestId] [int] NOT NULL,
	[CreateDate] [datetime] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ReportCombination] ADD  CONSTRAINT [DF_ReportCombination_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
