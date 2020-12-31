USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[CustomerOrderDetailError]    Script Date: 06/07/2017 09:18:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerOrderDetailError](
	[CustomerOrderHeaderInstance] [int] NOT NULL,
	[TransID] [int] NOT NULL,
	[ErrorInstance] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
 CONSTRAINT [aaaaaCustomerOrderDetailErr_PK] PRIMARY KEY CLUSTERED 
(
	[CustomerOrderHeaderInstance] ASC,
	[TransID] ASC,
	[ErrorInstance] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CustomerOrderDetailError] ADD  CONSTRAINT [DF__CustomerO__Custo__53B77545]  DEFAULT (0) FOR [CustomerOrderHeaderInstance]
GO
ALTER TABLE [dbo].[CustomerOrderDetailError] ADD  CONSTRAINT [DF__CustomerO__Trans__54AB997E]  DEFAULT (0) FOR [TransID]
GO
ALTER TABLE [dbo].[CustomerOrderDetailError] ADD  CONSTRAINT [DF__CustomerO__Error__559FBDB7]  DEFAULT (0) FOR [ErrorInstance]
GO
ALTER TABLE [dbo].[CustomerOrderDetailError] ADD  CONSTRAINT [DF__CustomerOr__Date__5693E1F0]  DEFAULT ('1/1/1995') FOR [Date]
GO
