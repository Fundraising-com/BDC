USE [eFundweb]
GO
/****** Object:  Table [dbo].[Price]    Script Date: 02/14/2014 16:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Price](
	[Product_ID] [int] NOT NULL,
	[Country_ID] [int] NOT NULL,
	[Price] [numeric](15, 4) NOT NULL,
	[Profit_Percentage] [int] NULL,
 CONSTRAINT [PK_Price] PRIMARY KEY CLUSTERED 
(
	[Product_ID] ASC,
	[Country_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Price]  WITH CHECK ADD  CONSTRAINT [FK_Price_Product] FOREIGN KEY([Product_ID])
REFERENCES [dbo].[Product] ([Product_ID])
GO
ALTER TABLE [dbo].[Price] CHECK CONSTRAINT [FK_Price_Product]
GO
