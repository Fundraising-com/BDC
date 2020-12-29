USE [eFundweb]
GO
/****** Object:  Table [dbo].[Unsubscribe]    Script Date: 02/14/2014 16:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Unsubscribe](
	[Unsubscribe_ID] [int] IDENTITY(1,1) NOT NULL,
	[email] [varchar](100) NOT NULL,
	[Unsubscribed] [bit] NOT NULL,
	[Unsubscribed_Date] [datetime] NOT NULL,
 CONSTRAINT [PK_Unsubscribe] PRIMARY KEY CLUSTERED 
(
	[Unsubscribe_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Unsubscribe] ADD  CONSTRAINT [DF_Unsubscribe_Unsubscribed]  DEFAULT (0) FOR [Unsubscribed]
GO
ALTER TABLE [dbo].[Unsubscribe] ADD  CONSTRAINT [DF__Unsubscri__Unsub__164452B1]  DEFAULT (getdate()) FOR [Unsubscribed_Date]
GO
