USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[CreditCardRefund]    Script Date: 06/07/2017 09:18:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CreditCardRefund](
	[CustomerOrderHeaderInstance] [int] NOT NULL,
	[Attempt] [int] NOT NULL,
	[CreditCardNumber] [varchar](25) NULL,
	[ExpirationDate] [varchar](10) NULL,
	[Amount] [float] NOT NULL,
	[ReasonCode] [int] NOT NULL,
	[AuthorizationSource] [varchar](2) NULL,
	[AuthorizationCode] [varchar](6) NULL,
	[AuthorizationDate] [datetime] NOT NULL,
	[AVSResponseCode] [varchar](2) NULL,
	[StatusInstance] [int] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[UserIDCreated] [varchar](4) NULL,
	[DateChanged] [datetime] NOT NULL,
	[UserIDChanged] [varchar](4) NULL,
	[BatchID] [int] NOT NULL,
	[ForcedRefund] [bit] NOT NULL,
 CONSTRAINT [aaaaaCreditCardRefund_PK] PRIMARY KEY CLUSTERED 
(
	[CustomerOrderHeaderInstance] ASC,
	[Attempt] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[CreditCardRefund] ADD  CONSTRAINT [DF__CreditCar__Custo__02A76E58]  DEFAULT (0) FOR [CustomerOrderHeaderInstance]
GO
ALTER TABLE [dbo].[CreditCardRefund] ADD  CONSTRAINT [DF__CreditCar__Attem__039B9291]  DEFAULT (0) FOR [Attempt]
GO
ALTER TABLE [dbo].[CreditCardRefund] ADD  CONSTRAINT [DF__CreditCar__Credi__048FB6CA]  DEFAULT (null) FOR [CreditCardNumber]
GO
ALTER TABLE [dbo].[CreditCardRefund] ADD  CONSTRAINT [DF__CreditCar__Expir__0583DB03]  DEFAULT (null) FOR [ExpirationDate]
GO
ALTER TABLE [dbo].[CreditCardRefund] ADD  CONSTRAINT [DF__CreditCar__Amoun__0677FF3C]  DEFAULT (0.0) FOR [Amount]
GO
ALTER TABLE [dbo].[CreditCardRefund] ADD  CONSTRAINT [DF__CreditCar__Reaso__076C2375]  DEFAULT (0) FOR [ReasonCode]
GO
ALTER TABLE [dbo].[CreditCardRefund] ADD  CONSTRAINT [DF__CreditCar__Autho__086047AE]  DEFAULT (null) FOR [AuthorizationSource]
GO
ALTER TABLE [dbo].[CreditCardRefund] ADD  CONSTRAINT [DF__CreditCar__Autho__09546BE7]  DEFAULT (null) FOR [AuthorizationCode]
GO
ALTER TABLE [dbo].[CreditCardRefund] ADD  CONSTRAINT [DF__CreditCar__Autho__0A489020]  DEFAULT ('1/1/1995') FOR [AuthorizationDate]
GO
ALTER TABLE [dbo].[CreditCardRefund] ADD  CONSTRAINT [DF__CreditCar__AVSRe__0B3CB459]  DEFAULT (null) FOR [AVSResponseCode]
GO
ALTER TABLE [dbo].[CreditCardRefund] ADD  CONSTRAINT [DF__CreditCar__Statu__0C30D892]  DEFAULT (0) FOR [StatusInstance]
GO
ALTER TABLE [dbo].[CreditCardRefund] ADD  CONSTRAINT [DF__CreditCar__DateC__0D24FCCB]  DEFAULT ('1/1/1995') FOR [DateCreated]
GO
ALTER TABLE [dbo].[CreditCardRefund] ADD  CONSTRAINT [DF__CreditCar__UserI__0E192104]  DEFAULT (' ') FOR [UserIDCreated]
GO
ALTER TABLE [dbo].[CreditCardRefund] ADD  CONSTRAINT [DF__CreditCar__DateC__0F0D453D]  DEFAULT ('1/1/1995') FOR [DateChanged]
GO
ALTER TABLE [dbo].[CreditCardRefund] ADD  CONSTRAINT [DF__CreditCar__UserI__10016976]  DEFAULT (' ') FOR [UserIDChanged]
GO
ALTER TABLE [dbo].[CreditCardRefund] ADD  CONSTRAINT [DF__CreditCar__Batch__10F58DAF]  DEFAULT (0) FOR [BatchID]
GO
ALTER TABLE [dbo].[CreditCardRefund] ADD  CONSTRAINT [DF__CreditCar__Force__11E9B1E8]  DEFAULT (0) FOR [ForcedRefund]
GO
