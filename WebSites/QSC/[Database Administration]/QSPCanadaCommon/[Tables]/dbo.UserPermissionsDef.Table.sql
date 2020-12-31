USE [QSPCanadaCommon]
GO
/****** Object:  Table [dbo].[UserPermissionsDef]    Script Date: 06/07/2017 09:29:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserPermissionsDef](
	[PName] [varchar](30) NOT NULL,
	[PDesc] [varchar](100) NOT NULL,
	[ModifiedBy] [varchar](20) NOT NULL,
	[ModifiedDate] [smalldatetime] NOT NULL,
	[CreatedBy] [varchar](20) NOT NULL,
	[CreatedDate] [smalldatetime] NOT NULL,
	[DeletedTF] [bit] NOT NULL,
 CONSTRAINT [PK_UserPermissionDefs] PRIMARY KEY CLUSTERED 
(
	[PName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[UserPermissionsDef] ADD  CONSTRAINT [DF_UserPermissionsDef_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[UserPermissionsDef] ADD  CONSTRAINT [DF_UserPermissionsDef_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[UserPermissionsDef] ADD  CONSTRAINT [DF_UserPermissionDefs_DeletedTF]  DEFAULT (0) FOR [DeletedTF]
GO
