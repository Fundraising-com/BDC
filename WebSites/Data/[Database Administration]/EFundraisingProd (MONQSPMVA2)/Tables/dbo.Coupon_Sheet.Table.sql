USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Coupon_Sheet]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Coupon_Sheet](
	[Coupon_Sheet_ID] [int] NOT NULL,
	[Product_Code] [varchar](10) NOT NULL,
	[Description] [varchar](50) NOT NULL,
	[Sheet_Per_Booklet] [smallint] NULL,
	[Expiration_Date] [datetime] NULL,
	[Commission_Payable] [bit] NULL,
	[Is_Active] [bit] NOT NULL,
 CONSTRAINT [PK_Coupon_Sheet] PRIMARY KEY NONCLUSTERED 
(
	[Coupon_Sheet_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Product_Code] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Product_Code] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Coupon_Sheet] ADD  CONSTRAINT [DF_Coupon_Sheet_Sheet_Per_Booklet]  DEFAULT (0) FOR [Sheet_Per_Booklet]
GO
ALTER TABLE [dbo].[Coupon_Sheet] ADD  CONSTRAINT [DF_Coupon_Sheet_Commission_Payable]  DEFAULT (0) FOR [Commission_Payable]
GO
