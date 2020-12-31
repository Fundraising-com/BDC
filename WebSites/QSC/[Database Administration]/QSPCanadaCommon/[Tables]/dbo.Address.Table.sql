USE [QSPCanadaCommon]
GO
/****** Object:  Table [dbo].[Address]    Script Date: 06/07/2017 09:29:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Address](
	[address_id] [int] IDENTITY(1300000,1) NOT NULL,
	[street1] [varchar](50) NULL,
	[street2] [varchar](50) NULL,
	[city] [varchar](50) NULL,
	[stateProvince] [varchar](10) NULL,
	[postal_code] [varchar](7) NULL,
	[zip4] [varchar](4) NULL,
	[country] [varchar](10) NULL,
	[address_type] [int] NOT NULL,
	[AddressListID] [int] NULL,
 CONSTRAINT [PK__Address__1BC821DD] PRIMARY KEY CLUSTERED 
(
	[address_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Address]  WITH NOCHECK ADD  CONSTRAINT [FK__Address__address__1CBC4616] FOREIGN KEY([address_type])
REFERENCES [dbo].[AddressType] ([addressType_id])
GO
ALTER TABLE [dbo].[Address] CHECK CONSTRAINT [FK__Address__address__1CBC4616]
GO
ALTER TABLE [dbo].[Address]  WITH NOCHECK ADD  CONSTRAINT [FK_Address_AddressList] FOREIGN KEY([AddressListID])
REFERENCES [dbo].[AddressList] ([ID])
GO
ALTER TABLE [dbo].[Address] CHECK CONSTRAINT [FK_Address_AddressList]
GO
