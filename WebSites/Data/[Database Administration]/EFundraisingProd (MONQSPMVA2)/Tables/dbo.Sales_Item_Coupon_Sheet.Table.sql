USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Sales_Item_Coupon_Sheet]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sales_Item_Coupon_Sheet](
	[Coupon_Sheet_ID] [int] NOT NULL,
	[Sales_ID] [int] NOT NULL,
	[Sales_Item_No] [smallint] NOT NULL,
	[Date_Assigned] [datetime] NOT NULL,
	[Sheet_Per_Booklet] [smallint] NOT NULL,
	[Sponsor_Consultant_ID] [int] NULL,
	[Brand_ID] [int] NULL,
	[Local_Sponsor_ID] [int] NULL,
 CONSTRAINT [PK_Sales_Item_Coupon_Sheet] PRIMARY KEY CLUSTERED 
(
	[Coupon_Sheet_ID] ASC,
	[Sales_ID] ASC,
	[Sales_Item_No] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Sales_Item_Coupon_Sheet]  WITH CHECK ADD  CONSTRAINT [fk_SICS_Coupon_Sheet_ID] FOREIGN KEY([Coupon_Sheet_ID])
REFERENCES [dbo].[Coupon_Sheet] ([Coupon_Sheet_ID])
GO
ALTER TABLE [dbo].[Sales_Item_Coupon_Sheet] CHECK CONSTRAINT [fk_SICS_Coupon_Sheet_ID]
GO
ALTER TABLE [dbo].[Sales_Item_Coupon_Sheet] ADD  CONSTRAINT [DF_Sales_Item_Coupon_Sheet_Sheet_Per_Booklet]  DEFAULT (0) FOR [Sheet_Per_Booklet]
GO
