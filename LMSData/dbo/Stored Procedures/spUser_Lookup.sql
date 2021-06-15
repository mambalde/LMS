CREATE PROCEDURE [dbo].[spUser_Lookup]
	@Id nvarchar(128)
AS
begin
    set nocount on;
	SELECT StaffId, StaffName, Email
	from [dbo].Staff
	where StaffId = @Id;
end
