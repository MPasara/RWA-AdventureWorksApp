--RWA--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

/*
KreditnaKartica
Kupac
Racun
Proizvod
Stavka
Kategorija
Potkategorija
Grad\
Drzava\
*/

--azuriranje kupaca,drzave,grada, filtriranje podataka po gradu i po drzavi(CRUD)[Web forms]
--azuriranje potkategorija, kategorija i proizvoda(CRUD)[MVC]

use AdventureWorksOBP
go

create table Users
(
	IDUser int primary key identity,
	Firstname nvarchar(100)not null,
	Surname nvarchar(100)not null,
	Username nvarchar(50)not null,
	Pwrd nvarchar(100)not null
)
go

insert into Users values ('Mate','Matic','admin',123456)
go


--USER PROCEDURES
create proc createUser
@IDUser int output,
@Firstname nvarchar(100),
@Surname nvarchar(100),
@Username nvarchar(50),
@Pwrd nvarchar(100)
as
begin
	if not exists(select * from Users where Username=@Username and Pwrd=@Pwrd)
	begin
	insert into Users values (@Firstname,@Surname,@Username,@Pwrd)
	set @IDUser = SCOPE_IDENTITY()
end
else
begin
	select @IDUser = IDUser from Users where Username=@Username and Pwrd=@Pwrd
end
end
go

create proc selectUser
@IDUser int
as
begin
	select * from Users where IDUser=@IDUser
end
go

create proc selectUsers
as
begin
	select * from Users
end
go

create proc updateUser
@IDUser int,
@Firstname nvarchar(100),
@Surname nvarchar(100),
@Username nvarchar(50),
@Pwrd nvarchar(100)
as
begin
	update Users set Firstname=@Firstname,Surname=@Surname,Username=@Username,Pwrd=@Pwrd
	where IDUser=@IDUser
end
go


create proc validateUser
@Username nvarchar(100),
@Pwrd nvarchar(100)
as
begin
	select * from Users where Username=@Username and Pwrd=@Pwrd
end
go

create proc deleteUser
@IDUser int
as
begin
	delete from Users
	where IDUser=@IDUser
end 
go

--COUNTRY PROCEDURES
--IDDrzava, Naziv
create proc createDrzava
@IDDrzava int output,
@Naziv nvarchar(100)
as
begin
	insert into Drzava values (@Naziv)
	set @IDDrzava = SCOPE_IDENTITY()
end
go

create proc selectDrzava
@IDDrzava int
as
begin
	select * from Drzava
	where IDDrzava = @IDDrzava
end
go

create proc selectDrzavas
as
begin
	select * from Drzava
end
go

create proc updateDrzava
@IDDrzava int,
@Naziv nvarchar(100)
as
begin
	update Drzava set @Naziv=Naziv
	where IDDrzava=@IDDrzava
end
go

create proc deleteDrzava
@IDDrzava int
as
begin
	delete from Drzava
	where IDDrzava=@IDDrzava
end
go

--CITY PROCEDURES
--IDGrad, Naziv, DrzavaID
create proc createGrad
@Naziv nvarchar(100),
@DrzavaID int
as 
begin 
	insert into Grad values (@Naziv,@DrzavaID)
end
go

create proc selectGrad
@IDGrad int
as
begin
	select * from Grad
	where IDGrad=@IDGrad
end
go


create proc selectGradovi
as
begin
	select * from Grad
end
go

create proc getCities
@DrzavaID int
as
begin
	select * from Grad
	where DrzavaID=@DrzavaID
end
go

create proc updateGrad
@IDGrad int,
@Naziv nvarchar(100),
@DrzavaID int
as 
begin 
	update Grad set @Naziv=Naziv, @DrzavaID=DrzavaID
	where IDGrad=@IDGrad
end
go

create proc updateDrzavaZaGrad
@IDGrad int,
@DrzavaID int
as
begin
	update Grad set @DrzavaID=DrzavaID
	where IDGrad=@IDGrad
end
go

create proc deleteGrad
@IDGrad int
as
begin
	delete from Grad
	where IDGrad=@IDGrad
end
go

