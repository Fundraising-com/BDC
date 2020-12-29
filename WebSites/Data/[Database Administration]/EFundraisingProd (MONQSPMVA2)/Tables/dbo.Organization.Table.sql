USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Organization]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Organization](
	[Organization_ID] [int] IDENTITY(27,1) NOT FOR REPLICATION NOT NULL,
	[FSM_ID] [int] NOT NULL,
	[Flag_Pole_ID] [int] NULL,
	[Organization_Name] [varchar](50) NOT NULL,
	[Organization_Status_ID] [int] NULL,
	[Address] [varchar](50) NULL,
	[City] [varchar](30) NULL,
	[Organization_Type_ID] [int] NULL,
	[Zip] [varchar](10) NULL,
	[Number_of_Members] [int] NULL,
	[Number_of_Class_Rooms] [int] NULL,
	[State_Code] [varchar](10) NOT NULL,
	[Country_Code] [varchar](10) NOT NULL,
	[Agent_ID] [int] NULL,
 CONSTRAINT [PK_Organization] PRIMARY KEY CLUSTERED 
(
	[Organization_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Organization]  WITH NOCHECK ADD  CONSTRAINT [fk_o_country_code] FOREIGN KEY([Country_Code])
REFERENCES [dbo].[Country] ([Country_Code])
GO
ALTER TABLE [dbo].[Organization] CHECK CONSTRAINT [fk_o_country_code]
GO
ALTER TABLE [dbo].[Organization]  WITH NOCHECK ADD  CONSTRAINT [fk_o_fsm_id] FOREIGN KEY([Agent_ID])
REFERENCES [dbo].[Field_Sales_Manager] ([FSM_ID])
GO
ALTER TABLE [dbo].[Organization] CHECK CONSTRAINT [fk_o_fsm_id]
GO
ALTER TABLE [dbo].[Organization]  WITH NOCHECK ADD  CONSTRAINT [fk_o_organization_status_id] FOREIGN KEY([Organization_Status_ID])
REFERENCES [dbo].[Organization_Status] ([Organization_Status_ID])
GO
ALTER TABLE [dbo].[Organization] CHECK CONSTRAINT [fk_o_organization_status_id]
GO
ALTER TABLE [dbo].[Organization]  WITH NOCHECK ADD  CONSTRAINT [fk_o_state_code] FOREIGN KEY([State_Code])
REFERENCES [dbo].[State] ([State_Code])
GO
ALTER TABLE [dbo].[Organization] CHECK CONSTRAINT [fk_o_state_code]
GO
ALTER TABLE [dbo].[Organization]  WITH NOCHECK ADD  CONSTRAINT [FK_Organization_Flag_Pole] FOREIGN KEY([Flag_Pole_ID])
REFERENCES [dbo].[Flag_Pole] ([Flag_Pole_ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Organization] CHECK CONSTRAINT [FK_Organization_Flag_Pole]
GO
