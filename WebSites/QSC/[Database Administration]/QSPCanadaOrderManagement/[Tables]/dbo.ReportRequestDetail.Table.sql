USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[ReportRequestDetail]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ReportRequestDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ReportRequestId] [int] NOT NULL,
	[ReportId] [int] NOT NULL,
	[CreateDate] [datetime] NULL,
	[LoggedBy] [varchar](50) NULL,
	[DeletedTF] [bit] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
