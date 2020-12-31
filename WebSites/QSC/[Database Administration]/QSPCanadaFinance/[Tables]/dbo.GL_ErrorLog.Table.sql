USE [QSPCanadaFinance]
GO
/****** Object:  Table [dbo].[GL_ErrorLog]    Script Date: 06/07/2017 09:16:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GL_ErrorLog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SourceType] [char](10) NULL,
	[SourceId] [char](10) NULL,
	[Message] [char](200) NULL,
	[Date] [datetime] NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
