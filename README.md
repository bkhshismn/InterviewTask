Task Interview
------------------------------------------------------------------------------------------------------
Implement CRUD operations for two tables, country and city, in code first approach.
------------------------------------------------------------------------------------------------------


The following are mandatory:
------------------------------------------------------------------------------------------------------
1. The country table must include the following columns:
   - Name
   - PersianName
   - CountryCode
   - Status: Active/Deactivate
   - IsDeleted: True/False

2. The city table must include the following columns:
   - Name
   - PersianName
   - CityCode
   - Status: Active/Deactivate
   - IsDeleted: True/False
   - Type: Urban - Rural - SmallCity
   - CountryId: Foreign key Relation To Country 

3. Each country can have multiple cities.

4. It is not possible to register a city for inactive countries.

5. Deletion should occur in two ways: complete deletion or setting the "isDeleted" field to true.

6. Upload the project to GitHub and send the repository link to kalanaki@pendarpajouh.com
------------------------------------------------------------------------------------------------------



The following are optional:
------------------------------------------------------------------------------------------------------
1. The use of authorization is optional and will be considered an advantage if handled.

2. The use of AutoMapper is considered an advantage.

3. The use of a service is considered an advantage.

4. Creating a Docker file and providing a Docker image file is considered an advantage.

5. It should be possible to edit each column of the tables without sending the remaining columns.
------------------------------------------------------------------------------------------------------



Happy Code :-)