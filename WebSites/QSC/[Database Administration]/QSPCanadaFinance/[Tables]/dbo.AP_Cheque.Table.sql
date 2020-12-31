USE [QSPCanadaFinance]
GO
/****** Object:  Table [dbo].[AP_Cheque]    Script Date: 06/07/2017 09:16:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AP_Cheque](
	[AP_Cheque_ID] [int] IDENTITY(1,1) NOT NULL,
	[AP_Cheque_Batch_ID] [int] NULL,
	[AP_Cheque_Status_ID] [int] NOT NULL,
	[ChequeNumber] [bigint] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[Bank_Account_ID] [int] NOT NULL,
	[ChequePayableDate] [datetime] NULL,
 CONSTRAINT [PK_AP_Cheque] PRIMARY KEY CLUSTERED 
(
	[AP_Cheque_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AP_Cheque]  WITH CHECK ADD  CONSTRAINT [FK_AP_Cheque_AP_Cheque_Status] FOREIGN KEY([AP_Cheque_Status_ID])
REFERENCES [dbo].[AP_Cheque_Status] ([AP_Cheque_Status_ID])
GO
ALTER TABLE [dbo].[AP_Cheque] CHECK CONSTRAINT [FK_AP_Cheque_AP_Cheque_Status]
GO
ALTER TABLE [dbo].[AP_Cheque]  WITH CHECK ADD  CONSTRAINT [FK_AP_Cheque_BANK_ACCOUNT] FOREIGN KEY([AP_Cheque_Batch_ID])
REFERENCES [dbo].[AP_Cheque_Batch] ([AP_Cheque_Batch_ID])
GO
ALTER TABLE [dbo].[AP_Cheque] CHECK CONSTRAINT [FK_AP_Cheque_BANK_ACCOUNT]
GO
