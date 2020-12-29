USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[prize_to_be_removed]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[prize_to_be_removed](
	[prize_item_id] [int] NOT NULL,
	[prize_id] [int] NOT NULL,
	[prize_item_code] [varchar](40) NOT NULL,
	[expiration_date] [datetime] NOT NULL,
	[create_date] [datetime] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
