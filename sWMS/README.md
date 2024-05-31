INSTRUKCJA

-1 na id oznacza usuniety element np.
It_Art_Id = -1 - usuniêta kartoteka

---------------------------------------------------------------

0 na id oznacza domyslna wartosc np.

Unit_Id = 1
Unit_Type = 100 - jednostka ilosci - sztuka

Unit_Id = 1
Unit_Type = 125 - jednostka rozmiaru - m2

Unit_Id = 1
Unit_Type = 140 - jednostka objetosci - m3 

Unit_Id = 1
Unit_Type = 140 - jednostka objetosci - litr 

Unit_Id = 1
Unit_Type = 160 - jednostka wagi - kg

---------------------------------------------------------------

Unit typy:

<100 - 124> - jednostki ilosci
<125 - 149> - jednostki rozmiaru
<150 - 174> - jednostki objetosci
<175 - 199> - jednostki wagi
<200 - 249> - zarezerwowane

---------------------------------------------------------------

It_Art_Id = null
It_Art_Type = null
It_Art_No = null - niedodana kartoteka

---------------------------------------------------------------

Art_Type = 250 ---> service
Art_Type = 251 ---> article

---------------------------------------------------------------

Doc_Type = 252 ---> PM
Doc_Type = 253 ---> WM

---------------------------------------------------------------

Dodanie modeli:
Scaffold-DbContext 'Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=sWMS;Persist Security Info=True;User ID=sa;Password=***********' Microsoft.EntityFrameworkCore.SqlServer -context ApplicationDbContext -OutputDir Models

---------------------------------------------------------------

