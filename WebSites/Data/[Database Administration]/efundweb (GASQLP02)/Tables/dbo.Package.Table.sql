USE [eFundweb]
GO
/****** Object:  Table [dbo].[Package]    Script Date: 02/14/2014 16:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Package](
	[Package_ID] [int] NOT NULL,
	[Description] [varchar](500) NULL,
	[Image_Path] [varchar](120) NULL,
	[Package_Profit] [int] NOT NULL,
 CONSTRAINT [PK_Package] PRIMARY KEY CLUSTERED 
(
	[Package_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
