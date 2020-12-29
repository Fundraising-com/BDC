USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[event_status]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[event_status](
	[event_status_id] [int] NOT NULL,
	[event_status_name] [varchar](50) NOT NULL,
	[create_date] [datetime] NOT NULL,
 CONSTRAINT [PK_event_status] PRIMARY KEY CLUSTERED 
(
	[event_status_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[event_status] ADD  CONSTRAINT [DF_event_status_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
