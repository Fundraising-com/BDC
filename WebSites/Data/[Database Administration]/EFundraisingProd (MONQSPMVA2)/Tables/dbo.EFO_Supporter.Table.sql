USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[EFO_Supporter]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EFO_Supporter](
	[Supporter_ID] [int] IDENTITY(34,1) NOT FOR REPLICATION NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Participant_ID] [int] NOT NULL,
	[Email] [varchar](75) NOT NULL,
	[Is_Email_Good] [bit] NOT NULL,
	[Is_Active] [bit] NOT NULL,
	[Comments] [varchar](150) NULL,
	[Email_Sent] [bit] NOT NULL,
	[Is_Deletable] [bit] NOT NULL,
	[Relation] [varchar](25) NOT NULL,
 CONSTRAINT [PK_EFO_Supporter] PRIMARY KEY NONCLUSTERED 
(
	[Supporter_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[EFO_Supporter]  WITH NOCHECK ADD  CONSTRAINT [fk_efos_participant_id] FOREIGN KEY([Participant_ID])
REFERENCES [dbo].[EFO_Participant] ([Participant_ID])
GO
ALTER TABLE [dbo].[EFO_Supporter] CHECK CONSTRAINT [fk_efos_participant_id]
GO
