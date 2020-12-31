USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[Package]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Package](
	[ID] [varchar](40) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[UserIDCreated] [varchar](4) NULL,
	[PackageType] [int] NOT NULL,
	[ToteInstance] [int] NOT NULL,
	[TroubleBoxHeaderInstance] [int] NOT NULL,
	[ShipperTrackingID] [varchar](40) NULL,
 CONSTRAINT [aaaaaPackage_PK] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Package] ADD  CONSTRAINT [DF__Package__ID__26CFC035]  DEFAULT (' ') FOR [ID]
GO
ALTER TABLE [dbo].[Package] ADD  CONSTRAINT [DF__Package__DateCre__27C3E46E]  DEFAULT ('1/1/1995') FOR [DateCreated]
GO
ALTER TABLE [dbo].[Package] ADD  CONSTRAINT [DF__Package__UserIDC__28B808A7]  DEFAULT (' ') FOR [UserIDCreated]
GO
ALTER TABLE [dbo].[Package] ADD  CONSTRAINT [DF__Package__Package__29AC2CE0]  DEFAULT (0) FOR [PackageType]
GO
ALTER TABLE [dbo].[Package] ADD  CONSTRAINT [DF__Package__ToteIns__2AA05119]  DEFAULT (0) FOR [ToteInstance]
GO
ALTER TABLE [dbo].[Package] ADD  CONSTRAINT [DF__Package__Trouble__2B947552]  DEFAULT (0) FOR [TroubleBoxHeaderInstance]
GO
ALTER TABLE [dbo].[Package] ADD  CONSTRAINT [DF_Package_ShipperTracki1__121]  DEFAULT (' ') FOR [ShipperTrackingID]
GO
