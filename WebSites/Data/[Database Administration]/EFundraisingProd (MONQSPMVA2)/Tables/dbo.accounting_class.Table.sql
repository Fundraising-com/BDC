USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[accounting_class]    Script Date: 02/14/2014 16:28:18 ******/
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
	[rank] [tinyint] NOT NULL,
	[delivery_days] [tinyint] NULL,
 CONSTRAINT [PK_accounting_class] PRIMARY KEY CLUSTERED 
(
	[accounting_class_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[accounting_class]  WITH CHECK ADD  CONSTRAINT [FK_accounting_class_carrier] FOREIGN KEY([carrier_id])
REFERENCES [dbo].[carrier] ([carrier_id])
GO
ALTER TABLE [dbo].[accounting_class] CHECK CONSTRAINT [FK_accounting_class_carrier]
GO
ALTER TABLE [dbo].[accounting_class]  WITH CHECK ADD  CONSTRAINT [FK_accounting_class_carrier_shipping_option] FOREIGN KEY([shipping_option_id])
REFERENCES [dbo].[carrier_shipping_option] ([shipping_option_id])
GO
ALTER TABLE [dbo].[accounting_class] CHECK CONSTRAINT [FK_accounting_class_carrier_shipping_option]
GO
ALTER TABLE [dbo].[accounting_class] ADD  CONSTRAINT [DF_accounting_class_carrier_id]  DEFAULT (1) FOR [carrier_id]
GO
ALTER TABLE [dbo].[accounting_class] ADD  CONSTRAINT [DF_accounting_class_shipping_option_id]  DEFAULT (1) FOR [shipping_option_id]
GO
