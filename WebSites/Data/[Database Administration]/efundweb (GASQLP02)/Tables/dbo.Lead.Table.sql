USE [eFundweb]
GO
/****** Object:  Table [dbo].[Lead]    Script Date: 02/14/2014 16:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Lead](
	[Division_ID] [int] NOT NULL,
	[Promotion_ID] [int] NOT NULL,
	[Lead_ID] [int] NOT NULL,
	[Channel_Code] [varchar](4) NOT NULL,
	[Lead_Status_ID] [int] NOT NULL,
	[Consultant_ID] [int] NULL,
	[Lead_Entry_Date] [datetime] NOT NULL,
	[Salutation] [varchar](10) NULL,
	[First_Name] [varchar](50) NULL,
	[Last_Name] [varchar](50) NULL,
	[Organization] [varchar](100) NULL,
	[Street_Address] [varchar](100) NULL,
	[City] [varchar](50) NULL,
	[State_Code] [varchar](10) NOT NULL,
	[Country_Code] [varchar](10) NOT NULL,
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
	[Comments] [varchar](2000) NULL,
	[Hear_ID] [int] NOT NULL,
	[kit_to_send] [bit] NOT NULL,
	[kit_sent] [bit] NOT NULL,
	[kit_sent_date] [datetime] NULL,
	[Old_Lead_ID] [int] NULL,
	[Lead_Assignment_Date] [datetime] NULL,
	[Interests] [varchar](2000) NULL,
	[Has_Been_Contacted] [bit] NULL,
	[fk_Kit_Type_ID] [int] NULL,
	[Lead_Priority_ID] [int] NULL,
	[Day_Phone_Ext] [varchar](10) NULL,
	[Evening_Phone_Ext] [varchar](10) NULL,
	[Other_Phone] [varchar](20) NULL,
	[Organization_Type_ID] [int] NOT NULL,
	[Group_Web_Site] [varchar](50) NULL,
	[Referee_ID] [int] NULL,
	[Title_ID] [int] NOT NULL,
	[Campaign_Reason_ID] [int] NOT NULL,
	[Web_Site_ID] [int] NOT NULL,
	[Promotion_Code_ID] [int] NULL,
	[Nb_Queries] [int] NULL,
	[Submit_Date] [datetime] NULL,
	[Cookie_Content] [varchar](255) NULL,
	[First_Contact_Date] [datetime] NULL,
	[Lead_Qualification_Type_ID] [int] NULL,
	[Temp_Lead_ID] [int] NULL,
	[Day_Phone_Is_Good] [bit] NOT NULL,
	[Evening_Phone_Is_Good] [bit] NOT NULL,
	[Assigner_ID] [int] NULL,
 CONSTRAINT [PK_Lead] PRIMARY KEY CLUSTERED 
(
	[Lead_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Lead] ADD  CONSTRAINT [DF__Lead__Division_I__2CF2ADDF]  DEFAULT (1) FOR [Division_ID]
GO
ALTER TABLE [dbo].[Lead] ADD  CONSTRAINT [DF__Lead__Lead_Statu__2DE6D218]  DEFAULT (1) FOR [Lead_Status_ID]
GO
ALTER TABLE [dbo].[Lead] ADD  CONSTRAINT [DF__Lead__Lead_Entry__2EDAF651]  DEFAULT (getdate()) FOR [Lead_Entry_Date]
GO
ALTER TABLE [dbo].[Lead] ADD  CONSTRAINT [DF__Lead__Group_Type__2FCF1A8A]  DEFAULT (99) FOR [Group_Type_ID]
GO
ALTER TABLE [dbo].[Lead] ADD  CONSTRAINT [DF__Lead__Member_Cou__30C33EC3]  DEFAULT (0) FOR [Member_Count]
GO
ALTER TABLE [dbo].[Lead] ADD  CONSTRAINT [DF__Lead__Participan__31B762FC]  DEFAULT (0) FOR [Participant_Count]
GO
ALTER TABLE [dbo].[Lead] ADD  CONSTRAINT [DF__Lead__Fund_Raisi__32AB8735]  DEFAULT (0) FOR [Fund_Raising_Goal]
GO
ALTER TABLE [dbo].[Lead] ADD  CONSTRAINT [DF__Lead__Decision_M__339FAB6E]  DEFAULT (0) FOR [Decision_Maker]
GO
ALTER TABLE [dbo].[Lead] ADD  CONSTRAINT [DF__Lead__Committee___3493CFA7]  DEFAULT (0) FOR [Committee_Meeting_Required]
GO
ALTER TABLE [dbo].[Lead] ADD  CONSTRAINT [DF__Lead__OnEmailLis__3587F3E0]  DEFAULT (1) FOR [OnEmailList]
GO
ALTER TABLE [dbo].[Lead] ADD  CONSTRAINT [DF__Lead__FaxKit__367C1819]  DEFAULT (0) FOR [FaxKit]
GO
ALTER TABLE [dbo].[Lead] ADD  CONSTRAINT [DF__Lead__EmailKit__37703C52]  DEFAULT (0) FOR [EmailKit]
GO
ALTER TABLE [dbo].[Lead] ADD  CONSTRAINT [DF__Lead__Hear_ID__3864608B]  DEFAULT (6) FOR [Hear_ID]
GO
ALTER TABLE [dbo].[Lead] ADD  CONSTRAINT [DF__Lead__kit_to_sen__395884C4]  DEFAULT (0) FOR [kit_to_send]
GO
ALTER TABLE [dbo].[Lead] ADD  CONSTRAINT [DF__Lead__kit_sent__3A4CA8FD]  DEFAULT (0) FOR [kit_sent]
GO
ALTER TABLE [dbo].[Lead] ADD  CONSTRAINT [DF__Lead__Has_Been_C__3B40CD36]  DEFAULT (0) FOR [Has_Been_Contacted]
GO
ALTER TABLE [dbo].[Lead] ADD  CONSTRAINT [DF__Lead__Organizati__3C34F16F]  DEFAULT (99) FOR [Organization_Type_ID]
GO
ALTER TABLE [dbo].[Lead] ADD  CONSTRAINT [DF__Lead__Title_ID__3D2915A8]  DEFAULT (99) FOR [Title_ID]
GO
ALTER TABLE [dbo].[Lead] ADD  CONSTRAINT [DF__Lead__Campaign_R__3E1D39E1]  DEFAULT (99) FOR [Campaign_Reason_ID]
GO
ALTER TABLE [dbo].[Lead] ADD  CONSTRAINT [DF__Lead__Web_Site_I__3F115E1A]  DEFAULT (2) FOR [Web_Site_ID]
GO
ALTER TABLE [dbo].[Lead] ADD  CONSTRAINT [DF__Lead__Nb_Queries__40058253]  DEFAULT (0) FOR [Nb_Queries]
GO
ALTER TABLE [dbo].[Lead] ADD  CONSTRAINT [DF__Lead__Day_Phone___40F9A68C]  DEFAULT (1) FOR [Day_Phone_Is_Good]
GO
ALTER TABLE [dbo].[Lead] ADD  CONSTRAINT [DF__Lead__Evening_Ph__41EDCAC5]  DEFAULT (1) FOR [Evening_Phone_Is_Good]
GO
