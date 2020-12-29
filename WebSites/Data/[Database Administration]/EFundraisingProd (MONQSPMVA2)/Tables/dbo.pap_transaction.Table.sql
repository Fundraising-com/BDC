USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[pap_transaction]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[pap_transaction](
	[pap_transaction_id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[order_id] [int] NULL,
	[pap_product_category_id] [int] NULL,
	[total_cost] [decimal](9, 2) NULL,
	[action_code] [varchar](50) NULL,
	[ext_transaction_id] [varchar](50) NULL,
	[ext_status_id] [varchar](50) NULL,
	[lead_id] [int] NULL,
	[lead_visit_id] [int] NULL,
	[campaign_id] [int] NULL,
	[cart_id] [int] NULL,
	[create_date] [datetime] NULL,
	[application_id] [int] NOT NULL,
 CONSTRAINT [PK_pap_transaction] PRIMARY KEY CLUSTERED 
(
	[pap_transaction_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[pap_transaction]  WITH CHECK ADD  CONSTRAINT [fk_pap_transaction_application_id] FOREIGN KEY([application_id])
REFERENCES [dbo].[application] ([application_id])
GO
ALTER TABLE [dbo].[pap_transaction] CHECK CONSTRAINT [fk_pap_transaction_application_id]
GO
ALTER TABLE [dbo].[pap_transaction] ADD  CONSTRAINT [DF_pap_transaction_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
