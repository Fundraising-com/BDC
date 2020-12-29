USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Partner_Fixed_Cost]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Partner_Fixed_Cost](
	[Partner_ID] [int] NOT NULL,
	[Cost_By_Lead] [decimal](10, 4) NOT NULL,
 CONSTRAINT [PK_Partner_Fixed_Cost] PRIMARY KEY CLUSTERED 
(
	[Partner_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
