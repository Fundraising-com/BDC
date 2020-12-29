USE [eFundweb]
GO
/****** Object:  Table [dbo].[Template]    Script Date: 02/14/2014 16:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Template](
	[Template_ID] [int] NOT NULL,
	[Language_ID] [int] NOT NULL,
	[Partner_ID] [int] NOT NULL,
	[Template_Path] [varchar](120) NOT NULL,
	[Is_Default] [bit] NULL,
 CONSTRAINT [PK_Template] PRIMARY KEY CLUSTERED 
(
	[Template_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Template]  WITH NOCHECK ADD  CONSTRAINT [FK_Template_Language] FOREIGN KEY([Language_ID])
REFERENCES [dbo].[Language] ([Language_ID])
GO
ALTER TABLE [dbo].[Template] CHECK CONSTRAINT [FK_Template_Language]
GO
