USE [eFundweb]
GO
/****** Object:  Table [dbo].[Unassigned_Consultant]    Script Date: 02/14/2014 16:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Unassigned_Consultant](
	[Lead_ID] [int] NOT NULL,
	[Old_Consultant_ID] [int] NOT NULL,
	[New_Consultant_ID] [int] NOT NULL,
	[Unassigned_Date] [datetime] NULL
) ON [PRIMARY]
GO
