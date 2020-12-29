USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Alias_State]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Alias_State](
	[Input_State_Code] [varchar](255) NOT NULL,
	[State_Code] [varchar](10) NOT NULL,
 CONSTRAINT [PK_Alias_State] PRIMARY KEY NONCLUSTERED 
(
	[Input_State_Code] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Alias_State]  WITH NOCHECK ADD  CONSTRAINT [fk_as_state_code] FOREIGN KEY([State_Code])
REFERENCES [dbo].[State] ([State_Code])
GO
ALTER TABLE [dbo].[Alias_State] CHECK CONSTRAINT [fk_as_state_code]
GO
