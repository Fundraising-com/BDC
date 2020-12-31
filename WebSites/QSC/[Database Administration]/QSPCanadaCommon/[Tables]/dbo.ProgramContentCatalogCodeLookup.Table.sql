USE [QSPCanadaCommon]
GO
/****** Object:  Table [dbo].[ProgramContentCatalogCodeLookup]    Script Date: 06/07/2017 09:29:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProgramContentCatalogCodeLookup](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProgramID] [int] NULL,
	[Province] [varchar](10) NULL,
	[FiscalYear] [int] NULL,
	[Season] [varchar](2) NULL,
	[Lang] [varchar](10) NULL,
	[ContentCatalogCode] [varchar](20) NULL,
	[PrimaryCatalogCode] [varchar](20) NULL,
	[SecondaryCatalogCode] [varchar](20) NULL,
	[IsComboCumulativeReward] [int] NULL,
 CONSTRAINT [PK_ProgramContentCatalogCodeLookup] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
