USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Adjustment_Audit]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Adjustment_Audit](
	[AUDIT_ID] [int] IDENTITY(1,1) NOT NULL,
	[AUDIT_OPERATION] [char](2) NOT NULL,
	[HOST] [varchar](50) NULL,
	[AUDIT_USERID] [varchar](50) NOT NULL,
	[AUDIT_DATETIME] [datetime] NOT NULL,
	[Sales_ID] [int] NULL,
	[Adjustment_No] [int] NULL,
	[Reason_ID] [int] NULL,
	[Adjustment_Date] [datetime] NULL,
	[Adjustment_Amount] [decimal](15, 4) NULL,
	[Comment] [text] NULL,
	[Adjustment_On_Shipping] [decimal](15, 4) NULL,
	[Adjustment_On_Taxes] [decimal](15, 4) NULL,
	[Adjustment_On_Sale_Amount] [decimal](15, 4) NULL,
	[Create_Date] [datetime] NULL,
	[Create_User_ID] [int] NULL,
	[Ext_Adjustment_Id] [int] NULL,
	[charge_id] [int] NULL,
 CONSTRAINT [PK_Adjustment_AUDIT] PRIMARY KEY CLUSTERED 
(
	[AUDIT_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
