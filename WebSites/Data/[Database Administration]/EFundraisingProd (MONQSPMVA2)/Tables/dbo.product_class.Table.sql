USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[product_class]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[product_class](
	[product_class_id] [int] NOT NULL,
	[division_id] [tinyint] NOT NULL,
	[accounting_class_id] [tinyint] NOT NULL,
	[description] [varchar](50) NOT NULL,
	[product_code] [varchar](10) NULL,
	[display_name] [varchar](100) NULL,
	[is_displayable] [bit] NOT NULL,
	[minimum_order_qty] [tinyint] NOT NULL,
	[tax_exempt] [bit] NULL,
 CONSTRAINT [PK_product_class] PRIMARY KEY CLUSTERED 
(
	[product_class_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[product_class] ADD  CONSTRAINT [DF_product_class_is_displayable]  DEFAULT (1) FOR [is_displayable]
GO
ALTER TABLE [dbo].[product_class] ADD  CONSTRAINT [DF_product_class_minimum_order_qty]  DEFAULT (0) FOR [minimum_order_qty]
GO
ALTER TABLE [dbo].[product_class] ADD  CONSTRAINT [DF__product_c__tax_e__45C0BDAE]  DEFAULT ((0)) FOR [tax_exempt]
GO
