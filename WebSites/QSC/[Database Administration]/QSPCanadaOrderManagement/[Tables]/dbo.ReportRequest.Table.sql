USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[ReportRequest]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ReportRequest](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BatchOrderId] [int] NULL,
	[ReportTypeId] [int] NULL,
	[RSSubscriptionId] [varchar](100) NULL,
	[CreateDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) NULL,
	[ArchiveTF] [int] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[ReportRequest] ADD  CONSTRAINT [DF_ReportRequest_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[ReportRequest] ADD  CONSTRAINT [DF_ReportRequest_ArchiveTF]  DEFAULT (0) FOR [ArchiveTF]
GO
