USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Sales_Change_Log]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Sales_Change_Log](
	[Sales_ID] [int] NOT NULL,
	[Table_Name] [varchar](50) NOT NULL,
	[Column_Name] [varchar](50) NOT NULL,
	[Change_Date_Time] [datetime] NOT NULL,
	[User_Name] [varchar](50) NOT NULL,
	[From_Value] [varchar](255) NULL,
	[To_Value] [varchar](255) NULL,
	[Comment] [varchar](255) NULL,
	[Computer_Name] [varchar](50) NOT NULL,
	[Cancelation_reason_Id] [int] NULL,
	[Other_Reason] [varchar](255) NULL,
 CONSTRAINT [PK_Sales_Change_Log] PRIMARY KEY NONCLUSTERED 
(
	[Sales_ID] ASC,
	[Table_Name] ASC,
	[Column_Name] ASC,
	[Change_Date_Time] ASC,
	[User_Name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
