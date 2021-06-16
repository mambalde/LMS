CREATE PROCEDURE [dbo].[spBook_Delete]
	@Id int
AS
begin
   set nocount on;
   begin
   update dbo.Books
   set Availabilty = 0
   where Id= @Id
   end
end