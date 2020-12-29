USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[campaign_reason]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[campaign_reason](
	[campaign_reason_id] [tinyint] NOT NULL,
	[party_type_id] [tinyint] NOT NULL,
	[campaign_reason_desc] [varchar](50) NOT NULL,
 CONSTRAINT [PK_campaign_reason] PRIMARY KEY CLUSTERED 
(
	[campaign_reason_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[campaign_reason]  WITH NOCHECK ADD  CONSTRAINT [FK_campaign_reason_party_type] FOREIGN KEY([party_type_id])
REFERENCES [dbo].[party_type] ([party_type_id])
GO
ALTER TABLE [dbo].[campaign_reason] CHECK CONSTRAINT [FK_campaign_reason_party_type]
GO
