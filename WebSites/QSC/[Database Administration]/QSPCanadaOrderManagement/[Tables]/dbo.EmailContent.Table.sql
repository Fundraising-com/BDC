USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[EmailContent]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EmailContent](
	[ContentNumber] [int] NOT NULL,
	[LanguageCode] [varchar](10) NOT NULL,
	[LineNumber] [int] NOT NULL,
	[Content] [varchar](7000) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateModified] [datetime] NULL,
	[LastUpdatedBy] [varchar](30) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
