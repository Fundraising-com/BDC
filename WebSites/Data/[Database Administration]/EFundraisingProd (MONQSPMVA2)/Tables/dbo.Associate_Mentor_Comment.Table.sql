USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Associate_Mentor_Comment]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Associate_Mentor_Comment](
	[Ass_Mentor_Comment_ID] [int] NOT NULL,
	[Associate_ID] [int] NOT NULL,
	[Mentor_ID] [int] NOT NULL,
	[Comment_Date] [smalldatetime] NOT NULL,
	[Comments] [varchar](255) NULL,
 CONSTRAINT [PK_Associate_Mentor_Comment] PRIMARY KEY NONCLUSTERED 
(
	[Ass_Mentor_Comment_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Associate_Mentor_Comment]  WITH CHECK ADD  CONSTRAINT [fk_amcomm_associate_id_mentor_id] FOREIGN KEY([Associate_ID], [Mentor_ID])
REFERENCES [dbo].[Associate_Mentor] ([Associate_ID], [Mentor_ID])
GO
ALTER TABLE [dbo].[Associate_Mentor_Comment] CHECK CONSTRAINT [fk_amcomm_associate_id_mentor_id]
GO
