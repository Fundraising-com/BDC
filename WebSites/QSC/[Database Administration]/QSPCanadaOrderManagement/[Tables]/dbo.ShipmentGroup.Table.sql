USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[ShipmentGroup]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShipmentGroup](
	[ShipmentGroupID] [int] NOT NULL,
	[ShipmentGroupName] [nvarchar](50) NOT NULL,
	[Created] [datetime] NOT NULL,
 CONSTRAINT [PK_ShipmentGroup] PRIMARY KEY CLUSTERED 
(
	[ShipmentGroupID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
