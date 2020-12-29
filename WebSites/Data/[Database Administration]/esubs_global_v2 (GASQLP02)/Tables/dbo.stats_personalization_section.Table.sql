USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[stats_personalization_section]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[stats_personalization_section](
	[stats_personalization_section_id] [int] IDENTITY(1,1) NOT NULL,
	[description] [varchar](50) NOT NULL,
	[create_date] [datetime] NOT NULL,
 CONSTRAINT [PK_stat_personalization_section] PRIMARY KEY CLUSTERED 
(
	[stats_personalization_section_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[stats_personalization_section] ADD  CONSTRAINT [DF_stat_personalization_section_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
