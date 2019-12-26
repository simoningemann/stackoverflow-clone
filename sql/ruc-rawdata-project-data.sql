-- beginning of script

-- cleanup
drop table if exists users, posts, questions, answers, comments, links, lemmas, profiles, bookmarks, queries;
drop sequence if exists profiles_sequence, queries_sequence, bookmarks_sequence;

-- step 1: create new tables for QA data
create table users (
userid integer,
name text,
creationdate timestamp,
location text,
age integer
);

create table posts (
postid integer,
creationdate timestamp,
score integer,
body text,
userid integer
);

create table questions (
postid integer,
title text,
tags text,
acceptedanswerid integer
);

create table links (
postid integer,
linkpostid integer
);

create table answers (
postid integer,
answertoid integer
);

create table comments (
commentid integer,
postid integer,
creationdate timestamp,
score integer,
body text,
userid integer
);

create table lemmas (
word varchar(100),
pos varchar(100),
lemma varchar(100)
);

-- step 2: fill QA tables with data
insert into users
select distinct ownerid, ownerdisplayname, ownercreationdate, ownerlocation, ownerage
from posts_universal;

insert into users
select distinct authorid, authordisplayname, authorcreationdate, authorlocation, authorage
from comments_universal
where authorid not in (
select userid
from users
);

alter table users rename column creationdate to timesignedup;

insert into posts
select distinct id, creationdate, score, body, ownerid
from posts_universal;

alter table posts rename column creationdate to timeposted;

insert into questions
select distinct id, title, tags, acceptedanswerid
from posts_universal
where posttypeid=1;

/* we see that a question can have more links and make a separate links relation
select distinct id
from posts_universal
where posttypeid=1
group by id
having count(id) > 1;

select *
from posts_universal
where id = 3611951; */

insert into answers
select distinct id, parentid
from posts_universal
where posttypeid=2;

/* we see that the given answer does not exist in the database and therefore acceptedanswerid cannot be foreign key
select *
from posts_universal
where posttypeid=1
and acceptedanswerid = 2293926;

select *
from posts_universal
where id = 2293926; */

insert into links
select distinct id, linkpostid
from posts_universal
where linkpostid notnull;

insert into comments
select distinct commentid, postid, commentcreatedate, commentscore, commenttext, authorid
from comments_universal;

alter table comments rename column creationdate to timecommented;

insert into lemmas
select distinct word, pos, lemma
from words;

-- create views
create or replace view fullquestions
as select questions.postid, title, tags, acceptedanswerid,
timeposted, score, body,
users.userid, name, timesignedup, location, age
from questions
join posts
on questions.postid = posts.postid
join users
on posts.userid = users.userid;

create or replace view fullanswers
as select answers.postid, answertoid,
timeposted, score, body,
users.userid, name, timesignedup, location, age
from answers
join posts
on answers.postid = posts.postid
join users
on posts.userid = users.userid;

create or replace view fullcomments
as select commentid, postid, timecommented, score, body,
users.userid, name, timesignedup, location, age
from comments
join users
on comments.userid = users.userid;

create or replace view fulllinks
as select links.postid, linkpostid,
title, tags, acceptedanswerid
from links
join questions
on linkpostid = questions.postid;

-- drop and alter old tables
alter table words
drop column lemma;
drop table posts_universal;
drop table comments_universal;


-- step 3: create constraints on QA data
alter table users
add primary key (userid);

alter table posts
add primary key (postid);
alter table posts
add foreign key (userid) references users(userid);

alter table questions
add primary key (postid);
alter table questions
add foreign key (postid)
references posts(postid);

alter table answers
add primary key (postid);
alter table answers
add foreign key (postid)
references posts(postid);
alter table answers
add foreign key (answertoid)
references questions(postid);

/* acceptedanswer cannot be foreign key because there are acceptedanswers that do not exist in answers
alter table questions /* added after answers because referenced attribute but be a primary key*/
add foreign key (acceptedanswerid)
references answers(postid); */

alter table links
add primary key (postid, linkpostid);

/* postid can not be a foreign key twice in the same table so we use the alternative below
alter table links
add foreign key (postid, linkpostid)
references questions(postid, postid); */

alter table links
add foreign key (postid)
references questions(postid);

alter table comments
add primary key (commentid);
alter table comments
add foreign key (postid)
references posts(postid);
alter table comments
add foreign key (userid)
references users(userid);

-- step 4: create tables for framework data
create table profiles (
profileid serial primary key,
email text unique not null,
salt text unique not null,
hash text unique not null
);

create sequence profiles_sequence
start 1
increment 1;

create table queries (
queryid serial primary key,
profileid integer references profiles(profileid) on delete cascade,
timesearched timestamp with time zone default current_timestamp,
querytext text
);

create sequence queries_sequence
start 1
increment 1;

create table bookmarks (
bookmarkid serial primary key,
profileid integer references profiles(profileid) on delete cascade,
postid integer references posts(postid),
note text
);

create sequence bookmarks_sequence
start 1
increment 1;

-- end of script
