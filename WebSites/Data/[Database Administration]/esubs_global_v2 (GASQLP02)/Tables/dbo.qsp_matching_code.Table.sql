USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[qsp_matching_code]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[qsp_matching_code](
	[qsp_matching_code_id] [int] IDENTITY(1,1) NOT NULL,
	[account_id] [int] NOT NULL,
	[cust_billing_matching_code] [varchar](10) NOT NULL,
	[cust_shipping_matching_code] [varchar](10) NOT NULL,
	[account_matching_code] [varchar](10) NOT NULL,
 CONSTRAINT [PK_qsp_matching_code] PRIMARY KEY NONCLUSTERED 
(
	[qsp_matching_code_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
