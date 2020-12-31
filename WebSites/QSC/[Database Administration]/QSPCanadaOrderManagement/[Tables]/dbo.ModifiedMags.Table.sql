USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[ModifiedMags]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ModifiedMags](
	[customerorderheaderinstance] [int] NOT NULL,
	[transid] [int] NOT NULL,
	[id] [int] NOT NULL,
	[productcode] [varchar](20) NULL,
	[quantity] [int] NULL,
	[price] [numeric](10, 2) NULL,
	[priceoverrideid] [int] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
