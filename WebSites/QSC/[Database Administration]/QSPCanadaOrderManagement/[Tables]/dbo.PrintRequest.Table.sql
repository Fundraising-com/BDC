USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[PrintRequest]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PrintRequest](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ReportRequestId] [int] NOT NULL,
	[PrinterId] [int] NOT NULL,
	[PrintRequestStatusId] [int] NOT NULL,
	[CreateDate] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[PrintRequest] ADD  CONSTRAINT [DF_PrintRequest_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
