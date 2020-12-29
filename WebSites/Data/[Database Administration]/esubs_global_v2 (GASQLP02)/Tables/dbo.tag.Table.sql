USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[tag]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tag](
	[tag_id] [int] IDENTITY(1,1) NOT NULL,
	[start_tag_name] [varchar](100) NOT NULL,
	[end_tag_name] [varchar](100) NOT NULL,
	[description] [varchar](4000) NOT NULL,
	[create_date] [datetime] NOT NULL,
 CONSTRAINT [PK_tag] PRIMARY KEY CLUSTERED 
(
	[tag_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[tag] ADD  DEFAULT (getdate()) FOR [create_date]
GO
