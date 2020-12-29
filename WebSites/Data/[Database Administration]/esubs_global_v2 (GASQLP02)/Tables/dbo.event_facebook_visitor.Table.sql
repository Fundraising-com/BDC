USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[event_facebook_visitor]    Script Date: 02/14/2014 16:26:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[event_facebook_visitor](
	[event_facebook_visitor] [int] IDENTITY(1,1) NOT NULL,
	[personalization_id] [int] NOT NULL,
	[facebook_id] [varchar](50) NOT NULL,
	[facebook_image_url] [varchar](500) NOT NULL,
	[facebook_firstname] [varchar](50) NOT NULL,
	[facebook_lastname] [varchar](50) NOT NULL,
	[update_date] [datetime] NOT NULL,
	[create_date] [datetime] NOT NULL,
	[deleted] [bit] NOT NULL,
 CONSTRAINT [PK_event_facebook_visitor] PRIMARY KEY CLUSTERED 
(
	[event_facebook_visitor] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[event_facebook_visitor] ADD  CONSTRAINT [DF_event_facebook_visitor_update_date]  DEFAULT (getdate()) FOR [update_date]
GO
ALTER TABLE [dbo].[event_facebook_visitor] ADD  CONSTRAINT [DF_event_facebook_visitor_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
ALTER TABLE [dbo].[event_facebook_visitor] ADD  DEFAULT ((0)) FOR [deleted]
GO
