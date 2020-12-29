USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[best_time_call_desc]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[best_time_call_desc](
	[best_time_call_id] [tinyint] NOT NULL,
	[language_id] [tinyint] NOT NULL,
	[description] [varchar](25) NOT NULL,
 CONSTRAINT [PK_best_time_call_desc] PRIMARY KEY CLUSTERED 
(
	[best_time_call_id] ASC,
	[language_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[best_time_call_desc]  WITH CHECK ADD  CONSTRAINT [FK_best_time_call_desc_best_time_call] FOREIGN KEY([best_time_call_id])
REFERENCES [dbo].[best_time_call] ([best_time_call_id])
GO
ALTER TABLE [dbo].[best_time_call_desc] CHECK CONSTRAINT [FK_best_time_call_desc_best_time_call]
GO
ALTER TABLE [dbo].[best_time_call_desc]  WITH CHECK ADD  CONSTRAINT [FK_best_time_call_desc_languages] FOREIGN KEY([language_id])
REFERENCES [dbo].[languages] ([language_id])
GO
ALTER TABLE [dbo].[best_time_call_desc] CHECK CONSTRAINT [FK_best_time_call_desc_languages]
GO
