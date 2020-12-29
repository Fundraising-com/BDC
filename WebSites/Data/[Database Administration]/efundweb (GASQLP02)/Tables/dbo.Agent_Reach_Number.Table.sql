USE [eFundweb]
GO
/****** Object:  Table [dbo].[Agent_Reach_Number]    Script Date: 02/14/2014 16:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Agent_Reach_Number](
	[Reach_Number_ID] [int] IDENTITY(1,1) NOT NULL,
	[Reach_Type_ID] [int] NULL,
	[Agent_ID] [int] NULL,
	[Reach_Number] [varchar](35) NULL,
 CONSTRAINT [PK_reach_number_id] PRIMARY KEY CLUSTERED 
(
	[Reach_Number_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
