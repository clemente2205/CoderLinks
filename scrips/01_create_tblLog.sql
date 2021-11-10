create table WarLog(
	IdGame int identity(1,1) primary key,
	WinPlayer varchar(50),
	RoundsToWin int
)