USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[temp_group_member]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[temp_group_member](
	[partner_id] [int] NULL,
	[email] [varchar](100) NOT NULL,
	[organization_id] [int] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
