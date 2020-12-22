
DECLARE @designId INT

-- sp_AddDesign inserts the record into the Designs table or updates if a record with
-- the same name exists. This procedure returns the design id used by this theme.
EXEC sp_AddStoreFrontDesign N'Theme 4 - Green', N'Theme4', N'Green', N'Theme 4', @designId OUT

-- sp_AddImage inserts the record into the Images table or updates if a record with
-- the same name and design id exists.
EXEC sp_AddImage @designId, N'Add', N'buttons/add.gif'
EXEC sp_AddImage @designId, N'AddAddress', N'buttons/add_addresses.gif'
EXEC sp_AddImage @designId, N'AddToOrder', N'buttons/add_to_cart.gif'
EXEC sp_AddImage @designId, N'AddToSavedCart', N'buttons/addtosavedcart.gif'
EXEC sp_AddImage @designId, N'Apply', N'buttons/apply.gif'
EXEC sp_AddImage @designId, N'BuyNow', N'buttons/buy_now.gif'
EXEC sp_AddImage @designId, N'Cancel', N'buttons/cancel.gif'
EXEC sp_AddImage @designId, N'CheckOut', N'buttons/checkout.gif'
EXEC sp_AddImage @designId, N'Clear', N'buttons/clear.gif'
EXEC sp_AddImage @designId, N'Close', N'buttons/close.gif'
EXEC sp_AddImage @designId, N'CompleteOrder', N'buttons/complete_order.gif'
EXEC sp_AddImage @designId, N'Continue', N'buttons/continue.gif'
EXEC sp_AddImage @designId, N'CreateAccount', N'buttons/continue.gif'
EXEC sp_AddImage @designId, N'Delete', N'buttons/delete.gif'
EXEC sp_AddImage @designId, N'Edit', N'buttons/edit.gif'
EXEC sp_AddImage @designId, N'EmailFriend', N'buttons/email_friend.gif'
EXEC sp_AddImage @designId, N'FilterResults', N'buttons/filter_results.gif'
EXEC sp_AddImage @designId, N'GiftWrap', N'buttons/gift_wrap.gif'
EXEC sp_AddImage @designId, N'LagardeLogo', N'ft-logo_lagarde.gif'
EXEC sp_AddImage @designId, N'ManageAddresses', N'buttons/manage_addresses.gif'
EXEC sp_AddImage @designId, N'Remove', N'buttons/remove.gif'
EXEC sp_AddImage @designId, N'ReOrder', N'buttons/reorder.gif'
EXEC sp_AddImage @designId, N'Save', N'buttons/save.gif'
EXEC sp_AddImage @designId, N'SaveCart', N'buttons/save_cart.gif'
EXEC sp_AddImage @designId, N'Search', N'buttons/icon_go.gif'
EXEC sp_AddImage @designId, N'Send', N'buttons/send.gif'
EXEC sp_AddImage @designId, N'SignIn', N'buttons/sign_in.gif'
EXEC sp_AddImage @designId, N'SignOut', N'buttons/sign_out.gif'
EXEC sp_AddImage @designId, N'Track', N'buttons/track.gif'
EXEC sp_AddImage @designId, N'UpdateQuantity', N'buttons/update_quantity.gif'
EXEC sp_AddImage @designId, N'View', N'buttons/view.gif'
EXEC sp_AddImage @designId, N'ViewAndPrintReceipt', N'buttons/viewandprintreceipt.gif'

-- sp_AddLayout inserts the record into the Layout table or updates if a record with
-- the same name and design id exists.
EXEC sp_AddLayout @designId, N'BodyTable', NULL, NULL, NULL, NULL, NULL, N'Center', NULL, 0, 0, 0, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL
EXEC sp_AddLayout @designId, N'Content', NULL, NULL, NULL, NULL, NULL, N'Left', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Arial', 11, NULL, N'Normal', NULL, NULL, NULL
EXEC sp_AddLayout @designId, N'ContentTableHeader', NULL, NULL, NULL, NULL, NULL, N'Left', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Arial', 11, NULL, N'Normal', NULL, NULL, NULL
EXEC sp_AddLayout @designId, N'ErrorMessages', NULL, NULL, NULL, NULL, NULL, N'Left', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Arial', 11, NULL, N'Normal', NULL, NULL, NULL
EXEC sp_AddLayout @designId, N'Footer', NULL, NULL, NULL, NULL, NULL, N'Left', N'Top', NULL, NULL, NULL, NULL, NULL, NULL, N'Arial', 11, NULL, N'Normal', NULL, NULL, 1
EXEC sp_AddLayout @designId, N'Headings', NULL, NULL, NULL, NULL, NULL, N'Left', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Arial', 11, NULL, N'Normal', NULL, NULL, NULL
EXEC sp_AddLayout @designId, N'Instruction', NULL, NULL, NULL, NULL, NULL, N'Left', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Arial', 11, NULL, N'Normal', NULL, NULL, NULL
EXEC sp_AddLayout @designId, N'LeftColumn', NULL, NULL, NULL, NULL, NULL, N'Left', N'Top', NULL, NULL, NULL, NULL, NULL, NULL, N'Arial', 11, NULL, N'Normal', NULL, NULL, 1
EXEC sp_AddLayout @designId, N'Messages', NULL, NULL, NULL, NULL, NULL, N'Left', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Arial', 11, NULL, N'Normal', NULL, NULL, NULL
EXEC sp_AddLayout @designId, N'RightColumn', NULL, NULL, NULL, NULL, NULL, N'Left', N'Top', NULL, NULL, NULL, NULL, NULL, NULL, N'Arial', 11, NULL, N'Normal', NULL, NULL, 0
EXEC sp_AddLayout @designId, N'TopBanner', NULL, NULL, NULL, NULL, NULL, N'Left', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Arial', 11, NULL, N'Normal', N'bn-logo.gif', 1, 1
EXEC sp_AddLayout @designId, N'TopSubBanner', NULL, NULL, NULL, NULL, NULL, N'Left', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Arial', 11, NULL, N'Normal', NULL, 0, 1

-- sp_AddMenuBar inserts the record into the MenuBar table or updates if a record with
-- the same page name, menu text and design id exists.
EXEC sp_AddMenuBar @designId, N'Default.aspx', N'Home', NULL, NULL, N'default.aspx', 0, 0, 12
EXEC sp_AddMenuBar @designId, N'Default.aspx', N'Categories', NULL, NULL, N'SearchResult.aspx?CategoryID=', 1, 0, 12
