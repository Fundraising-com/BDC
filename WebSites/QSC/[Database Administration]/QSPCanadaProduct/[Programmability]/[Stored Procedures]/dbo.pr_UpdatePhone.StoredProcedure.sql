USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_UpdatePhone]    Script Date: 06/07/2017 09:18:05 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_UpdatePhone]

	@iPhoneID		int,
	@iType			int,
	@zPhoneNumber	varchar(50),
	@zBestTimeToCall	varchar(2000)

AS

	UPDATE	Phone
	SET		Type = @iType - 30500,
			PhoneNumber = @zPhoneNumber,
			BestTimeToCall = @zBestTimeToCall
	WHERE	ID = @iPhoneID
GO
