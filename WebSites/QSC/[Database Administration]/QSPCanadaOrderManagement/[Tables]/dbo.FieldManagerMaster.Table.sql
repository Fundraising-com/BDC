USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[FieldManagerMaster]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FieldManagerMaster](
	[FMNo] [varchar](4) NOT NULL,
	[District] [varchar](2) NULL,
	[FieldManagerSS] [varchar](16) NULL,
	[Name] [varchar](25) NULL,
	[Street] [varchar](25) NULL,
	[FourthLine] [varchar](25) NULL,
	[City] [varchar](16) NULL,
	[State] [dbo].[State_UDDT] NULL,
	[Zip] [varchar](5) NULL,
	[Telephone] [varchar](10) NULL,
	[DistrictManagerNo] [varchar](4) NULL,
	[MagCommPct] [float] NOT NULL,
	[BMagCommPct] [float] NOT NULL,
	[MusicCommPct] [float] NOT NULL,
	[BMusicCommPct] [float] NOT NULL,
	[BOACommPct] [float] NOT NULL,
	[BookCommPct] [float] NOT NULL,
	[NMagCommPct] [float] NOT NULL,
	[NBMagCommPct] [float] NOT NULL,
	[NMusicCommPct] [float] NOT NULL,
	[NBMusicCommPct] [float] NOT NULL,
	[NBOACommPct] [float] NOT NULL,
	[NBookCommPct] [float] NOT NULL,
	[Override] [varchar](4) NULL,
	[OMagCommPct] [float] NOT NULL,
	[OBMagCommPct] [float] NOT NULL,
	[OMusicCommPct] [float] NOT NULL,
	[OBMusicCommPct] [float] NOT NULL,
	[OBOACommPct] [float] NOT NULL,
	[OBookCommPct] [float] NOT NULL,
	[ONMagCommPct] [float] NOT NULL,
	[ONBMagCommPct] [float] NOT NULL,
	[ONMusicCommPct] [float] NOT NULL,
	[ONBMusicCommPct] [float] NOT NULL,
	[ONBOACommPct] [float] NOT NULL,
	[ONBookCommPct] [float] NOT NULL,
	[Zip4] [varchar](4) NULL,
	[FMEnd] [varchar](2) NULL,
 CONSTRAINT [aaaaaFieldManagerMaster_PK] PRIMARY KEY CLUSTERED 
(
	[FMNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[FieldManagerMaster] ADD  CONSTRAINT [DF__FieldManag__FMNo__4BE14953]  DEFAULT (' ') FOR [FMNo]
GO
ALTER TABLE [dbo].[FieldManagerMaster] ADD  CONSTRAINT [DF__FieldMana__Distr__4CD56D8C]  DEFAULT (null) FOR [District]
GO
ALTER TABLE [dbo].[FieldManagerMaster] ADD  CONSTRAINT [DF__FieldMana__Field__4DC991C5]  DEFAULT (null) FOR [FieldManagerSS]
GO
ALTER TABLE [dbo].[FieldManagerMaster] ADD  CONSTRAINT [DF__FieldManag__Name__4EBDB5FE]  DEFAULT (null) FOR [Name]
GO
ALTER TABLE [dbo].[FieldManagerMaster] ADD  CONSTRAINT [DF__FieldMana__Stree__4FB1DA37]  DEFAULT (null) FOR [Street]
GO
ALTER TABLE [dbo].[FieldManagerMaster] ADD  CONSTRAINT [DF__FieldMana__Fourt__50A5FE70]  DEFAULT (null) FOR [FourthLine]
GO
ALTER TABLE [dbo].[FieldManagerMaster] ADD  CONSTRAINT [DF__FieldManag__City__519A22A9]  DEFAULT (null) FOR [City]
GO
ALTER TABLE [dbo].[FieldManagerMaster] ADD  CONSTRAINT [DF__FieldMana__State__528E46E2]  DEFAULT (' ') FOR [State]
GO
ALTER TABLE [dbo].[FieldManagerMaster] ADD  CONSTRAINT [DF__FieldManage__Zip__53826B1B]  DEFAULT (null) FOR [Zip]
GO
ALTER TABLE [dbo].[FieldManagerMaster] ADD  CONSTRAINT [DF__FieldMana__Telep__54768F54]  DEFAULT (null) FOR [Telephone]
GO
ALTER TABLE [dbo].[FieldManagerMaster] ADD  CONSTRAINT [DF__FieldMana__Distr__556AB38D]  DEFAULT (null) FOR [DistrictManagerNo]
GO
ALTER TABLE [dbo].[FieldManagerMaster] ADD  CONSTRAINT [DF__FieldMana__MagCo__565ED7C6]  DEFAULT (0.0) FOR [MagCommPct]
GO
ALTER TABLE [dbo].[FieldManagerMaster] ADD  CONSTRAINT [DF__FieldMana__BMagC__5752FBFF]  DEFAULT (0.0) FOR [BMagCommPct]
GO
ALTER TABLE [dbo].[FieldManagerMaster] ADD  CONSTRAINT [DF__FieldMana__Music__58472038]  DEFAULT (0.0) FOR [MusicCommPct]
GO
ALTER TABLE [dbo].[FieldManagerMaster] ADD  CONSTRAINT [DF__FieldMana__BMusi__593B4471]  DEFAULT (0.0) FOR [BMusicCommPct]
GO
ALTER TABLE [dbo].[FieldManagerMaster] ADD  CONSTRAINT [DF__FieldMana__BOACo__5A2F68AA]  DEFAULT (0.0) FOR [BOACommPct]
GO
ALTER TABLE [dbo].[FieldManagerMaster] ADD  CONSTRAINT [DF__FieldMana__BookC__5B238CE3]  DEFAULT (0.0) FOR [BookCommPct]
GO
ALTER TABLE [dbo].[FieldManagerMaster] ADD  CONSTRAINT [DF__FieldMana__NMagC__5C17B11C]  DEFAULT (0.0) FOR [NMagCommPct]
GO
ALTER TABLE [dbo].[FieldManagerMaster] ADD  CONSTRAINT [DF__FieldMana__NBMag__5D0BD555]  DEFAULT (0.0) FOR [NBMagCommPct]
GO
ALTER TABLE [dbo].[FieldManagerMaster] ADD  CONSTRAINT [DF__FieldMana__NMusi__5DFFF98E]  DEFAULT (0.0) FOR [NMusicCommPct]
GO
ALTER TABLE [dbo].[FieldManagerMaster] ADD  CONSTRAINT [DF__FieldMana__NBMus__5EF41DC7]  DEFAULT (0.0) FOR [NBMusicCommPct]
GO
ALTER TABLE [dbo].[FieldManagerMaster] ADD  CONSTRAINT [DF__FieldMana__NBOAC__5FE84200]  DEFAULT (0.0) FOR [NBOACommPct]
GO
ALTER TABLE [dbo].[FieldManagerMaster] ADD  CONSTRAINT [DF__FieldMana__NBook__60DC6639]  DEFAULT (0.0) FOR [NBookCommPct]
GO
ALTER TABLE [dbo].[FieldManagerMaster] ADD  CONSTRAINT [DF__FieldMana__Overr__61D08A72]  DEFAULT (null) FOR [Override]
GO
ALTER TABLE [dbo].[FieldManagerMaster] ADD  CONSTRAINT [DF__FieldMana__OMagC__62C4AEAB]  DEFAULT (0.0) FOR [OMagCommPct]
GO
ALTER TABLE [dbo].[FieldManagerMaster] ADD  CONSTRAINT [DF__FieldMana__OBMag__63B8D2E4]  DEFAULT (0.0) FOR [OBMagCommPct]
GO
ALTER TABLE [dbo].[FieldManagerMaster] ADD  CONSTRAINT [DF__FieldMana__OMusi__64ACF71D]  DEFAULT (0.0) FOR [OMusicCommPct]
GO
ALTER TABLE [dbo].[FieldManagerMaster] ADD  CONSTRAINT [DF__FieldMana__OBMus__65A11B56]  DEFAULT (0.0) FOR [OBMusicCommPct]
GO
ALTER TABLE [dbo].[FieldManagerMaster] ADD  CONSTRAINT [DF__FieldMana__OBOAC__66953F8F]  DEFAULT (0.0) FOR [OBOACommPct]
GO
ALTER TABLE [dbo].[FieldManagerMaster] ADD  CONSTRAINT [DF__FieldMana__OBook__678963C8]  DEFAULT (0.0) FOR [OBookCommPct]
GO
ALTER TABLE [dbo].[FieldManagerMaster] ADD  CONSTRAINT [DF__FieldMana__ONMag__687D8801]  DEFAULT (0.0) FOR [ONMagCommPct]
GO
ALTER TABLE [dbo].[FieldManagerMaster] ADD  CONSTRAINT [DF__FieldMana__ONBMa__6971AC3A]  DEFAULT (0.0) FOR [ONBMagCommPct]
GO
ALTER TABLE [dbo].[FieldManagerMaster] ADD  CONSTRAINT [DF__FieldMana__ONMus__6A65D073]  DEFAULT (0.0) FOR [ONMusicCommPct]
GO
ALTER TABLE [dbo].[FieldManagerMaster] ADD  CONSTRAINT [DF__FieldMana__ONBMu__6B59F4AC]  DEFAULT (0.0) FOR [ONBMusicCommPct]
GO
ALTER TABLE [dbo].[FieldManagerMaster] ADD  CONSTRAINT [DF__FieldMana__ONBOA__6C4E18E5]  DEFAULT (0.0) FOR [ONBOACommPct]
GO
ALTER TABLE [dbo].[FieldManagerMaster] ADD  CONSTRAINT [DF__FieldMana__ONBoo__6D423D1E]  DEFAULT (0.0) FOR [ONBookCommPct]
GO
ALTER TABLE [dbo].[FieldManagerMaster] ADD  CONSTRAINT [DF__FieldManag__Zip4__6E366157]  DEFAULT (null) FOR [Zip4]
GO
ALTER TABLE [dbo].[FieldManagerMaster] ADD  CONSTRAINT [DF__FieldMana__FMEnd__6F2A8590]  DEFAULT (null) FOR [FMEnd]
GO
