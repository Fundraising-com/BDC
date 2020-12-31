USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[InternalAccount]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InternalAccount](
	[AccountID] [int] NOT NULL,
	[FirstOrderDate] [datetime] NOT NULL,
	[SchoolLetterDone] [bit] NOT NULL,
	[OrderReceivedDate] [datetime] NOT NULL,
	[ActivityReportDone] [bit] NOT NULL,
 CONSTRAINT [aaaaaInternalAccount_PK] PRIMARY KEY CLUSTERED 
(
	[AccountID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[InternalAccount] ADD  CONSTRAINT [DF__InternalA__Accou__1D4655FB]  DEFAULT (0) FOR [AccountID]
GO
ALTER TABLE [dbo].[InternalAccount] ADD  CONSTRAINT [DF__InternalA__First__1E3A7A34]  DEFAULT ('1/1/1995') FOR [FirstOrderDate]
GO
ALTER TABLE [dbo].[InternalAccount] ADD  CONSTRAINT [DF__InternalA__Order__1F2E9E6D]  DEFAULT ('1/1/1995') FOR [OrderReceivedDate]
GO
