USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[InsertInternetOrderID]    Script Date: 06/07/2017 09:19:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertInternetOrderID]  
@COHInstance int,
@InternetID int

AS

Insert Into InternetOrderID values (@COHInstance, @InternetID)
GO
