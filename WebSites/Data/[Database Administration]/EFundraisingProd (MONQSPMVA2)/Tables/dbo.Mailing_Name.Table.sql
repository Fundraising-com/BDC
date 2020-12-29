USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Mailing_Name]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Mailing_Name](
	[Mailing_Name_ID] [int] IDENTITY(365451,1) NOT FOR REPLICATION NOT NULL,
	[List_Name] [varchar](25) NOT NULL,
	[List_ID] [int] NOT NULL,
	[Contact_Name] [varchar](50) NULL,
	[Title] [varchar](35) NULL,
	[School_Name] [varchar](60) NULL,
	[School_Address] [varchar](70) NULL,
	[City] [varchar](30) NULL,
	[State_Code] [varchar](4) NULL,
	[Zip] [varchar](15) NULL,
	[Phone_Number] [varchar](15) NULL,
	[Fax_Number] [varchar](15) NULL,
	[email] [varchar](50) NULL,
	[School_Type] [varchar](2) NULL,
 CONSTRAINT [PK_Mailing_Name] PRIMARY KEY CLUSTERED 
(
	[Mailing_Name_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
