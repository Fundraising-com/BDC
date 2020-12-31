USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[BadAddress]    Script Date: 06/07/2017 09:18:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BadAddress](
	[customerorderheaderinstance] [int] NOT NULL,
	[transid] [int] NOT NULL,
	[RemitBatchID] [int] NOT NULL,
	[Instance] [int] NOT NULL,
	[CustomerInstance] [int] NULL,
	[StatusInstance] [int] NULL,
	[LastName] [varchar](50) NULL,
	[FirstName] [varchar](50) NULL,
	[Address1] [varchar](50) NULL,
	[Address2] [varchar](50) NULL,
	[City] [varchar](50) NULL,
	[State] [varchar](10) NULL,
	[Zip] [varchar](20) NULL,
	[ZipPlusFour] [varchar](4) NULL,
	[DateModified] [datetime] NULL,
	[UserIDModified] [dbo].[UserID_UDDT] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
