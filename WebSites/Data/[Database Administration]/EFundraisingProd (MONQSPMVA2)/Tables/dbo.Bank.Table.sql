USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Bank]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Bank](
	[Bank_ID] [int] NOT NULL,
	[Name] [varchar](50) NULL,
	[Contact] [varchar](50) NULL,
	[Street_Address] [varchar](100) NULL,
	[State_Code] [varchar](10) NULL,
	[City] [varchar](50) NULL,
	[Zip_Code] [varchar](10) NULL,
	[Country_Code] [varchar](10) NULL,
	[Telephone] [varchar](20) NULL,
	[Fax] [varchar](20) NULL,
 CONSTRAINT [PK_Bank] PRIMARY KEY CLUSTERED 
(
	[Bank_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
