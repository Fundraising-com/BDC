USE [QSPCanadaCommon]
GO
/****** Object:  Table [dbo].[Contact]    Script Date: 06/07/2017 09:29:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Contact](
	[Id] [int] IDENTITY(11000,1) NOT NULL,
	[ContactListID] [int] NOT NULL,
	[CAccountID] [int] NOT NULL,
	[Title] [varchar](10) NOT NULL,
	[FirstName] [varchar](20) NOT NULL,
	[LastName] [varchar](30) NOT NULL,
	[MiddleInitial] [varchar](10) NULL,
	[TypeId] [int] NOT NULL,
	[Function] [varchar](50) NULL,
	[Email] [varchar](60) NULL,
	[AddressID] [int] NOT NULL,
	[PhoneListID] [int] NOT NULL,
	[DeletedTF] [bit] NOT NULL,
	[DateChanged] [datetime] NULL,
 CONSTRAINT [PK_Contact] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Contact]  WITH CHECK ADD  CONSTRAINT [FK_Contact_PhoneList] FOREIGN KEY([PhoneListID])
REFERENCES [dbo].[PhoneList] ([ID])
GO
ALTER TABLE [dbo].[Contact] CHECK CONSTRAINT [FK_Contact_PhoneList]
GO
ALTER TABLE [dbo].[Contact] ADD  CONSTRAINT [DF_Contact_Deleted_TF]  DEFAULT (0) FOR [DeletedTF]
GO
