USE [QSPCanadaFinance]
GO
/****** Object:  Table [dbo].[InvoiceInfo]    Script Date: 06/07/2017 09:16:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[InvoiceInfo](
	[Name] [varchar](50) NULL,
	[Address] [varchar](50) NULL,
	[City] [varchar](50) NULL,
	[State] [char](10) NULL,
	[Zip] [varchar](10) NULL,
	[TaxID] [varchar](10) NULL,
	[CustomerServicePhone] [varchar](50) NULL,
	[MagazineBilling] [varchar](50) NULL,
	[PrizeDivision] [varchar](50) NULL,
	[Note1] [varchar](250) NULL,
	[Note2] [varchar](250) NULL,
	[Note3] [varchar](250) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
