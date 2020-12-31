USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 06/07/2017 09:18:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Customer](
	[Instance] [int] NOT NULL,
	[StatusInstance] [int] NOT NULL,
	[LastName] [varchar](50) NULL,
	[FirstName] [varchar](50) NULL,
	[Address1] [varchar](50) NULL,
	[Address2] [varchar](50) NULL,
	[City] [varchar](50) NULL,
	[County] [varchar](31) NULL,
	[State] [varchar](10) NULL,
	[Zip] [varchar](10) NULL,
	[ZipPlusFour] [varchar](4) NULL,
	[OverrideAddress] [bit] NOT NULL,
	[ChangeUserID] [varchar](4) NULL,
	[ChangeDate] [datetime] NOT NULL,
	[Email] [varchar](50) NULL,
	[Phone] [varchar](25) NULL,
	[Type] [int] NULL,
 CONSTRAINT [aaaaaCustomer_PK] PRIMARY KEY CLUSTERED 
(
	[Instance] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Customer] ADD  CONSTRAINT [DF__Customer__Status__1C67405B]  DEFAULT (0) FOR [StatusInstance]
GO
ALTER TABLE [dbo].[Customer] ADD  CONSTRAINT [DF__Customer__LastNa__1D5B6494]  DEFAULT (null) FOR [LastName]
GO
ALTER TABLE [dbo].[Customer] ADD  CONSTRAINT [DF__Customer__FirstN__1E4F88CD]  DEFAULT (null) FOR [FirstName]
GO
ALTER TABLE [dbo].[Customer] ADD  CONSTRAINT [DF__Customer__Addres__1F43AD06]  DEFAULT (null) FOR [Address1]
GO
ALTER TABLE [dbo].[Customer] ADD  CONSTRAINT [DF__Customer__Addres__2037D13F]  DEFAULT (null) FOR [Address2]
GO
ALTER TABLE [dbo].[Customer] ADD  CONSTRAINT [DF__Customer__City__212BF578]  DEFAULT (null) FOR [City]
GO
ALTER TABLE [dbo].[Customer] ADD  CONSTRAINT [DF__Customer__County__222019B1]  DEFAULT (null) FOR [County]
GO
ALTER TABLE [dbo].[Customer] ADD  CONSTRAINT [DF__Customer__State__23143DEA]  DEFAULT (' ') FOR [State]
GO
ALTER TABLE [dbo].[Customer] ADD  CONSTRAINT [DF__Customer__Zip__24086223]  DEFAULT (null) FOR [Zip]
GO
ALTER TABLE [dbo].[Customer] ADD  CONSTRAINT [DF__Customer__ZipPlu__24FC865C]  DEFAULT (null) FOR [ZipPlusFour]
GO
ALTER TABLE [dbo].[Customer] ADD  CONSTRAINT [DF__Customer__Change__25F0AA95]  DEFAULT (' ') FOR [ChangeUserID]
GO
ALTER TABLE [dbo].[Customer] ADD  CONSTRAINT [DF__Customer__Change__26E4CECE]  DEFAULT ('1/1/1995') FOR [ChangeDate]
GO
ALTER TABLE [dbo].[Customer] ADD  CONSTRAINT [DF__Customer__Email__68687968]  DEFAULT (null) FOR [Email]
GO
ALTER TABLE [dbo].[Customer] ADD  CONSTRAINT [DF__Customer__Phone__695C9DA1]  DEFAULT (null) FOR [Phone]
GO
