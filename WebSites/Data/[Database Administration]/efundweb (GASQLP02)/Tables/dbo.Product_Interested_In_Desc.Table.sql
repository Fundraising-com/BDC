USE [eFundweb]
GO
/****** Object:  Table [dbo].[Product_Interested_In_Desc]    Script Date: 02/14/2014 16:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Product_Interested_In_Desc](
	[Product_Interested_In_ID] [int] NOT NULL,
	[Language_ID] [int] NOT NULL,
	[Product_Interested_In_Description] [varchar](200) NOT NULL,
 CONSTRAINT [PK_Product_Interested_In_Desc] PRIMARY KEY CLUSTERED 
(
	[Product_Interested_In_ID] ASC,
	[Language_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Product_Interested_In_Desc]  WITH CHECK ADD  CONSTRAINT [FK_Product_Interested_In_Desc_Product_Interested_In] FOREIGN KEY([Product_Interested_In_ID])
REFERENCES [dbo].[Product_Interested_In] ([Product_Interested_In_ID])
GO
ALTER TABLE [dbo].[Product_Interested_In_Desc] CHECK CONSTRAINT [FK_Product_Interested_In_Desc_Product_Interested_In]
GO
