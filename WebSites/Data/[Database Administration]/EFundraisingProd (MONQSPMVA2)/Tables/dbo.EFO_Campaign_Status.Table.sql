USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[EFO_Campaign_Status]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EFO_Campaign_Status](
	[Campaign_ID] [int] NOT NULL,
	[Date_To_Change] [smalldatetime] NOT NULL,
	[Status_ID] [int] NOT NULL,
	[Email_Type_ID] [int] NULL,
 CONSTRAINT [PK_EFO_Campaign_Status] PRIMARY KEY NONCLUSTERED 
(
	[Campaign_ID] ASC,
	[Status_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[EFO_Campaign_Status]  WITH NOCHECK ADD  CONSTRAINT [fk_efocs_campaign_id] FOREIGN KEY([Campaign_ID])
REFERENCES [dbo].[EFO_Campaign] ([Campaign_ID])
GO
ALTER TABLE [dbo].[EFO_Campaign_Status] CHECK CONSTRAINT [fk_efocs_campaign_id]
GO
ALTER TABLE [dbo].[EFO_Campaign_Status]  WITH NOCHECK ADD  CONSTRAINT [fk_efocs_email_type_id] FOREIGN KEY([Email_Type_ID])
REFERENCES [dbo].[EFO_Email_Type] ([Email_Type_ID])
GO
ALTER TABLE [dbo].[EFO_Campaign_Status] CHECK CONSTRAINT [fk_efocs_email_type_id]
GO
ALTER TABLE [dbo].[EFO_Campaign_Status]  WITH NOCHECK ADD  CONSTRAINT [fk_efocs_status_id] FOREIGN KEY([Status_ID])
REFERENCES [dbo].[EFO_Status] ([Status_ID])
GO
ALTER TABLE [dbo].[EFO_Campaign_Status] CHECK CONSTRAINT [fk_efocs_status_id]
GO
