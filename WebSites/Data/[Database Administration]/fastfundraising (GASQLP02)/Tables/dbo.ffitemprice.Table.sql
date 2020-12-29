USE [fastfundraising]
GO
/****** Object:  Table [dbo].[ffitemprice]    Script Date: 02/14/2014 16:35:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ffitemprice](
	[itemid] [int] NOT NULL,
	[tierid] [int] NOT NULL,
	[price] [float] NULL,
	[tierminval] [int] NULL,
	[upperlimit] [int] NULL,
 CONSTRAINT [PK_ffitemprice] PRIMARY KEY NONCLUSTERED 
(
	[itemid] ASC,
	[tierid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
