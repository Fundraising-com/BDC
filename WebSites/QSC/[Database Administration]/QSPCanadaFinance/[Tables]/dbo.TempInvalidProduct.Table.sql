USE [QSPCanadaFinance]
GO
/****** Object:  Table [dbo].[TempInvalidProduct]    Script Date: 06/07/2017 09:16:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TempInvalidProduct](
	[AccountID] [int] NULL,
	[CampaignID] [int] NULL,
	[Amount] [numeric](10, 2) NULL
) ON [PRIMARY]
GO
