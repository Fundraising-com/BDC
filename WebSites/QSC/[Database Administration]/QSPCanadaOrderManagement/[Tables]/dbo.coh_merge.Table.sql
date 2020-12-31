USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[coh_merge]    Script Date: 06/07/2017 09:18:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[coh_merge](
	[oldcampaignid] [int] NULL,
	[oldcustomerorderheaderinstance] [int] NOT NULL,
	[oldtransid] [int] NOT NULL,
	[oldrecipient] [varchar](81) NULL,
	[oldproductcode] [varchar](20) NULL,
	[oldcustomerbilltoinstance] [int] NULL,
	[campaignid] [int] NULL,
	[customerorderheaderinstance] [int] NOT NULL,
	[transid] [int] NOT NULL,
	[recipient] [varchar](81) NULL,
	[productcode] [varchar](20) NULL,
	[customerbilltoinstance] [int] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
