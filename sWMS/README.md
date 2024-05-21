Art_Type = 0 ---> service
Art_Type = 1 ---> article

Doc_Type = 0 ---> PM
Doc_Type = 1 ---> WM

It_Art_Id = -1
It_Art_Type = -1
It_Art_No = -1 - usuniêta kartoteka

It_Art_Id = null
It_Art_Type = null
It_Art_No = null - niedodana kartoteka

Dodanie modeli:
Scaffold-DbContext 'Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=sWMS;Persist Security Info=True;User ID=sa;Password=***********' Microsoft.EntityFrameworkCore.SqlServer -context ApplicationDbContext -OutputDir Models

