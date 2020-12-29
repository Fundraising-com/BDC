USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[FSM_Address]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FSM_Address](
	[FSM_Address_ID] [int] IDENTITY(678,1) NOT FOR REPLICATION NOT NULL,
	[FSM_ID] [int] NOT NULL,
	[Country_Code] [varchar](10) NULL,
	[State_Code] [varchar](10) NULL,
	[FSM_Address_Type] [varchar](5) NOT NULL,
	[City] [varchar](30) NULL,
	[Zip] [varchar](10) NULL,
	[Street_Address] [varchar](35) NULL,
 CONSTRAINT [PK_FSM_Address] PRIMARY KEY NONCLUSTERED 
(
	[FSM_Address_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[FSM_Address]  WITH NOCHECK ADD  CONSTRAINT [FK_FSM_Address_Country] FOREIGN KEY([Country_Code])
REFERENCES [dbo].[Country] ([Country_Code])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[FSM_Address] CHECK CONSTRAINT [FK_FSM_Address_Country]
GO
ALTER TABLE [dbo].[FSM_Address]  WITH NOCHECK ADD  CONSTRAINT [FK_FSM_Address_State] FOREIGN KEY([State_Code])
REFERENCES [dbo].[State] ([State_Code])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[FSM_Address] CHECK CONSTRAINT [FK_FSM_Address_State]
GO
ALTER TABLE [dbo].[FSM_Address]  WITH NOCHECK ADD  CONSTRAINT [fk_fsma_fsm_id] FOREIGN KEY([FSM_ID])
REFERENCES [dbo].[Field_Sales_Manager] ([FSM_ID])
GO
ALTER TABLE [dbo].[FSM_Address] CHECK CONSTRAINT [fk_fsma_fsm_id]
GO
