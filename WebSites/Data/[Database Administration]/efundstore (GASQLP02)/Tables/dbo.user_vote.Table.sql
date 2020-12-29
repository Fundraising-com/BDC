USE [eFundstore]
GO
/****** Object:  Table [dbo].[user_vote]    Script Date: 02/14/2014 16:26:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[user_vote](
	[session_id] [char](20) NOT NULL,
	[choice_id] [int] NOT NULL,
	[survey_id] [int] NOT NULL,
 CONSTRAINT [PK_user_vote] PRIMARY KEY CLUSTERED 
(
	[session_id] ASC,
	[survey_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[user_vote]  WITH CHECK ADD  CONSTRAINT [FK_user_vote_choice] FOREIGN KEY([choice_id])
REFERENCES [dbo].[choice] ([choice_id])
GO
ALTER TABLE [dbo].[user_vote] CHECK CONSTRAINT [FK_user_vote_choice]
GO
ALTER TABLE [dbo].[user_vote]  WITH CHECK ADD  CONSTRAINT [FK_user_vote_survey] FOREIGN KEY([survey_id])
REFERENCES [dbo].[survey] ([survey_id])
GO
ALTER TABLE [dbo].[user_vote] CHECK CONSTRAINT [FK_user_vote_survey]
GO
