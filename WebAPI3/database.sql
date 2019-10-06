create table books (id int not null,
				title varchar(255),
				author varchar(255))

go

insert into books (id, title, author) values
(1,  'War and Peace', 'lev Nikolayevich Tolstoy'),
(2,  'Les Miserables', 'Victor Hugo'),
(3, 'The Time Machine', 'H. G. Wells'),
(4, 'A Journey into the Center of the Earth', 'Jules Verne'),
(5,  'The Dark World', 'Henry Kuttner'),
(6, 'The Wind in the Willows', 'Kenneth Grahame'),
(7, 'Life On The Mississippi', 'Mark Twain'),
(8, 'Childhood', 'Lev Nikolayevich Tolstoy')

go

create table dbo.authors (id int not null,
				name varchar(255),
				contact varchar(255))

go

insert into dbo.authors (id, name, contact) values
(1, 'lev Nikolayevich Tolstoy', 'warandpeace@gmail.com'),
(2, 'Victor Hugo', '' ),
(3, 'H. G. Wells', 'gh@gmail'),
(4, 'Jules Verne', 'test@gmail'),
(5, 'Henry Kuttner', 'henry@gmail.com'),
(6, 'Kenneth Grahame', 'kenneth@yahoo.com'),
(7, 'Mark Twain', 'mississippi@gmail.com'),
(8, 'Lev Nikolayevich Tolstoy', 'childhood@gmail.com')

go
