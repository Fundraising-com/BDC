USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[qsp_matching_code]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[qsp_matching_code](
	[id] [int] NULL,
	[group_name] [varchar](200) NULL,
	[address] [varchar](100) NULL,
	[zip_code] [varchar](10) NULL,
	[zzzzz] [varchar](5) NULL,
	[nnn] [varchar](3) NULL,
	[aa99] [varchar](4) NULL,
	[zzzzzaa99] [varchar](9) NULL,
	[zzzzznnnaa99] [varchar](12) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
