USE [eFundstore]
GO
/****** Object:  Table [dbo].[shopping_cart_item]    Script Date: 02/14/2014 16:26:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[shopping_cart_item](
	[shopping_cart_id] [int] NOT NULL,
	[scratch_book_id] [int] NOT NULL,
	[carrier_id] [tinyint] NULL,
	[shipping_option_id] [tinyint] NULL,
	[quantity] [smallint] NOT NULL,
	[client_uploaded_img] [varchar](50) NULL,
	[group_name] [varchar](50) NULL,
 CONSTRAINT [PK_Shopping_Cart_Items] PRIMARY KEY CLUSTERED 
(
	[shopping_cart_id] ASC,
	[scratch_book_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
