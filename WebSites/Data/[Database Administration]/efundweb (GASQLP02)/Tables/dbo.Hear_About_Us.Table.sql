USE [eFundweb]
GO
/****** Object:  Table [dbo].[Hear_About_Us]    Script Date: 02/14/2014 16:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Hear_About_Us](
	[hear_ID] [int] NOT NULL,
	[name] [varchar](50) NOT NULL,
	[Order_On_Web] [int] NOT NULL,
	[Is_Active] [bit] NULL,
	[Party_Type_ID] [int] NOT NULL,
	[Name_Fr] [varchar](50) NULL,
 CONSTRAINT [PK_Hear_About_Us] PRIMARY KEY CLUSTERED 
(
	[hear_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
