USE [eFundweb]
GO
/****** Object:  Table [dbo].[Salutation]    Script Date: 02/14/2014 16:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Salutation](
	[Salutation_ID] [int] NOT NULL,
	[Salutation_Desc] [varchar](100) NOT NULL,
	[Salutation_Fr] [varchar](100) NULL,
 CONSTRAINT [PK_Salutation] PRIMARY KEY CLUSTERED 
(
	[Salutation_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
