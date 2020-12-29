USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[client_address]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[client_address](
	[address_id] [int] NOT NULL,
	[client_sequence_code] [char](2) NOT NULL,
	[client_id] [int] NOT NULL,
	[address_type] [char](2) NOT NULL,
	[street_address] [varchar](100) NULL,
	[state_code] [varchar](10) NOT NULL,
	[country_code] [varchar](10) NOT NULL,
	[city] [varchar](50) NOT NULL,
	[zip_code] [varchar](10) NULL,
	[attention_of] [varchar](100) NULL,
	[matching_code] [varchar](50) NULL,
	[address_zone_id] [int] NOT NULL,
	[phone_1] [varchar](20) NULL,
	[phone_2] [varchar](20) NULL,
	[Location] [varchar](100) NULL,
	[pick_up] [bit] NOT NULL,
	[warehouse_id] [int] NULL,
 CONSTRAINT [PK_Client_Address] PRIMARY KEY CLUSTERED 
(
	[address_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[client_address]  WITH CHECK ADD  CONSTRAINT [FK_client_activity_country_code] FOREIGN KEY([country_code])
REFERENCES [dbo].[Country] ([Country_Code])
GO
ALTER TABLE [dbo].[client_address] CHECK CONSTRAINT [FK_client_activity_country_code]
GO
ALTER TABLE [dbo].[client_address]  WITH CHECK ADD  CONSTRAINT [FK_client_activity_state_code] FOREIGN KEY([state_code])
REFERENCES [dbo].[State] ([State_Code])
GO
ALTER TABLE [dbo].[client_address] CHECK CONSTRAINT [FK_client_activity_state_code]
GO
ALTER TABLE [dbo].[client_address]  WITH NOCHECK ADD  CONSTRAINT [FK_client_address_client] FOREIGN KEY([client_sequence_code], [client_id])
REFERENCES [dbo].[client] ([client_sequence_code], [client_id])
GO
ALTER TABLE [dbo].[client_address] CHECK CONSTRAINT [FK_client_address_client]
GO
ALTER TABLE [dbo].[client_address]  WITH CHECK ADD  CONSTRAINT [FK_client_address_client_address_type] FOREIGN KEY([address_type])
REFERENCES [dbo].[client_address_type] ([address_type])
GO
ALTER TABLE [dbo].[client_address] CHECK CONSTRAINT [FK_client_address_client_address_type]
GO
ALTER TABLE [dbo].[client_address] ADD  CONSTRAINT [DF_client_address_adress_zone_id]  DEFAULT (3) FOR [address_zone_id]
GO
ALTER TABLE [dbo].[client_address] ADD  CONSTRAINT [DF_client_address_pick_up]  DEFAULT (0) FOR [pick_up]
GO
