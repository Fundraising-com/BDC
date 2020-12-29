USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[State_Tax]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[State_Tax](
	[State_Code] [varchar](10) NOT NULL,
	[Tax_Code] [varchar](4) NOT NULL,
	[Effective_Date] [datetime] NOT NULL,
	[Tax_Rate] [decimal](15, 4) NULL,
	[Tax_order] [int] NOT NULL,
 CONSTRAINT [PK_State_Tax] PRIMARY KEY NONCLUSTERED 
(
	[State_Code] ASC,
	[Tax_Code] ASC,
	[Effective_Date] ASC,
	[Tax_order] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[State_Tax]  WITH NOCHECK ADD  CONSTRAINT [fk_ST_State_Code] FOREIGN KEY([State_Code])
REFERENCES [dbo].[State] ([State_Code])
GO
ALTER TABLE [dbo].[State_Tax] CHECK CONSTRAINT [fk_ST_State_Code]
GO
ALTER TABLE [dbo].[State_Tax]  WITH CHECK ADD  CONSTRAINT [fk_ST_Tax_Code] FOREIGN KEY([Tax_Code])
REFERENCES [dbo].[Tax_Table] ([Tax_Code])
GO
ALTER TABLE [dbo].[State_Tax] CHECK CONSTRAINT [fk_ST_Tax_Code]
GO
ALTER TABLE [dbo].[State_Tax] ADD  CONSTRAINT [DF_State_Tax_Tax_Rate]  DEFAULT (0) FOR [Tax_Rate]
GO
