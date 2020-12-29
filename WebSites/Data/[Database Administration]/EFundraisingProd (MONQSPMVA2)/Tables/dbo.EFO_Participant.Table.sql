USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[EFO_Participant]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EFO_Participant](
	[Participant_ID] [int] IDENTITY(25,1) NOT FOR REPLICATION NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Campaign_ID] [int] NOT NULL,
	[Email] [varchar](50) NULL,
	[Comments] [varchar](150) NULL,
	[Email_Sent] [bit] NOT NULL,
	[Is_Active] [bit] NOT NULL,
	[Is_Default] [bit] NOT NULL,
	[Is_Deletable] [bit] NOT NULL,
 CONSTRAINT [PK_EFO_Participant] PRIMARY KEY NONCLUSTERED 
(
	[Participant_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[EFO_Participant]  WITH NOCHECK ADD  CONSTRAINT [fk_efop_campaign_id] FOREIGN KEY([Campaign_ID])
REFERENCES [dbo].[EFO_Campaign] ([Campaign_ID])
GO
ALTER TABLE [dbo].[EFO_Participant] CHECK CONSTRAINT [fk_efop_campaign_id]
GO
