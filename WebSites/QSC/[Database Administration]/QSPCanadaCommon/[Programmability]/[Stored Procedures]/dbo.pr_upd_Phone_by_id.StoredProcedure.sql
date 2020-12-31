USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_upd_Phone_by_id]    Script Date: 06/07/2017 09:33:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_upd_Phone_by_id]
	@ID int,
	@Type int,
	@PhoneListID int,
	@PhoneNumber varchar(50),
	@BestTimeToCall varchar(2000)
AS
UPDATE Phone SET 
	Type = @Type,
	PhoneListID = @PhoneListID,
	PhoneNumber = @PhoneNumber,
	BestTimeToCall = @BestTimeToCall


WHERE ID = @ID
GO
