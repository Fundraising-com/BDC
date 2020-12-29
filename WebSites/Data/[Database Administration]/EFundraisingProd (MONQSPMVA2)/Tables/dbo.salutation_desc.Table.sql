USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[salutation_desc]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[salutation_desc](
	[salutation_id] [tinyint] NOT NULL,
	[language_id] [tinyint] NOT NULL,
	[description] [varchar](15) NOT NULL,
 CONSTRAINT [PK_salutation_desc] PRIMARY KEY CLUSTERED 
(
	[salutation_id] ASC,
	[language_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[salutation_desc]  WITH CHECK ADD  CONSTRAINT [FK_salutation_desc_languages] FOREIGN KEY([language_id])
REFERENCES [dbo].[languages] ([language_id])
GO
ALTER TABLE [dbo].[salutation_desc] CHECK CONSTRAINT [FK_salutation_desc_languages]
GO
ALTER TABLE [dbo].[salutation_desc]  WITH CHECK ADD  CONSTRAINT [FK_salutation_desc_salutation] FOREIGN KEY([salutation_id])
REFERENCES [dbo].[salutation] ([salutation_id])
GO
ALTER TABLE [dbo].[salutation_desc] CHECK CONSTRAINT [FK_salutation_desc_salutation]
GO
