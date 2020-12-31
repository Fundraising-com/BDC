USE [QSPCanadaCommon]
GO
/****** Object:  Table [dbo].[HomePageNewsItems]    Script Date: 06/07/2017 09:29:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[HomePageNewsItems](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[startdate] [datetime] NOT NULL,
	[enddate] [datetime] NULL,
	[title] [varchar](100) NOT NULL,
	[text] [varchar](400) NULL,
	[color] [varchar](10) NOT NULL,
	[weight] [smallint] NOT NULL,
	[DeletedTF] [bit] NOT NULL,
 CONSTRAINT [PK_HomePageNewsItems] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[HomePageNewsItems] ADD  CONSTRAINT [DF_QSPFulfNewsItems_weight]  DEFAULT (0) FOR [weight]
GO
ALTER TABLE [dbo].[HomePageNewsItems] ADD  CONSTRAINT [DF_QSPFulfNewsItems_DeletedTF]  DEFAULT (0) FOR [DeletedTF]
GO
