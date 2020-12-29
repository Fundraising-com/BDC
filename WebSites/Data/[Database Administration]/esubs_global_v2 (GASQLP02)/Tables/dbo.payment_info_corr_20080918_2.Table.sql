USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[payment_info_corr_20080918_2]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[payment_info_corr_20080918_2](
	[group_id] [int] NULL,
	[postal_address_id] [int] NOT NULL,
	[phone_number_id] [int] NULL,
	[payment_name] [varchar](100) NOT NULL,
	[on_behalf_of_name] [varchar](100) NULL,
	[ship_to_name] [varchar](100) NULL,
	[ssn] [varchar](50) NULL,
	[active] [int] NOT NULL,
	[event_id] [int] NOT NULL,
	[done] [bit] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[payment_info_corr_20080918_2] ADD  CONSTRAINT [DF_payment_info_corr_20080918_2_done]  DEFAULT (0) FOR [done]
GO
