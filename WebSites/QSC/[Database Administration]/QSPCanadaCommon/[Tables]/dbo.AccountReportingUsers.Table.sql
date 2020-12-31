USE [QSPCanadaCommon]
GO
/****** Object:  Table [dbo].[AccountReportingUsers]    Script Date: 06/07/2017 09:29:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AccountReportingUsers](
	[x_business_division_id] [int] NULL,
	[fulf_campaign_id] [int] NULL,
	[fulf_account_id] [int] NULL,
	[Password] [varchar](20) NULL,
	[Create_Date] [datetime] NULL,
	[Modify_Date] [datetime] NULL,
	[Modified_By] [varchar](50) NULL,
	[DeletedTF] [bit] NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
