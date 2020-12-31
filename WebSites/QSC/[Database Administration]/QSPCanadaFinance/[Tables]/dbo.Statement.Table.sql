USE [QSPCanadaFinance]
GO
/****** Object:  Table [dbo].[Statement]    Script Date: 06/07/2017 09:16:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Statement](
	[StatementID] [int] IDENTITY(1,1) NOT NULL,
	[StatementRunID] [int] NULL,
	[StatementDate] [datetime] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[AccountID] [int] NOT NULL,
	[CampaignID] [int] NOT NULL,
	[IsStaffCampaign] [bit] NOT NULL,
	[Lang] [nvarchar](50) NOT NULL,
	[Balance] [numeric](12, 2) NULL,
	[CampaignPrograms] [nvarchar](200) NULL,
	[FMID] [nvarchar](50) NOT NULL,
	[FMFirstName] [nvarchar](50) NOT NULL,
	[FMLastName] [nvarchar](50) NOT NULL,
	[PaymentTerms] [nvarchar](50) NOT NULL,
	[CorpAttn] [nvarchar](100) NULL,
	[CorpAddress1] [nvarchar](100) NOT NULL,
	[CorpAddress2] [nvarchar](100) NULL,
	[CorpCity] [nvarchar](100) NOT NULL,
	[CorpProvince] [nvarchar](15) NOT NULL,
	[CorpPostalCode] [nvarchar](15) NOT NULL,
	[CorpPhoneNumber] [nvarchar](100) NOT NULL,
	[CorpGSTNumber] [nvarchar](100) NOT NULL,
	[CorpQSTNumber] [nvarchar](100) NULL,
	[AccountName] [nvarchar](100) NOT NULL,
	[AccountContactFirstName] [nvarchar](100) NOT NULL,
	[AccountContactLastName] [nvarchar](100) NOT NULL,
	[AccountAddress1] [nvarchar](100) NOT NULL,
	[AccountAddress2] [nvarchar](100) NULL,
	[AccountCity] [nvarchar](100) NOT NULL,
	[AccountProvince] [nvarchar](15) NOT NULL,
	[AccountPostalCode] [nvarchar](15) NOT NULL,
	[AccountZip4] [nvarchar](4) NULL,
	[AccountPhoneNumber] [nvarchar](100) NULL,
	[Refund_ID] [int] NULL,
 CONSTRAINT [PK_Statement] PRIMARY KEY CLUSTERED 
(
	[StatementID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Statement]  WITH CHECK ADD  CONSTRAINT [FK_Statement_Refund] FOREIGN KEY([Refund_ID])
REFERENCES [dbo].[Refund] ([Refund_ID])
GO
ALTER TABLE [dbo].[Statement] CHECK CONSTRAINT [FK_Statement_Refund]
GO
ALTER TABLE [dbo].[Statement]  WITH CHECK ADD  CONSTRAINT [FK_Statement_StatementRun] FOREIGN KEY([StatementRunID])
REFERENCES [dbo].[StatementRun] ([StatementRunID])
GO
ALTER TABLE [dbo].[Statement] CHECK CONSTRAINT [FK_Statement_StatementRun]
GO
