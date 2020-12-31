USE [QSPCanadaCommon]
GO
/****** Object:  Table [dbo].[SeasonNames]    Script Date: 06/07/2017 09:29:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SeasonNames](
	[SeasonID] [int] NOT NULL,
	[Lang] [varchar](10) NOT NULL,
	[Name] [varchar](50) NULL,
	[DateCreated] [datetime] NULL,
	[UserIDCreated] [varchar](4) NULL,
	[DateChanged] [datetime] NULL,
	[UserIDChanged] [varchar](4) NULL,
 CONSTRAINT [PK_SeasonNames] PRIMARY KEY CLUSTERED 
(
	[SeasonID] ASC,
	[Lang] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
