USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[temp_member_bak]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[temp_member_bak](
	[member_id] [int] NOT NULL,
	[email] [varchar](100) NOT NULL,
	[name] [varchar](100) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
