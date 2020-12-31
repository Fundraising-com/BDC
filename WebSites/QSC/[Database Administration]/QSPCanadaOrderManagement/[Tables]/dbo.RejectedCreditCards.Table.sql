USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[RejectedCreditCards]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RejectedCreditCards](
	[fmid] [varchar](4) NOT NULL,
	[fmf] [varchar](50) NULL,
	[fml] [varchar](50) NULL,
	[accountid] [int] NULL,
	[name] [varchar](50) NULL,
	[id] [int] NOT NULL,
	[OrderID] [int] NOT NULL,
	[orderbatchid] [int] NOT NULL,
	[orderbatchdate] [datetime] NOT NULL,
	[customerorderheaderinstance] [int] NOT NULL,
	[transid] [int] NOT NULL,
	[firstname] [varchar](40) NULL,
	[lastname] [varchar](40) NULL,
	[recipient] [varchar](81) NULL,
	[productcode] [varchar](20) NULL,
	[productname] [varchar](50) NULL,
	[isstafforder] [bit] NULL,
	[price] [numeric](14, 6) NULL,
	[creditcardnumber] [varchar](25) NULL,
	[customerpaymentheaderinstance] [int] NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
