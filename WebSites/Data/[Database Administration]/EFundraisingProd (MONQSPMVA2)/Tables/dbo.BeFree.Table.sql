USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[BeFree]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BeFree](
	[Merchant_ID] [char](10) NOT NULL,
	[Record_Type] [char](1) NOT NULL,
	[Date_Insert] [datetime] NOT NULL,
	[Source_ID] [char](20) NOT NULL,
	[Transaction_ID] [char](20) NOT NULL,
	[Product_Key] [char](20) NOT NULL,
	[Qty_Product] [decimal](5, 0) NOT NULL,
	[Unit_Price] [decimal](11, 2) NOT NULL,
	[Currency_Type] [char](3) NOT NULL,
	[Merchandise_Type] [char](20) NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
