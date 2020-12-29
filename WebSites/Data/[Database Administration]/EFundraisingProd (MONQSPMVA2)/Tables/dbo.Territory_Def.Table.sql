USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Territory_Def]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Territory_Def](
	[Zip] [varchar](10) NOT NULL,
	[Territory_ID] [int] NOT NULL,
 CONSTRAINT [PK_Territory_Def] PRIMARY KEY CLUSTERED 
(
	[Zip] ASC,
	[Territory_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
