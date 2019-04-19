use DvdRepoEF

insert into dvdMain(title, realeaseyear, director, rating, notes)

values 

('Jaws','1975','Speilberg','R','Shark has a toothy smile.'),
('Star Wars','1976','Lucas','PG','It is a space-age western.')
('The Breakfast Club','1986','Hughs','PG','Detention humour.')

select * from dvdMain
GO