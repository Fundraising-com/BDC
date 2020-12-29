USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[payment]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[payment](
	[payment_id] [int] IDENTITY(20000,1) NOT FOR REPLICATION NOT NULL,
	[payment_type_id] [int] NOT NULL,
	[payment_info_id] [int] NOT NULL,
	[payment_period_id] [int] NOT NULL,
	[cheque_number] [int] NOT NULL,
	[cheque_date] [datetime] NOT NULL,
	[paid_amount] [money] NOT NULL,
	[name] [varchar](100) NOT NULL,
	[phone_number] [varchar](50) NULL,
	[address_1] [varchar](100) NULL,
	[address_2] [varchar](100) NULL,
	[city] [varchar](100) NULL,
	[zip_code] [varchar](10) NULL,
	[country_code] [nvarchar](2) NULL,
	[subdivision_code] [nvarchar](7) NULL,
	[create_date] [datetime] NOT NULL,
	[payment_batch_id] [int] NULL,
	[is_validated] [bit] NULL,
	[is_processed] [bit] NULL,
 CONSTRAINT [PK_payment] PRIMARY KEY CLUSTERED 
(
	[payment_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[payment]  WITH CHECK ADD  CONSTRAINT [FK_payment_country] FOREIGN KEY([country_code])
REFERENCES [dbo].[country] ([country_code])
GO
ALTER TABLE [dbo].[payment] CHECK CONSTRAINT [FK_payment_country]
GO
ALTER TABLE [dbo].[payment]  WITH CHECK ADD  CONSTRAINT [FK_payment_payment_batch] FOREIGN KEY([payment_batch_id])
REFERENCES [dbo].[payment_batch] ([payment_batch_id])
GO
ALTER TABLE [dbo].[payment] CHECK CONSTRAINT [FK_payment_payment_batch]
GO
ALTER TABLE [dbo].[payment]  WITH CHECK ADD  CONSTRAINT [FK_payment_payment_info] FOREIGN KEY([payment_info_id])
REFERENCES [dbo].[payment_info] ([payment_info_id])
GO
ALTER TABLE [dbo].[payment] CHECK CONSTRAINT [FK_payment_payment_info]
GO
ALTER TABLE [dbo].[payment]  WITH CHECK ADD  CONSTRAINT [FK_payment_payment_period] FOREIGN KEY([payment_period_id])
REFERENCES [dbo].[payment_period] ([payment_period_id])
GO
ALTER TABLE [dbo].[payment] CHECK CONSTRAINT [FK_payment_payment_period]
GO
ALTER TABLE [dbo].[payment]  WITH CHECK ADD  CONSTRAINT [FK_payment_payment_type] FOREIGN KEY([payment_type_id])
REFERENCES [dbo].[payment_type] ([payment_type_id])
GO
ALTER TABLE [dbo].[payment] CHECK CONSTRAINT [FK_payment_payment_type]
GO
ALTER TABLE [dbo].[payment]  WITH CHECK ADD  CONSTRAINT [FK_payment_subdivision] FOREIGN KEY([subdivision_code])
REFERENCES [dbo].[subdivision] ([subdivision_code])
GO
ALTER TABLE [dbo].[payment] CHECK CONSTRAINT [FK_payment_subdivision]
GO
ALTER TABLE [dbo].[payment] ADD  CONSTRAINT [DF_payment_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
ALTER TABLE [dbo].[payment] ADD  CONSTRAINT [DF_payment_is_validated]  DEFAULT ((0)) FOR [is_validated]
GO
ALTER TABLE [dbo].[payment] ADD  CONSTRAINT [DF_payment_is_processed]  DEFAULT ((0)) FOR [is_processed]
GO
