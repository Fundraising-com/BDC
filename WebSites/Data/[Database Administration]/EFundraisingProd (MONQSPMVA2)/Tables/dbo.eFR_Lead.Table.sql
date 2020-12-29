USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[eFR_Lead]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[eFR_Lead](
	[Lead_ID] [int] NOT NULL,
	[First_Name] [varchar](50) NULL,
	[Last_Name] [varchar](50) NULL,
	[Organization_Name] [varchar](100) NULL,
	[Promotion_Description] [varchar](60) NULL,
	[Lead_Activity_Detail] [varchar](255) NULL,
	[Lead_Comment] [varchar](255) NULL,
	[Activity_Scheduled_Date] [datetime] NULL,
	[Consultant_ID] [int] NULL,
	[Consultant_Ext] [int] NULL,
	[Is_Done] [int] NULL,
	[Phone_Number] [varchar](20) NOT NULL,
	[phone_extension] [varchar](15) NULL,
	[Promotion_Type] [varchar](50) NULL,
	[2ndPhone_Number] [varchar](20) NULL,
	[2ndPhone_Extension] [varchar](15) NULL,
 CONSTRAINT [PK_eFR_Lead] PRIMARY KEY NONCLUSTERED 
(
	[Lead_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Lead_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Lead_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[eFR_Lead] ADD  CONSTRAINT [DF_eFR_Lead_Is_Done]  DEFAULT (0) FOR [Is_Done]
GO
