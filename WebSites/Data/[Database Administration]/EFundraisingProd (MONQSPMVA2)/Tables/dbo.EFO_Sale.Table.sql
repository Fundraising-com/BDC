USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[EFO_Sale]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EFO_Sale](
	[Sale_ID] [int] IDENTITY(14,1) NOT FOR REPLICATION NOT NULL,
	[Supporter_ID] [int] NOT NULL,
	[Sale_Date] [smalldatetime] NOT NULL,
	[Amount_To_Group] [smallmoney] NULL,
	[Amount_To_Supplier] [smallmoney] NULL,
	[Amount] [smallmoney] NULL,
	[Delivery_Address] [varchar](75) NULL,
	[State_Code] [varchar](10) NULL,
	[Country_Code] [varchar](10) NULL,
	[Delivery_City] [varchar](50) NULL,
	[Delivery_Zip_Code] [varchar](10) NULL,
	[Card_Name] [varchar](30) NULL,
	[Card_Address] [varchar](75) NULL,
	[Transaction_ID] [varchar](15) NULL,
 CONSTRAINT [PK_EFO_Sale] PRIMARY KEY NONCLUSTERED 
(
	[Sale_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[EFO_Sale]  WITH NOCHECK ADD  CONSTRAINT [fk_efos_country_code] FOREIGN KEY([Country_Code])
REFERENCES [dbo].[Country] ([Country_Code])
GO
ALTER TABLE [dbo].[EFO_Sale] CHECK CONSTRAINT [fk_efos_country_code]
GO
ALTER TABLE [dbo].[EFO_Sale]  WITH NOCHECK ADD  CONSTRAINT [fk_efos_state_code] FOREIGN KEY([State_Code])
REFERENCES [dbo].[State] ([State_Code])
GO
ALTER TABLE [dbo].[EFO_Sale] CHECK CONSTRAINT [fk_efos_state_code]
GO
ALTER TABLE [dbo].[EFO_Sale]  WITH NOCHECK ADD  CONSTRAINT [fk_efos_supporter_id] FOREIGN KEY([Supporter_ID])
REFERENCES [dbo].[EFO_Supporter] ([Supporter_ID])
GO
ALTER TABLE [dbo].[EFO_Sale] CHECK CONSTRAINT [fk_efos_supporter_id]
GO
