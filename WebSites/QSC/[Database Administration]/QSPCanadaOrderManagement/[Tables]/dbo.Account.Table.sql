USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 06/07/2017 09:18:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Account](
	[ID] [int] NOT NULL,
	[Name] [varchar](50) NULL,
	[Address1] [varchar](50) NULL,
	[Address2] [varchar](50) NULL,
	[City] [varchar](50) NULL,
	[State] [dbo].[State_UDDT] NULL,
	[Zip] [varchar](12) NULL,
	[ZipPlusFour] [varchar](4) NULL,
	[AttnLine] [varchar](25) NULL,
	[FieldManagerNo] [varchar](4) NULL,
	[FieldManagerRegion] [varchar](2) NULL,
	[County] [varchar](15) NULL,
	[CountyCode] [varchar](3) NULL,
	[SchoolType] [varchar](2) NULL,
	[IsNational] [dbo].[Boolean_UDDT] NOT NULL,
	[PublicCatholic] [varchar](2) NULL,
	[TaxExemptNumber] [varchar](10) NULL,
	[CampaignStart] [datetime] NOT NULL,
	[CampaignEnd] [datetime] NOT NULL,
	[UnitType] [varchar](1) NULL,
	[Commission] [float] NOT NULL,
	[NationalDistrict] [varchar](2) NULL,
	[NationalFieldManager] [varchar](4) NULL,
	[SchoolDistrictName] [varchar](30) NULL,
	[NumberOfClassrooms] [int] NOT NULL,
	[NumberOfStudents] [int] NOT NULL,
	[ShipToAcctOrFM] [varchar](1) NULL,
	[AMFMInd] [varchar](1) NULL,
 CONSTRAINT [aaaaaAccount_PK] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Account] ADD  CONSTRAINT [DF__Account__ID__03D09CBB]  DEFAULT (0) FOR [ID]
GO
ALTER TABLE [dbo].[Account] ADD  CONSTRAINT [DF__Account__Name__04C4C0F4]  DEFAULT (null) FOR [Name]
GO
ALTER TABLE [dbo].[Account] ADD  CONSTRAINT [DF__Account__Address__05B8E52D]  DEFAULT (null) FOR [Address1]
GO
ALTER TABLE [dbo].[Account] ADD  CONSTRAINT [DF__Account__Address__06AD0966]  DEFAULT (null) FOR [Address2]
GO
ALTER TABLE [dbo].[Account] ADD  CONSTRAINT [DF__Account__City__07A12D9F]  DEFAULT (null) FOR [City]
GO
ALTER TABLE [dbo].[Account] ADD  CONSTRAINT [DF__Account__State__089551D8]  DEFAULT (' ') FOR [State]
GO
ALTER TABLE [dbo].[Account] ADD  CONSTRAINT [DF__Account__Zip__09897611]  DEFAULT (null) FOR [Zip]
GO
ALTER TABLE [dbo].[Account] ADD  CONSTRAINT [DF__Account__ZipPlus__0A7D9A4A]  DEFAULT (null) FOR [ZipPlusFour]
GO
ALTER TABLE [dbo].[Account] ADD  CONSTRAINT [DF__Account__AttnLin__0B71BE83]  DEFAULT (null) FOR [AttnLine]
GO
ALTER TABLE [dbo].[Account] ADD  CONSTRAINT [DF__Account__FieldMa__0C65E2BC]  DEFAULT (null) FOR [FieldManagerNo]
GO
ALTER TABLE [dbo].[Account] ADD  CONSTRAINT [DF__Account__FieldMa__0D5A06F5]  DEFAULT (null) FOR [FieldManagerRegion]
GO
ALTER TABLE [dbo].[Account] ADD  CONSTRAINT [DF__Account__County__0E4E2B2E]  DEFAULT (null) FOR [County]
GO
ALTER TABLE [dbo].[Account] ADD  CONSTRAINT [DF__Account__CountyC__0F424F67]  DEFAULT (null) FOR [CountyCode]
GO
ALTER TABLE [dbo].[Account] ADD  CONSTRAINT [DF__Account__SchoolT__103673A0]  DEFAULT (null) FOR [SchoolType]
GO
ALTER TABLE [dbo].[Account] ADD  CONSTRAINT [DF__Account__PublicC__112A97D9]  DEFAULT (null) FOR [PublicCatholic]
GO
ALTER TABLE [dbo].[Account] ADD  CONSTRAINT [DF__Account__TaxExem__121EBC12]  DEFAULT (null) FOR [TaxExemptNumber]
GO
ALTER TABLE [dbo].[Account] ADD  CONSTRAINT [DF__Account__Campaig__1312E04B]  DEFAULT ('1/1/1995') FOR [CampaignStart]
GO
ALTER TABLE [dbo].[Account] ADD  CONSTRAINT [DF__Account__Campaig__14070484]  DEFAULT ('1/1/1995') FOR [CampaignEnd]
GO
ALTER TABLE [dbo].[Account] ADD  CONSTRAINT [DF__Account__UnitTyp__14FB28BD]  DEFAULT (null) FOR [UnitType]
GO
ALTER TABLE [dbo].[Account] ADD  CONSTRAINT [DF__Account__Commiss__15EF4CF6]  DEFAULT (0.0) FOR [Commission]
GO
ALTER TABLE [dbo].[Account] ADD  CONSTRAINT [DF__Account__Nationa__16E3712F]  DEFAULT (null) FOR [NationalDistrict]
GO
ALTER TABLE [dbo].[Account] ADD  CONSTRAINT [DF__Account__Nationa__17D79568]  DEFAULT (null) FOR [NationalFieldManager]
GO
ALTER TABLE [dbo].[Account] ADD  CONSTRAINT [DF__Account__SchoolD__18CBB9A1]  DEFAULT (null) FOR [SchoolDistrictName]
GO
ALTER TABLE [dbo].[Account] ADD  CONSTRAINT [DF__Account__NumberO__19BFDDDA]  DEFAULT (0) FOR [NumberOfClassrooms]
GO
ALTER TABLE [dbo].[Account] ADD  CONSTRAINT [DF__Account__NumberO__1AB40213]  DEFAULT (0) FOR [NumberOfStudents]
GO
ALTER TABLE [dbo].[Account] ADD  CONSTRAINT [DF__Account__ShipToA__1BA8264C]  DEFAULT (null) FOR [ShipToAcctOrFM]
GO
