USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[group]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[group](
	[group_id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[parent_group_id] [int] NULL,
	[sponsor_id] [int] NOT NULL,
	[partner_id] [int] NOT NULL,
	[lead_id] [int] NULL,
	[external_group_id] [varchar](20) NULL,
	[group_name] [varchar](200) NULL,
	[test_group] [bit] NOT NULL,
	[expected_membership] [int] NULL,
	[group_url] [varchar](255) NULL,
	[redirect] [varchar](255) NULL,
	[comments] [varchar](1024) NULL,
	[create_date] [datetime] NOT NULL,
 CONSTRAINT [PK_group] PRIMARY KEY CLUSTERED 
(
	[group_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[group]  WITH CHECK ADD  CONSTRAINT [FK_group_group] FOREIGN KEY([parent_group_id])
REFERENCES [dbo].[group] ([group_id])
GO
ALTER TABLE [dbo].[group] CHECK CONSTRAINT [FK_group_group]
GO
ALTER TABLE [dbo].[group]  WITH CHECK ADD  CONSTRAINT [FK_group_member_hierarchy] FOREIGN KEY([sponsor_id])
REFERENCES [dbo].[member_hierarchy] ([member_hierarchy_id])
GO
ALTER TABLE [dbo].[group] CHECK CONSTRAINT [FK_group_member_hierarchy]
GO
ALTER TABLE [dbo].[group] ADD  CONSTRAINT [DF_group_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
