USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[product_business_rule_profit_range]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[product_business_rule_profit_range](
	[product_business_rule_id] [int] NOT NULL,
	[profit_range_id] [int] NOT NULL,
 CONSTRAINT [PK_product_business_rule_profit_range] PRIMARY KEY CLUSTERED 
(
	[product_business_rule_id] ASC,
	[profit_range_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[product_business_rule_profit_range]  WITH CHECK ADD  CONSTRAINT [FK_product_business_rule_profit_range_profit_range] FOREIGN KEY([profit_range_id])
REFERENCES [dbo].[profit_range] ([profit_range_id])
GO
ALTER TABLE [dbo].[product_business_rule_profit_range] CHECK CONSTRAINT [FK_product_business_rule_profit_range_profit_range]
GO
