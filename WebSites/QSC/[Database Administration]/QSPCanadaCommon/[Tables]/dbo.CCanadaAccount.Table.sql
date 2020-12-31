USE [QSPCanadaCommon]
GO
/****** Object:  Table [dbo].[CCanadaAccount]    Script Date: 06/07/2017 09:29:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CCanadaAccount](
	[Id] [int] NOT NULL,
	[Name] [varchar](50) NULL,
	[Address1] [varchar](50) NULL,
	[Address2] [varchar](50) NULL,
	[City] [varchar](50) NULL,
	[State] [char](2) NULL,
	[Zip] [varchar](6) NULL,
	[Zip4] [varchar](4) NULL,
	[Sponsor] [varchar](50) NULL,
	[FMID] [char](4) NULL,
	[FMRegion] [char](2) NULL,
	[ProgramStartDate] [smalldatetime] NULL,
	[ProgramEndDate] [smalldatetime] NULL,
	[SchoolType] [varchar](2) NULL,
	[NumberOfStudents] [int] NULL,
	[NumberOfClassRooms] [smallint] NULL,
	[ProgramType] [tinyint] NULL,
	[IsNational] [bit] NULL,
	[Status] [tinyint] NULL,
	[FlagpoleInstance] [int] NULL,
	[Comment] [varchar](500) NULL,
	[CreateDate] [smalldatetime] NULL,
	[CreateUser] [varchar](50) NULL,
	[LastUpdateDate] [smalldatetime] NULL,
	[UpdatedBy] [varchar](50) NULL,
	[County] [varchar](15) NULL,
	[ATrackSchoolType] [varchar](10) NULL,
	[TaxExemptNumber] [varchar](23) NULL,
	[Commission] [float] NULL,
	[ATrackDateCreated] [datetime] NULL,
	[ATrackUserIDCreated] [int] NULL,
	[ATrackDateChanged] [datetime] NULL,
	[ATrackUserIDChanged] [int] NULL,
	[InvoiceCalculationMethod] [int] NULL,
	[ATrackStatus] [int] NULL,
	[InvoicePercentage] [numeric](10, 2) NULL,
	[ShowSalesFiguresOnInvoice] [char](1) NULL,
	[ATrackProgramType] [int] NULL,
	[ATrackType] [int] NULL,
	[TaxRate] [float] NULL,
	[QMSStatus] [int] NULL,
	[OriginalAddress1] [varchar](50) NULL,
	[OriginalAddress2] [varchar](50) NULL,
	[OriginalCity] [varchar](50) NULL,
	[OriginalState] [char](2) NULL,
	[OriginalZip] [varchar](6) NULL,
	[OriginalZip4] [varchar](4) NULL,
	[MagNetAcctID] [int] NULL,
	[UnitsComm] [numeric](10, 2) NULL,
 CONSTRAINT [PK_CCanadaAccount] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[CCanadaAccount] ADD  CONSTRAINT [DF_CCanadaAccount_InvoiceCalculationMethod]  DEFAULT (0) FOR [InvoiceCalculationMethod]
GO
ALTER TABLE [dbo].[CCanadaAccount] ADD  CONSTRAINT [DF_CCanadaAccount_ATrackStatus]  DEFAULT (1400) FOR [ATrackStatus]
GO
ALTER TABLE [dbo].[CCanadaAccount] ADD  CONSTRAINT [DF_CCanadaAccount_InvoicePercentage]  DEFAULT (0.0) FOR [InvoicePercentage]
GO
ALTER TABLE [dbo].[CCanadaAccount] ADD  CONSTRAINT [DF_CCanadaAccount_ShowSalesFiguresOnInvoice]  DEFAULT ('N') FOR [ShowSalesFiguresOnInvoice]
GO
ALTER TABLE [dbo].[CCanadaAccount] ADD  CONSTRAINT [DF_CCanadaAccount_ATrackProgramType]  DEFAULT (0) FOR [ATrackProgramType]
GO
ALTER TABLE [dbo].[CCanadaAccount] ADD  CONSTRAINT [DF_CCanadaAccount_ATrackType]  DEFAULT (1600) FOR [ATrackType]
GO
ALTER TABLE [dbo].[CCanadaAccount] ADD  CONSTRAINT [DF_CCanadaAccount_TaxRate]  DEFAULT (0.0) FOR [TaxRate]
GO
ALTER TABLE [dbo].[CCanadaAccount] ADD  CONSTRAINT [DF_CCanadaAccount_QMSSta__25A691D2]  DEFAULT (null) FOR [QMSStatus]
GO
