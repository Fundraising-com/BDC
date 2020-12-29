USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[campaign_reason_desc]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[campaign_reason_desc](
	[campaign_reason_id] [tinyint] NOT NULL,
	[language_id] [tinyint] NOT NULL,
	[description] [varchar](100) NOT NULL,
 CONSTRAINT [PK_campaign_reason_desc] PRIMARY KEY CLUSTERED 
(
	[campaign_reason_id] ASC,
	[language_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[campaign_reason_desc]  WITH CHECK ADD  CONSTRAINT [FK_campaign_reason_desc_campaign_reason] FOREIGN KEY([campaign_reason_id])
REFERENCES [dbo].[campaign_reason] ([campaign_reason_id])
GO
ALTER TABLE [dbo].[campaign_reason_desc] CHECK CONSTRAINT [FK_campaign_reason_desc_campaign_reason]
GO
ALTER TABLE [dbo].[campaign_reason_desc]  WITH CHECK ADD  CONSTRAINT [FK_campaign_reason_desc_languages] FOREIGN KEY([language_id])
REFERENCES [dbo].[languages] ([language_id])
GO
ALTER TABLE [dbo].[campaign_reason_desc] CHECK CONSTRAINT [FK_campaign_reason_desc_languages]
GO
