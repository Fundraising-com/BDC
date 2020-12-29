USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[_tbd_promotion_destination]    Script Date: 02/14/2014 16:26:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[_tbd_promotion_destination](
	[promotion_destination_id] [int] NOT NULL,
	[promotion_destination_url] [varchar](255) NOT NULL,
	[create_date] [datetime] NOT NULL,
 CONSTRAINT [PK_promotion_destination] PRIMARY KEY CLUSTERED 
(
	[promotion_destination_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[_tbd_promotion_destination] ADD  CONSTRAINT [DF_promotion_destination_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
