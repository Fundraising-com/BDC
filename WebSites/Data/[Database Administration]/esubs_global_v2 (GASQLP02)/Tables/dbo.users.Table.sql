USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[users]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[users](
	[user_id] [int] IDENTITY(1,1) NOT NULL,
	[culture_code] [nvarchar](5) NOT NULL,
	[first_name] [varchar](100) NOT NULL,
	[last_name] [varchar](100) NOT NULL,
	[email_address] [varchar](100) NOT NULL,
	[username] [varchar](100) NOT NULL,
	[password] [varchar](100) NOT NULL,
	[partner_id] [int] NULL,
	[create_date] [datetime] NOT NULL,
	[member_id] [int] NULL,
	[coppa_month] [int] NULL,
	[coppa_year] [int] NULL,
	[agree_term_services] [bit] NULL,
	[unsubscribe] [bit] NULL,
	[unsubscribe_date] [datetime] NULL,
	[opt_status_id] [bit] NOT NULL,
 CONSTRAINT [PK_users] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[users] ADD  DEFAULT ((0)) FOR [partner_id]
GO
ALTER TABLE [dbo].[users] ADD  CONSTRAINT [DF_user_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
ALTER TABLE [dbo].[users] ADD  DEFAULT ((0)) FOR [agree_term_services]
GO
ALTER TABLE [dbo].[users] ADD  DEFAULT ((0)) FOR [opt_status_id]
GO
