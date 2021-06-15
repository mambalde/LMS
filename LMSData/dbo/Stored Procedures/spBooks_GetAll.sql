CREATE PROCEDURE [dbo].[spBooks_GetAll]
As
begin
set nocount on;
	 select [Id], [Title], [Price], [Category], [ISBN], [PublisherId], [State], [Availabilty], [BookImage], [CreatedDate]
	 from dbo.Books
end
