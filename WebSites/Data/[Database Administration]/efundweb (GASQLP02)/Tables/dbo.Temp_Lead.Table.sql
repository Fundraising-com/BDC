USE [eFundweb]
GO
/****** Object:  Table [dbo].[Temp_Lead]    Script Date: 02/14/2014 16:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Temp_Lead](
	[Division_ID] [int] NULL,
	[Promotion_ID] [int] NULL,
	[Temp_Lead_ID] [int] IDENTITY(1,1) NOT NULL,
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
	[Comments] [varchar](2000) NULL,
	[Hear_ID] [int] NULL,
	[kit_to_send] [bit] NOT NULL,
	[kit_sent] [bit] NOT NULL,
	[kit_sent_date] [datetime] NULL,
	[Day_Phone_Ext] [varchar](10) NULL,
	[Evening_Phone_Ext] [varchar](10) NULL,
	[Rejection_reason] [varchar](2000) NULL,
	[Other_Phone] [varchar](20) NULL,
	[Cookie_Content] [varchar](255) NULL,
	[Group_Web_Site] [varchar](50) NULL,
	[Organization_Type_ID] [int] NULL,
	[Title_ID] [int] NULL,
	[Campaign_Reason_ID] [int] NULL,
	[Web_Site_ID] [int] NULL,
	[Other_Phone_Ext] [varchar](10) NULL,
	[IsNew] [bit] NOT NULL,
	[Opt_In_Sweepstakes] [bit] NULL,
	[Group_ID] [int] NULL,
	[create_login] [varchar](128) NULL,
	[create_appname] [varchar](128) NULL,
	[create_hostname] [varchar](128) NULL,
	[create_date] [datetime] NULL,
 CONSTRAINT [PK_Temp_Lead] PRIMARY KEY CLUSTERED 
(
	[Temp_Lead_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Temp_Lead] ADD  CONSTRAINT [DF__Temp_Lead__Divis__5070F446]  DEFAULT (1) FOR [Division_ID]
GO
ALTER TABLE [dbo].[Temp_Lead] ADD  CONSTRAINT [DF__Temp_Lead__Chann__5165187F]  DEFAULT ('INT') FOR [Channel_Code]
GO
ALTER TABLE [dbo].[Temp_Lead] ADD  CONSTRAINT [DF__Temp_Lead__Lead___52593CB8]  DEFAULT (1) FOR [Lead_Status_ID]
GO
ALTER TABLE [dbo].[Temp_Lead] ADD  CONSTRAINT [DF__Temp_Lead__Lead___534D60F1]  DEFAULT (getdate()) FOR [Lead_Entry_Date]
GO
ALTER TABLE [dbo].[Temp_Lead] ADD  CONSTRAINT [DF__Temp_Lead__Parti__5441852A]  DEFAULT (0) FOR [Participant_Count]
GO
ALTER TABLE [dbo].[Temp_Lead] ADD  CONSTRAINT [DF__Temp_Lead__Fund___5535A963]  DEFAULT (0) FOR [Fund_Raising_Goal]
GO
ALTER TABLE [dbo].[Temp_Lead] ADD  CONSTRAINT [DF__Temp_Lead__Decis__5629CD9C]  DEFAULT ('0') FOR [Decision_Maker]
GO
ALTER TABLE [dbo].[Temp_Lead] ADD  CONSTRAINT [DF__Temp_Lead__OnEma__571DF1D5]  DEFAULT (1) FOR [OnEmailList]
GO
ALTER TABLE [dbo].[Temp_Lead] ADD  CONSTRAINT [DF__Temp_Lead__Hear___5812160E]  DEFAULT (0) FOR [Hear_ID]
GO
ALTER TABLE [dbo].[Temp_Lead] ADD  CONSTRAINT [DF__Temp_Lead__kit_t__59063A47]  DEFAULT ('0') FOR [kit_to_send]
GO
ALTER TABLE [dbo].[Temp_Lead] ADD  CONSTRAINT [DF__Temp_Lead__kit_s__59FA5E80]  DEFAULT ('0') FOR [kit_sent]
GO
ALTER TABLE [dbo].[Temp_Lead] ADD  CONSTRAINT [DF__Temp_Lead__Organ__5AEE82B9]  DEFAULT (99) FOR [Organization_Type_ID]
GO
ALTER TABLE [dbo].[Temp_Lead] ADD  CONSTRAINT [DF__Temp_Lead__Campa__5BE2A6F2]  DEFAULT (99) FOR [Campaign_Reason_ID]
GO
ALTER TABLE [dbo].[Temp_Lead] ADD  CONSTRAINT [DF__Temp_Lead__IsNew__5CD6CB2B]  DEFAULT (1) FOR [IsNew]
GO
