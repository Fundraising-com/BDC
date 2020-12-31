USE [QSPCanadaCommon]
GO
/****** Object:  Table [dbo].[postcard_data_2011x]    Script Date: 06/07/2017 09:29:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[postcard_data_2011x](
	[postcard_data_id] [int] IDENTITY(1,1) NOT NULL,
	[HEADER_KEY_FIELD] [varchar](1) NOT NULL,
	[HEADER_ACCT_NAME] [varchar](50) NULL,
	[HEADER_ACCT_#] [int] NOT NULL,
	[HEADER_FM_DISTRICT] [int] NULL,
	[HEADER_FMID] [varchar](4) NULL,
	[HEADER_FSM_ID] [varchar](34) NULL,
	[HEADER_FSM_NAME_FIRST] [varchar](50) NULL,
	[HEADER_FSM_NAME_LAST] [varchar](50) NULL,
	[HEADER_FSM_NAME] [varchar](101) NULL,
	[HEADER_FSM_STREET_ADDRESS] [varchar](50) NULL,
	[HEADER_CITY] [varchar](50) NULL,
	[HEADER_STATE] [varchar](2) NULL,
	[HEADER_ZIP] [varchar](6) NULL,
	[HEADER_CITY_STATE_ZIP] [varchar](60) NULL,
	[STUDENT_DET_KEY_FIELD] [varchar](1) NOT NULL,
	[STUDENT_DET_STUDENT_NAME_FIRST] [varchar](50) NULL,
	[STUDENT_DET_STUDENT_NAME_LAST] [varchar](50) NULL,
	[CALCULATED_STUDENT_NAME] [varchar](101) NULL,
	[STUDENT_DET_STUDENT_NAME] [varchar](50) NULL,
	[STUDENT_DET_CUSTOMER_NAME_FIRST] [varchar](50) NULL,
	[STUDENT_DET_CUSTOMER_NAME_LAST] [varchar](50) NULL,
	[STUDENT_DET_CUSTOMER_NAME] [varchar](101) NULL,
	[STUDENT_DET_TITLE1] [varchar](30) NULL,
	[STUDENT_DET_PAGE_BREAK_INDICATOR] [varchar](1) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
