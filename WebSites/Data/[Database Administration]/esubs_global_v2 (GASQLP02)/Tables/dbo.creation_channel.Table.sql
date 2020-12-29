USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[creation_channel]    Script Date: 02/14/2014 16:26:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[creation_channel](
	[creation_channel_id] [int] NOT NULL,
	[creation_channel_name] [varchar](150) NOT NULL,
	[description] [varchar](255) NULL,
	[create_date] [datetime] NOT NULL,
	[active] [bit] NOT NULL,
	[member_type_id] [int] NOT NULL,
	[is_contact] [bit] NULL,
 CONSTRAINT [PK_creation_channel] PRIMARY KEY CLUSTERED 
(
	[creation_channel_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[creation_channel] ADD  CONSTRAINT [DF_creation_channel_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
ALTER TABLE [dbo].[creation_channel] ADD  DEFAULT ((0)) FOR [is_contact]
GO
