USE [eFundstore]
GO
/****** Object:  Table [dbo].[survey_choice]    Script Date: 02/14/2014 16:26:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[survey_choice](
	[survey_id] [int] NOT NULL,
	[choice_id] [int] NOT NULL,
 CONSTRAINT [PK_survey_choice] PRIMARY KEY CLUSTERED 
(
	[survey_id] ASC,
	[choice_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[survey_choice]  WITH CHECK ADD  CONSTRAINT [FK_survey_choice_choice] FOREIGN KEY([choice_id])
REFERENCES [dbo].[choice] ([choice_id])
GO
ALTER TABLE [dbo].[survey_choice] CHECK CONSTRAINT [FK_survey_choice_choice]
GO
ALTER TABLE [dbo].[survey_choice]  WITH CHECK ADD  CONSTRAINT [FK_survey_choice_survey] FOREIGN KEY([survey_id])
REFERENCES [dbo].[survey] ([survey_id])
GO
ALTER TABLE [dbo].[survey_choice] CHECK CONSTRAINT [FK_survey_choice_survey]
GO
