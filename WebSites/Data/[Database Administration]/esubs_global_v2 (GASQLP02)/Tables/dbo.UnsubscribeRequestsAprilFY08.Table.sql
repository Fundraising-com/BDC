USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[UnsubscribeRequestsAprilFY08]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UnsubscribeRequestsAprilFY08](
	[Date] [datetime] NULL,
	[Event ID] [nvarchar](255) NULL,
	[Name] [nvarchar](255) NULL,
	[Email] [nvarchar](255) NULL,
	[Date Unsubsribed] [nvarchar](255) NULL
) ON [PRIMARY]
GO