--KUPAC PROCEDURES
--IDKupac, Ime, Prezime, Email, Telefon, GradID
create proc createKupac
@IDKupac int output,
@Ime nvarchar(100),
@Prezime nvarchar(100),
@Email nvarchar(50),
@Telefon nvarchar(50),
@GradID int
as
begin
	insert into Kupac values (@Ime,@Prezime,@Email,@Telefon,@GradID)
	set @IDKupac = SCOPE_IDENTITY()
end
go

create proc selectKupac
@IDKupac int
as
begin
	select * from Kupac
	where IDKupac=@IDKupac
end
go

create proc getKupacDetails
	@IDKupac int
as
begin
	select k.IDKupac, k.Ime, k.Prezime, k.Email, k.Telefon, g.IDGrad, g.Naziv, d.IDDrzava, d.Naziv from Kupac as k
	inner join Grad as g on k.GradID = g.IDGrad
	inner join Drzava as d on g.DrzavaID = d.IDDrzava
	where IDKupac = @IDKupac
end
go

create proc selectKupci
as
begin
	select * from Kupac
end
go

create proc updateGradZaKupac
@IDKupac int,
@GradID int
as
begin
	update Kupac set @GradID=GradID
	where IDKupac=@IDKupac
end
go

create proc getKupacById
@kupacId int
as
begin
	select * from Kupac
	where IDKupac=@kupacId
end
go

create proc updateKupac
@IDKupac int,
@Ime nvarchar(100),
@Prezime nvarchar(100),
@Email nvarchar(50),
@Telefon nvarchar(50),
@GradID int
as
begin
	update Kupac set Ime=@Ime, Prezime=@Prezime, Email=@Email, Telefon=@Telefon,GradID=@GradID
	where IDKupac=@IDKupac
end
go

create proc getRacuniOdKupac
@KupacID int
as
begin
	select * from Racun
	where KupacID=@KupacID
end
go


CREATE PROCEDURE GetKupciGrada
	@gradId int
AS
BEGIN
	select top 10 * from Kupac
	where GradID=@gradId
	order by IDKupac desc	
END
GO

create proc sortCustomersByFirstnameAsc
as
begin
	select * from Kupac as k
	order by k.Ime asc
end
go

create proc sortCustomersByFirstnameDesc
as
begin
	select * from Kupac as k
	order by k.Ime desc
end
go

create proc sortCustomersBySurnameAsc
as
begin
	select * from Kupac as k
	order by k.Prezime asc
end
go

create proc sortCustomersBySurnameDesc
as
begin
	select * from Kupac as k
	order by k.Prezime desc
end
go

create proc deleteKupac
@IDKupac int
as
begin
	delete from Racun
	where KupacID=@IDKupac

	delete from Kupac
	where IDKupac=@IDKupac
end
go

--RACUN PROCEDURES
--IDRacun, DatumIzdavanja,BrojRacuna,KupacID,KomercijalistID,KreditnaKarticaID,Komentar
create proc createRacun
@IDRacun int output,
@DatumIzdavanja datetime,
@BrojRacuna nvarchar(100),
@KupacID int,
@KomercijalistID int,
@KreditnaKarticaID int,
@Komentar nvarchar(max)
as
begin
	insert into Racun values (@DatumIzdavanja,@BrojRacuna,@KupacID,@KomercijalistID,@KreditnaKarticaID,@Komentar)
	set @IDRacun = SCOPE_IDENTITY()
end
go

create proc selectRacun
@IDRacun int
as
begin
	select * from Racun
	where IDRacun=@IDRacun
end
go

create proc selectRacuni
as
begin
	select * from Racun
end
go

create proc updateRacun
@IDRacun int,
@DatumIzdavanja datetime,
@BrojRacuna nvarchar(100),
@KupacID int,
@KomercijalistID int,
@KreditnaKarticaID int,
@Komentar nvarchar(max)
as
begin
	update Racun set DatumIzdavanja=@DatumIzdavanja,BrojRacuna=@BrojRacuna,KupacID=@KupacID,KomercijalistID=@KomercijalistID,KreditnaKarticaID=@KreditnaKarticaID,Komentar=@Komentar
	where IDRacun=@IDRacun
end
go

create proc deleteRacun
@IDRacun int
as
begin
	delete from Racun
	where IDRacun=@IDRacun
end
go

