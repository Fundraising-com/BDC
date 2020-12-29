USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[crm_static_all_accounts]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[crm_static_all_accounts](
	[account_no] [int] NULL,
	[account_name] [varchar](50) NULL,
	[qsp_cust_billing_matching_code] [varchar](9) NULL,
	[qsp_cust_shipping_matching_code] [varchar](9) NULL,
	[qsp_account_matching_code] [varchar](9) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
