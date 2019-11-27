##SECTION 3 NOTES

### A. Application design
Make wwwroot folder with libraries, css and javascript.
- A.1. Make diagram like the one in the project description
- A.2. Choose multi-device layout pattern: Mostly Fluid, Column Drop or Layout Shifter.
If there is time go for layout-shifter (best for more advanced apps) otherwise go for column drop (matches our initial sketch better than mostly fluid) inspiration: https://www.lukew.com/ff/entry.asp?1514
- A.3. Make sketches of pages showing how bootstrap is used.
Make drawings and point to elements made with bootstrap.

### B. Data Access

- B.1. Explain the revealing module pattern
- B.2. Make the different services that make the http request using callbacks.

### C. Business Logic

### D. Presentation
Must use bootstrap for some reason.. Use mobile-first principle, meaning, first design layout for mobile, then (theoretically) tablet and then PC.
- D.1. Must be SPA (single page application). Use knockout templates.
- D.2. Must use a navigation bar (lookup bs navigation bar)
- D.3. Pagination where needed (see exercise 3.2 for inspiration)
- D.4. Must use bootstrap form-group/form-control classes(lookup bs forms) and include validation
- D.5. Make wordcloud
Get creative. Look here for inspiration: https://www.anychart.com/blog/2019/04/30/create-javascript-word-cloud-chart-tutorial/
- D.6. OPTIONAL: Make it pretty
- D.7. OPTIONAL: Advanced controls like dropdowns, modals, alerts and what do I know..

### E. OPTIONAL: Make other client app like mobile app or similar.
### F. OPTIONAL: Deploy(make available) the service on the rawdata-app.ruc.dk server

### Changes and other notes

#### Changes to web service

#### Changes to data base

#### Future works

## SECTION 2 NOTES

###A. Application Design (Not done)

- A.0 Define use cases and user stories (Alma)
- A.1 Backend architecture (figure with layers and names)
- A.2 Class diagram for dependencies between layers (names with lines in between and layer borders)
- A.3 Structure of objects (domain, transfer, json/xml) (names and attributes)


###B. The Data Access Layer (Mostly done)

- B.1 relations in database -> objects in service
- B.2 IDataService (interface)
- B.3 DataService (functionality object)


###C. Web Service Layer (c.3 and c.4 mostly done, need c.1 2 5 & 6)

- C.1 design URIs for readonly data: api/posts/body, document
- C.2 implement C.1.
- C.3 design URIs for annotation data (CRUD) create read update delete
- C.4 implement c.3
- C.5 provide paths in responses 
- C.6 implement paging (default and custom page size, next and prev)
- C.7 OPTIONAL use etags for caching
- C.8 OPTIONAL use etags for optimistic locking


###D. Security (Mostly done, implement for readonly data)

-D.1 (choose from one of the options)
-D.i hardcode for 1 user
-D.ii Http authentication header
-D.iii https, Json web tokens, authentication header, 
-help: http://jasonwatmore.com/post/2018/08/14/aspnet-core-21-jwt-authentication-tutorial-with-example-api

###E. Testing (Mostly done, add readonly data examples)

Examples from all layers

Handin
- database changes on rawdata server if any
- commit projects to github with "Section2" tag
- report 10 pages(24000 characters) (with link to github)

/////////////////////////////////////////////////
### SQL CHANGES 

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
tagging
