USE [QSPCanadaFinance]
GO
/****** Object:  Table [dbo].[CA_down]    Script Date: 06/07/2017 09:16:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CA_down](
	[fmid] [varchar](4) NOT NULL,
	[accountid] [int] NULL,
	[campaignid] [int] NULL,
	[total] [numeric](38, 2) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
