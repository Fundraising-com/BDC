USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Commission_Earning]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Commission_Earning](
	[Commission_Earning_ID] [int] IDENTITY(34305,1) NOT FOR REPLICATION NOT NULL,
	[Sales_ID] [int] NULL,
	[Product_Description] [varchar](255) NULL,
	[Payment_Amount] [decimal](15, 4) NULL,
	[Payment_Entry_Date] [datetime] NULL,
	[Commission_Amount] [varchar](125) NULL,
	[Commission_Rate] [decimal](15, 4) NULL,
	[Payment_No] [int] NULL,
	[Consultant_ID] [int] NULL,
	[Record_Entry_Date] [datetime] NOT NULL,
	[Associate_ID] [int] NULL,
	[Sales_Amount] [decimal](15, 4) NULL,
	[Currency_Code] [varchar](10) NULL,
	[Exchange_Rate] [decimal](15, 4) NULL,
	[Commission_Amount_Ca] [varchar](125) NULL,
	[Lead_ID] [int] NULL,
	[Sale_Date] [datetime] NULL,
 CONSTRAINT [PK_Commission_Earning] PRIMARY KEY NONCLUSTERED 
(
	[Commission_Earning_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Commission_Earning] ADD  CONSTRAINT [DF_Commission_Earning_Record_Entry_Date]  DEFAULT (getdate()) FOR [Record_Entry_Date]
GO
