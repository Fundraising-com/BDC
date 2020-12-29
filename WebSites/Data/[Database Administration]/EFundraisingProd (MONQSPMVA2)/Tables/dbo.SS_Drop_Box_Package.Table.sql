USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[SS_Drop_Box_Package]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SS_Drop_Box_Package](
	[SS_Drop_Box_Id] [int] NOT NULL,
	[Package_Id] [int] NOT NULL,
	[Display_Order] [smallint] NULL,
 CONSTRAINT [PK_SS_Drop_Box_Package] PRIMARY KEY NONCLUSTERED 
(
	[SS_Drop_Box_Id] ASC,
	[Package_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[SS_Drop_Box_Package]  WITH NOCHECK ADD  CONSTRAINT [fk_sdbp_package_id] FOREIGN KEY([Package_Id])
REFERENCES [dbo].[Package] ([Package_Id])
GO
ALTER TABLE [dbo].[SS_Drop_Box_Package] CHECK CONSTRAINT [fk_sdbp_package_id]
GO
ALTER TABLE [dbo].[SS_Drop_Box_Package]  WITH NOCHECK ADD  CONSTRAINT [fk_sdbp_ss_drop_box_id] FOREIGN KEY([SS_Drop_Box_Id])
REFERENCES [dbo].[SS_Drop_Box] ([SS_Drop_Box_Id])
GO
ALTER TABLE [dbo].[SS_Drop_Box_Package] CHECK CONSTRAINT [fk_sdbp_ss_drop_box_id]
GO
