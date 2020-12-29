USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Applicable_Tax]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Applicable_Tax](
	[Sales_ID] [int] NOT NULL,
	[Tax_Code] [varchar](4) NOT NULL,
	[Tax_Amount] [decimal](15, 4) NOT NULL,
 CONSTRAINT [PK_Applicable_Tax] PRIMARY KEY NONCLUSTERED 
(
	[Sales_ID] ASC,
	[Tax_Code] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Applicable_Tax]  WITH CHECK ADD  CONSTRAINT [fk_Tax_Code] FOREIGN KEY([Tax_Code])
REFERENCES [dbo].[Tax_Table] ([Tax_Code])
GO
ALTER TABLE [dbo].[Applicable_Tax] CHECK CONSTRAINT [fk_Tax_Code]
GO
