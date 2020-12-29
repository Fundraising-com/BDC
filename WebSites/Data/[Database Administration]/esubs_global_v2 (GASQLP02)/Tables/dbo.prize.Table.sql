USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[prize]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[prize](
	[prize_id] [int] NOT NULL,
	[prize_type_id] [int] NOT NULL,
	[prize_name] [varchar](50) NOT NULL,
	[create_date] [datetime] NOT NULL,
	[parent_prize_id] [int] NULL,
 CONSTRAINT [PK_prize] PRIMARY KEY CLUSTERED 
(
	[prize_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[prize]  WITH CHECK ADD  CONSTRAINT [FK_prize_parent_prize] FOREIGN KEY([parent_prize_id])
REFERENCES [dbo].[prize] ([prize_id])
GO
ALTER TABLE [dbo].[prize] CHECK CONSTRAINT [FK_prize_parent_prize]
GO
ALTER TABLE [dbo].[prize]  WITH CHECK ADD  CONSTRAINT [FK_prize_prize_type] FOREIGN KEY([prize_type_id])
REFERENCES [dbo].[prize_type] ([prize_type_id])
GO
ALTER TABLE [dbo].[prize] CHECK CONSTRAINT [FK_prize_prize_type]
GO
ALTER TABLE [dbo].[prize] ADD  CONSTRAINT [DF_prize_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
