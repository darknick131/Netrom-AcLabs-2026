# Database Schema

## Tables

### Categories

| Column      | Key  | Constraints | Data Type     |
| :---------- | :--- | :---------- | :------------ |
| Id          | PK   |             | int           |
| Name        |      | REQ         | nvarchar(100) |
| Description |      |             | nvarchar(500) |

---

### Products

| Column      | Key  | Constraints | Data Type      |
| :---------- | :--- | :---------- | :------------- |
| Id          | PK   |             | int            |
| Name        |      | REQ         | nvarchar(200)  |
| Description |      |             | nvarchar(1000) |
| Price       |      | REQ         | decimal(10,2)  |
| ImageUrl    |      |             | nvarchar(500)  |

---

### ProductCategories

| Column     | Key    | Constraints | Data Type |
| :--------- | :----- | :---------- | :-------- |
| ProductId  | PK, FK |             | int       |
| CategoryId | PK, FK |             | int       |

---

### Promotions

| Column      | Key  | Constraints | Data Type     |
| :---------- | :--- | :---------- | :------------ |
| Id          | PK   |             | int           |
| Name        |      | REQ         | nvarchar(200) |
| Type        |      | REQ         | int (enum)    |
| Threshold   |      | REQ         | decimal(10,2) |
| Reward      |      | REQ         | int (enum)    |
| RewardValue |      | REQ         | int           |
| ProductId   | FK   |             | int?          |
| CategoryId  | FK   |             | int?          |
| IsActive    |      |             | bit           |

---

### CartItems

| Column    | Key  | Constraints | Data Type |
| :-------- | :--- | :---------- | :-------- |
| Id        | PK   |             | int       |
| ProductId | FK   |             | int       |
| Quantity  |      | REQ         | int       |
