USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[touch_info]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[touch_info](
	[touch_info_id] [int] IDENTITY(1000000,1) NOT FOR REPLICATION NOT NULL,
	[business_rule_id] [int] NULL,
	[visitor_log_id] [int] NULL,
	[launch_date] [datetime] NOT NULL,
	[create_date] [datetime] NOT NULL,
	[reminder_interval_day] [int] NULL,
 CONSTRAINT [PK_touch_info] PRIMARY KEY CLUSTERED 
(
	[touch_info_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[touch_info]  WITH NOCHECK ADD  CONSTRAINT [FK_touch_info_business_rule] FOREIGN KEY([business_rule_id])
REFERENCES [dbo].[business_rule] ([business_rule_id])
GO
ALTER TABLE [dbo].[touch_info] CHECK CONSTRAINT [FK_touch_info_business_rule]
GO
ALTER TABLE [dbo].[touch_info] ADD  CONSTRAINT [DF_touch_info_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
ALTER TABLE [dbo].[touch_info] ADD  DEFAULT ((0)) FOR [reminder_interval_day]
GO
