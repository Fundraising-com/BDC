USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[CustomerError]    Script Date: 06/07/2017 09:18:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerError](
	[CustomerInstance] [int] NOT NULL,
	[ErrorInstance] [int] NOT NULL,
	[Address1] [varchar](30) NULL,
	[Address2] [varchar](30) NULL,
	[City] [varchar](20) NULL,
	[State] [varchar](2) NULL,
	[Zip] [varchar](6) NULL,
	[ZipPlusFour] [varchar](4) NULL,
	[ChangeUserID] [varchar](4) NULL,
	[ChangeDate] [datetime] NOT NULL,
 CONSTRAINT [aaaaaCustomerError_PK] PRIMARY KEY CLUSTERED 
(
	[CustomerInstance] ASC,
	[ErrorInstance] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[CustomerError] ADD  CONSTRAINT [DF__CustomerE__Custo__2AB55FB2]  DEFAULT (0) FOR [CustomerInstance]
GO
ALTER TABLE [dbo].[CustomerError] ADD  CONSTRAINT [DF__CustomerE__Error__2BA983EB]  DEFAULT (0) FOR [ErrorInstance]
GO
ALTER TABLE [dbo].[CustomerError] ADD  CONSTRAINT [DF__CustomerE__Addre__2C9DA824]  DEFAULT (null) FOR [Address1]
GO
ALTER TABLE [dbo].[CustomerError] ADD  CONSTRAINT [DF__CustomerE__Addre__2D91CC5D]  DEFAULT (null) FOR [Address2]
GO
ALTER TABLE [dbo].[CustomerError] ADD  CONSTRAINT [DF__CustomerEr__City__2E85F096]  DEFAULT (null) FOR [City]
GO
ALTER TABLE [dbo].[CustomerError] ADD  CONSTRAINT [DF__CustomerE__State__2F7A14CF]  DEFAULT (' ') FOR [State]
GO
ALTER TABLE [dbo].[CustomerError] ADD  CONSTRAINT [DF__CustomerErr__Zip__306E3908]  DEFAULT (null) FOR [Zip]
GO
ALTER TABLE [dbo].[CustomerError] ADD  CONSTRAINT [DF__CustomerE__ZipPl__31625D41]  DEFAULT (null) FOR [ZipPlusFour]
GO
ALTER TABLE [dbo].[CustomerError] ADD  CONSTRAINT [DF__CustomerE__Chang__3256817A]  DEFAULT (' ') FOR [ChangeUserID]
GO
ALTER TABLE [dbo].[CustomerError] ADD  CONSTRAINT [DF__CustomerE__Chang__334AA5B3]  DEFAULT ('1/1/1995') FOR [ChangeDate]
GO
