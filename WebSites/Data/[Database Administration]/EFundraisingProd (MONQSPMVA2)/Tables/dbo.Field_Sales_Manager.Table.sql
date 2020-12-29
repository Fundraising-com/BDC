USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Field_Sales_Manager]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Field_Sales_Manager](
	[FSM_ID] [int] IDENTITY(7320,1) NOT FOR REPLICATION NOT NULL,
	[QSP_ID] [varchar](20) NOT NULL,
	[Area_Manager_ID] [int] NULL,
	[First_Name] [varchar](50) NOT NULL,
	[Password] [varchar](15) NULL,
	[Last_Name] [varchar](50) NULL,
	[email] [varchar](50) NULL,
	[Home_Phone] [varchar](15) NULL,
	[Work_Phone] [varchar](15) NULL,
	[Fax_Number] [varchar](15) NULL,
	[Toll_Free_Phone] [varchar](15) NULL,
	[Mobile_Phone] [varchar](15) NULL,
	[Pager_Phone] [varchar](15) NULL,
	[Region] [varchar](30) NULL,
 CONSTRAINT [PK_Field_Sales_Manager] PRIMARY KEY NONCLUSTERED 
(
	[FSM_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Field_Sales_Manager]  WITH NOCHECK ADD  CONSTRAINT [FK_Field_Sales_Manager_Area_Manager] FOREIGN KEY([Area_Manager_ID])
REFERENCES [dbo].[Area_Manager] ([Area_Manager_ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Field_Sales_Manager] CHECK CONSTRAINT [FK_Field_Sales_Manager_Area_Manager]
GO
