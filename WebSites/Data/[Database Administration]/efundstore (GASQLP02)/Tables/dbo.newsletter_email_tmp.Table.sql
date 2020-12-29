USE [eFundstore]
GO
/****** Object:  Table [dbo].[newsletter_email_tmp]    Script Date: 02/14/2014 16:26:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[newsletter_email_tmp](
	[email] [varchar](255) NULL,
	[first_name] [varchar](255) NULL,
	[last_name] [varchar](255) NULL,
	[group_type_description] [varchar](255) NULL,
	[organization] [varchar](255) NULL,
	[organization_type_description] [varchar](255) NULL,
	[consultant] [varchar](255) NULL,
	[consultant_ext] [int] NULL,
	[is_active] [int] NULL,
	[partner_name] [varchar](255) NULL,
	[count_of_sales] [int] NULL,
	[quantity_sold] [int] NULL,
	[total_amount] [decimal](18, 0) NULL,
	[payment_amount] [decimal](18, 0) NULL,
	[product_class] [varchar](255) NULL,
	[product] [varchar](255) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
