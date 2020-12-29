USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[tellafriend]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tellafriend](
	[tellafriend_id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[name] [varchar](100) NOT NULL,
	[email] [varchar](100) NOT NULL,
	[subject] [varchar](100) NULL,
	[body_html] [varchar](8000) NULL,
	[body_txt] [varchar](8000) NULL,
	[create_date] [datetime] NOT NULL,
 CONSTRAINT [PK_tellafriend] PRIMARY KEY CLUSTERED 
(
	[tellafriend_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[tellafriend] ADD  CONSTRAINT [DF_tellafriend_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
