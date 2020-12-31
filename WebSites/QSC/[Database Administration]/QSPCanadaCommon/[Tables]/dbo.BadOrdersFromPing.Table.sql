USE [QSPCanadaCommon]
GO
/****** Object:  Table [dbo].[BadOrdersFromPing]    Script Date: 06/07/2017 09:29:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BadOrdersFromPing](
	[internetorderid] [int] NULL,
	[PL_Order_#] [int] NULL,
	[CampaignID] [int] NULL,
	[AccountID] [int] NULL,
	[OrderID] [int] NULL,
	[ke3filename] [nvarchar](255) NULL,
	[date] [smalldatetime] NULL,
	[id] [int] NULL,
	[Fixable] [int] NULL,
	[Fixed] [int] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[BadOrdersFromPing] ADD  CONSTRAINT [DF_BadOrdersFromPing_Fixable]  DEFAULT (4) FOR [Fixable]
GO
ALTER TABLE [dbo].[BadOrdersFromPing] ADD  CONSTRAINT [DF_BadOrdersFromPing_Fixed]  DEFAULT (0) FOR [Fixed]
GO
