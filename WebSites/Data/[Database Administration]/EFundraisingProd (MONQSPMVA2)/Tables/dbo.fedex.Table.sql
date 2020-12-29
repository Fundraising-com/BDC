USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[fedex]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[fedex](
	[fedex_id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[fedex_uid] [varchar](25) NULL,
	[company_name] [varchar](35) NULL,
	[contact_name] [varchar](35) NULL,
	[address_line_1] [varchar](35) NULL,
	[address_line_2] [varchar](35) NULL,
	[city] [varchar](35) NULL,
	[province_state] [varchar](2) NULL,
	[country] [varchar](2) NULL,
	[zip_postal_code] [varchar](10) NULL,
	[telephone] [varchar](10) NULL,
	[extention] [varchar](5) NULL,
	[tax_id_ssn] [varchar](15) NULL,
	[fedex_account] [int] NULL,
	[shipalert_email_address] [varchar](120) NULL,
	[shipalert_email_message] [varchar](450) NULL,
	[shipalert_email_option] [int] NULL,
	[total_package_weight] [int] NULL,
	[number_of_packages] [int] NULL,
	[dimension_height] [int] NULL,
	[dimension_width] [int] NULL,
	[dimension_length] [int] NULL,
	[sevice_level] [varchar](3) NULL,
	[bill_freight_charges_to] [int] NULL,
	[inter_part_description] [varchar](148) NULL,
	[inter_unit_value] [decimal](15, 4) NULL,
	[inter_currency] [varchar](3) NULL,
	[inter_unit_of_measure] [varchar](3) NULL,
	[inter_quantity] [int] NULL,
	[inter_country_of_manufacture] [varchar](2) NULL,
	[inter_harmonized_code] [bigint] NULL,
	[inter_part_number] [varchar](20) NULL,
	[inter_marks_number] [varchar](15) NULL,
	[inter_sku_upc_item] [varchar](15) NULL,
	[inter_bill_duties_taxes_to] [int] NULL,
	[inter_create_date] [datetime] NULL,
	[inter_tracking_number] [varchar](127) NULL,
	[inter_label_date_shipped_date] [datetime] NULL,
	[inter_update_sale_date] [datetime] NULL,
	[inter_shipping_quote] [decimal](15, 4) NULL,
	[cancelled] [smallint] NULL,
	[cod_amount] [decimal](15, 4) NULL,
	[cod_payment_method] [smallint] NULL,
 CONSTRAINT [PK_fedex] PRIMARY KEY CLUSTERED 
(
	[fedex_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[fedex] ADD  CONSTRAINT [DF_fedex_cancelled]  DEFAULT (0) FOR [cancelled]
GO