--STAVKA PROCEDURES
--IDStavka,RacunID,Kolicina,ProizvodID,CijenaPoKomadu,PopustUPostotcima,UkupnaCijena
create proc createStavka
@IDStavka int output,
@RacunID int,
@Kolicina smallint,
@ProizvodID int,
@CijenaPoKomadu money,
@PopustUPostotcima money,
@UkupnaCijena numeric
as
begin
	insert into Stavka values (@RacunID,@Kolicina,@ProizvodID,@CijenaPoKomadu,@PopustUPostotcima,@UkupnaCijena)
	set @IDStavka = SCOPE_IDENTITY()
end
go

create proc selectStavka
@IDStavka int
as
begin
	select * from Stavka
	where IDStavka=@IDStavka
end
go

create proc selectStavke
as
begin
	select * from Stavka
end
go

create proc updateStavka
@IDStavka int,
@RacunID int,
@Kolicina smallint,
@ProizvodID int,
@CijenaPoKomadu money,
@PopustUPostotcima money,
@UkupnaCijena numeric
as
begin
	update Stavka set RacunID=@RacunID,Kolicina=@Kolicina,ProizvodID=@ProizvodID,CijenaPoKomadu=@CijenaPoKomadu,PopustUPostocima=@PopustUPostotcima,UkupnaCijena=@UkupnaCijena
	where IDStavka=@IDStavka
end
go

create proc deleteStavka
@IDStavka int
as
begin
	delete from Stavka 
	where IDStavka=@IDStavka
end
go

--KOMERCIJALIST PROCEDURES
--IDKomercijalist,Ime,Prezime,StalniZaposlenik
create proc createKomercijalist
@IDKomercijalist int output,
@Ime nvarchar(100),
@Prezime nvarchar(100),
@StalniZaposlenik bit
as
begin
	insert into Komercijalist values (@Ime,@Prezime,@StalniZaposlenik)
	set @IDKomercijalist = SCOPE_IDENTITY()
end
go

create proc selectKomercijalist
@IDKomercijalist int
as
begin
	select * from Komercijalist
	where IDKomercijalist=@IDKomercijalist
end
go

create proc selectKomercijalisti
as
begin
	select * from Komercijalist
end
go

create proc updateKomercijalist
@IDKomercijalist int,
@Ime nvarchar(100),
@Prezime nvarchar(100),
@StalniZaposlenik bit
as
begin
	update Komercijalist set Ime=@Ime,Prezime=@Prezime,StalniZaposlenik=@StalniZaposlenik
end
go

create proc deleteKomercijalist
@IDKomercijalist int
as
begin
	delete from Komercijalist
	where IDKomercijalist=@IDKomercijalist
end
go

--KATEGORIJA PROCEDURES
--IDKategorija,Naziv
create proc createKategorija
@IDKategorija int output,
@Naziv nvarchar(100)
as
begin
	insert into Kategorija values (@Naziv)
	set @IDKategorija=SCOPE_IDENTITY()
end
go

create proc selectKategorija
@IDKategorija int
as
begin
	select * from Kategorija
	where IDKategorija=@IDKategorija
end
go

create proc selectKategorije
as
begin
	select * from Kategorija
end
go

create proc updateKategorija
@IDKategorija int,
@Naziv nvarchar(100)
as
begin
	update Kategorija set Naziv=@Naziv
	where IDKategorija=@IDKategorija
end
go

create proc deleteKategorija
@IDKategorija int
as
begin
	delete from Stavka
	where ProizvodID in (select IDProizvod from Proizvod where IDProizvod=ProizvodID)
	delete from Proizvod
	where PotkategorijaID in (select IDPotkategorija from Potkategorija where KategorijaID=@IDKategorija)
	delete Potkategorija
	where KategorijaID=@IDKategorija
	delete from Kategorija
	where IDKategorija=@IDKategorija
end
go

--POTKATEGORIJA PROCEDURES
--IDPotkategorija, KategorijaID, Naziv
create proc createPotkategorija
@IDPotkategorija int output,
@KategorijaID int,
@Naziv nvarchar
as
begin
	insert into Potkategorija values (@KategorijaID,@Naziv)
	set @IDPotkategorija = SCOPE_IDENTITY()
end
go

create proc selectPotkategorija
@IDPotkategorija int
as
begin
	select * from Potkategorija
	where IDPotkategorija=@IDPotkategorija
