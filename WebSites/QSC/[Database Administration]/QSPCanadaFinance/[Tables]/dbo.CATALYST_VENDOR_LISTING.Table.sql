USE [QSPCanadaFinance]
GO
/****** Object:  Table [dbo].[CATALYST_VENDOR_LISTING]    Script Date: 06/07/2017 09:16:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CATALYST_VENDOR_LISTING](
	[Vendor_Name] [nvarchar](255) NULL,
	[Vendor_Number] [float] NULL,
	[Vendor_Site] [nvarchar](255) NULL,
	[Address_Line1] [nvarchar](255) NULL,
	[Address_Line2] [nvarchar](255) NULL,
	[Address_Line3] [nvarchar](255) NULL,
	[City] [nvarchar](255) NULL,
	[Country] [nvarchar](255) NULL,
	[Inactive_Date] [smalldatetime] NULL,
	[Invoice_Currency] [nvarchar](255) NULL,
	[Pay_Group] [nvarchar](255) NULL,
	[Pay_Site] [nvarchar](255) NULL,
	[Payment_Currency] [nvarchar](255) NULL,
	[Province] [nvarchar](255) NULL,
	[State] [nvarchar](255) NULL,
	[Postal] [nvarchar](255) NULL,
	[Last_Updated] [smalldatetime] NULL
) ON [PRIMARY]
GO
