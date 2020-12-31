USE [QSPCanadaProduct]
GO
/****** Object:  Table [dbo].[PUBLISHERS]    Script Date: 06/07/2017 09:17:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PUBLISHERS](
	[Pub_Nbr] [int] NOT NULL,
	[Pub_Status] [varchar](10) NULL,
	[Pub_Name] [varchar](80) NOT NULL,
	[Pub_Addr_1] [varchar](50) NULL,
	[Pub_Addr_2] [varchar](50) NULL,
	[Pub_City] [varchar](50) NULL,
	[Pub_State] [varchar](2) NULL,
	[Pub_Zip] [varchar](10) NULL,
	[Pub_Zip_Four] [varchar](4) NULL,
	[Pub_Tel] [varchar](14) NULL,
	[Pub_Fax] [varchar](14) NULL,
	[Pub_Change_Dt] [datetime] NULL,
	[Pub_Change_By] [int] NULL,
	[Pub_CountryCode] [varchar](10) NULL,
	[Pub_Contact_Name] [varchar](50) NULL,
	[Pub_Contact_Title] [varchar](50) NULL,
	[Pub_Contact_Email] [varchar](100) NULL,
	[Pub_Contact_Phone] [varchar](50) NULL,
	[Pub_Contact_Fax] [varchar](50) NULL,
	[Pub_UserName] [varchar](50) NULL,
	[Pub_Password] [varchar](50) NULL,
 CONSTRAINT [PK_PUBLISHERS] PRIMARY KEY CLUSTERED 
(
	[Pub_Nbr] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
