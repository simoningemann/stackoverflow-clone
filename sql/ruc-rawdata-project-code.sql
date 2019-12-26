-- framework api
/* obsolete
create or replace function create_profile(email_in text, pw_in text)
returns void as $$
declare
new_id integer := nextval('profiles_sequence');
new_salt text := md5(new_id::text);
new_hash text := md5(new_salt || pw_in);
begin
insert into profiles
values (new_id, email_in, new_salt, new_hash);
end; $$
language 'plpgsql';

create or replace function delete_profile(email_in text)
returns void as $$
begin
delete from profiles where email_in = email;
end; $$
language 'plpgsql';

create or replace function profile_login(email_in text, pw_in text)
returns boolean as $$
declare
actual_hash text := (select pwhash from profiles where email = email_in);
test_hash text := md5((select salt from profiles where email = email_in)||pw_in);
begin
if test_hash = actual_hash then
return true;
else
return false;
end if;
end $$ language 'plpgsql';

create or replace function change_password(email_in text, pw_in text)
returns void as $$
declare
new_hash text := md5((select salt from profiles where email = email_in)|| pw_in);
begin
update profiles
set pwhash = new_hash
where email = email_in;
end; $$
language 'plpgsql';
select change_password('person2email', 'newpw');

create or replace function create_bookmark(profileid_in integer, postid_in integer)
returns void as
$$ begin
insert into bookmarks
values (nextval('bookmarks_sequence'), profileid_in, postid_in, '');
end; $$
language 'plpgsql';

create or replace function store_query(profileid_in integer, query_in text)
returns void as
$$ begin
insert into queries
values (nextval('queries_sequence'), profileid_in, now(), query_in);
end; $$
language 'plpgsql';

create or replace function update_note(profileid_in integer, postid_in integer, note_in text)
returns void as
$$ begin
update bookmarks
set note = note_in
where profileid_in = profileid
and postid_in = postid;
end; $$
language 'plpgsql';
*/

-- search api
-- simple_search
create or replace function simple_search(string_in text)
returns table(pid integer)
as $$
begin
return query
select distinct posts.postid
from posts full outer join questions on posts.postid = questions.postid
where body ilike '%'||string_in||'%'
or title ilike '%'||string_in||'%';
end; $$
language 'plpgsql';

-- exact_match
create or replace function exact_match(kw1 text)
returns table(pid integer) as $$
begin
return query select distinct postid
from boolean_inverted_index
where word = kw1;
end; $$
language 'plpgsql';

create or replace function exact_match(kw1 text, kw2 text)
returns table(pid integer) as $$
begin
return query select distinct postid
from boolean_inverted_index
where word = kw1
intersect
select distinct postid
from boolean_inverted_index
where word = kw2;
end; $$
language 'plpgsql';

-- boolean_inverted_index
drop table if exists boolean_inverted_index;
create table boolean_inverted_index (
postid integer,
word text
);

insert into boolean_inverted_index
select distinct id, word
from words
where tablename = 'posts';

create or replace function exact_match(kw1 text, kw2 text, kw3 text)
returns table(pid integer) as $$
begin
return query select distinct postid
from boolean_inverted_index
where word = kw1
intersect
select distinct postid
from boolean_inverted_index
where word = kw2
intersect
select distinct postid
from boolean_inverted_index
where word = kw3;
end; $$
language 'plpgsql';

--best_match
create or replace function best_match(kw1 text, kw2 text)
returns table(pid integer, num integer) as $$
begin
    return query select postid, count(postid)::integer
    from (select distinct postid
    from boolean_inverted_index
    where word = kw1
    union all
    select distinct postid
    from boolean_inverted_index
    where word = kw2) as matches
    group by postid
    order by count(postid) desc;
    raise notice 'best_match executed';
end; $$ language 'plpgsql';

create or replace function best_match(kw1 text, kw2 text, kw3 text)
returns table(pid integer, num integer) as $$
begin
    return query select postid, count(postid)::integer
    from (select distinct postid
    from boolean_inverted_index
    where word = kw1
    union all
    select distinct postid
    from boolean_inverted_index
    where word = kw2
    union all
    select distinct postid
    from boolean_inverted_index
    where word = kw3) as matches
    group by postid
    order by count(postid) desc;
    raise notice 'best_match executed';
end; $$ language 'plpgsql';

-- start weighted index
drop table if exists weighted_inverted_index;
create table weighted_inverted_index (
postid integer,
word text,
weight float
);

