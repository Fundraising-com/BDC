USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Req_Priority]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Req_Priority](
	[Priority_Id] [int] NOT NULL,
	[Language_Id] [int] NOT NULL,
	[Description] [varchar](50) NULL,
	[Is_Default] [bit] NOT NULL,
 CONSTRAINT [PK_Req_Priority] PRIMARY KEY NONCLUSTERED 
(
	[Priority_Id] ASC,
	[Language_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Req_Priority]  WITH CHECK ADD  CONSTRAINT [fk_reqp_language_id] FOREIGN KEY([Language_Id])
REFERENCES [dbo].[Req_Language] ([Language_Id])
GO
ALTER TABLE [dbo].[Req_Priority] CHECK CONSTRAINT [fk_reqp_language_id]
GO
