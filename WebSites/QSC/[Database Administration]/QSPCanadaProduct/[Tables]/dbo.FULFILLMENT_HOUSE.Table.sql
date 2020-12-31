USE [QSPCanadaProduct]
GO
/****** Object:  Table [dbo].[FULFILLMENT_HOUSE]    Script Date: 06/07/2017 09:17:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FULFILLMENT_HOUSE](
	[Ful_Nbr] [varchar](3) NOT NULL,
	[Ful_Status] [varchar](10) NULL,
	[Ful_Name] [varchar](80) NULL,
	[Ful_Addr_1] [varchar](50) NULL,
	[Ful_Addr_2] [varchar](50) NULL,
	[Ful_City] [varchar](25) NULL,
	[Ful_State] [varchar](2) NULL,
	[Ful_Zip] [varchar](10) NULL,
	[Ful_Zip_Four] [varchar](4) NULL,
	[Ful_Tel] [varchar](14) NULL,
	[Ful_Fax] [varchar](14) NULL,
	[Ful_Change_Dt] [datetime] NULL,
	[Ful_Change_By] [int] NULL,
	[InterfaceMediaID] [int] NULL,
	[InterfaceLayoutID] [int] NULL,
	[QSPAgencyCode] [varchar](20) NULL,
	[CountryCode] [varchar](2) NULL,
	[IsEffortKey] [char](1) NULL,
	[IsCancelFileReqd] [char](1) NULL,
	[TransmissionMethodID] [int] NULL,
	[HardCopy] [bit] NULL,
 CONSTRAINT [PK_FULFILLMENT_HOUSE] PRIMARY KEY CLUSTERED 
(
	[Ful_Nbr] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
