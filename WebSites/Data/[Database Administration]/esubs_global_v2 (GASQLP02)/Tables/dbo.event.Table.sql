USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[event]    Script Date: 02/14/2014 16:26:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[event](
	[event_id] [int] IDENTITY(1000000,1) NOT FOR REPLICATION NOT NULL,
	[event_type_id] [int] NOT NULL,
	[culture_code] [nvarchar](5) NOT NULL,
	[event_name] [varchar](200) NULL,
	[start_date] [datetime] NOT NULL,
	[end_date] [datetime] NULL,
	[active] [bit] NOT NULL,
	[comments] [varchar](1024) NULL,
	[create_date] [datetime] NOT NULL,
	[redirect] [varchar](255) NULL,
	[displayable] [bit] NOT NULL,
	[want_sales_rep_call] [bit] NULL,
	[group_type_id] [int] NOT NULL,
	[processing_fee] [bit] NULL,
	[profit_calculated] [float] NOT NULL,
	[event_status_id] [int] NOT NULL,
	[profit_group_id] [int] NOT NULL,
	[donation] [bit] NOT NULL,
	[date_of_event] [datetime] NULL,
	[discount_site] [bit] NULL,
	[humeur_representative] [varchar](200) NULL,
 CONSTRAINT [PK_event] PRIMARY KEY CLUSTERED 
(
	[event_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[event]  WITH CHECK ADD  CONSTRAINT [FK_event_culture] FOREIGN KEY([culture_code])
REFERENCES [dbo].[culture] ([culture_code])
GO
ALTER TABLE [dbo].[event] CHECK CONSTRAINT [FK_event_culture]
GO
ALTER TABLE [dbo].[event]  WITH CHECK ADD  CONSTRAINT [FK_event_event_status] FOREIGN KEY([event_status_id])
REFERENCES [dbo].[event_status] ([event_status_id])
GO
ALTER TABLE [dbo].[event] CHECK CONSTRAINT [FK_event_event_status]
GO
ALTER TABLE [dbo].[event]  WITH CHECK ADD  CONSTRAINT [FK_event_event_type] FOREIGN KEY([event_type_id])
REFERENCES [dbo].[event_type] ([event_type_id])
GO
ALTER TABLE [dbo].[event] CHECK CONSTRAINT [FK_event_event_type]
GO
ALTER TABLE [dbo].[event]  WITH CHECK ADD  CONSTRAINT [grouptype_id_fk] FOREIGN KEY([group_type_id])
REFERENCES [dbo].[group_type] ([group_type_id])
GO
ALTER TABLE [dbo].[event] CHECK CONSTRAINT [grouptype_id_fk]
GO
ALTER TABLE [dbo].[event] ADD  CONSTRAINT [DF_event_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
ALTER TABLE [dbo].[event] ADD  CONSTRAINT [DF_event_displayable]  DEFAULT (1) FOR [displayable]
GO
ALTER TABLE [dbo].[event] ADD  CONSTRAINT [DF_event_want_sales_rep_call]  DEFAULT (0) FOR [want_sales_rep_call]
GO
ALTER TABLE [dbo].[event] ADD  DEFAULT ((1)) FOR [group_type_id]
GO
ALTER TABLE [dbo].[event] ADD  CONSTRAINT [DF_processing_fee]  DEFAULT ((0)) FOR [processing_fee]
GO
ALTER TABLE [dbo].[event] ADD  CONSTRAINT [DF_profit_calc]  DEFAULT ((40)) FOR [profit_calculated]
GO
ALTER TABLE [dbo].[event] ADD  DEFAULT ((0)) FOR [event_status_id]
GO
ALTER TABLE [dbo].[event] ADD  DEFAULT ((2)) FOR [profit_group_id]
GO
ALTER TABLE [dbo].[event] ADD  CONSTRAINT [DF__event__donation__72C6D7A1]  DEFAULT ((1)) FOR [donation]
GO
ALTER TABLE [dbo].[event] ADD  DEFAULT ((0)) FOR [discount_site]
GO
