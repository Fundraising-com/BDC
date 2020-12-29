USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[payment]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[payment](
	[sales_id] [int] NOT NULL,
	[payment_no] [int] NOT NULL,
	[payment_method_id] [tinyint] NOT NULL,
	[collection_status_id] [int] NULL,
	[payment_entry_date] [datetime] NOT NULL,
	[cashable_date] [datetime] NOT NULL,
	[credit_card_no] [varchar](16) NULL,
	[expiry_date] [varchar](7) NULL,
	[name_on_card] [varchar](50) NULL,
	[authorization_number] [varchar](10) NULL,
	[payment_amount] [decimal](15, 4) NOT NULL,
	[commission_paid] [bit] NOT NULL,
	[foreign_orderid] [int] NULL,
	[create_date] [datetime] NOT NULL,
	[ext_payment_id] [int] NULL,
	[create_user_id] [int] NULL,
	[payment_status_id] [int] NULL,
 CONSTRAINT [PK_payment] PRIMARY KEY CLUSTERED 
(
	[sales_id] ASC,
	[payment_no] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[payment]  WITH NOCHECK ADD  CONSTRAINT [FK_payment_collection_status] FOREIGN KEY([collection_status_id])
REFERENCES [dbo].[Collection_Status] ([Collection_Status_ID])
GO
ALTER TABLE [dbo].[payment] CHECK CONSTRAINT [FK_payment_collection_status]
GO
ALTER TABLE [dbo].[payment]  WITH CHECK ADD  CONSTRAINT [FK_payment_payment_method] FOREIGN KEY([payment_method_id])
REFERENCES [dbo].[payment_method] ([payment_method_id])
GO
ALTER TABLE [dbo].[payment] CHECK CONSTRAINT [FK_payment_payment_method]
GO
ALTER TABLE [dbo].[payment]  WITH CHECK ADD  CONSTRAINT [fk_payment_payment_status] FOREIGN KEY([payment_status_id])
REFERENCES [dbo].[Payment_status] ([Payment_status_id])
GO
ALTER TABLE [dbo].[payment] CHECK CONSTRAINT [fk_payment_payment_status]
GO
ALTER TABLE [dbo].[payment] ADD  CONSTRAINT [DF_payment_commission_paid]  DEFAULT (0) FOR [commission_paid]
GO
ALTER TABLE [dbo].[payment] ADD  CONSTRAINT [DF_payment_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
