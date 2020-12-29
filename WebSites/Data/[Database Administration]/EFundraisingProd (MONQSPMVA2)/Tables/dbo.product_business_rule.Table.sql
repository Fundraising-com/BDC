USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[product_business_rule]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[product_business_rule](
	[product_business_rule_id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[product_class_id] [int] NOT NULL,
	[product_id] [int] NULL,
	[min_order] [int] NULL,
	[free] [float] NULL,
	[average_delivery_time] [int] NULL,
	[package_id] [int] NULL,
 CONSTRAINT [PK_product_business_rule] PRIMARY KEY CLUSTERED 
(
	[product_business_rule_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[product_business_rule]  WITH CHECK ADD  CONSTRAINT [FK_product_business_rule_product_class] FOREIGN KEY([product_class_id])
REFERENCES [dbo].[product_class] ([product_class_id])
GO
ALTER TABLE [dbo].[product_business_rule] CHECK CONSTRAINT [FK_product_business_rule_product_class]
GO
ALTER TABLE [dbo].[product_business_rule]  WITH CHECK ADD  CONSTRAINT [FK_product_business_rule_scratch_book] FOREIGN KEY([product_id])
REFERENCES [dbo].[scratch_book] ([scratch_book_id])
GO
ALTER TABLE [dbo].[product_business_rule] CHECK CONSTRAINT [FK_product_business_rule_scratch_book]
GO
