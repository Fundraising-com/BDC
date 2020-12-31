USE [QSPCanadaProduct]
GO
/****** Object:  Table [dbo].[PremiumDescription]    Script Date: 06/07/2017 09:17:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PremiumDescription](
	[PremiumID] [int] NOT NULL,
	[Lang] [char](2) NOT NULL,
	[Description] [varchar](100) NULL,
	[DateCreated] [datetime] NULL,
	[UserIDCreated] [varchar](10) NULL,
	[DateChanged] [datetime] NULL,
	[UserIDChanged] [varchar](10) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
