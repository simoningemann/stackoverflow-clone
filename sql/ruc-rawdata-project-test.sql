-- create_profile test
-- before:
select * from profiles;
do $$ begin
    perform "create_profile"('person1email', 'person1pw');
    perform "create_profile"('person2email', 'person2pw');
    perform "create_profile"('person3email', 'person3pw');
end $$
language 'plpgsql';
-- after:
select * from profiles;

-- profile_login
do $$ begin
    perform "profile_login"('person1email', 'person1pw');
    perform "profile_login"('bademail', 'badpassword');
end $$
language 'plpgsql';

-- create_bookmark
-- before:
select * from bookmarks;
do $$ begin
    perform "create_bookmark"(1, 19);
end $$
language 'plpgsql';
-- after:
select * from bookmarks;

-- update_note
-- before:
select * from bookmarks;
do $$ begin
    perform "update_note"(1, 19, 'OP is such a noob');
end $$
language 'plpgsql';
-- after:
select * from bookmarks;

-- store_query
-- before:
select * from queries;
do $$ begin
    perform "store_query"(1, 'noob');
end $$
language 'plpgsql';
-- after:
select * from queries;

-- delete_profile
-- before:
select * from profiles;
do $$ begin
    perform "delete_profile"('person1email');
end $$
language 'plpgsql';
-- after:
select * from profiles;

-- simple_search:
select simple_search('noob') limit 5;

-- exact_match: supports up to 3 copy paste for more support
-- could not get dynamic function to work :/
select exact_match('noob') limit 5;
select exact_match('noob', 'new');
select exact_match('noob', 'new', 'post');

-- best_match: also overloaded
select * from best_match('noob', 'new') limit 5;
select * from best_match('noob', 'new', 'post') limit 5;

-- ranked_weight: also overloaded
select * from ranked_weight('noob') limit 5;
select * from ranked_weight('noob', 'new') limit 5;
select * from ranked_weight('noob', 'new', 'post') limit 5;

-- word_to_word
select *
from word_to_word_concat('noob', 'new')
where pid=6438012
limit 5;

select *
from word_to_word_weight_concat('noob', 'new')
where pid=6438012
limit 5;
