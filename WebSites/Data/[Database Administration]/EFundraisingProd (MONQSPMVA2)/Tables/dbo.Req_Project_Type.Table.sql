USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Req_Project_Type]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Req_Project_Type](
	[Project_Type_ID] [int] NOT NULL,
	[Language_Id] [int] NOT NULL,
	[Description] [varchar](200) NULL,
 CONSTRAINT [PK_Req_Project_Type] PRIMARY KEY NONCLUSTERED 
(
	[Project_Type_ID] ASC,
	[Language_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Req_Project_Type]  WITH CHECK ADD  CONSTRAINT [fk_reqpt_language_id] FOREIGN KEY([Language_Id])
REFERENCES [dbo].[Req_Language] ([Language_Id])
GO
ALTER TABLE [dbo].[Req_Project_Type] CHECK CONSTRAINT [fk_reqpt_language_id]
GO
