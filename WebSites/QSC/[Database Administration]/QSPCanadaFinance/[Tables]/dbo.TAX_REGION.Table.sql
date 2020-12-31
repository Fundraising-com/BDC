USE [QSPCanadaFinance]
GO
/****** Object:  Table [dbo].[TAX_REGION]    Script Date: 06/07/2017 09:16:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TAX_REGION](
	[TAX_REGION_ID] [int] NULL,
	[NAME_RESOURCE_ID] [int] NULL,
	[CONSOLIDATE_TAX_RATE] [numeric](10, 2) NULL,
	[EFFECTIVE_DATE] [datetime] NULL
) ON [PRIMARY]
GO
