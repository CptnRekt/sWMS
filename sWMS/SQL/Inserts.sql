insert into sWMS.Config (Conf_CodeName, Conf_Name, Conf_Value) values ('NumeracjaSeparator', 'Separator w dokumentach', '/')
insert into sWMS.Config (Conf_CodeName, Conf_Name, Conf_Value) values ('NumeracjaCzlon1', 'Pierwszy cz�on w numeracji dokument�w', 'Numer')
insert into sWMS.Config (Conf_CodeName, Conf_Name, Conf_Value) values ('NumeracjaCzlon2', 'Drugi cz�on w numeracji dokument�w', 'Miesi�c')
insert into sWMS.Config (Conf_CodeName, Conf_Name, Conf_Value) values ('NumeracjaCzlon3', 'Trzeci cz�on w numeracji dokument�w', 'Rok')
insert into sWMS.Config (Conf_CodeName, Conf_Name, Conf_Value) values ('NumeracjaCzlon4', 'Czwarty cz�on w numeracji dokument�w', 'Seria')
insert into sWMS.Config (Conf_CodeName, Conf_Name, Conf_Value) values ('DomyslnaSeria', 'Domy�lna warto�� dla serii', 'PL')

insert into sWMS.Config (Conf_CodeName, Conf_Name, Conf_Value) values ('DomyslnaJednostkaIlosci', 'Domy�lna jednostka ilo�ci', 'szt')
insert into sWMS.Config (Conf_CodeName, Conf_Name, Conf_Value) values ('DomyslnaJednostkaRozmiaru', 'Domy�lna jednostka rozmiaru', 'm2')
insert into sWMS.Config (Conf_CodeName, Conf_Name, Conf_Value) values ('DomyslnaJednostkaObjetosci', 'Domy�lna jednostka obj�to�ci', 'm3')
insert into sWMS.Config (Conf_CodeName, Conf_Name, Conf_Value) values ('DomyslnaJednostkaWagi', 'Domy�lna jednostka wagi', 'kg')

insert into sWMS.Users (Usr_Login, Usr_Password, Usr_Autologin) values ('Admin', '', 1)