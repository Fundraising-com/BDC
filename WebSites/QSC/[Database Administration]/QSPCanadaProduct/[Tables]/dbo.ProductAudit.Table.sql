USE [QSPCanadaProduct]
GO
/****** Object:  Table [dbo].[ProductAudit]    Script Date: 06/07/2017 09:17:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProductAudit](
	[AuditInstance] [int] IDENTITY(1,1) NOT NULL,
	[AuditDate] [datetime] NOT NULL,
	[Product_Instance] [int] NOT NULL,
	[Product_Code] [varchar](20) NOT NULL,
	[Product_Year] [int] NULL,
	[Product_Season] [char](1) NULL,
	[Alpha_Code] [varchar](4) NULL,
	[Product_Name] [varchar](55) NULL,
	[Product_Sort_Name] [varchar](55) NULL,
	[Pub_Nbr] [int] NULL,
	[Ages] [varchar](20) NULL,
	[Internet] [varchar](3) NULL,
	[Issue_Rcvd_Dt] [smalldatetime] NULL,
	[CoverReceived] [varchar](3) NULL,
	[HighlightCover] [int] NULL,
	[Featuring] [int] NULL,
	[Status] [varchar](15) NULL,
	[Comment] [varchar](200) NULL,
	[CommentDate] [datetime] NULL,
	[Category_Code] [int] NULL,
	[Fulfill_House_Nbr] [varchar](3) NULL,
	[Mail_Dt] [varchar](30) NULL,
	[Auth_Form_Rtrn_Dt] [smalldatetime] NULL,
	[IssueDateUsed] [varchar](30) NULL,
	[Logged_By] [varchar](15) NULL,
	[Log_Dt] [smalldatetime] NULL,
	[Lang] [varchar](10) NULL,
	[ProductLine] [int] NULL,
	[DaysLeadTime] [int] NULL,
	[VendorNumber] [varchar](30) NULL,
	[VendorSiteName] [varchar](15) NULL,
	[PayGroupLookUpCode] [varchar](25) NULL,
	[TermsName] [varchar](50) NULL,
	[Currency] [int] NULL,
	[CountryCode] [varchar](10) NULL,
	[Type] [int] NULL,
	[UnitOfMeasure] [varchar](10) NULL,
	[UOMConvFactor] [int] NULL,
	[UnitWeight] [numeric](10, 2) NULL,
	[UnitCost] [numeric](10, 2) NULL,
	[OracleCode] [varchar](50) NULL,
	[Prize_Level] [varchar](10) NULL,
	[Prize_Level_Qty_Required] [int] NULL,
	[Nbr_Of_Issues_Per_Year] [int] NULL,
	[RemitCode] [varchar](20) NULL,
	[IsQSPExclusive] [bit] NULL,
 CONSTRAINT [PK_ProductAudit] PRIMARY KEY CLUSTERED 
(
	[AuditInstance] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
