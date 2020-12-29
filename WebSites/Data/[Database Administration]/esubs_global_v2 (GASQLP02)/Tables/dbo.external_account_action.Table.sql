USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[external_account_action]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[external_account_action](
	[external_account_action_id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[external_account_id] [int] NOT NULL,
	[action_id] [int] NOT NULL,
	[create_date] [datetime] NOT NULL,
 CONSTRAINT [PK_fsm_action] PRIMARY KEY CLUSTERED 
(
	[external_account_action_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[external_account_action] ADD  CONSTRAINT [DF_fsm_action_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
