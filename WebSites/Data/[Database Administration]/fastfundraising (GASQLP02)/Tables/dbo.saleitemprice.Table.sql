USE [fastfundraising]
GO
/****** Object:  Table [dbo].[saleitemprice]    Script Date: 02/14/2014 16:35:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[saleitemprice](
	[saleid] [int] NULL,
	[itemid] [int] NULL,
	[tierid] [int] NULL,
	[price] [float] NULL,
	[tierminval] [int] NULL,
	[upperlimit] [int] NULL
) ON [PRIMARY]
GO
