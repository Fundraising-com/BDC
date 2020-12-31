USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[CustomerAccount]    Script Date: 06/07/2017 09:18:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerAccount](
	[AccountID] [int] NOT NULL,
	[CustomerInstance] [int] NOT NULL,
 CONSTRAINT [PK_CustomerAccount] PRIMARY KEY CLUSTERED 
(
	[AccountID] ASC,
	[CustomerInstance] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
