USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[audit_member_hierarchy]    Script Date: 02/14/2014 16:26:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[audit_member_hierarchy](
	[audit_id] [int] IDENTITY(1,1) NOT NULL,
	[audit_date] [datetime] NOT NULL,
	[audit_user_name] [varchar](200) NULL,
	[audit_opcode] [char](1) NOT NULL,
	[audit_modifier] [varchar](200) NOT NULL,
	[member_hierarchy_id] [int] NOT NULL,
	[parent_member_hierarchy_id] [int] NULL,
	[member_id] [int] NOT NULL,
	[creation_channel_id] [int] NULL,
	[create_date] [datetime] NOT NULL,
	[active] [bit] NOT NULL,
	[unsubscribe] [bit] NOT NULL,
	[unsubscribe_date] [datetime] NULL,
 CONSTRAINT [PK_audit_id_member_hierarchy] PRIMARY KEY CLUSTERED 
(
	[audit_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[audit_member_hierarchy] ADD  CONSTRAINT [DF_member_hierarchy_audit_date]  DEFAULT (getdate()) FOR [audit_date]
GO
