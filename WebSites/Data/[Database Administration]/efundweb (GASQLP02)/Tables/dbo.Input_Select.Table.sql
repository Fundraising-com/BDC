USE [eFundweb]
GO
/****** Object:  Table [dbo].[Input_Select]    Script Date: 02/14/2014 16:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Input_Select](
	[Table_Source] [varchar](100) NOT NULL,
	[Column_Desc] [varchar](100) NOT NULL,
	[Column_ID] [varchar](100) NOT NULL,
	[Clauses] [varchar](200) NULL,
	[Questions_ID] [int] NOT NULL,
	[Language_ID] [int] NOT NULL,
 CONSTRAINT [PK_Input_Select] PRIMARY KEY CLUSTERED 
(
	[Questions_ID] ASC,
	[Language_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
