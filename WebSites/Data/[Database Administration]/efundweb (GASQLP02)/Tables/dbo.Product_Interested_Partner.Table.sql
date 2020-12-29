USE [eFundweb]
GO
/****** Object:  Table [dbo].[Product_Interested_Partner]    Script Date: 02/14/2014 16:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product_Interested_Partner](
	[Partner_ID] [int] NOT NULL,
	[Product_Interested_In_ID] [int] NOT NULL,
 CONSTRAINT [PK_Product_Interested_Partner] PRIMARY KEY CLUSTERED 
(
	[Partner_ID] ASC,
	[Product_Interested_In_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Product_Interested_Partner]  WITH CHECK ADD  CONSTRAINT [FK_Product_Interested_Partner_Product_Interested_In] FOREIGN KEY([Product_Interested_In_ID])
REFERENCES [dbo].[Product_Interested_In] ([Product_Interested_In_ID])
GO
ALTER TABLE [dbo].[Product_Interested_Partner] CHECK CONSTRAINT [FK_Product_Interested_Partner_Product_Interested_In]
GO
