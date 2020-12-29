USE [eFundweb]
GO
/****** Object:  Table [dbo].[Visit]    Script Date: 02/14/2014 16:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Visit](
	[Visit_ID] [int] IDENTITY(1,1) NOT NULL,
	[Session_ID] [varchar](255) NOT NULL,
	[Ads_Script] [varchar](255) NULL,
	[Referer] [varchar](255) NULL,
	[Expire] [int] NOT NULL,
	[Filled_Form] [bit] NULL,
	[Promotion_ID] [int] NULL,
	[Cookie_Content] [varchar](255) NULL,
	[Temp_Lead_ID] [int] NULL,
	[Entry_Date] [datetime] NULL,
	[Host] [varchar](255) NULL,
	[Query_String] [varchar](255) NULL,
	[Script_Name] [varchar](255) NULL,
	[GTSE] [varchar](50) NULL,
	[Keyword] [varchar](100) NULL,
	[PID] [varchar](100) NULL,
	[Web_Site_ID] [int] NULL,
	[Partner_ID] [int] NULL,
	[Has_Visited_Form_Page] [bit] NOT NULL,
	[Elapsed_Completion_Time] [numeric](5, 2) NULL,
	[Elapsed_Visit_Time] [numeric](5, 2) NULL,
	[Is_Submitted] [bit] NOT NULL,
	[Viewed_Page] [smallint] NULL,
	[IP_Address] [char](15) NULL,
 CONSTRAINT [PK_Visit] PRIMARY KEY CLUSTERED 
(
	[Visit_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Visit] ADD  CONSTRAINT [DF__Visit__Entry_Dat__1920BF5C]  DEFAULT (getdate()) FOR [Entry_Date]
GO
ALTER TABLE [dbo].[Visit] ADD  CONSTRAINT [DF_Visit_Has_Visited_Form_Page]  DEFAULT (0) FOR [Has_Visited_Form_Page]
GO
ALTER TABLE [dbo].[Visit] ADD  CONSTRAINT [DF_Visit_Is_Submitted]  DEFAULT (0) FOR [Is_Submitted]
GO
