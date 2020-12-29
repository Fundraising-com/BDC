USE [eFundstore]
GO
/****** Object:  Table [dbo].[order_coupon]    Script Date: 02/14/2014 16:26:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[order_coupon](
	[order_id] [int] NOT NULL,
	[coupon_id] [int] NOT NULL,
 CONSTRAINT [PK_order_coupons] PRIMARY KEY CLUSTERED 
(
	[order_id] ASC,
	[coupon_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
