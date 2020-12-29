USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[direct_mail]    Script Date: 02/14/2014 16:26:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[direct_mail](
	[direct_mail_id] [int] IDENTITY(1,1) NOT NULL,
	[direct_mail_info_id] [int] NOT NULL,
	[direct_mail_status] [smallint] NOT NULL,
	[event_participation_id] [int] NOT NULL,
	[member_hierarchy_id] [int] NOT NULL,
	[postal_address_id] [int] NOT NULL,
	[create_date] [datetime] NOT NULL
) ON [PRIMARY]
GO
