USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_scratch_book_current_price_by_id]    Script Date: 02/14/2014 13:06:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create  procedure [dbo].[efrcrm_get_scratch_book_current_price_by_id] --516577
           @scratch_book_id as int
   
           
as
SELECT Unit_Price FROM Scratch_Book_Price_Info where
       Scratch_Book_ID = @scratch_book_id and Effective_Date <= getdate()
ORDER BY Effective_Date DESC
GO
