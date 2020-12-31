USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[CreditCardBatch]    Script Date: 06/07/2017 09:18:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CreditCardBatch](
	[InputFileName] [varchar](200) NULL,
	[OutputFileName] [varchar](200) NOT NULL,
	[StartImportTime] [datetime] NOT NULL,
	[EndImportTime] [datetime] NOT NULL,
	[Status] [int] NOT NULL,
	[TotalRecordCount] [int] NOT NULL,
	[TotalDollarAmount] [int] NOT NULL,
	[ID] [int] NOT NULL,
	[DateCreated] [datetime] NULL,
	[UserIDCreated] [varchar](4) NULL,
	[ChangeDate] [datetime] NULL,
	[ChangeUserID] [varchar](4) NULL,
	[Type] [int] NULL,
	[IsGLDone] [int] NULL,
 CONSTRAINT [aaaaaCreditCardBatch_PK] PRIMARY KEY CLUSTERED 
(
	[OutputFileName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[CreditCardBatch] ADD  CONSTRAINT [DF__CreditCar__Input__67F3781C]  DEFAULT (null) FOR [InputFileName]
GO
ALTER TABLE [dbo].[CreditCardBatch] ADD  CONSTRAINT [DF__CreditCar__Outpu__68E79C55]  DEFAULT (' ') FOR [OutputFileName]
GO
ALTER TABLE [dbo].[CreditCardBatch] ADD  CONSTRAINT [DF__CreditCar__Start__69DBC08E]  DEFAULT ('1/1/1995') FOR [StartImportTime]
GO
ALTER TABLE [dbo].[CreditCardBatch] ADD  CONSTRAINT [DF__CreditCar__EndIm__6ACFE4C7]  DEFAULT ('1/1/1995') FOR [EndImportTime]
GO
ALTER TABLE [dbo].[CreditCardBatch] ADD  CONSTRAINT [DF__CreditCar__Statu__6BC40900]  DEFAULT (0) FOR [Status]
GO
ALTER TABLE [dbo].[CreditCardBatch] ADD  CONSTRAINT [DF__CreditCar__Total__6CB82D39]  DEFAULT (0) FOR [TotalRecordCount]
GO
ALTER TABLE [dbo].[CreditCardBatch] ADD  CONSTRAINT [DF__CreditCar__Total__6DAC5172]  DEFAULT (0) FOR [TotalDollarAmount]
GO
ALTER TABLE [dbo].[CreditCardBatch] ADD  CONSTRAINT [DF__CreditCardBa__ID__6EA075AB]  DEFAULT (0) FOR [ID]
GO
ALTER TABLE [dbo].[CreditCardBatch] ADD  CONSTRAINT [DF__CreditCar__DateC__2022C2A6]  DEFAULT ('1/1/1995') FOR [DateCreated]
GO
ALTER TABLE [dbo].[CreditCardBatch] ADD  CONSTRAINT [DF__CreditCar__UserI__2116E6DF]  DEFAULT ('ADMIN') FOR [UserIDCreated]
GO
ALTER TABLE [dbo].[CreditCardBatch] ADD  CONSTRAINT [DF__CreditCar__Chang__220B0B18]  DEFAULT ('1/1/1995') FOR [ChangeDate]
GO
ALTER TABLE [dbo].[CreditCardBatch] ADD  CONSTRAINT [DF__CreditCar__Chang__22FF2F51]  DEFAULT ('ADMIN') FOR [ChangeUserID]
GO
ALTER TABLE [dbo].[CreditCardBatch] ADD  CONSTRAINT [DF_CreditCardBatch_Type]  DEFAULT (58001) FOR [Type]
GO
