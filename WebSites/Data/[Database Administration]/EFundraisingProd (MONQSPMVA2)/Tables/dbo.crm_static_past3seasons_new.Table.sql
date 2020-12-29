USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[crm_static_past3seasons_new]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[crm_static_past3seasons_new](
	[crm_static_past3seasons_new_id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[account_no] [int] NULL,
	[account_name] [varchar](50) NULL,
	[total_sold] [decimal](18, 0) NULL,
	[qsp_cust_billing_matching_code] [varchar](9) NULL,
	[qsp_cust_shipping_matching_code] [varchar](9) NULL,
	[qsp_account_matching_code] [varchar](9) NULL,
	[fm_id] [varchar](4) NULL,
	[status] [int] NULL,
	[email] [varchar](50) NULL,
	[first_name] [varchar](20) NULL,
	[last_name] [varchar](30) NULL,
	[home_phone] [varchar](20) NULL,
	[work_phone] [varchar](20) NULL,
	[mobile_phone] [varchar](20) NULL,
	[create_date] [datetime] NULL,
 CONSTRAINT [PK_crm_static_past3seasons_new] PRIMARY KEY CLUSTERED 
(
	[crm_static_past3seasons_new_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[crm_static_past3seasons_new] ADD  CONSTRAINT [DF_crm_static_past3seasons_new_inserted_date]  DEFAULT (getdate()) FOR [create_date]
GO
