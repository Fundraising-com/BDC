USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[State]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[State](
	[State_Code] [varchar](10) NOT NULL,
	[State_Name] [varchar](50) NOT NULL,
	[Avg_Delivery_Days] [smallint] NOT NULL,
	[Time_Zone_Difference] [int] NULL,
	[Country_Code] [varchar](10) NULL,
	[SAP_State_Code] [varchar](10) NULL,
 CONSTRAINT [PK_State] PRIMARY KEY CLUSTERED 
(
	[State_Code] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY],
 CONSTRAINT [UQ_State] UNIQUE NONCLUSTERED 
(
	[State_Name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[State]  WITH CHECK ADD  CONSTRAINT [fk_State_Country_Code] FOREIGN KEY([Country_Code])
REFERENCES [dbo].[Country] ([Country_Code])
GO
ALTER TABLE [dbo].[State] CHECK CONSTRAINT [fk_State_Country_Code]
GO
ALTER TABLE [dbo].[State] ADD  CONSTRAINT [DF_State_Avg_Delivery_Days]  DEFAULT (0) FOR [Avg_Delivery_Days]
GO
ALTER TABLE [dbo].[State] ADD  CONSTRAINT [DF_State_Time_Zone_Difference]  DEFAULT (0) FOR [Time_Zone_Difference]
GO
