USE [eFundweb]
GO
/****** Object:  Table [dbo].[Category_Partner]    Script Date: 02/14/2014 16:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category_Partner](
	[Partner_ID] [int] NOT NULL,
	[Category_ID] [int] NOT NULL,
 CONSTRAINT [PK_Category_Partner] PRIMARY KEY CLUSTERED 
(
	[Partner_ID] ASC,
	[Category_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
