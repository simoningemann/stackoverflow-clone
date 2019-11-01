NOTES FOR PORTFOLIO PROJECT

A. Application Design

A.0 Define use cases and user stories (Alma)
A.1 Backend architecture (figure with layers and names)
A.2 Class diagram for dependencies between layers (names with lines in between and layer borders)
A.3 Structure of objects (domain, transfer, json/xml) (names and attributes)


B. The Data Access Layer

B.1 relations in database -> objects in service
B.2 IDataService (interface)
B.3 DataService (functionality object)


C. Web Service Layer

C.1 design URIs for readonly data: api/posts/body, document
C.2 implement C.1.
C.3 design URIs for annotation data (CRUD) create read update delete
C.4 implement c.3
C.5 provide paths in responses 
C.6 implement paging (default and custom page size, next and prev)
C.7 OPTIONAL use etags for caching
C.8 OPTIONAL use etags for optimistic locking


D. Security

D.1 (choose from one of the options)
D.i hardcode for 1 user
D.ii Http authentication header
D.iii https, Json web tokens, authentication header, 
help: http://jasonwatmore.com/post/2018/08/14/aspnet-core-21-jwt-authentication-tutorial-with-example-api

E. Testing

Examples from all layers


Handin
database changes on rawdata server if any
commit projects to github with "Section2" tag
report 10 pages(24000 characters) (with link to github)


