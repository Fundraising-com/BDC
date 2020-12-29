USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[audit_users]    Script Date: 02/14/2014 16:26:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[audit_users](
	[audit_id] [int] IDENTITY(1,1) NOT NULL,
	[audit_date] [datetime] NOT NULL,
	[audit_user_name] [varchar](200) NULL,
	[audit_opcode] [char](1) NOT NULL,
	[audit_modifier] [varchar](200) NOT NULL,
	[user_id] [int] NOT NULL,
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
	[opt_status_id] [int] NULL,
 CONSTRAINT [PK_audit_id_users] PRIMARY KEY CLUSTERED 
(
	[audit_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[audit_users] ADD  CONSTRAINT [DF_users_audit_date]  DEFAULT (getdate()) FOR [audit_date]
GO
