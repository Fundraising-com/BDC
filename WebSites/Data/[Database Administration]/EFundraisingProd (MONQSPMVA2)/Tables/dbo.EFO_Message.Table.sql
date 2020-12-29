USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[EFO_Message]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EFO_Message](
	[Message_ID] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[Participant_ID] [int] NOT NULL,
	[Is_Read] [bit] NOT NULL,
	[Date_Sent] [smalldatetime] NOT NULL,
	[Date_Received] [smalldatetime] NOT NULL,
	[From_Name] [varchar](50) NULL,
	[From_Email] [varchar](50) NULL,
	[To_Name] [varchar](50) NULL,
	[To_Email] [varchar](50) NOT NULL,
	[Subject] [varchar](20) NOT NULL,
	[Body] [text] NULL,
	[Content_Type] [varchar](20) NOT NULL,
 CONSTRAINT [PK_EFO_Message] PRIMARY KEY NONCLUSTERED 
(
	[Message_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[EFO_Message]  WITH NOCHECK ADD  CONSTRAINT [fk_efom_participant_id] FOREIGN KEY([Participant_ID])
REFERENCES [dbo].[EFO_Participant] ([Participant_ID])
GO
ALTER TABLE [dbo].[EFO_Message] CHECK CONSTRAINT [fk_efom_participant_id]
GO
