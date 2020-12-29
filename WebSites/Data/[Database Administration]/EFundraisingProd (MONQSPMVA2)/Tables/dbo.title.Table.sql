USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[title]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[title](
	[title_id] [tinyint] NOT NULL,
	[party_type_id] [tinyint] NOT NULL,
	[title_desc] [varchar](50) NOT NULL,
 CONSTRAINT [PK_title] PRIMARY KEY CLUSTERED 
(
	[title_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[title]  WITH NOCHECK ADD  CONSTRAINT [FK_title_party_type] FOREIGN KEY([party_type_id])
REFERENCES [dbo].[party_type] ([party_type_id])
GO
ALTER TABLE [dbo].[title] CHECK CONSTRAINT [FK_title_party_type]
GO
