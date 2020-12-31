USE [QSPCanadaCommon]
GO
/****** Object:  Table [dbo].[HomePageDateItems]    Script Date: 06/07/2017 09:29:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[HomePageDateItems](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[displayfrom] [datetime] NOT NULL,
	[displayto] [datetime] NOT NULL,
	[startdate] [datetime] NOT NULL,
	[enddate] [datetime] NULL,
	[text] [varchar](100) NOT NULL,
	[DeletedTF] [bit] NOT NULL,
 CONSTRAINT [PK_HomePageDateItems] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[HomePageDateItems] ADD  CONSTRAINT [DF_QSPFulfHomeInfo_DeletedTF]  DEFAULT (0) FOR [DeletedTF]
GO
