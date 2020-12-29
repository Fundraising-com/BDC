USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[featured_event]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[featured_event](
	[event_id] [int] NOT NULL,
	[display_order] [int] NULL,
 CONSTRAINT [ix_featured_event_event_id] UNIQUE CLUSTERED 
(
	[event_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
