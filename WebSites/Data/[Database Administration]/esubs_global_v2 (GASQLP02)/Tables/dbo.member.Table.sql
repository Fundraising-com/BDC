USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[member]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[member](
	[member_id] [int] IDENTITY(1000000,1) NOT FOR REPLICATION NOT NULL,
	[culture_code] [nvarchar](5) NOT NULL,
	[opt_status_id] [int] NOT NULL,
	[first_name] [varchar](100) NOT NULL,
	[middle_name] [varchar](100) NULL,
	[last_name] [varchar](100) NOT NULL,
	[gender] [char](1) NULL,
	[email_address] [varchar](100) NULL,
	[password] [varchar](100) NULL,
	[bounced] [bit] NOT NULL,
	[external_member_id] [varchar](20) NULL,
	[comments] [varchar](1024) NULL,
	[create_date] [datetime] NOT NULL,
	[parent_first_name] [varchar](100) NULL,
	[parent_last_name] [varchar](100) NULL,
	[partner_id] [int] NULL,
	[lead_id] [int] NULL,
	[unsubscribe] [bit] NULL,
	[unsubscribe_date] [datetime] NULL,
	[facebook_id] [int] NULL,
	[user_id] [int] NULL,
	[deleted] [bit] NOT NULL,
	[greeting] [varchar](100) NULL,
 CONSTRAINT [PK_member] PRIMARY KEY CLUSTERED 
(
	[member_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[member] ADD  CONSTRAINT [DF_member_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
ALTER TABLE [dbo].[member] ADD  CONSTRAINT [DF__member__partner___5EDF0F2E]  DEFAULT ((0)) FOR [partner_id]
GO
ALTER TABLE [dbo].[member] ADD  CONSTRAINT [DF_member_unsubscribe]  DEFAULT ((0)) FOR [unsubscribe]
GO
ALTER TABLE [dbo].[member] ADD  CONSTRAINT [DF_member_unsubscribe_date]  DEFAULT (NULL) FOR [unsubscribe_date]
GO
ALTER TABLE [dbo].[member] ADD  CONSTRAINT [DF__member__deleted__06CDD04E]  DEFAULT ((0)) FOR [deleted]
GO
