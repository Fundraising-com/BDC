USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Automaton_Shipping_Status]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Automaton_Shipping_Status](
	[From_Status_ID] [int] NOT NULL,
	[To_Status_ID] [int] NOT NULL,
 CONSTRAINT [PK_Automaton_Shipping_Status] PRIMARY KEY CLUSTERED 
(
	[From_Status_ID] ASC,
	[To_Status_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
