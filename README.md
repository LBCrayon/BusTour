# BusTour
create table Bus
(
    Id          int identity
        primary key,
    Name        nvarchar(30),
    Description nvarchar(100),
    TotalSeat   int,
    Status      int
)
go

create table Journey
(
    Id          int identity
        primary key,
    Name        nvarchar(30),
    Price       float,
    Description nvarchar(100)
)
go

create table Media
(
    Id       int identity
        primary key,
    Video    nvarchar(max),
    Music    nvarchar(max),
    Audio    nvarchar(max),
    ImgUrl   nvarchar(max),
    Language nvarchar(20),
    Blog     nvarchar(100)
)
go

create table Place
(
    Id          int identity
        primary key,
    Name        nvarchar(30),
    Description nvarchar(100),
    TimeStay    datetime,
    MediaId     int
        constraint FK_Place_Media
            references Media,
    TimeLineId  int
)
go

create table Surcharge
(
    Id   int identity
        primary key,
    Name nvarchar(30),
    Date datetime,
    Fee  float
)
go

create table Ticket
(
    Id              int identity
        primary key,
    TotalPrice      float,
    AvailableTicket bit,
    TotalTicket     int,
    Departure       nvarchar(50),
    Arrival         nvarchar(50),
    StartTime       datetime,
    EndTime         datetime,
    TourId          int
)
go

create table Tour
(
    Id          int identity
        primary key,
    Name        nvarchar(30),
    Price       float,
    Description nvarchar(100),
    BusId       int
        constraint FK_Tour_Bus
            references Bus,
    JourneyId   int
        constraint FK_Tour_Journey
            references Journey,
    SurchargeId int
        constraint FK_Tour_Surcharge
            references Surcharge
)
go

create table TourPlace
(
    Id        int identity
        primary key,
    JourneyId int
        constraint FK_TourPlace_Journey
            references Journey,
    PlaceId   int
        constraint FK_TourPlace_Place
            references Place
)
go

