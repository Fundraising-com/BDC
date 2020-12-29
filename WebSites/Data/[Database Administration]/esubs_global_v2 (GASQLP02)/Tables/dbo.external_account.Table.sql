USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[external_account]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[external_account](
	[external_account_id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[food_account_id] [int] NOT NULL,
	[fsm_id] [varchar](4) NULL,
	[online_account_id] [int] NULL,
	[event_participation_id] [int] NULL,
	[touch_id] [int] NULL,
	[create_date] [datetime] NOT NULL,
 CONSTRAINT [PK_external_account] PRIMARY KEY CLUSTERED 
(
	[external_account_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[external_account] ADD  CONSTRAINT [DF_external_account_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
