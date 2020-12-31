USE [QSPCanadaCommon]
GO
/****** Object:  Table [dbo].[UserPermissions]    Script Date: 06/07/2017 09:29:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserPermissions](
	[ProfileID] [int] NOT NULL,
	[PName] [varchar](30) NOT NULL,
	[ModifiedBy] [varchar](20) NOT NULL,
	[ModifiedDate] [smalldatetime] NOT NULL,
	[CreatedBy] [varchar](20) NOT NULL,
	[CreatedDate] [smalldatetime] NOT NULL,
	[DeletedTF] [bit] NOT NULL,
 CONSTRAINT [PK_UserPermissions] PRIMARY KEY CLUSTERED 
(
	[ProfileID] ASC,
	[PName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[UserPermissions]  WITH NOCHECK ADD  CONSTRAINT [FK_UserPermissions_UserPermissionsDef] FOREIGN KEY([PName])
REFERENCES [dbo].[UserPermissionsDef] ([PName])
GO
ALTER TABLE [dbo].[UserPermissions] CHECK CONSTRAINT [FK_UserPermissions_UserPermissionsDef]
GO
ALTER TABLE [dbo].[UserPermissions] ADD  CONSTRAINT [DF_UserPermissions_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[UserPermissions] ADD  CONSTRAINT [DF_UserPermissions_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[UserPermissions] ADD  CONSTRAINT [DF_UserPermission_DeletedTF]  DEFAULT (0) FOR [DeletedTF]
GO
