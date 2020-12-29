USE [efundstore]
GO

/****** Object:  Table [dbo].[newsletter]    Script Date: 02/12/2015 15:10:04 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[newsletter](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[title] [nvarchar](200) NOT NULL,
	[body] [nvarchar](max) NOT NULL,
	[created_on] [datetime] NOT NULL,
	[enabled] [bit] NOT NULL,
	[author] [int] NOT NULL,
	[updated_on] [datetime] NULL,
	[partner] [int] NULL,
	[display_order] [tinyint] NOT NULL,
 CONSTRAINT [PK_newsletter] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[newsletter]  WITH CHECK ADD  CONSTRAINT [FK_newsletter_online_user] FOREIGN KEY([author])
REFERENCES [dbo].[online_user] ([online_user_id])
GO

ALTER TABLE [dbo].[newsletter] CHECK CONSTRAINT [FK_newsletter_online_user]
GO

ALTER TABLE [dbo].[newsletter] ADD  CONSTRAINT [DF_newsletter_enabled]  DEFAULT ((1)) FOR [enabled]
GO

