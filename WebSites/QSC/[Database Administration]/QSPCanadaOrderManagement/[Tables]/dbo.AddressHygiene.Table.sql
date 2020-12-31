USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[AddressHygiene]    Script Date: 06/07/2017 09:18:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AddressHygiene](
	[AddressHygieneID] [int] IDENTITY(1,1) NOT NULL,
	[BatchID] [int] NOT NULL,
	[BatchDate] [datetime] NOT NULL,
	[DateRun] [datetime] NOT NULL,
 CONSTRAINT [PK_AddressHygiene] PRIMARY KEY CLUSTERED 
(
	[AddressHygieneID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
