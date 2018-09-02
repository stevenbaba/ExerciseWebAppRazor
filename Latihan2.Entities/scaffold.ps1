Remove-Item *.cs 
dotnet ef dbcontext scaffold "Data Source=DESKTOP-DN1K3JV\SQLDEV2017;Initial Catalog=DatabaseCoba;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False" "Microsoft.EntityFrameworkCore.SqlServer" -d -f -c "StockDbContext" -v 
