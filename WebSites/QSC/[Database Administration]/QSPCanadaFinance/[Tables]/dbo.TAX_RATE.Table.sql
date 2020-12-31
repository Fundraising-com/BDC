USE [QSPCanadaFinance]
GO
/****** Object:  Table [dbo].[TAX_RATE]    Script Date: 06/07/2017 09:16:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TAX_RATE](
	[TAX_RATE_ID] [int] NULL,
	[TAX_ID] [int] NULL,
	[TAX_RATE] [numeric](10, 2) NULL,
	[EFFECTIVE_DATE] [datetime] NULL
) ON [PRIMARY]
GO
