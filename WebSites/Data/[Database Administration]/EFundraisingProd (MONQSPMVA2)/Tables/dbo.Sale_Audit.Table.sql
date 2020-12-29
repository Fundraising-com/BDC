USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Sale_Audit]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Sale_Audit](
	[AUDIT_ID] [int] IDENTITY(1,1) NOT NULL,
	[AUDIT_OPERATION] [char](2) NOT NULL,
	[HOST] [varchar](50) NULL,
	[AUDIT_USERID] [varchar](50) NOT NULL,
	[AUDIT_DATETIME] [datetime] NOT NULL,
	[sales_id] [int] NULL,
	[consultant_id] [int] NULL,
	[carrier_id] [tinyint] NULL,
	[shipping_option_id] [tinyint] NULL,
	[payment_term_id] [tinyint] NULL,
	[client_sequence_code] [char](2) NULL,
	[client_id] [int] NULL,
	[sales_status_id] [int] NULL,
	[payment_method_id] [tinyint] NULL,
	[po_status_id] [tinyint] NULL,
	[production_status_id] [int] NULL,
	[sponsor_consultant_id] [int] NULL,
	[ar_consultant_id] [int] NULL,
	[ar_status_id] [int] NULL,
	[lead_id] [int] NULL,
	[billing_company_id] [int] NULL,
	[upfront_payment_method_id] [tinyint] NULL,
	[confirmer_id] [int] NULL,
	[collection_status_id] [int] NULL,
	[confirmation_method_id] [int] NULL,
	[credit_approval_method_id] [int] NULL,
	[po_number] [varchar](50) NULL,
	[expiry_date] [varchar](7) NULL,
	[sales_date] [datetime] NULL,
	[shipping_fees] [decimal](15, 4) NULL,
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
	[is_ordered] [bit] NULL,
	[po_received_on] [smalldatetime] NULL,
	[is_delivered] [bit] NULL,
	[local_sponsor_found] [bit] NULL,
	[box_return_date] [smalldatetime] NULL,
	[reship_date] [smalldatetime] NULL,
	[upfront_payment_required] [decimal](10, 2) NULL,
	[upfront_payment_due_date] [smalldatetime] NULL,
	[sponsor_required] [bit] NULL,
	[actual_delivery_date] [smalldatetime] NULL,
	[accounting_comments] [text] NULL,
	[ssn_number] [varchar](9) NULL,
	[ssn_address] [varchar](50) NULL,
	[ssn_city] [varchar](50) NULL,
	[ssn_state_code] [varchar](10) NULL,
	[ssn_country_code] [varchar](10) NULL,
	[ssn_zip_code] [varchar](10) NULL,
	[is_validated] [bit] NULL,
	[promised_due_date] [datetime] NULL,
	[general_flag] [bit] NULL,
	[fuelsurcharge] [tinyint] NULL,
	[is_packed_by_participant] [bit] NULL,
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
 CONSTRAINT [PK_Sale_AUDIT] PRIMARY KEY CLUSTERED 
(
	[AUDIT_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
