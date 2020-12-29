USE [eFundstore]
GO
/****** Object:  Table [dbo].[online_user]    Script Date: 02/14/2014 16:26:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[online_user](
	[online_user_id] [int] IDENTITY(1,1) NOT NULL,
	[client_sequence_code] [char](2) NOT NULL,
	[client_id] [int] NOT NULL,
	[email] [varchar](75) NOT NULL,
	[online_user_pwd] [varbinary](30) NOT NULL,
	[date_created] [datetime] NOT NULL,
 CONSTRAINT [PK_online_users] PRIMARY KEY CLUSTERED 
(
	[online_user_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY],
 CONSTRAINT [CK_online_users_email] UNIQUE NONCLUSTERED 
(
	[email] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[online_user] ADD  CONSTRAINT [DF_online_users_date_created]  DEFAULT (getdate()) FOR [date_created]
GO
