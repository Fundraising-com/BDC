USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[CurrentUsers]    Script Date: 06/07/2017 09:18:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CurrentUsers](
	[UserName] [varchar](50) NOT NULL,
	[ComputerName] [varchar](50) NOT NULL,
	[DateLoggedIn] [datetime] NOT NULL,
 CONSTRAINT [aaaaaCurrentUsers_PK] PRIMARY KEY CLUSTERED 
(
	[UserName] ASC,
	[ComputerName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[CurrentUsers] ADD  CONSTRAINT [DF__CurrentUs__UserN__15BA42CC]  DEFAULT (' ') FOR [UserName]
GO
ALTER TABLE [dbo].[CurrentUsers] ADD  CONSTRAINT [DF__CurrentUs__Compu__16AE6705]  DEFAULT (' ') FOR [ComputerName]
GO
ALTER TABLE [dbo].[CurrentUsers] ADD  CONSTRAINT [DF__CurrentUs__DateL__17A28B3E]  DEFAULT ('1/1/1995') FOR [DateLoggedIn]
GO
