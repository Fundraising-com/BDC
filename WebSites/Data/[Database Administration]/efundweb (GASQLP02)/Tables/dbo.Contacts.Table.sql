USE [eFundweb]
GO
/****** Object:  Table [dbo].[Contacts]    Script Date: 02/14/2014 16:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Contacts](
	[Contact_ID] [int] NOT NULL,
	[First_Name] [varchar](50) NULL,
	[Last_Name] [varchar](50) NULL,
	[Phone_Number] [varchar](20) NULL,
	[Phone_Ext] [varchar](10) NULL,
	[Street_Address] [varchar](20) NULL,
	[City] [varchar](20) NULL,
	[State_Code] [varchar](10) NULL,
	[Country_Code] [varchar](10) NULL,
	[Zip_Code] [varchar](10) NULL,
	[Comments] [varchar](100) NULL,
 CONSTRAINT [PK_Contacts] PRIMARY KEY CLUSTERED 
(
	[Contact_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