create table temp_num_terms_in_post (
  postid integer,
  num integer
);

create table temp_num_specific_in_post (
  postid integer,
  word text,
  num integer
);

create table temp_num_post_with_term (
  word text,
  num integer
);

create table temp_term_freq (
postid integer,
word text,
term_freq float
);

create table temp_inv_post_freq (
word text,
inv_post_freq float
);

insert into temp_num_terms_in_post
select distinct id, count(word)::integer
from words
where tablename = 'posts'
group by id;

insert into temp_num_specific_in_post
select distinct id, word, count(word)::integer
from words
where tablename = 'posts'
group by id, word;

insert into temp_num_post_with_term
select distinct word, count(id)::integer
from words
where tablename = 'posts'
group by word;

insert into temp_term_freq
select distinct id, words.word, log(1+((temp_num_specific_in_post.num::float)/(temp_num_terms_in_post.num)))
from words, temp_num_specific_in_post, temp_num_terms_in_post
where id = temp_num_specific_in_post.postid
and id = temp_num_terms_in_post.postid
and words.word = temp_num_specific_in_post.word
and tablename = 'posts';

insert into temp_inv_post_freq
select distinct words.word, ((1)/temp_num_post_with_term.num::float)
from words, temp_num_post_with_term
where words.word = temp_num_post_with_term.word
and tablename = 'posts';

insert into weighted_inverted_index
select distinct id, words.word, temp_inv_post_freq.inv_post_freq::float * temp_term_freq.term_freq
from words, temp_inv_post_freq, temp_term_freq
where id = temp_term_freq.postid
and words.word = temp_term_freq.word
and words.word = temp_inv_post_freq.word
and tablename = 'posts';

delete from weighted_inverted_index
where word !~* '^[a-z][a-z0-9_]+$'
or word in (SELECT word from stopwords);

drop table if exists temp_inv_post_freq, temp_term_freq, temp_num_post_with_term, temp_num_specific_in_post, temp_num_terms_in_post;
-- end weighted index

-- ranked_weight
create or replace function ranked_weight(kw1 text)
returns table(pid integer, w float) as $$
begin
    return query select postid, sum(weight)::float
    from (select distinct postid, weight
    from weighted_inverted_index
    where word = kw1) as matches
    group by postid
    order by sum(weight) desc;
end; $$ language 'plpgsql';

create or replace function ranked_weight(kw1 text, kw2 text)
returns table(pid integer, w float) as $$
begin
    return query select postid, sum(weight)::float
    from (select distinct postid, weight
    from weighted_inverted_index
    where word = kw1
    union all
    select distinct postid, weight
    from weighted_inverted_index
    where word = kw2) as matches
    group by postid
    order by sum(weight) desc;
end; $$ language 'plpgsql';

create or replace function ranked_weight(kw1 text, kw2 text, kw3 text)
returns table(pid integer, w float) as $$
begin
    return query select postid, sum(weight)::float
    from (select distinct postid, weight
    from weighted_inverted_index
    where word = kw1
    union all
    select distinct postid, weight
    from weighted_inverted_index
    where word = kw2
    union all
    select distinct postid, weight
    from weighted_inverted_index
    where word = kw3) as matches
    group by postid
    order by sum(weight) desc;
end; $$ language 'plpgsql';

create or replace function ranked_weight_variadic(variadic keywords text[])
 returns table (pid integer, w float) as $$
declare
    elem text;
    numkeywords integer = array_length(keywords, 1);
    counter integer = 0;
    query text := 'select postid, sum(weight*100000)::float from(';
begin
    foreach elem in array keywords
    loop
        counter := counter +1;
        query := query || 'select distinct postid, weight from weighted_inverted_index where word = ';
        query := query || '''' || elem || '''';
        if (counter < numkeywords) then
          query := query || ' union all ';
        else
            query := query || ' ';
        end if;
    end loop;
    query := query || ') as matches group by postid order by sum(weight) desc;';
    return query execute query;
end$$
language 'plpgsql';

create or replace function searchquestions(variadic keywords text[])
 returns table (postid integer, title text, tags text, acceptedanswerid integer) as $$
declare
    elem text;
    numkeywords integer = array_length(keywords, 1);
    counter integer = 0;
    query text := 'select postid, title, tags, acceptedanswerid from ranked_weight_variadic(';
begin
    foreach elem in array keywords
    loop
        counter := counter +1;
            query := query || '''' || elem || '''';
        if (counter < numkeywords) then
            query := query || ', ';
        end if;
    end loop;
    query := query || '), questions where pid=questions.postid;';
    return query execute query;
