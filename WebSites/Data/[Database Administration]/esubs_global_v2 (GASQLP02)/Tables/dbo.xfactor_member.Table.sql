USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[xfactor_member]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[xfactor_member](
	[external_member_id] [varchar](128) NOT NULL,
	[external_group_id] [varchar](128) NOT NULL,
	[partner_id] [int] NOT NULL,
	[first_name] [varchar](100) NULL,
	[middle_name] [varchar](100) NULL,
	[last_name] [varchar](100) NULL,
	[email_address] [varchar](100) NULL,
	[culture_code] [nvarchar](5) NULL,
	[opt_status_id] [int] NULL,
	[creation_channel_id] [int] NULL,
	[password] [varchar](100) NULL,
	[comments] [varchar](1024) NULL,
	[created_date] [datetime] NULL,
	[deleted] [bit] NULL,
 CONSTRAINT [PK_xfactor_member] PRIMARY KEY CLUSTERED 
(
	[external_member_id] ASC,
	[external_group_id] ASC,
	[partner_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[xfactor_member] ADD  CONSTRAINT [DF_xfactor_member_deleted]  DEFAULT (0) FOR [deleted]
GO
