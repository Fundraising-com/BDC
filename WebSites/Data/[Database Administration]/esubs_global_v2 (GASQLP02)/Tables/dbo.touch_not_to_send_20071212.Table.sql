USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[touch_not_to_send_20071212]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[touch_not_to_send_20071212](
	[touch_id] [int] NOT NULL,
 CONSTRAINT [PK_touch_not_to_send_20071212] PRIMARY KEY CLUSTERED 
(
	[touch_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
