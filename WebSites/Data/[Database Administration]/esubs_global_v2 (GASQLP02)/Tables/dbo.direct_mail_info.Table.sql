USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[direct_mail_info]    Script Date: 02/14/2014 16:26:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[direct_mail_info](
	[direct_mail_info_id] [int] IDENTITY(1,1) NOT NULL,
	[message] [text] NOT NULL,
	[image_url] [varchar](256) NOT NULL,
	[moderated] [bit] NOT NULL,
	[direct_mail_status] [smallint] NOT NULL,
	[create_date] [datetime] NOT NULL,
	[document_path] [varchar](256) NULL,
	[event_participation_id] [int] NULL,
	[member_hierarchy_id] [nchar](10) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
