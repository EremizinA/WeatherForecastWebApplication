create database WeatherDataTest

create table Countries
([ID] int not null primary key identity(1,1),
[Name] varchar (30) not null,
[CountryCode] varchar(3) not null)

create table Cities
([ID] int not null primary key identity(1,1),
[Name] varchar (30) not null,
[CountryID] int not null FOREIGN KEY ([CountryID]) REFERENCES Countries([ID]))

create table WeatherToday
([ID] int not null primary key identity(1,1),
[Temperature] int not null,
[Scale] char (2) not null,
[CityID] int not null FOREIGN KEY ([CityID]) REFERENCES Cities([ID]))

insert into Countries
([Name],[CountryCode])
values
('Ukraine', 'UA'),
('United States Of America', 'US'),
('Great Britain', 'GB'),
('Poland', 'PL'),
('Norway', 'NO')

insert into Cities
([Name],[CountryID])
values
('Kyiv', 1),
('New York', 2),
('London', 3),
('Warsaw', 4),
('Oslo', 5),
('Lviv', 1),
('Washington', 2),
('Manchester', 3),
('Krakow', 4),
('Bergen', 5)

insert into WeatherToday
([Temperature], [Scale], [CityID])
values
(22, '°C', 1),
(70, '°F', 2),
(18, '°C', 3),
(19, '°C', 4),
(17, '°C', 5),
(23, '°C', 6),
(75, '°F', 7),
(15, '°C', 8),
(20, '°C', 9),
(16, '°C', 10)