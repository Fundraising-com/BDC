USE [eFundweb]
GO
/****** Object:  Table [dbo].[Organization_Type]    Script Date: 02/14/2014 16:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Organization_Type](
	[Organization_Type_ID] [int] NOT NULL,
	[Organization_Type_Desc] [varchar](50) NOT NULL,
	[Party_Type_ID] [int] NOT NULL,
	[Organization_Type_Desc_Fr] [varchar](50) NULL,
 CONSTRAINT [PK_Organization_Type] PRIMARY KEY CLUSTERED 
(
	[Organization_Type_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
