USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[General_Comment]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[General_Comment](
	[General_Comment_Id] [int] IDENTITY(26,1) NOT FOR REPLICATION NOT NULL,
	[Lead_Id] [int] NULL,
	[Sales_Id] [int] NULL,
	[Entry_Date] [smalldatetime] NOT NULL,
	[General_Comment] [text] NULL,
	[User_Name] [varchar](50) NOT NULL,
	[Department_ID] [int] NOT NULL,
 CONSTRAINT [PK_General_Comment] PRIMARY KEY NONCLUSTERED 
(
	[General_Comment_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[General_Comment]  WITH NOCHECK ADD  CONSTRAINT [fk_gc_department_id] FOREIGN KEY([Department_ID])
REFERENCES [dbo].[Department] ([Department_Id])
GO
ALTER TABLE [dbo].[General_Comment] CHECK CONSTRAINT [fk_gc_department_id]
GO
ALTER TABLE [dbo].[General_Comment]  WITH NOCHECK ADD  CONSTRAINT [FK_general_comment_lead] FOREIGN KEY([Lead_Id])
REFERENCES [dbo].[lead] ([lead_id])
GO
ALTER TABLE [dbo].[General_Comment] CHECK CONSTRAINT [FK_general_comment_lead]
GO
