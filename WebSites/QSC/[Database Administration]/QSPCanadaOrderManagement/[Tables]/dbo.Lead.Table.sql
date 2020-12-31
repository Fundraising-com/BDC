USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[Lead]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Lead](
	[UserID] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
	[ContactName] [varchar](100) NOT NULL,
	[ContactHomePhoneNumber] [varchar](14) NULL,
	[ContactWorkPhoneNumber] [varchar](14) NOT NULL,
	[ContactFaxNumber] [varchar](14) NULL,
	[ContactEMail] [varchar](50) NULL,
	[SchoolGroup] [varchar](50) NULL,
	[CityTown] [varchar](50) NULL,
	[Province] [varchar](20) NOT NULL,
	[InterestedInWhat] [varchar](250) NULL,
	[WhereHearAboutQSP] [varchar](250) NULL,
	[Comments] [varchar](250) NULL,
	[FMID] [varchar](4) NULL,
	[DateSent] [datetime] NULL,
	[Instance] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Lead] PRIMARY KEY CLUSTERED 
(
	[Instance] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
