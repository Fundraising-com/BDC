USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[client]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[client](
	[client_sequence_code] [char](2) NOT NULL,
	[client_id] [int] NOT NULL,
	[organization_class_code] [varchar](4) NOT NULL,
	[group_type_id] [tinyint] NULL,
	[channel_code] [varchar](4) NOT NULL,
	[promotion_id] [int] NOT NULL,
	[lead_id] [int] NULL,
	[division_id] [tinyint] NOT NULL,
	[csr_consultant_id] [int] NULL,
	[title_id] [tinyint] NULL,
	[salutation] [varchar](10) NULL,
	[first_name] [varchar](50) NOT NULL,
	[last_name] [varchar](50) NOT NULL,
	[title] [varchar](50) NULL,
	[organization] [varchar](100) NULL,
	[day_phone] [varchar](20) NULL,
	[day_time_call] [varchar](45) NULL,
	[evening_phone] [varchar](20) NULL,
	[evening_time_call] [varchar](20) NULL,
	[fax] [varchar](20) NULL,
	[email] [varchar](50) NULL,
	[extra_comment] [text] NULL,
	[interested_in_agent] [bit] NOT NULL,
	[interested_in_online] [bit] NOT NULL,
	[day_phone_ext] [varchar](10) NULL,
	[evening_phone_ext] [varchar](10) NULL,
	[other_phone] [varchar](20) NULL,
	[other_phone_ext] [varchar](10) NULL,
 CONSTRAINT [PK_client] PRIMARY KEY CLUSTERED 
(
	[client_sequence_code] ASC,
	[client_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[client]  WITH NOCHECK ADD  CONSTRAINT [FK_channel_code] FOREIGN KEY([channel_code])
REFERENCES [dbo].[Lead_Channel] ([Channel_Code])
GO
ALTER TABLE [dbo].[client] CHECK CONSTRAINT [FK_channel_code]
GO
ALTER TABLE [dbo].[client]  WITH NOCHECK ADD  CONSTRAINT [FK_client_consultant] FOREIGN KEY([csr_consultant_id])
REFERENCES [dbo].[consultant] ([consultant_id])
GO
ALTER TABLE [dbo].[client] CHECK CONSTRAINT [FK_client_consultant]
GO
ALTER TABLE [dbo].[client]  WITH CHECK ADD  CONSTRAINT [FK_client_division] FOREIGN KEY([division_id])
REFERENCES [dbo].[division] ([division_id])
GO
ALTER TABLE [dbo].[client] CHECK CONSTRAINT [FK_client_division]
GO
ALTER TABLE [dbo].[client]  WITH NOCHECK ADD  CONSTRAINT [FK_client_lead] FOREIGN KEY([lead_id])
REFERENCES [dbo].[lead] ([lead_id])
GO
ALTER TABLE [dbo].[client] CHECK CONSTRAINT [FK_client_lead]
GO
ALTER TABLE [dbo].[client]  WITH NOCHECK ADD  CONSTRAINT [FK_client_sequence_code] FOREIGN KEY([client_sequence_code])
REFERENCES [dbo].[client_sequence] ([client_sequence_code])
GO
ALTER TABLE [dbo].[client] CHECK CONSTRAINT [FK_client_sequence_code]
GO
ALTER TABLE [dbo].[client]  WITH CHECK ADD  CONSTRAINT [FK_client_title] FOREIGN KEY([title_id])
REFERENCES [dbo].[title] ([title_id])
GO
ALTER TABLE [dbo].[client] CHECK CONSTRAINT [FK_client_title]
GO
ALTER TABLE [dbo].[client]  WITH NOCHECK ADD  CONSTRAINT [FK_organization_class_code] FOREIGN KEY([organization_class_code])
REFERENCES [dbo].[Organization_Class] ([Organization_Class_Code])
GO
ALTER TABLE [dbo].[client] CHECK CONSTRAINT [FK_organization_class_code]
GO
ALTER TABLE [dbo].[client] ADD  CONSTRAINT [DF_client_interested_in_agent]  DEFAULT (0) FOR [interested_in_agent]
GO
ALTER TABLE [dbo].[client] ADD  CONSTRAINT [DF_client_interested_in_online]  DEFAULT (0) FOR [interested_in_online]
GO
