USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[tellafriend_recipient]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tellafriend_recipient](
	[tellafriend_recipient_id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[tellafriend_id] [int] NOT NULL,
	[email] [varchar](100) NOT NULL,
	[name] [varchar](100) NOT NULL,
	[processed] [tinyint] NOT NULL,
	[create_date] [datetime] NOT NULL,
 CONSTRAINT [PK_tellafriend_recipient] PRIMARY KEY CLUSTERED 
(
	[tellafriend_recipient_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[tellafriend_recipient]  WITH CHECK ADD  CONSTRAINT [FK_tellafriend_recipient] FOREIGN KEY([tellafriend_id])
REFERENCES [dbo].[tellafriend] ([tellafriend_id])
GO
ALTER TABLE [dbo].[tellafriend_recipient] CHECK CONSTRAINT [FK_tellafriend_recipient]
GO
ALTER TABLE [dbo].[tellafriend_recipient] ADD  DEFAULT (0) FOR [processed]
GO
ALTER TABLE [dbo].[tellafriend_recipient] ADD  CONSTRAINT [DF_tellafriend_recipient_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
