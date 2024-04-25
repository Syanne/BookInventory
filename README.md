# BookInventory solution

| Project | Description | Platform|
| --- | --- | --- |
| BookInventory | WebForms project | .NET Framework 4.8 |
| BookInventory.DataAccess | Data Access Layer (utilizes SQL server connection) | .NET Standard 2.0 |
| BookInventory.Models | Models project | .NET Standard 2.0 |
| BookInventory.WebApi | Web Api | .NET Framework 4.8 |
| BookInventory.Tests | Unit Tests project | .NET Framework 4.8 |

### BookInventory.DataAccess

Please replace following connection string with your connection string:

``` "Data Source=DESKTOP-AUN9LHE;Initial Catalog=bookInventory;Integrated Security=True;Encrypt=False" ```


Please execute ``` Update-Database -Project BookInventory.DataAccess ``` on your empty db


ALso please enable Full-Text search on your MS SQL Server. You can do this using 
``` exec sp_fulltext_database 'enable'; ```

### BookInventory.WebApi APIs

Home page is preserved from scaffold.
| API | Description |
| --- | --- | 
| GET api/books | Returns book view model with category name | 
| GET api/books/{id} | Returns book view model | 
| POST api/books/ | Create a book | 
| PUT api/books/{id} | Update a book | 
| DELETE api/books/{id} | Deletes a book | 
| GET api/category | Returns book categories | 

please preserve default launch url:
``` https://localhost:44394/ ```

### BookInventory Web Forms

Contains following forms:

| Form | Description |
| --- | --- | 
| Home | Main form with a book list | 
| AddBookForm | Form that can be used to add a book | 
| DeleteBookForm | Form that can be used to  delete a book | 
| EditBookForm | Form that can be used to edit a book | 

If default BookInventory.WebApi launch URL is not preserved, please change ```_url``` and ``` _port ``` in ``` BookInventoryClient ``` 
