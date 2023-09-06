# Система управления проектами

Project Management System — это API проект на чистой архитектуре для занесения данных о сотрудниках и проектах с возможностью: добавления, удаления, обновления и получения данных. Реализована валидация, фильтрация, пагинация, поиск и формирование данных.



## Запуск приложения
Перед запуском приложения необходимо проверить строку подключения и изменить её при необходимости в файле **appsettings.json** в главном проекте **ProjectManagementSystem**.
```json
"ConnectionStrings":{
	"sqlConnection": "Server=(localdb)\\MSSQLLocalDB;Database=ProjectsEmployees;Trusted_Connection=True;"
}
```
Следующим шагом необходимо создать базу данных со сконфигурированными начальными данными.
Для этого есть два способа: 
1. Используем "Package Manager Console". Выбираем **Package Source: nuget.org** и **DefaultProject: ProjectManagementSystem**. далее в консоле вводим команду:
```
Update-Database
```
2. В командной строке заходим в корень главного проекта **ProjectManagementSystem** далее в консоле вводим команду:
```
dotnet ef database update
```
В проекте реализована документация с помощью Swagger, поэтому можно смело его запускать.



## Дальнейшие планы
1. Произвести покрытие интеграционными и юнит тестами.
1. Поместить приложение в Docker контейнер.
1. Добавить аутентификацию и авторизацию.
1. Добавить сущность "Задача".