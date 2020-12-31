USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[repcc]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[repcc](
	[fmid] [varchar](4) NOT NULL,
	[fmfirstname] [varchar](50) NULL,
	[fmlastname] [varchar](50) NULL,
	[accountid] [int] NULL,
	[name] [varchar](50) NULL,
	[id] [int] NOT NULL,
	[OrderID] [int] NOT NULL,
	[customerorderheaderinstance] [int] NOT NULL,
	[transid] [int] NOT NULL,
	[firstname] [varchar](40) NULL,
	[lastname] [varchar](40) NULL,
	[recipient] [varchar](81) NULL,
	[productcode] [varchar](20) NULL,
	[productname] [varchar](50) NULL,
	[price] [numeric](10, 2) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
