USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[EFO_Status_Email]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EFO_Status_Email](
	[Email_Type_ID] [int] NOT NULL,
	[Status_ID] [int] NOT NULL,
 CONSTRAINT [PK_EFO_Status_Email] PRIMARY KEY NONCLUSTERED 
(
	[Email_Type_ID] ASC,
	[Status_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[EFO_Status_Email]  WITH NOCHECK ADD  CONSTRAINT [fk_efose_email_type_id] FOREIGN KEY([Email_Type_ID])
REFERENCES [dbo].[EFO_Email_Type] ([Email_Type_ID])
GO
ALTER TABLE [dbo].[EFO_Status_Email] CHECK CONSTRAINT [fk_efose_email_type_id]
GO
ALTER TABLE [dbo].[EFO_Status_Email]  WITH NOCHECK ADD  CONSTRAINT [fk_efose_status_id] FOREIGN KEY([Status_ID])
REFERENCES [dbo].[EFO_Status] ([Status_ID])
GO
ALTER TABLE [dbo].[EFO_Status_Email] CHECK CONSTRAINT [fk_efose_status_id]
GO
