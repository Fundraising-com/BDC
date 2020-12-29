USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Unassigned_Consultant]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Unassigned_Consultant](
	[Lead_ID] [int] NOT NULL,
	[Old_Consultant_ID] [int] NULL,
	[New_Consultant_ID] [int] NULL,
	[Unassigned_Date] [datetime] NULL,
	[Unassignation_ID] [int] IDENTITY(51844,1) NOT FOR REPLICATION NOT NULL,
 CONSTRAINT [PK_Unassigned_Consultant] PRIMARY KEY NONCLUSTERED 
(
	[Unassignation_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
