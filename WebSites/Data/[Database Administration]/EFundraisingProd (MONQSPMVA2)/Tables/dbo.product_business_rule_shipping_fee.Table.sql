USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[product_business_rule_shipping_fee]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[product_business_rule_shipping_fee](
	[product_business_rule_id] [int] NOT NULL,
	[shipping_fee_id] [int] NOT NULL,
 CONSTRAINT [PK_product_busines_rule_shipping_fee] PRIMARY KEY CLUSTERED 
(
	[product_business_rule_id] ASC,
	[shipping_fee_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[product_business_rule_shipping_fee]  WITH CHECK ADD  CONSTRAINT [FK_product_business_rule_shipping_fee_shipping_fee] FOREIGN KEY([shipping_fee_id])
REFERENCES [dbo].[shipping_fee] ([shipping_fee_id])
GO
ALTER TABLE [dbo].[product_business_rule_shipping_fee] CHECK CONSTRAINT [FK_product_business_rule_shipping_fee_shipping_fee]
GO
