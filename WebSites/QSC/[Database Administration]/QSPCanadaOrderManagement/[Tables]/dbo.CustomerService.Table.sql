USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[CustomerService]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CustomerService](
	[CustomerInstance] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
	[CustomerOrderHeaderInstance] [int] NOT NULL,
	[CustomerOrderDetailTransID] [int] NOT NULL,
	[Description] [varchar](200) NULL,
	[ServiceSequence] [int] NOT NULL,
	[ActionInstance] [int] NOT NULL,
	[ChangeUserID] [varchar](4) NULL,
	[ChangeDate] [datetime] NOT NULL,
	[BeforeAmount] [int] NOT NULL,
	[AfterAmount] [int] NOT NULL,
 CONSTRAINT [PK_CustomerService] PRIMARY KEY CLUSTERED 
(
	[CustomerInstance] ASC,
	[Date] ASC,
	[CustomerOrderHeaderInstance] ASC,
	[ServiceSequence] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
