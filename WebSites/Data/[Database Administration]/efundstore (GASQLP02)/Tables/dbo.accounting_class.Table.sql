USE [eFundstore]
GO
/****** Object:  Table [dbo].[accounting_class]    Script Date: 02/14/2014 16:26:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[accounting_class](
	[accounting_class_id] [tinyint] NOT NULL,
	[carrier_id] [tinyint] NOT NULL,
	[shipping_option_id] [tinyint] NOT NULL,
	[description] [varchar](50) NOT NULL,
	[rank] [int] NOT NULL,
	[delivery_days] [tinyint] NULL,
	[shipping_fees] [tinyint] NULL,
	[free_shipping_amount] [int] NULL,
 CONSTRAINT [PK_accounting_class] PRIMARY KEY CLUSTERED 
(
	[accounting_class_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
