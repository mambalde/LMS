CREATE PROCEDURE [dbo].[spBooking_Insert]
	@BookId int,
	@UserId nvarchar(128),
	@BookedDate datetime2
AS
begin
	set nocount on;
	begin
	insert into dbo.Bookings(BookId, UserId, BookedDate)
	values (@BookId, @UserId, @BookedDate);
	end
	begin
	update dbo.Books
	set Availabilty = 0
	where Books.Id = @BookId
	end
end