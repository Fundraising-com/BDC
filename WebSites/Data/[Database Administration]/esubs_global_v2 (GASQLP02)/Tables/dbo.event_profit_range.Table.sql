USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[event_profit_range]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[event_profit_range](
	[event_profit_range_id] [int] IDENTITY(1,1) NOT NULL,
	[event_id] [int] NULL,
	[profit_id] [int] NULL,
	[profit_range_id] [int] NULL,
	[create_date] [datetime] NULL,
	[cancelled_date] [datetime] NULL,
	[is_cancelled] [bit] NULL,
 CONSTRAINT [PK_event_profit_range] PRIMARY KEY CLUSTERED 
(
	[event_profit_range_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[event_profit_range]  WITH CHECK ADD  CONSTRAINT [FK_event_profit_range_event] FOREIGN KEY([event_id])
REFERENCES [dbo].[event] ([event_id])
GO
ALTER TABLE [dbo].[event_profit_range] CHECK CONSTRAINT [FK_event_profit_range_event]
GO
ALTER TABLE [dbo].[event_profit_range] ADD  CONSTRAINT [DF_event_profit_range_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
ALTER TABLE [dbo].[event_profit_range] ADD  CONSTRAINT [DF_event_profit_range_is_cancelled]  DEFAULT ((0)) FOR [is_cancelled]
GO
