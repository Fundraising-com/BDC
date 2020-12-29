USE [eFundstore]
GO
/****** Object:  Table [dbo].[product_class]    Script Date: 02/14/2014 16:26:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[product_class](
	[product_class_id] [int] NOT NULL,
	[division_id] [int] NOT NULL,
	[accounting_class_id] [tinyint] NULL,
	[description] [varchar](50) NULL,
	[display] [bit] NOT NULL,
	[minimum_order_qty] [tinyint] NOT NULL,
 CONSTRAINT [PK_product_class] PRIMARY KEY CLUSTERED 
(
	[product_class_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[product_class] ADD  CONSTRAINT [DF_product_class_is_displayable]  DEFAULT (1) FOR [display]
GO
ALTER TABLE [dbo].[product_class] ADD  CONSTRAINT [DF_product_class_minimum_order_qty]  DEFAULT (0) FOR [minimum_order_qty]
GO
