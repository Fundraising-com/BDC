USE [QSPCanadaProduct]
GO
/****** Object:  Table [dbo].[Premium]    Script Date: 06/07/2017 09:17:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Premium](
	[ID] [int] NOT NULL,
	[Year] [int] NOT NULL,
	[Season] [varchar](1) NOT NULL,
	[Code] [varchar](20) NULL,
	[Valid] [char](1) NULL,
	[CountryCode] [varchar](10) NULL,
	[DateCreated] [datetime] NULL,
	[UserIDCreated] [varchar](10) NULL,
	[DateModified] [datetime] NULL,
	[UserIDModified] [varchar](10) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
