USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[accounting_class_shipping_fees]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[accounting_class_shipping_fees](
	[accounting_class_id] [tinyint] NOT NULL,
	[min_amount] [numeric](10, 2) NOT NULL,
	[max_amount] [numeric](10, 2) NOT NULL,
	[shipping_fee] [tinyint] NOT NULL,
 CONSTRAINT [PK_accounting_class_shipping_fees] PRIMARY KEY CLUSTERED 
(
	[accounting_class_id] ASC,
	[min_amount] ASC,
	[max_amount] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[accounting_class_shipping_fees]  WITH CHECK ADD  CONSTRAINT [FK_accounting_class_shipping_fees_accounting_class] FOREIGN KEY([accounting_class_id])
REFERENCES [dbo].[accounting_class] ([accounting_class_id])
GO
ALTER TABLE [dbo].[accounting_class_shipping_fees] CHECK CONSTRAINT [FK_accounting_class_shipping_fees_accounting_class]
GO
