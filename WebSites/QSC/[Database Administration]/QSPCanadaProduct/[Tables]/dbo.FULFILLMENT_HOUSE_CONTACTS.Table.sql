USE [QSPCanadaProduct]
GO
/****** Object:  Table [dbo].[FULFILLMENT_HOUSE_CONTACTS]    Script Date: 06/07/2017 09:17:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FULFILLMENT_HOUSE_CONTACTS](
	[Instance] [int] IDENTITY(1,1) NOT NULL,
	[Product_Code] [varchar](10) NULL,
	[Ful_Nbr] [int] NOT NULL,
	[FirstName] [varchar](50) NULL,
	[LastName] [varchar](50) NULL,
	[Title] [varchar](50) NULL,
	[Email] [varchar](100) NULL,
	[WorkPhone] [varchar](20) NULL,
	[Fax] [varchar](20) NULL,
	[CustSvcContactQSPFirstName] [varchar](50) NULL,
	[CustSvcContactQSPLastName] [varchar](50) NULL,
	[CustSvcContactQSPEmail] [varchar](100) NULL,
	[CustSvcContactQSPPhone] [varchar](50) NULL,
	[DateChanged] [datetime] NULL,
	[UserIDChanged] [int] NULL,
 CONSTRAINT [PK_FULFILLMENT_HOUSE_CONTACTS] PRIMARY KEY CLUSTERED 
(
	[Instance] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
