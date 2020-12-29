USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[payment_info]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[payment_info](
	[payment_info_id] [int] IDENTITY(800000,1) NOT FOR REPLICATION NOT NULL,
	[group_id] [int] NULL,
	[postal_address_id] [int] NULL,
	[phone_number_id] [int] NULL,
	[payment_name] [varchar](100) NOT NULL,
	[on_behalf_of_name] [varchar](100) NULL,
	[ship_to_name] [varchar](100) NULL,
	[ssn] [varchar](50) NULL,
	[active] [bit] NOT NULL,
	[create_date] [datetime] NOT NULL,
	[event_id] [int] NULL,
 CONSTRAINT [PK_payment_info] PRIMARY KEY CLUSTERED 
(
	[payment_info_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[payment_info]  WITH CHECK ADD  CONSTRAINT [FK_payment_info_group] FOREIGN KEY([group_id])
REFERENCES [dbo].[group] ([group_id])
GO
ALTER TABLE [dbo].[payment_info] CHECK CONSTRAINT [FK_payment_info_group]
GO
ALTER TABLE [dbo].[payment_info]  WITH CHECK ADD  CONSTRAINT [FK_payment_info_postal_address] FOREIGN KEY([postal_address_id])
REFERENCES [dbo].[postal_address] ([postal_address_id])
GO
ALTER TABLE [dbo].[payment_info] CHECK CONSTRAINT [FK_payment_info_postal_address]
GO
ALTER TABLE [dbo].[payment_info] ADD  CONSTRAINT [DF_payment_info_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
