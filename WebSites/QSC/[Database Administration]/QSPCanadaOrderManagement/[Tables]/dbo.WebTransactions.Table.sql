USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[WebTransactions]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WebTransactions](
	[CustomerOrderHeaderInstance] [int] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[StatusInstance] [int] NOT NULL,
	[TransAmt] [money] NOT NULL,
	[TransInt] [int] NOT NULL,
 CONSTRAINT [PK_WebTransactions] PRIMARY KEY CLUSTERED 
(
	[CustomerOrderHeaderInstance] ASC,
	[DateCreated] ASC,
	[StatusInstance] ASC,
	[TransInt] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[WebTransactions] ADD  CONSTRAINT [DF_WebTransactions_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[WebTransactions] ADD  CONSTRAINT [DF_WebTransactions_TransAmt]  DEFAULT (0.0) FOR [TransAmt]
GO
ALTER TABLE [dbo].[WebTransactions] ADD  CONSTRAINT [DF_WebTransactions_TransInt]  DEFAULT (0) FOR [TransInt]
GO
