USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[PaymentBatch]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PaymentBatch](
	[PaymentDate] [datetime] NOT NULL,
	[PaymentID] [int] NOT NULL,
	[DepositDate] [datetime] NOT NULL,
	[DepositID] [int] NOT NULL,
	[EnterredAmount] [float] NOT NULL,
	[EnterredCount] [int] NOT NULL,
	[CalculatedAmount] [float] NOT NULL,
	[CalculatedCount] [int] NOT NULL,
	[StatusInstance] [int] NOT NULL,
	[Clerk] [varchar](4) NULL,
	[FileName] [varchar](200) NULL,
	[StartImportTime] [datetime] NOT NULL,
	[EndImportTime] [datetime] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[UserIDCreated] [varchar](4) NULL,
	[DateChanged] [datetime] NOT NULL,
	[UserIDChanged] [varchar](4) NULL,
	[IsDirty] [bit] NOT NULL,
	[DirtyStatus] [int] NOT NULL,
	[IsCreditCard] [bit] NOT NULL,
 CONSTRAINT [aaaaaPaymentBatch_PK] PRIMARY KEY CLUSTERED 
(
	[PaymentDate] ASC,
	[PaymentID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[PaymentBatch] ADD  CONSTRAINT [DF__PaymentBa__Payme__0DAF0CB0]  DEFAULT ('1/1/1995') FOR [PaymentDate]
GO
ALTER TABLE [dbo].[PaymentBatch] ADD  CONSTRAINT [DF__PaymentBa__Payme__0EA330E9]  DEFAULT (0) FOR [PaymentID]
GO
ALTER TABLE [dbo].[PaymentBatch] ADD  CONSTRAINT [DF__PaymentBa__Depos__0F975522]  DEFAULT ('1/1/1995') FOR [DepositDate]
GO
ALTER TABLE [dbo].[PaymentBatch] ADD  CONSTRAINT [DF__PaymentBa__Depos__108B795B]  DEFAULT (0) FOR [DepositID]
GO
ALTER TABLE [dbo].[PaymentBatch] ADD  CONSTRAINT [DF__PaymentBa__Enter__117F9D94]  DEFAULT (0.0) FOR [EnterredAmount]
GO
ALTER TABLE [dbo].[PaymentBatch] ADD  CONSTRAINT [DF__PaymentBa__Enter__1273C1CD]  DEFAULT (0) FOR [EnterredCount]
GO
ALTER TABLE [dbo].[PaymentBatch] ADD  CONSTRAINT [DF__PaymentBa__Calcu__1367E606]  DEFAULT (0.0) FOR [CalculatedAmount]
GO
ALTER TABLE [dbo].[PaymentBatch] ADD  CONSTRAINT [DF__PaymentBa__Calcu__145C0A3F]  DEFAULT (0) FOR [CalculatedCount]
GO
ALTER TABLE [dbo].[PaymentBatch] ADD  CONSTRAINT [DF__PaymentBa__Statu__15502E78]  DEFAULT (0) FOR [StatusInstance]
GO
ALTER TABLE [dbo].[PaymentBatch] ADD  CONSTRAINT [DF__PaymentBa__Clerk__164452B1]  DEFAULT (null) FOR [Clerk]
GO
ALTER TABLE [dbo].[PaymentBatch] ADD  CONSTRAINT [DF__PaymentBa__FileN__173876EA]  DEFAULT (null) FOR [FileName]
GO
ALTER TABLE [dbo].[PaymentBatch] ADD  CONSTRAINT [DF__PaymentBa__Start__182C9B23]  DEFAULT ('1/1/1995') FOR [StartImportTime]
GO
ALTER TABLE [dbo].[PaymentBatch] ADD  CONSTRAINT [DF__PaymentBa__EndIm__1920BF5C]  DEFAULT ('1/1/1995') FOR [EndImportTime]
GO
ALTER TABLE [dbo].[PaymentBatch] ADD  CONSTRAINT [DF__PaymentBa__DateC__1A14E395]  DEFAULT ('1/1/1995') FOR [DateCreated]
GO
ALTER TABLE [dbo].[PaymentBatch] ADD  CONSTRAINT [DF__PaymentBa__UserI__1B0907CE]  DEFAULT (' ') FOR [UserIDCreated]
GO
ALTER TABLE [dbo].[PaymentBatch] ADD  CONSTRAINT [DF__PaymentBa__DateC__1BFD2C07]  DEFAULT ('1/1/1995') FOR [DateChanged]
GO
ALTER TABLE [dbo].[PaymentBatch] ADD  CONSTRAINT [DF__PaymentBa__UserI__1CF15040]  DEFAULT (null) FOR [UserIDChanged]
GO
ALTER TABLE [dbo].[PaymentBatch] ADD  CONSTRAINT [DF__PaymentBa__Dirty__1DE57479]  DEFAULT (0) FOR [DirtyStatus]
GO
