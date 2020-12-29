USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[campaign_merges]    Script Date: 02/14/2014 16:26:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[campaign_merges](
	[original_campaign] [int] NOT NULL,
	[old_campaign] [int] NOT NULL,
	[user_name] [varchar](50) NULL,
	[date_changed] [datetime] NOT NULL,
	[comment] [nvarchar](200) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[campaign_merges] ADD  CONSTRAINT [DF_campaign_merges_date_changed]  DEFAULT (getdate()) FOR [date_changed]
GO
