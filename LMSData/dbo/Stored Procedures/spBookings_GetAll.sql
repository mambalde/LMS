CREATE PROCEDURE [dbo].[spBookings_GetAll]
	As
begin
set nocount on;
	 select [Bookings].Id, [BookId], [UserId], [BookedDate], Books.Title, Staff.StaffName
	 from ((dbo.Bookings
	 inner join dbo.Books on Bookings.BookId = Books.Id)
	 inner join dbo.Staff on Bookings.UserId = Staff.StaffId)
end

