USE [eFundweb]
GO
/****** Object:  Table [dbo].[Newsletter]    Script Date: 02/14/2014 16:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Newsletter](
	[Newsletter_ID] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[Referrer] [varchar](120) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[Fullname] [varchar](100) NOT NULL,
	[Unsubscribed] [bit] NULL,
	[Subscribe_Date] [datetime] NULL,
	[Partner_ID] [int] NOT NULL,
 CONSTRAINT [PK_Newsletter] PRIMARY KEY CLUSTERED 
(
	[Newsletter_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Newsletter] ADD  CONSTRAINT [DF__Newslette__Subsc__1273C1CD]  DEFAULT (getdate()) FOR [Subscribe_Date]
GO
ALTER TABLE [dbo].[Newsletter] ADD  CONSTRAINT [DF__Newslette__Partn__1367E606]  DEFAULT (1) FOR [Partner_ID]
GO
