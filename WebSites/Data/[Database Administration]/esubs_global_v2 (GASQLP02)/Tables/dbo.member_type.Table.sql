USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[member_type]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[member_type](
	[member_type_id] [smallint] NOT NULL,
	[member_type_name] [varchar](20) NOT NULL,
	[email_description] [varchar](100) NULL,
 CONSTRAINT [PK_member_type] PRIMARY KEY CLUSTERED 
(
	[member_type_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
