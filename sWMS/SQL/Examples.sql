declare @dataOd datetime = cast('20-04-2024 00:00:00' as datetime)
declare @dataDo datetime = cast('20-05-2024 00:00:00' as datetime)
select @dataOd, @dataDo

exec sWMS.ViewDocuments 100, '20-05-2024 00:00:00', '20-06-2024 00:00:00', '20-03-2024 00:00:00', '20-04-2024 00:00:00', 'iwo gej', 1