end
go

create proc selectPotkategorije
as
begin
	select * from Potkategorija
end
go

create proc updatePotkategorija
@IDPotkategorija int output,
@KategorijaID int,
@Naziv nvarchar
as
begin
	update Potkategorija set KategorijaID=@KategorijaID,@Naziv=Naziv
	where IDPotkategorija=@IDPotkategorija
end
go

create proc getPotkategorijaById
@IDPotkategorija int
as
begin
	select pk.Naziv from Potkategorija as pk
	where pk.IDPotkategorija=@IDPotkategorija
end
go

create proc getPotkategorijaByName
@Name nvarchar
as 
begin 
	select pk.IDPotkategorija, pk.Naziv from Potkategorija as pk
	where pk.Naziv=@Name
end
go

create proc deletePotkategorija
@IDPotkategorija int
as
begin
	delete from Proizvod 
	where PotkategorijaID=@IDPotkategorija
	
	delete from Potkategorija
	where IDPotkategorija=@IDPotkategorija
end
go

--KREDITNAKARTICA PROCEDURES
--IDKreditnaKartica, Tip, Broj, IstekMjesec, IstekGodina
create proc createKreditnaKartica
@IDKreditnaKartica int output,
@Tip nvarchar,
@Broj nvarchar,
@IstekMjesec tinyint,
@IstekGodina smallint
as
begin
	insert into KreditnaKartica values (@Tip,@Broj,@IstekMjesec,@IstekGodina)
	set @IDKreditnaKartica = SCOPE_IDENTITY()
end
go

create proc selectKreditnaKartica
@IDKreditnaKartica int
as
begin
	select * from KreditnaKartica
	where IDKreditnaKartica=@IDKreditnaKartica
end
go

create proc selectKreditneKartice
as
begin
	select * from KreditnaKartica
end
go

create proc updateKreditnaKartica
@IDKreditnaKartica int output,
@Tip nvarchar,
@Broj nvarchar,
@IstekMjesec tinyint,
@IstekGodina smallint
as
begin
	update KreditnaKartica set Tip=@Tip,Broj=@Broj,IstekMjesec=@IstekMjesec,IstekGodina=@IstekGodina
	where IDKreditnaKartica=@IDKreditnaKartica
end
go

create proc deleteKreditnaKartica
@IDKreditnaKartica int
as
begin
	delete from KreditnaKartica
	where IDKreditnaKartica=@IDKreditnaKartica
end
go

--PROIZVOD PROCEDURES
--IDProizvod, Naziv, BrojProizvoda, Boja, MinimalnaKolicinaNaSkladistu, CijenaBezPDV, PotkategorijaID
create proc createProizvod
@IDProizvod int output,
@Naziv nvarchar(100),
@BrojProizvoda nvarchar(100),
@Boja nvarchar(50),
@MinimalnaKolicinaNaSkladistu smallint,
@CijenaBezPDV money,
@PotkategorijaID int
as
begin
	insert into Proizvod values (@Naziv,@BrojProizvoda,@Boja,@MinimalnaKolicinaNaSkladistu,@CijenaBezPDV,@PotkategorijaID)
	set @IDProizvod = SCOPE_IDENTITY()
end
go

create proc selectProizvod
@IDProizvod int
as
begin
	select * from Proizvod
	where IDProizvod=@IDProizvod
end
go

create proc selectProizvodi
as
begin
	select * from Proizvod
end
go

create proc updateProizvod
@IDProizvod int output,
@Naziv nvarchar(100),
@BrojProizvoda nvarchar(100),
@Boja nvarchar(50),
@MinimalnaKolicinaNaSkladistu smallint,
@CijenaBezPDV money,
@PotkategorijaID int
as
begin
	update Proizvod set Naziv=@Naziv, BrojProizvoda=@BrojProizvoda, Boja=@Boja,MinimalnaKolicinaNaSkladistu=@MinimalnaKolicinaNaSkladistu,CijenaBezPDV=@CijenaBezPDV,PotkategorijaID=@PotkategorijaID
	where IDProizvod=@IDProizvod
end
go

create proc deleteProizvod
@IDProizvod int
as
begin
	delete from Proizvod
	where IDProizvod=@IDProizvod
end
go