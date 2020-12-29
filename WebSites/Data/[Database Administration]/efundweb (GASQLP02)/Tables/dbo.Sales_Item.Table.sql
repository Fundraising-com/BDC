USE [eFundweb]
GO
/****** Object:  Table [dbo].[Sales_Item]    Script Date: 02/14/2014 16:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Sales_Item](
	[Sales_ID] [int] NOT NULL,
	[Sales_Item_No] [smallint] NOT NULL,
	[Scratch_Book_ID] [int] NOT NULL,
	[Group_Name] [varchar](2000) NULL,
	[Quantity_Sold] [smallint] NOT NULL,
	[Unit_Price_Sold] [numeric](15, 4) NOT NULL,
	[Quantity_Free] [smallint] NULL,
	[Suggested_Coupons] [varchar](2000) NULL,
	[Sales_Amount] [numeric](15, 4) NOT NULL,
	[Paid_Amount] [numeric](15, 4) NOT NULL,
	[Adjusted_Amount] [numeric](15, 4) NOT NULL,
	[Discount_Amount] [numeric](15, 4) NULL,
	[Sales_Commission_Amount] [numeric](15, 4) NOT NULL,
	[Sponsor_Commission_Amount] [numeric](15, 4) NOT NULL,
	[Nb_units_sold] [numeric](15, 4) NULL,
	[manual_product_description] [varchar](255) NULL,
	[Service_Type_ID] [int] NULL,
	[Product_Class_ID] [int] NULL,
 CONSTRAINT [PK_Sales_Item] PRIMARY KEY CLUSTERED 
(
	[Sales_ID] ASC,
	[Sales_Item_No] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Sales_Item] ADD  DEFAULT (0) FOR [Quantity_Sold]
GO
ALTER TABLE [dbo].[Sales_Item] ADD  DEFAULT (0) FOR [Quantity_Free]
GO
ALTER TABLE [dbo].[Sales_Item] ADD  DEFAULT (0) FOR [Sales_Amount]
GO
ALTER TABLE [dbo].[Sales_Item] ADD  DEFAULT (0) FOR [Paid_Amount]
GO
ALTER TABLE [dbo].[Sales_Item] ADD  DEFAULT (0) FOR [Adjusted_Amount]
GO
ALTER TABLE [dbo].[Sales_Item] ADD  DEFAULT (0) FOR [Discount_Amount]
GO
ALTER TABLE [dbo].[Sales_Item] ADD  DEFAULT (0) FOR [Sales_Commission_Amount]
GO
ALTER TABLE [dbo].[Sales_Item] ADD  DEFAULT (0) FOR [Sponsor_Commission_Amount]
GO
ALTER TABLE [dbo].[Sales_Item] ADD  DEFAULT (0) FOR [Nb_units_sold]
GO
