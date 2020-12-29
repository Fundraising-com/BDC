USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[efrtrans1]    Script Date: 02/14/2014 16:26:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[efrtrans1](
	[First Name] [nvarchar](255) NULL,
	[Last Name] [nvarchar](255) NULL,
	[Email] [nvarchar](255) NULL,
	[Order] [float] NULL,
	[ID] [float] NULL,
	[Total] [money] NULL,
	[Event ID] [float] NULL,
	[Comments] [nvarchar](255) NULL
) ON [PRIMARY]
GO
