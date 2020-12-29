USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[dm_personalization_image]    Script Date: 02/14/2014 16:26:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[dm_personalization_image](
	[image_id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[direct_mail_info_id] [int] NOT NULL,
	[image_url] [varchar](255) NULL,
	[deleted] [bit] NOT NULL,
	[create_date] [datetime] NOT NULL,
	[isCoverAlbum] [bit] NULL,
	[image_approval_status_id] [int] NOT NULL,
	[approver_name] [varchar](255) NULL,
	[approved_date] [datetime] NULL,
 CONSTRAINT [PK_dm_personalization_image] PRIMARY KEY CLUSTERED 
(
	[image_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[dm_personalization_image] ADD  CONSTRAINT [DF_dm_personalization_image_deleted]  DEFAULT ((0)) FOR [deleted]
GO
ALTER TABLE [dbo].[dm_personalization_image] ADD  CONSTRAINT [DF_dm_personalization_image_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
ALTER TABLE [dbo].[dm_personalization_image] ADD  DEFAULT ((1)) FOR [image_approval_status_id]
GO
