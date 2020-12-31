USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[CustomerAddressHistory]    Script Date: 06/07/2017 09:18:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerAddressHistory](
	[Date] [datetime] NOT NULL,
	[Instance] [int] NOT NULL,
	[StatusInstance] [int] NULL,
	[LastName] [varchar](40) NULL,
	[FirstName] [varchar](40) NULL,
	[Address1] [varchar](50) NULL,
	[Address2] [varchar](50) NULL,
	[City] [varchar](50) NULL,
	[County] [varchar](31) NULL,
	[State] [varchar](10) NULL,
	[Zip] [varchar](10) NULL,
	[ZipPlusFour] [varchar](4) NULL,
	[OverrideAddress] [bit] NULL,
	[ChangeUserID] [varchar](4) NULL,
	[ChangeDate] [datetime] NULL,
	[Email] [varchar](50) NULL,
	[Phone] [varchar](25) NULL,
	[Type] [int] NULL,
	[CustomerOrderHeaderInstance] [int] NULL,
	[TransID] [int] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
