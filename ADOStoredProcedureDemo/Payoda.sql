
create or alter procedure spGetAll
as
begin
   select * from student order by join_date;
end

spGetAll

create procedure spAddStudent
(@stid int,@stname varchar(30),@email varchar(30),@join_date date)
as
begin
     insert into student values(@stid,@stname,@email,@join_date)
end

spAddStudent 104,'Sam','Samu@gmail.com','2022-09-12'


create procedure spUpdateStudent
(@stid int,@stname varchar(30),@email varchar(30),@join_date date)
as
begin
     update student set name=@stname,email=@email,join_date=@join_date where id=@stid
end

exec spUpdateStudent 101,'Anu','Anu@gmail.com','2018-12-20'

--Aggregate Functions - Count,Min,MAx,Sum,Avg
select count(id) from student where year(join_date)=2018


create or alter procedure spCountStudent(@year int,@count int out)
as
begin
     select  @count = count(id) from student where year(join_date) > @year;
end
 
 declare @count int 
 exec spCountStudent 2021,@count output 
 print @count 

