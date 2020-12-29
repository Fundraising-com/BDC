USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Temp_Lead]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Temp_Lead](
	[Division_ID] [int] NULL,
	[Promotion_ID] [int] NULL,
	[Temp_Lead_Id] [int] IDENTITY(129511,1) NOT FOR REPLICATION NOT NULL,
	[Channel_Code] [varchar](4) NULL,
	[Lead_Status_ID] [int] NULL,
	[Consultant_ID] [int] NULL,
	[Lead_Entry_Date] [datetime] NULL,
	[Salutation] [varchar](10) NULL,
	[First_Name] [varchar](50) NULL,
	[Last_Name] [varchar](50) NULL,
	[Title] [varchar](50) NULL,
	[Organization] [varchar](100) NULL,
	[Street_Address] [varchar](100) NULL,
	[City] [varchar](50) NULL,
	[State_Code] [varchar](10) NULL,
	[Country_Code] [varchar](10) NULL,
	[Zip_Code] [varchar](10) NULL,
	[Day_Phone] [varchar](20) NULL,
	[Day_Time_Call] [varchar](20) NULL,
	[Evening_Phone] [varchar](20) NULL,
	[Evening_Time_Call] [varchar](20) NULL,
	[Fax] [varchar](20) NULL,
	[Email] [varchar](50) NULL,
	[Group_Type_ID] [int] NULL,
	[Member_Count] [int] NULL,
	[Participant_Count] [int] NULL,
	[Fund_Raising_Goal] [int] NULL,
	[Decision_Date] [datetime] NULL,
	[Decision_Maker] [bit] NOT NULL,
	[Committee_Meeting_Required] [bit] NOT NULL,
	[Committee_Meeting_Date] [datetime] NULL,
	[Fund_Raiser_Start_Date] [datetime] NULL,
	[OnEmailList] [bit] NOT NULL,
	[FaxKit] [bit] NOT NULL,
	[EmailKit] [bit] NOT NULL,
	[Comments] [text] NULL,
	[Hear_Id] [int] NULL,
	[kit_to_send] [bit] NOT NULL,
	[kit_sent] [bit] NOT NULL,
	[kit_sent_date] [datetime] NULL,
	[Old_Lead_Id] [int] NULL,
	[Lead_Assignment_Date] [datetime] NULL,
	[Interests] [text] NULL,
	[Has_Been_Contacted] [bit] NOT NULL,
	[fk_Kit_Type_ID] [int] NULL,
	[Lead_Priority_Id] [int] NULL,
	[Day_Phone_Ext] [varchar](10) NULL,
	[Evening_Phone_Ext] [varchar](10) NULL,
	[Rejection_reason] [text] NULL,
	[Other_Phone] [varchar](20) NULL,
	[Other_Phone_Ext] [varchar](10) NULL,
	[Group_Web_Site] [varchar](50) NULL,
	[Organization_Type_Id] [int] NULL,
	[Campaign_Reason_Id] [int] NULL,
	[Title_Id] [int] NULL,
	[Cookie_Content] [varchar](255) NULL,
	[Campaign_Reason] [varchar](50) NULL,
	[Web_Site_Id] [int] NULL,
	[IsNew] [bit] NOT NULL,
 CONSTRAINT [PK_Temp_Lead] PRIMARY KEY NONCLUSTERED 
(
	[Temp_Lead_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY],
 CONSTRAINT [UQ__Temp_Lead__0E8E2250] UNIQUE NONCLUSTERED 
(
	[Temp_Lead_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY],
 CONSTRAINT [UQ__Temp_Lead__0F824689] UNIQUE NONCLUSTERED 
(
	[Temp_Lead_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
