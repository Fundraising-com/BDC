USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Local_Sponsor]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Local_Sponsor](
	[Brand_ID] [int] NOT NULL,
	[Local_Sponsor_ID] [int] NOT NULL,
	[Salutation] [varchar](10) NULL,
	[First_Name] [varchar](50) NULL,
	[Middle_Initial] [varchar](50) NULL,
	[Last_Name] [varchar](50) NULL,
	[Title] [varchar](50) NULL,
	[Street_Address] [varchar](100) NULL,
	[City_Name] [varchar](50) NULL,
	[State_Code] [varchar](10) NOT NULL,
	[Zip_Code] [varchar](10) NULL,
	[Country_Code] [varchar](10) NOT NULL,
	[Day_Phone] [varchar](20) NULL,
	[Day_Time_Call] [varchar](20) NULL,
	[Evening_Phone] [varchar](20) NULL,
	[Evening_Time_Call] [varchar](20) NULL,
	[Alternate_Phone] [varchar](50) NULL,
	[Fax_Number] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[Approval_Date] [datetime] NULL,
	[Comment] [text] NULL,
	[Sponsor_Consultant_ID] [int] NOT NULL,
	[Last_Contact] [datetime] NULL,
	[Local_Sponsor_Steps_Id] [int] NULL,
 CONSTRAINT [PK_Local_Sponsor] PRIMARY KEY NONCLUSTERED 
(
	[Brand_ID] ASC,
	[Local_Sponsor_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Local_Sponsor]  WITH CHECK ADD  CONSTRAINT [fk_ls_Brand_ID] FOREIGN KEY([Brand_ID])
REFERENCES [dbo].[Brand] ([Brand_ID])
GO
ALTER TABLE [dbo].[Local_Sponsor] CHECK CONSTRAINT [fk_ls_Brand_ID]
GO
ALTER TABLE [dbo].[Local_Sponsor]  WITH CHECK ADD  CONSTRAINT [fk_ls_Local_Sponsor_Steps_Id] FOREIGN KEY([Local_Sponsor_Steps_Id])
REFERENCES [dbo].[Local_Sponsor_Steps] ([Step_Id])
GO
ALTER TABLE [dbo].[Local_Sponsor] CHECK CONSTRAINT [fk_ls_Local_Sponsor_Steps_Id]
GO
ALTER TABLE [dbo].[Local_Sponsor]  WITH CHECK ADD  CONSTRAINT [fk_ls_Sponsor_Consultant_ID] FOREIGN KEY([Sponsor_Consultant_ID])
REFERENCES [dbo].[Sponsor_Consultant] ([Sponsor_Consultant_ID])
GO
ALTER TABLE [dbo].[Local_Sponsor] CHECK CONSTRAINT [fk_ls_Sponsor_Consultant_ID]
GO
ALTER TABLE [dbo].[Local_Sponsor]  WITH NOCHECK ADD  CONSTRAINT [fk_LS_State_Code] FOREIGN KEY([State_Code])
REFERENCES [dbo].[State] ([State_Code])
GO
ALTER TABLE [dbo].[Local_Sponsor] CHECK CONSTRAINT [fk_LS_State_Code]
GO
ALTER TABLE [dbo].[Local_Sponsor] ADD  CONSTRAINT [DF_Local_Sponsor_Sponsor_Consultant_ID]  DEFAULT (1) FOR [Sponsor_Consultant_ID]
GO
ALTER TABLE [dbo].[Local_Sponsor] ADD  CONSTRAINT [DF_Local_Sponsor_Local_Sponsor_Steps_Id]  DEFAULT (0) FOR [Local_Sponsor_Steps_Id]
GO
