USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[crm_users]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[crm_users](
	[consultant_ID] [char](10) NULL,
	[user_name] [varchar](50) NOT NULL,
	[password] [varchar](50) NOT NULL,
	[access_level] [int] NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
