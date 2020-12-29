USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[MSysConf]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MSysConf](
	[Config] [smallint] NOT NULL,
	[CHValue] [varchar](255) NULL,
	[NValue] [int] NULL,
	[Comments] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[Config] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
