USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Double_Lead]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Double_Lead](
	[Division_ID] [int] NULL,
	[Promotion_ID] [int] NULL,
	[Temp_Lead_Id] [int] NOT NULL,
	[Channel_Code] [varchar](4) NULL,
	[Lead_Status_ID] [int] NULL,
	[Consultant_ID] [int] NULL,
	[Lead_Entry_Date] [datetime] NULL,
	[Salutation] [varchar](10) NULL,
	[First_Name] [varchar](50) NULL,
	[Last_Name] [varchar](50) NULL,
	[Organization] [varchar](100) NULL,
	[Street_Address] [varchar](100) NULL,
	[City] [varchar](50) NULL,
	[State_Code] [varchar](10) NULL,
	[Country_Code] [varchar](10) NULL,
	[Zip_Code] [varchar](10) NULL,
	[Day_Phone] [varchar](20) NULL,
	[Day_Time_Call] [varchar](20) NULL,
	[Evening_Phone] [varchar](20) NULL,
	[Fax] [varchar](20) NULL,
	[Email] [varchar](50) NULL,
	[Group_Type_ID] [int] NULL,
	[Participant_Count] [int] NULL,
	[Fund_Raising_Goal] [int] NULL,
	[Decision_Date] [datetime] NULL,
	[Decision_Maker] [bit] NOT NULL,
	[Fund_Raiser_Start_Date] [datetime] NULL,
	[OnEmailList] [bit] NOT NULL,
	[Comments] [text] NULL,
	[Hear_Id] [int] NULL,
	[kit_to_send] [bit] NOT NULL,
	[kit_sent] [bit] NOT NULL,
	[kit_sent_date] [datetime] NULL,
	[Day_Phone_Ext] [varchar](10) NULL,
	[Evening_Phone_Ext] [varchar](10) NULL,
	[Rejection_reason] [text] NULL,
	[Other_Phone] [varchar](20) NULL,
	[Cookie_Content] [varchar](255) NULL,
	[Group_Web_Site] [varchar](50) NULL,
	[Organization_Type_Id] [int] NULL,
	[Title_Id] [int] NULL,
	[Other_Phone_Ext] [varchar](10) NULL,
	[Campaign_Reason_Id] [int] NULL,
	[Web_Site_Id] [int] NULL,
 CONSTRAINT [PK_Double_Lead] PRIMARY KEY NONCLUSTERED 
(
	[Temp_Lead_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Temp_Lead_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Temp_Lead_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Double_Lead] ADD  CONSTRAINT [DF_Double_Lead_Division_ID]  DEFAULT (1) FOR [Division_ID]
GO
ALTER TABLE [dbo].[Double_Lead] ADD  CONSTRAINT [DF_Double_Lead_Channel_Code]  DEFAULT ('INT') FOR [Channel_Code]
GO
ALTER TABLE [dbo].[Double_Lead] ADD  CONSTRAINT [DF_Double_Lead_Lead_Status_ID]  DEFAULT (1) FOR [Lead_Status_ID]
GO
ALTER TABLE [dbo].[Double_Lead] ADD  CONSTRAINT [DF_Double_Lead_Lead_Entry_Date]  DEFAULT (getdate()) FOR [Lead_Entry_Date]
GO
ALTER TABLE [dbo].[Double_Lead] ADD  CONSTRAINT [DF_Double_Lead_Participant_Count]  DEFAULT (0) FOR [Participant_Count]
GO
ALTER TABLE [dbo].[Double_Lead] ADD  CONSTRAINT [DF_Double_Lead_Fund_Raising_Goal]  DEFAULT (0) FOR [Fund_Raising_Goal]
GO
ALTER TABLE [dbo].[Double_Lead] ADD  CONSTRAINT [DF_Double_Lead_Decision_Maker]  DEFAULT (0) FOR [Decision_Maker]
GO
ALTER TABLE [dbo].[Double_Lead] ADD  CONSTRAINT [DF_Double_Lead_OnEmailList]  DEFAULT (1) FOR [OnEmailList]
GO
ALTER TABLE [dbo].[Double_Lead] ADD  CONSTRAINT [DF_Double_Lead_Hear_Id]  DEFAULT (0) FOR [Hear_Id]
GO
ALTER TABLE [dbo].[Double_Lead] ADD  CONSTRAINT [DF_Double_Lead_kit_to_send]  DEFAULT (0) FOR [kit_to_send]
GO
ALTER TABLE [dbo].[Double_Lead] ADD  CONSTRAINT [DF_Double_Lead_kit_sent]  DEFAULT (0) FOR [kit_sent]
GO
ALTER TABLE [dbo].[Double_Lead] ADD  CONSTRAINT [DF_Double_Lead_Organization_Type_Id]  DEFAULT (99) FOR [Organization_Type_Id]
GO
ALTER TABLE [dbo].[Double_Lead] ADD  CONSTRAINT [DF_Double_Lead_Campaign_Reason_Id]  DEFAULT (99) FOR [Campaign_Reason_Id]
GO
