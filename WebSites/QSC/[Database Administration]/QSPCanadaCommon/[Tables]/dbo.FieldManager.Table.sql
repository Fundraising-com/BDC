USE [QSPCanadaCommon]
GO
/****** Object:  Table [dbo].[FieldManager]    Script Date: 06/07/2017 09:29:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FieldManager](
	[FMID] [varchar](4) NOT NULL,
	[Status] [int] NULL,
	[Country] [dbo].[CountryCode_UDDT] NULL,
	[PhoneListID] [int] NULL,
	[AddressListID] [int] NULL,
	[FirstName] [varchar](50) NULL,
	[LastName] [varchar](50) NULL,
	[MiddleInitial] [varchar](10) NULL,
	[Email] [varchar](60) NULL,
	[DMID] [varchar](4) NULL,
	[UserIDModified] [dbo].[UserID_UDDT] NULL,
	[DateModified] [datetime] NULL,
	[Comment] [varchar](256) NULL,
	[DMIndicator] [char](1) NULL,
	[Lang] [varchar](10) NULL,
	[DeletedTF] [bit] NOT NULL,
	[SAPAcctNo] [int] NULL,
 CONSTRAINT [PK_FieldManager] PRIMARY KEY CLUSTERED 
(
	[FMID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[FieldManager]  WITH CHECK ADD  CONSTRAINT [FK_FieldManager_AddressList] FOREIGN KEY([AddressListID])
REFERENCES [dbo].[AddressList] ([ID])
GO
ALTER TABLE [dbo].[FieldManager] CHECK CONSTRAINT [FK_FieldManager_AddressList]
GO
ALTER TABLE [dbo].[FieldManager]  WITH CHECK ADD  CONSTRAINT [FK_FieldManager_PhoneList] FOREIGN KEY([PhoneListID])
REFERENCES [dbo].[PhoneList] ([ID])
GO
ALTER TABLE [dbo].[FieldManager] CHECK CONSTRAINT [FK_FieldManager_PhoneList]
GO
ALTER TABLE [dbo].[FieldManager] ADD  CONSTRAINT [DF_FieldManager_DeletedTF]  DEFAULT (0) FOR [DeletedTF]
GO
