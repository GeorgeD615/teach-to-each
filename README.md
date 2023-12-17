# Описание
Образовательная платформа для индивидуальных занятий учителей и учеников 

|Фамилия | Имя | Должность |
| ------------------ | :---: | :-----------: | 
| Давлятшин | Георгий | Backend|
| Антропова| Полина | Frontend| 
</p>

## Наименование
teach-to-each
## Предметная область
Как пользователь платформы, вы можете выступать как учителем так и учеником. Каждый пользователь характеризуется фамилией, именем, возрастом, контактными данными(электронной почтой). Как учитель вы можете, добавлять себе новых учеников, для уже существующих учеников добавлять домашнее задание(описание д/з, срок сдачи, выполнено/невыполнено). Как ученик вы можете добавлять себе учителей, добавлять выполненые дз(отметить дз выполненным, добавить решённое д/з), добавить отзыв о преподавателе.
# Данные

_<details><summary><h3>Users</h3></summary>_
  <p> 
Все пользователи платформы

| Название атрибута | Тип | Ограничения | PR | Внешний ключ для |
| ------------------ | :---: | :-----------: | :--: | :----------------: |
| user_id | int64 | >0, not null|  + | TeacherStudentRelation, TeacherSubjects   |
| first_name| string | not null, len>0, len<=32| | | |
| last_name | string | not null, len>0, len<=32| | |
| age | int64 | not null| | |
| email | string | | | |
| role_id | int64 | one of the Roles.role_id | | |
| login | string | not null, len>0, len<32, unique| | |
| password | string |not null | | |
</p>
</details>

_<details><summary><h3>Roles</h3></summary>_
  <p> 
Предметы

| Название атрибута | Тип | Ограничения | PR | Внешний ключ для |
| ------------------ | :---: | :-----------: | :--: | :----------------: |
| role_id | int64 | >0, not null| + | Users |
| name | string | not null, len>0, len<32| | | |
</p>
</details>

_<details><summary><h3>TeacherStudentRelation</h3></summary>_
  <p> 
Отношения учитель-ученик

| Название атрибута | Тип | Ограничения | PR | Внешний ключ для |
| ------------------ | :---: | :-----------: | :--: | :----------------: |
| relation_id | int64 | >0, not null|  + | Homeworks, Ratings |
| teacher_id | int64 | >0, not null, one of the Users.user_id| | | |
| student_id | int64 | >0, not null, one of the Users.user_id| | |
| subject_id | int64 | >0, not null, one of the Subjects.subject_id| | |
| status_id | int64 | >0, not null, one of the StatusOfRelations.status_id | | |
</p>
</details>

_<details><summary><h3>StatusOfRelation</h3></summary>_
  <p> 
Предметы

| Название атрибута | Тип | Ограничения | PR | Внешний ключ для |
| ------------------ | :---: | :-----------: | :--: | :----------------: |
| status_id | int64 | >0, not null|  + | TeacherStudentRelation  |
| name | string | not null, len>0, len<32| | | |
</p>
</details>

_<details><summary><h3>Subjects</h3></summary>_
  <p> 
Предметы

| Название атрибута | Тип | Ограничения | PR | Внешний ключ для |
| ------------------ | :---: | :-----------: | :--: | :----------------: |
| subject_id | int64 | >0, not null|  + | TeacherStudentRelation, TeacherSubjects  |
| name | string | not null, len>0, len<32| | | |
</p>
</details>

_<details><summary><h3>TeacherSubjects</h3></summary>_
  <p> 
Отношения учитель-предмет

| Название атрибута | Тип | Ограничения | PR | Внешний ключ для |
| ------------------ | :---: | :-----------: | :--: | :----------------: |
| relation_id | int64 | >0, not null|  + |  |
| teacher_id | int64 | >0, not null, one of the Users.user_id|   |  |
| subject_id | int64 | >0, not null, one of the Subjects.subject_id|   |  |
</p>
</details>

_<details><summary><h3>Homeworks</h3></summary>_
  <p> 
Домашние задания

| Название атрибута | Тип | Ограничения | PR | Внешний ключ для |
| ------------------ | :---: | :-----------: | :--: | :----------------: |
| homework_id | int64 | >0, not null|  + |  |
| relation_id | int64 | >0, not null, one of the TeacherStudentRelation.relation_id|  |  |
| desciption | string | not null, len>0| | | |
| deadline | DateTime | | | |
| solution_time | DateTime | | | |
| complete | bool | not null| | |
| solution | string | | | |
| comment | string | | | |
</p>
</details>

_<details><summary><h3>Ratings</h3></summary>_
  <p> 
Домашние задания

| Название атрибута | Тип | Ограничения | PR | Внешний ключ для |
| ------------------ | :---: | :-----------: | :--: | :----------------: |
| rating_id | int64 | >0, not null|  + |  |
| value | short | >0, <6, not null|  |  |
| review | string | len>0 | | | |
| relation_id | int64 |>0, not null, one of the TeacherStudentRelation.relation_id | | |
</p>
</details>

![1](https://github.com/GeorgeD615/teach-to-each/blob/main/TeachToEach/teach-to-each(DatabaseScheme).png)


## Общие ограничения целостности
  - Если для поля указан внешний ключ, то должен существовать документ, на который указывает этот ключ
# Пользовательские роли
1. **Неавторизорованный пользователь** - может авторизоваться
2. **Обычный пользователь** - может залогиниться и войти как Учитель или Ученик
3. **Администратор** - может управлять удалять пользовательские аккаунты

1. Учитель
    *  добавлять учеников
    *  добавлять дз своим ученикам 
    *  комментировать выполненное дз
    *  принимать выполненное дз
2. Ученик
    *  отправлять запрос учителю 
    *  отправлять выполненное дз
    *  оставлять отзыв на преподавателя
# UI / API 
??
# Технологии разработки
ASP.NET, EntityFarmework
## Язык программирования
С#, HTML, CSS, JavaScript
## СУБД
PostgreSQL
# Тестирование
??
