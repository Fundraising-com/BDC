USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[mag_lead]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[mag_lead](
	[id] [int] NOT NULL,
	[BATCHNO] [varchar](8000) NULL,
	[SEQNO] [varchar](8000) NULL,
	[ISS_MM] [varchar](8000) NULL,
	[ISS_DD] [varchar](8000) NULL,
	[ISS_YY] [varchar](8000) NULL,
	[PUB_CODE] [varchar](8000) NULL,
	[CARD_TYPE] [varchar](8000) NULL,
	[FIRST] [varchar](8000) NULL,
	[MIDDLE] [varchar](8000) NULL,
	[LAST] [varchar](8000) NULL,
	[COMPANY] [varchar](8000) NULL,
	[ADDRESS1] [varchar](8000) NULL,
	[ADDRESS2] [varchar](8000) NULL,
	[CITY] [varchar](8000) NULL,
	[STATE] [varchar](8000) NULL,
	[ZIP] [varchar](8000) NULL,
	[PLUS4] [varchar](8000) NULL,
	[PHONE] [varchar](8000) NULL,
	[COUNTRY] [varchar](8000) NULL,
	[RS_NO] [varchar](8000) NULL,
	[EMAIL] [varchar](8000) NULL,
	[done] [bit] NOT NULL,
	[remove] [int] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