end$$
language 'plpgsql';

create or replace function wordcloud(postid_in integer)
returns table(word text, weight float) as
$$ begin
return query execute 'select weighted_inverted_index.word, weighted_inverted_index.weight*100000::float
from weighted_inverted_index
where public.weighted_inverted_index.postid = ' || postid_in;
end $$ language 'plpgsql';

create or replace function word_to_word(kw1 text, kw2 text)
returns table(pid integer, w_count integer, w text) as
$$ begin
return query select distinct id, count(word)::integer, word::text
from words
where id in (select * from exact_match(kw1, kw2))
group by  id, word
order by id, count(word)::integer desc;
end $$ language 'plpgsql';

create or replace function word_to_word_concat(kw1 text, kw2 text)
returns table(pid integer, w_count integer, w text, wac text) as
$$ begin
return query select distinct id, count(word)::integer, word::text,  count(word)||' '||word::text
from words
where id in (select * from exact_match(kw1, kw2))
group by  id, word
order by id, count(word)::integer desc;
end $$ language 'plpgsql';

create or replace function word_to_word_weight_concat(kw1 text, kw2 text)
returns table(pid integer, w_weight float, w text, waw text) as
$$ begin
return query select distinct postid, weight, word::text,  weight*100000||' '||word::text
from weighted_inverted_index
where postid in (select * from exact_match(kw1, kw2))
group by  postid, word, weight
order by postid, weight desc;
end $$ language 'plpgsql';

/* omitted stuff

   -- word_to_word
-- d7 make into function

select distinct word, count(word)
from words
where id in (select * from exact_match('noob','new'))
group by word
order by count(word) desc;


-- d7 alternative

-- D5 (Needs framework compatibility). Other TF-IDF methods can be implemented. Unsure if requirement
-- Is for an array of keywords or just one word. From the document looks like it is meant for
-- only one keyword... From my understanding, D7 asks for the same but with an array of keywords(Query)
-- But with a word list as an answer
CREATE OR REPLACE FUNCTION weighted_indexing(keyword TEXT)
	RETURNS TABLE(iid INT4, weight double precision)
AS $$
DECLARE
	N BIGINT := (
		SELECT
			COUNT(DISTINCT id)		/* Total number of posts, N */
		FROM public.words
	);
	n_f BIGINT := (
		SELECT
			COUNT(DISTINCT id)
		FROM public.words
		WHERE public.words.word = keyword /* Total number of posts that contain keyword, n_f */

	);
BEGIN
	RETURN QUERY SELECT
	id, COUNT(id)*ln(N/n_f) 		/* tf-idf. f*ln(N/n_f) */
	FROM public.words
	WHERE public.words.word=keyword
	GROUP BY id;
END; $$
LANGUAGE 'plpgsql';

SELECT *
	FROM weighted_indexing('way');	/* Sample */

-- D7 (Needs framework compatib   ility). If you guys can tell me why and how to fix the error of function not existing for exact_match (After having created it), I can have this ready by tuesday
CREATE OR REPLACE FUNCTION word_to_words(VARIADIC keywords TEXT[])
	RETURNS TABLE(iid INT4)
AS $$
BEGIN
	RETURN QUERY SELECT *
	FROM exact_match(keywords);
END; $$
LANGUAGE 'plpgsql';

SELECT *
	FROM word_to_words('way','value');

-- Some other functions
create or replace function num_terms_in_post(id_in integer)
returns integer as $$
begin
    return (select count(word)::integer
    from words
    where id = id_in
    and tablename = 'posts' );
end; $$ language 'plpgsql';

create or replace function num_specific_in_post(id_in integer, word_in text)
returns integer as $$
begin
    return (select count(word)::integer
    from words
    where id = id_in
    and word = word_in
    and tablename = 'posts' );
end; $$ language 'plpgsql';

create or replace function num_post_to_term( word_in text)
returns integer as $$
begin
    return (select count (distinct id)::integer
    from words
    where word = word_in
    and tablename = 'posts' );
end; $$ language 'plpgsql';

create or replace function term_freq(id_in integer, word_in text)
returns float as $$
begin
    return log(1+((num_specific_in_post(id_in, word_in)::float)/(num_terms_in_post(id_in))))::float;
end; $$ language 'plpgsql';

select num_terms_in_post(19);
select num_specific_in_post(19, 'is');
select num_post_to_term('noob');
select term_freq(19, 'is');
*/
