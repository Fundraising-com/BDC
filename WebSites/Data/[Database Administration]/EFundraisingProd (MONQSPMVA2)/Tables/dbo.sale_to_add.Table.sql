USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[sale_to_add]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[sale_to_add](
	[sale_to_add_id] [int] NOT NULL,
	[consultant_id] [int] NULL,
	[payment_method_id] [tinyint] NULL,
	[po_status_id] [tinyint] NULL,
	[sales_status_id] [int] NULL,
	[lead_id] [int] NOT NULL,
	[payment_term_id] [tinyint] NULL,
	[carrier_id] [tinyint] NULL,
	[shipping_option_id] [tinyint] NULL,
	[upfront_payment_method_id] [tinyint] NULL,
	[po_number] [varchar](50) NULL,
	[credit_card_no] [varchar](16) NULL,
	[expiry_date] [varchar](7) NULL,
	[sales_date] [datetime] NOT NULL,
	[shipping_fees] [decimal](15, 4) NOT NULL,
	[shipping_fees_discount] [decimal](15, 4) NULL,
	[payment_due_date] [datetime] NULL,
	[scheduled_delivery_date] [datetime] NULL,
	[comment] [text] NULL,
	[total_amount] [decimal](15, 4) NULL,
	[confirmed_date] [datetime] NULL,
	[upfront_payment_required] [decimal](10, 2) NULL,
	[upfront_payment_due_date] [smalldatetime] NULL,
	[is_new] [bit] NOT NULL,
	[sponsor_required] [bit] NOT NULL,
	[ssn_number] [varchar](9) NULL,
	[ssn_address] [varchar](50) NULL,
	[ssn_city] [varchar](50) NULL,
	[ssn_state_code] [varchar](10) NULL,
	[ssn_country_code] [varchar](10) NULL,
	[ssn_zip_code] [varchar](10) NULL,
 CONSTRAINT [PK_sale_to_add] PRIMARY KEY CLUSTERED 
(
	[sale_to_add_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[sale_to_add]  WITH NOCHECK ADD  CONSTRAINT [FK_sale_to_add_carrier] FOREIGN KEY([carrier_id])
REFERENCES [dbo].[carrier] ([carrier_id])
GO
ALTER TABLE [dbo].[sale_to_add] CHECK CONSTRAINT [FK_sale_to_add_carrier]
GO
ALTER TABLE [dbo].[sale_to_add]  WITH NOCHECK ADD  CONSTRAINT [FK_sale_to_add_carrier_shipping_option] FOREIGN KEY([shipping_option_id])
REFERENCES [dbo].[carrier_shipping_option] ([shipping_option_id])
GO
ALTER TABLE [dbo].[sale_to_add] CHECK CONSTRAINT [FK_sale_to_add_carrier_shipping_option]
GO
ALTER TABLE [dbo].[sale_to_add]  WITH NOCHECK ADD  CONSTRAINT [FK_sale_to_add_consultant] FOREIGN KEY([consultant_id])
REFERENCES [dbo].[consultant] ([consultant_id])
GO
ALTER TABLE [dbo].[sale_to_add] CHECK CONSTRAINT [FK_sale_to_add_consultant]
GO
ALTER TABLE [dbo].[sale_to_add]  WITH NOCHECK ADD  CONSTRAINT [FK_sale_to_add_lead] FOREIGN KEY([lead_id])
REFERENCES [dbo].[lead] ([lead_id])
GO
ALTER TABLE [dbo].[sale_to_add] CHECK CONSTRAINT [FK_sale_to_add_lead]
GO
ALTER TABLE [dbo].[sale_to_add]  WITH NOCHECK ADD  CONSTRAINT [FK_sale_to_add_payment_method] FOREIGN KEY([payment_method_id])
REFERENCES [dbo].[payment_method] ([payment_method_id])
GO
ALTER TABLE [dbo].[sale_to_add] CHECK CONSTRAINT [FK_sale_to_add_payment_method]
GO
ALTER TABLE [dbo].[sale_to_add]  WITH NOCHECK ADD  CONSTRAINT [Fk_sale_to_add_payment_term] FOREIGN KEY([payment_term_id])
REFERENCES [dbo].[payment_term] ([payment_term_id])
GO
ALTER TABLE [dbo].[sale_to_add] CHECK CONSTRAINT [Fk_sale_to_add_payment_term]
GO
ALTER TABLE [dbo].[sale_to_add]  WITH NOCHECK ADD  CONSTRAINT [FK_sale_to_add_po_status] FOREIGN KEY([po_status_id])
REFERENCES [dbo].[po_status] ([po_status_id])
GO
ALTER TABLE [dbo].[sale_to_add] CHECK CONSTRAINT [FK_sale_to_add_po_status]
GO
ALTER TABLE [dbo].[sale_to_add]  WITH NOCHECK ADD  CONSTRAINT [FK_sale_to_add_sales_status] FOREIGN KEY([sales_status_id])
REFERENCES [dbo].[Sales_Status] ([Sales_Status_ID])
GO
ALTER TABLE [dbo].[sale_to_add] CHECK CONSTRAINT [FK_sale_to_add_sales_status]
GO
ALTER TABLE [dbo].[sale_to_add]  WITH NOCHECK ADD  CONSTRAINT [FK_sale_to_add_ssn_country_code] FOREIGN KEY([ssn_country_code])
REFERENCES [dbo].[Country] ([Country_Code])
GO
ALTER TABLE [dbo].[sale_to_add] CHECK CONSTRAINT [FK_sale_to_add_ssn_country_code]
GO
ALTER TABLE [dbo].[sale_to_add]  WITH NOCHECK ADD  CONSTRAINT [FK_sale_to_add_ssn_state_code] FOREIGN KEY([ssn_state_code])
REFERENCES [dbo].[State] ([State_Code])
GO
ALTER TABLE [dbo].[sale_to_add] CHECK CONSTRAINT [FK_sale_to_add_ssn_state_code]
GO
ALTER TABLE [dbo].[sale_to_add]  WITH NOCHECK ADD  CONSTRAINT [FK_sale_to_add_upfront_payment_method] FOREIGN KEY([upfront_payment_method_id])
REFERENCES [dbo].[payment_method] ([payment_method_id])
GO
ALTER TABLE [dbo].[sale_to_add] CHECK CONSTRAINT [FK_sale_to_add_upfront_payment_method]
GO
ALTER TABLE [dbo].[sale_to_add] ADD  CONSTRAINT [DF_sale_to_add_payment_method_id]  DEFAULT (0) FOR [payment_method_id]
GO
ALTER TABLE [dbo].[sale_to_add] ADD  CONSTRAINT [DF_sale_to_add_shipping_fees]  DEFAULT (0) FOR [shipping_fees]
GO
ALTER TABLE [dbo].[sale_to_add] ADD  CONSTRAINT [DF_sale_to_add_shipping_fees_discount]  DEFAULT (0) FOR [shipping_fees_discount]
GO
ALTER TABLE [dbo].[sale_to_add] ADD  CONSTRAINT [DF_sale_to_add_total_amount]  DEFAULT (0) FOR [total_amount]
GO
ALTER TABLE [dbo].[sale_to_add] ADD  CONSTRAINT [DF_sale_to_add_is_new]  DEFAULT (1) FOR [is_new]
GO
ALTER TABLE [dbo].[sale_to_add] ADD  CONSTRAINT [DF_sale_to_add_sponsor_required]  DEFAULT (1) FOR [sponsor_required]
GO
