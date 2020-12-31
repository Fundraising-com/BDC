USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[address_to_correct]    Script Date: 06/07/2017 09:18:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[address_to_correct](
	[customerbilltoinstance] [int] NULL,
	[customerorderheaderinstance] [int] NOT NULL,
	[transid] [int] NOT NULL,
	[new_coh] [int] NOT NULL,
	[new_transid] [int] NOT NULL
) ON [PRIMARY]
GO
