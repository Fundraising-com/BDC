USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[ReportRequestBatchType]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ReportRequestBatchType](
	[ReportTypeId] [int] NOT NULL,
	[Description] [varchar](100) NOT NULL,
	[DestDesc] [varchar](50) NULL,
	[Tip] [varchar](255) NULL,
	[ReportPath] [varchar](255) NULL,
 CONSTRAINT [PK_ReportType] PRIMARY KEY CLUSTERED 
(
	[ReportTypeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
