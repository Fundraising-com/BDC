USE [QSPCanadaOrderManagement]
GO
/****** Object:  View [dbo].[vw_address_to_correct_all]    Script Date: 06/07/2017 09:18:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create view [dbo].[vw_address_to_correct_all] as
select * from address_to_correct_2
union
select * from address_to_correct
GO
