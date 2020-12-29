USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[prize_item]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[prize_item](
	[prize_item_id] [int] IDENTITY(100000,1) NOT FOR REPLICATION NOT NULL,
	[prize_id] [int] NOT NULL,
	[prize_item_code] [varchar](255) NULL,
	[expiration_date] [datetime] NOT NULL,
	[create_date] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[prize_item_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[prize_item]  WITH CHECK ADD  CONSTRAINT [FK_prize_item_prize] FOREIGN KEY([prize_id])
REFERENCES [dbo].[prize] ([prize_id])
GO
ALTER TABLE [dbo].[prize_item] CHECK CONSTRAINT [FK_prize_item_prize]
GO
ALTER TABLE [dbo].[prize_item] ADD  CONSTRAINT [DF_prize_item_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
