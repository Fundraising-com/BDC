USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[InvoiceProgram]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InvoiceProgram](
	[CustomerOrderHeaderInstance] [int] NOT NULL,
	[ProgramTypeInstance] [int] NULL,
	[OriginalUnits] [int] NULL,
	[OriginalUnitsPaid] [int] NULL,
	[NewUnitsPaid] [int] NULL,
 CONSTRAINT [aaaaaInvoiceProgram_PK] PRIMARY KEY CLUSTERED 
(
	[CustomerOrderHeaderInstance] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[InvoiceProgram] ADD  CONSTRAINT [DF__InvoicePr__Progr__3B95D2F1]  DEFAULT (0) FOR [ProgramTypeInstance]
GO
ALTER TABLE [dbo].[InvoiceProgram] ADD  CONSTRAINT [DF__InvoicePr__Origi__3C89F72A]  DEFAULT (0) FOR [OriginalUnits]
GO
ALTER TABLE [dbo].[InvoiceProgram] ADD  CONSTRAINT [DF__InvoicePr__Origi__3D7E1B63]  DEFAULT (0) FOR [OriginalUnitsPaid]
GO
ALTER TABLE [dbo].[InvoiceProgram] ADD  CONSTRAINT [DF__InvoicePr__NewUn__3E723F9C]  DEFAULT (0) FOR [NewUnitsPaid]
GO
