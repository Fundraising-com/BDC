USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[bounce_to_transfer]    Script Date: 02/14/2014 16:26:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[bounce_to_transfer](
	[email_address] [char](100) NOT NULL,
	[bounce_id] [int] NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
