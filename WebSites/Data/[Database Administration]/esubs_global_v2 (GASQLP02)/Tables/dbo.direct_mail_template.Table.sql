USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[direct_mail_template]    Script Date: 02/14/2014 16:26:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[direct_mail_template](
	[direct_mail_id] [int] IDENTITY(1,1) NOT NULL,
	[message] [text] NOT NULL,
	[image_url] [varchar](256) NOT NULL,
	[create_date] [datetime] NOT NULL,
	[document_path] [varchar](256) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
