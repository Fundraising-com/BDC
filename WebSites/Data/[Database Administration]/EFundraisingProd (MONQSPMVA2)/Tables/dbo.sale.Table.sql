USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[sale]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[sale](
	[sales_id] [int] NOT NULL,
	[consultant_id] [int] NOT NULL,
	[carrier_id] [tinyint] NULL,
	[shipping_option_id] [tinyint] NULL,
	[payment_term_id] [tinyint] NOT NULL,
	[client_sequence_code] [char](2) NOT NULL,
	[client_id] [int] NOT NULL,
	[sales_status_id] [int] NOT NULL,
	[payment_method_id] [tinyint] NOT NULL,
	[po_status_id] [tinyint] NULL,
	[production_status_id] [int] NULL,
	[sponsor_consultant_id] [int] NULL,
	[ar_consultant_id] [int] NULL,
	[ar_status_id] [int] NOT NULL,
	[lead_id] [int] NULL,
	[billing_company_id] [int] NULL,
	[upfront_payment_method_id] [tinyint] NULL,
	[confirmer_id] [int] NULL,
	[collection_status_id] [int] NULL,
	[confirmation_method_id] [int] NULL,
	[credit_approval_method_id] [int] NULL,
	[po_number] [varchar](50) NULL,
	[expiry_date] [varchar](7) NULL,
	[sales_date] [datetime] NOT NULL,
	[shipping_fees] [decimal](15, 4) NOT NULL,
	[shipping_fees_discount] [decimal](15, 4) NULL,
	[payment_due_date] [datetime] NULL,
	[confirmed_date] [datetime] NULL,
	[scheduled_delivery_date] [datetime] NULL,
	[scheduled_ship_date] [datetime] NULL,
	[actual_ship_date] [datetime] NULL,
	[waybill_no] [varchar](20) NULL,
	[comment] [varchar](200) NULL,
	[coupon_sheet_assigned] [bit] NULL,
	[total_amount] [decimal](15, 4) NULL,
	[invoice_date] [datetime] NULL,
	[cancellation_date] [datetime] NULL,
	[is_ordered] [bit] NOT NULL,
	[po_received_on] [smalldatetime] NULL,
	[is_delivered] [bit] NOT NULL,
	[local_sponsor_found] [bit] NOT NULL,
	[box_return_date] [smalldatetime] NULL,
	[reship_date] [smalldatetime] NULL,
	[upfront_payment_required] [decimal](10, 2) NULL,
	[upfront_payment_due_date] [smalldatetime] NULL,
	[sponsor_required] [bit] NOT NULL,
	[actual_delivery_date] [smalldatetime] NULL,
	[accounting_comments] [text] NULL,
	[ssn_number] [varchar](9) NULL,
	[ssn_address] [varchar](50) NULL,
	[ssn_city] [varchar](50) NULL,
	[ssn_state_code] [varchar](10) NULL,
	[ssn_country_code] [varchar](10) NULL,
	[ssn_zip_code] [varchar](10) NULL,
	[is_validated] [bit] NOT NULL,
	[promised_due_date] [datetime] NULL,
	[general_flag] [bit] NOT NULL,
	[fuelsurcharge] [tinyint] NULL,
	[is_packed_by_participant] [bit] NOT NULL,
	[carrier_tracking_id] [int] NULL,
	[qsp_order_id] [int] NULL,
	[ext_order_id] [int] NULL,
	[credit_card_no] [varbinary](max) NULL,
	[wfc_invoice_number] [nvarchar](50) NULL,
	[cvv2] [char](3) NULL,
	[po_consultant_commission] [int] NULL,
	[ext_sales_status_id] [int] NULL,
	[ext_shipping_account_id] [varchar](20) NULL,
	[ext_billing_account_id] [varchar](20) NULL,
 CONSTRAINT [PK_sale] PRIMARY KEY CLUSTERED 
(
	[sales_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[sale]  WITH CHECK ADD  CONSTRAINT [fk_sale_ext_sales_status] FOREIGN KEY([ext_sales_status_id])
REFERENCES [dbo].[Ext_sales_status] ([Ext_sales_status_id])
GO
ALTER TABLE [dbo].[sale] CHECK CONSTRAINT [fk_sale_ext_sales_status]
GO
ALTER TABLE [dbo].[sale] ADD  CONSTRAINT [DF_sale_sales_status]  DEFAULT (1) FOR [sales_status_id]
GO
ALTER TABLE [dbo].[sale] ADD  CONSTRAINT [DF_sale_production_status_id]  DEFAULT (1) FOR [production_status_id]
GO
ALTER TABLE [dbo].[sale] ADD  CONSTRAINT [DF_sale_ar_status_id]  DEFAULT (21) FOR [ar_status_id]
GO
ALTER TABLE [dbo].[sale] ADD  CONSTRAINT [DF_sale_collection_status_id]  DEFAULT (9) FOR [collection_status_id]
GO
ALTER TABLE [dbo].[sale] ADD  CONSTRAINT [DF_sale_shipping_fees]  DEFAULT (0) FOR [shipping_fees]
GO
ALTER TABLE [dbo].[sale] ADD  CONSTRAINT [DF_sale_shipping_fees_discount]  DEFAULT (0) FOR [shipping_fees_discount]
GO
ALTER TABLE [dbo].[sale] ADD  CONSTRAINT [DF_sale_coupon_sheet_assigned]  DEFAULT (0) FOR [coupon_sheet_assigned]
GO
ALTER TABLE [dbo].[sale] ADD  CONSTRAINT [DF_sale_total_amount]  DEFAULT (0) FOR [total_amount]
GO
ALTER TABLE [dbo].[sale] ADD  CONSTRAINT [DF_sale_is_ordered]  DEFAULT (0) FOR [is_ordered]
GO
ALTER TABLE [dbo].[sale] ADD  CONSTRAINT [DF_sale_is_delivered]  DEFAULT (0) FOR [is_delivered]
GO
ALTER TABLE [dbo].[sale] ADD  CONSTRAINT [DF_sale_local_sponsor_found]  DEFAULT (0) FOR [local_sponsor_found]
GO
ALTER TABLE [dbo].[sale] ADD  CONSTRAINT [DF_sale_sponsor_required]  DEFAULT (1) FOR [sponsor_required]
GO
ALTER TABLE [dbo].[sale] ADD  CONSTRAINT [DF_sale_is_validated]  DEFAULT (0) FOR [is_validated]
GO
ALTER TABLE [dbo].[sale] ADD  CONSTRAINT [DF_sale_general_flag]  DEFAULT (0) FOR [general_flag]
GO
ALTER TABLE [dbo].[sale] ADD  CONSTRAINT [DF_sale_fuelsurcharge]  DEFAULT (0) FOR [fuelsurcharge]
GO
ALTER TABLE [dbo].[sale] ADD  CONSTRAINT [DF_sale_is_packed_by_participant]  DEFAULT (0) FOR [is_packed_by_participant]
GO
