USE [eFundstore]
GO
/****** Object:  Table [dbo].[unsubscribe]    Script Date: 02/14/2014 16:26:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[unsubscribe](
	[unsubscribe_id] [int] IDENTITY(1,1) NOT NULL,
	[email] [varchar](100) NOT NULL,
	[unsubscribed] [bit] NOT NULL,
	[unsubscribed_date] [datetime] NOT NULL,
 CONSTRAINT [PK_unsubscribe] PRIMARY KEY CLUSTERED 
(
	[unsubscribe_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[unsubscribe] ADD  CONSTRAINT [DF_unsubscribe_unsubscribed]  DEFAULT (0) FOR [unsubscribed]
GO
ALTER TABLE [dbo].[unsubscribe] ADD  CONSTRAINT [DF_unsubscribe_unsubscribed_date]  DEFAULT (getdate()) FOR [unsubscribed_date]
GO
