USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[partner_payment_info]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[partner_payment_info](
	[partner_payment_info_id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[partner_id] [int] NOT NULL,
	[payment_info_id] [int] NOT NULL,
	[active] [bit] NOT NULL,
	[create_date] [datetime] NOT NULL,
 CONSTRAINT [PK_partner_payment_info_id] PRIMARY KEY CLUSTERED 
(
	[partner_payment_info_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[partner_payment_info] ADD  CONSTRAINT [DF_partner_payment_info_id_active]  DEFAULT (1) FOR [active]
GO
ALTER TABLE [dbo].[partner_payment_info] ADD  CONSTRAINT [DF_partner_payment_info_id_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
