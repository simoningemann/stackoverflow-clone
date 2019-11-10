NOTES FOR PORTFOLIO PROJECT

A. Application Design (Not done)

A.0 Define use cases and user stories (Alma)
A.1 Backend architecture (figure with layers and names)
A.2 Class diagram for dependencies between layers (names with lines in between and layer borders)
A.3 Structure of objects (domain, transfer, json/xml) (names and attributes)


B. The Data Access Layer (Mostly done)

B.1 relations in database -> objects in service
B.2 IDataService (interface)
B.3 DataService (functionality object)


C. Web Service Layer (c.3 and c.4 mostly done, need c.1 2 5 & 6)

C.1 design URIs for readonly data: api/posts/body, document
C.2 implement C.1.
C.3 design URIs for annotation data (CRUD) create read update delete
C.4 implement c.3
C.5 provide paths in responses 
C.6 implement paging (default and custom page size, next and prev)
C.7 OPTIONAL use etags for caching
C.8 OPTIONAL use etags for optimistic locking


D. Security (Mostly done, implement for readonly data)

D.1 (choose from one of the options)
D.i hardcode for 1 user
D.ii Http authentication header
D.iii https, Json web tokens, authentication header, 
help: http://jasonwatmore.com/post/2018/08/14/aspnet-core-21-jwt-authentication-tutorial-with-example-api

E. Testing (Mostly done, add readonly data examples)

Examples from all layers

Handin
database changes on rawdata server if any
commit projects to github with "Section2" tag
report 10 pages(24000 characters) (with link to github)

/////////////////////////////////////////////////
////////////// SQL CHANGES //////////////////////

create or replace function profile_login(email_in text, pw_in text)
returns void as $$
declare
actual_hash text := (select pwhash from profiles where email = email_in);
test_hash text := md5((select salt from profiles where email = email_in)||pw_in);
begin
if test_hash = actual_hash then
raise notice 'email and password correct';
else
raise notice 'email and password not correct';
end if;
end $$ language 'plpgsql';

changed too:

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

///////////// Removal of stopwords and symbols

delete from weighted_inverted_index
where word !~* '^[a-z][a-z0-9_]+$'
or word in (SELECT word from stopwords)

added this dynamic/variadic search function:
CREATE OR REPLACE FUNCTION ranked_weight_variadic(variadic keywords text[])
 RETURNS TABLE (pid integer, w float) as $$
DECLARE
    elem text;
    numkeywords integer = array_length(keywords, 1);
    counter integer = 0;
    query text := 'select postid, sum(weight)::float from(';
BEGIN
    raise notice '%', numkeywords;
    foreach elem in array keywords
    loop
        raise notice '%', counter;
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
    raise notice '%', query;
    RETURN QUERY EXECUTE query;
END$$
LANGUAGE 'plpgsql';

added change_password function
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

/////////////
git test

