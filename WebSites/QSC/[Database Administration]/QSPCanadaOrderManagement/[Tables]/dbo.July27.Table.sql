USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[July27]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[July27](
	[ID] [int] NOT NULL,
	[CampaignID] [int] NULL,
	[OrderID] [int] NOT NULL,
	[OrderQualifierID] [int] NULL,
	[Date] [datetime] NOT NULL,
	[AccountID] [int] NULL
) ON [PRIMARY]
GO
