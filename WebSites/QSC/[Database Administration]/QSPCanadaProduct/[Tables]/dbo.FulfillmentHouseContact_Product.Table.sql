USE [QSPCanadaProduct]
GO
/****** Object:  Table [dbo].[FulfillmentHouseContact_Product]    Script Date: 06/07/2017 09:17:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FulfillmentHouseContact_Product](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FulfillmentHouseContactID] [int] NOT NULL,
	[Product_Code] [varchar](20) NOT NULL,
	[DateCreated] [datetime] NULL,
	[UserIDCreated] [int] NULL,
	[DateChanged] [datetime] NULL,
	[UserIDChanged] [int] NULL,
 CONSTRAINT [PK_FulfillmentHouseContact_Product] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[FulfillmentHouseContact_Product]  WITH CHECK ADD  CONSTRAINT [FK_FulfillmentHouseContact_Product_FULFILLMENT_HOUSE_CONTACTS] FOREIGN KEY([FulfillmentHouseContactID])
REFERENCES [dbo].[FULFILLMENT_HOUSE_CONTACTS] ([Instance])
GO
ALTER TABLE [dbo].[FulfillmentHouseContact_Product] CHECK CONSTRAINT [FK_FulfillmentHouseContact_Product_FULFILLMENT_HOUSE_CONTACTS]
GO
