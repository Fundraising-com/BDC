USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[audit_personalization_image]    Script Date: 02/14/2014 16:26:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[audit_personalization_image](
	[audit_id] [int] IDENTITY(1,1) NOT NULL,
	[audit_date] [datetime] NOT NULL,
	[audit_user_name] [varchar](200) NULL,
	[audit_opcode] [char](1) NOT NULL,
	[audit_modifier] [varchar](200) NOT NULL,
	[image_id] [int] NOT NULL,
	[personalization_id] [int] NOT NULL,
	[image_url] [varchar](255) NULL,
	[deleted] [bit] NOT NULL,
	[create_date] [datetime] NOT NULL,
	[isCoverAlbum] [bit] NULL,
	[image_approval_status_id] [int] NOT NULL,
	[approver_name] [varchar](255) NULL,
	[approved_date] [datetime] NULL,
 CONSTRAINT [PK_audit_id_personalization_image] PRIMARY KEY CLUSTERED 
(
	[audit_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[audit_personalization_image] ADD  CONSTRAINT [DF_personalization_image_audit_date]  DEFAULT (getdate()) FOR [audit_date]
GO
