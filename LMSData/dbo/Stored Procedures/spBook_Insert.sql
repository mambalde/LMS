CREATE PROCEDURE [dbo].[spBook_Insert]
    @Id int,
	@PublisherId int,
	@Price decimal,
	@Category nvarchar(50),
	@Isbn nvarchar(50),
	@State nvarchar(50),
	@Availabilty bit,
	@BookImage image,
	@Title nvarchar(10),
	@CreatedDate datetime2

AS
begin
	set nocount on;
	if @Id = 0
	begin
	insert into dbo.Books(Price, Category, ISBN, [State], Availabilty, BookImage, Title, CreatedDate, PublisherId)
	values (@Price, @Category, @Isbn, @State, @Availabilty, @BookImage, @Title, @CreatedDate, @PublisherId);
	end
	else if @Id > 0
	begin
	update dbo.Books
	set Price = @Price, Category = @Category, ISBN = @Isbn, [State] = @State, Availabilty = @Availabilty,
	BookImage = @BookImage, Title = @Title, CreatedDate = @CreatedDate, PublisherId = @PublisherId
	where Books.Id = @Id
	end
end
