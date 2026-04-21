# Labs_02 - 21.04.2026
## Cum cream prima migrare

![alt text](image.png)

Tools -> NuGet Package Manager -> Package Manager Console

!! IMPORTANT
Inainte de a rula orice cod trebuie sa fim siguri ca avem selectat SmartShoppingAssistant.DataAccess ca Default Project.

In consola pentru a crea o migrare se scrie: 
Add-Migration InitialCreate

In consola pentru a aplica migrarea se scrie: 
Update-Database

## Alte comenzi
1. Crearea și aplicarea modificărilor
Add-Migration NumeleMigrarii

Ce face: „Face o poză” (un snapshot) la clasele tale de C# din acel moment și creează un fișier cu instrucțiuni despre cum trebuie modificată baza de date.

Când se folosește: De fiecare dată când adaugi o clasă nouă, adaugi o coloană (proprietate) nouă sau modifici o relație în codul tău C#.

Exemplu: Add-Migration AdaugareTabelProduse

Update-Database

Ce face: Ia toate migrarile pe care le-ai creat cu comanda de mai sus și care nu au fost încă rulate, și le aplică fizic pe baza de date SQL.

Când se folosește: Imediat după un Add-Migration reușit.

2. Anularea greșelilor (Undo)
Remove-Migration

Ce face: Șterge ultima migrare pe care ai creat-o (fișierul C# generat).

Atenție: Funcționează doar dacă NU ai dat încă Update-Database. Este utilă când dai Add-Migration și realizezi imediat că ai uitat să adaugi un câmp în cod.

Update-Database NumeMigrareVeche

Ce face: Anulează modificările din baza de date și te întoarce la un punct din trecut.

Când se folosește: Dacă ai stricat ceva în baza de date și vrei să dai "Rollback" la o versiune anterioară stabilă.

Exemplu: Dacă ai migrările Migrare1, Migrare2, Migrare3 și vrei să anulezi tot ce a făcut Migrare3, scrii: Update-Database Migrare2. După ce ai făcut asta pe baza de date, poți folosi Remove-Migration ca să ștergi și fișierul pentru Migrare3.

3. Utilitare și "Distrugere"
Drop-Database

Ce face: Șterge complet baza de date. Te va întreba dacă ești sigur (Are you sure? (Y/N)).

Când se folosește: Foarte des în faza de dezvoltare/teste (niciodată în producție!), când baza de date s-a corupt sau s-au încurcat migrările și vrei să o iei de la zero curat. După ce o ștergi, dai din nou un simplu Update-Database și se va recrea perfect.

Script-Migration

Ce face: În loc să aplice modificările, îți generează tot codul SQL brut de care ar fi nevoie pentru a crea baza de date.

Când se folosește: De obicei când lucrezi într-o echipă și administratorul bazei de date cere scriptul SQL manual pentru a-l rula pe serverul de producție.

## Cum scrii un query

![alt text](image-1.png)

In SQL Server Management Studio poti sa dai new query si apoi sa dai execute dupa ce ai pus query ul tau

## CRUD (Create - Insert, Read - Select, Update - Update, Delete - Delete)


## HTTP Methods

Hypertext Transfer Protocol (HTTP) este un protocol de comunicare utilizat pentru transferul de date între client și server.

```mermaid
sequenceDiagram
    participant Client
    participant Server

    Client->>Server: GET /api/products
    Server-->>Client: 200 OK + JSON list

    Client->>Server: POST /api/products
    Server-->>Client: 201 Created + new product

    Client->>Server: PUT /api/products/1
    Server-->>Client: 200 OK + updated product

    Client->>Server: DELETE /api/products/1
    Server-->>Client: 204 No Content
```

HTTP - Request

Method Path Version
Header: Value 1
Header: Value 2
Header: Value 3

Body (optional)

```
GET /api/products HTTP/1.1
Host: example.com
User-Agent: Mozilla/5.0
Accept: application/json
Authorization: Bearer <token>
Pragma: no-cache
```

Operatiile de baza utilizate pentru gestionarea datelor dintr-o aplicatie:

- GET: Obtine date de la server
- POST: Trimite date la server
- PUT: Actualizeaza date la server
- PATCH: Actualizeaza partial date la server
- DELETE: Sterge date de la server

Diferenta PUT vs PATCH

PUT: Inlocuieste complet resursa
PATCH: Actualizeaza partial resursa

update - ne referim la o singura resursa
la patch modifici doar ce ai nevoie
la put modifici tot

HTTP Response

Version Status code Message
Header: Value 1
Header: Value 2
Header: Value 3

Body (optional)

```
HTTP/1.1 200 OK
Content-Type: application/json
Content-Length: 123
Date: Mon, 21 Apr 2026 12:00:00 GMT

{
    "id": 1,
    "name": "Product 1",
    "price": 10.0,
    "description": "Description 1"
}
```

Status Codes

1xx Informational
2xx Success
3xx Redirection
4xx Client Error
5xx Server Error

200 OK
201 Created
204 No Content
400 Bad Request
401 Unauthorized
403 Forbidden
404 Not Found
500 Internal Server Error
301 Moved Permanently
302 Found
304 Not Modified


http.cat - https://http.cat/

## REST API

Representational State Transfer (REST) este un stil arhitectural pentru proiectarea aplicațiilor distribuite.

Se refera la cum interactioneaza clientul cu serverul.

### Principii

1. Client-Server Architecture - Clientul si serverul sunt separate
2. Stateless - Serverul nu stocheaza date despre client
3. Cacheable - Serverul poate stoca date in cache
4. Uniform Interface - Serverul expune date prin resurse
5. Layered System - Serverul poate fi accesat prin mai multe straturi
6. Code on Demand (optional) - Serverul poate trimite cod clientului

## Architecture - API

Set de definitii si protocoale care permit comunicarea intre aplicatii.

Endpoint - URL-ul la care clientul poate accesa o resursa.

### Flow

```mermaid
 API DTO    <--->  Controller  <--->  Service  <--->  Repository  <--->  Database
``` 

controller - expune endpoint-urile, interactiunea cu clientul

service - contine logica de business (daca se calculeaza ceva)

repository - contine logica de acces la date

database - contine datele

DTO - Data Transfer Object - Obiect care transfera date intre straturi
merge si de la frontend la backend si invers
ex: in frontend ai un model de date, in backend ai alt model de date, DTO-ul face conversia intre ele, daca de ex am un buton care vreau sa se afiseze doar pentru utilizatorii mai mare de un an in frontend

## JWT (JSON Web Token)

JWT este un standard deschis (RFC 7519) pentru crearea de token-uri de acces, bazat pe JSON.

Structura unui JWT

Un JWT este format din trei părți separate prin puncte (.):

Header
Payload
Signature

```
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c
```

Header

```json
{
    "alg": "HS256",
    "typ": "JWT"
}
```

Payload

```json
{
    "sub": "1234567890",
    "name": "John Doe",
    "iat": 1516239022
}
```

Signature

```
SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c
```