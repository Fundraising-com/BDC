USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[generated_pass]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[generated_pass](
	[member_id] [int] NOT NULL,
	[password] [varchar](6) NULL,
	[id] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
