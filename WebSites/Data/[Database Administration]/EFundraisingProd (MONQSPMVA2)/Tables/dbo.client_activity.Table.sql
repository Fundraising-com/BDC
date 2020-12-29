USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[client_activity]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[client_activity](
	[client_activity_id] [int] NOT NULL,
	[client_id] [int] NOT NULL,
	[client_sequence_code] [char](2) NOT NULL,
	[client_activity_type_id] [tinyint] NOT NULL,
	[client_activity_date] [datetime] NOT NULL,
	[completed_date] [datetime] NULL,
	[comments] [text] NULL,
	[is_contacted] [bit] NOT NULL,
 CONSTRAINT [PK_client_activity] PRIMARY KEY CLUSTERED 
(
	[client_activity_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[client_activity]  WITH NOCHECK ADD  CONSTRAINT [FK_client_activity_client] FOREIGN KEY([client_sequence_code], [client_id])
REFERENCES [dbo].[client] ([client_sequence_code], [client_id])
GO
ALTER TABLE [dbo].[client_activity] CHECK CONSTRAINT [FK_client_activity_client]
GO
ALTER TABLE [dbo].[client_activity]  WITH NOCHECK ADD  CONSTRAINT [FK_client_activity_client_activity_type] FOREIGN KEY([client_activity_type_id])
REFERENCES [dbo].[client_activity_type] ([client_activity_type_id])
GO
ALTER TABLE [dbo].[client_activity] CHECK CONSTRAINT [FK_client_activity_client_activity_type]
GO
ALTER TABLE [dbo].[client_activity] ADD  CONSTRAINT [DF_client_activity_is_contacted]  DEFAULT (0) FOR [is_contacted]
GO
