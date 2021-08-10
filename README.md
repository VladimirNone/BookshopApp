# BookshopApp
## Использованные технологии
Front-end part. Используется React в связке с Bootstrap. 

Back-end part. Используется Asp.Net Core, Entity Framework Core для БД (а также CodeFirst для манипуляции структуры БД), ASP.NET Core Identity.
## Первый запуск
Перед запуском приложения необходимо создать базу данных. Сделать это можно с помощью Package Manager Console, а именно использовав команду Update-database или использовав альтернативную команду в консоли.

Создав базу данных ее необходимо наполнить, для это был разработан класс DataGenerator. Функционал для автоматической генерации данных был создан, однако необходимо разкомитить строку в следующем блоке кода:

    public static async Task Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        //await SeedDatabase(host);
        host.Run();
    }
  
Данный метод находится в классе Program. В нем можно увидеть создание обычного пользователя и админа, а также логины и пароли для них.

    await IdentityInitializer.InitializeUserAsync(userManager, "admin@gmail.com", "123456", "admin");
    await IdentityInitializer.InitializeUserAsync(userManager, "user@gmail.com", "123456", "user");

Однако стоит обратить внимание на то, что данный метод будет генерировать данные при каждом запуске. Поэтому после нескольких запусков его следует закомитить. 

Помимо этого, необходимо с помощью, к примеру, nmp скачать пакеты для front-end part of app.
## Общее описание программы
Программа разделена на несколько контекстов для того, чтобы каждый можно было использовать в иных продуктах.

Использовался паттерн MVC. Контроллеры находятся в папке Controllers. Models в проекте BookshopApp.Models. Views в папке ClientApp. (несколько остались в папке Pages, однако они предназначены исключительно для Development'a)

Использовался паттерн UnitOfWork в связке с Repository для работы с бд. 

Техническое задание требовало использованием встроенного функционала Submit Form для отправки данных на сервер, это было продемонстрировано на странице ProductCreate. На странице ProductChange также использовался данных функционал, однако в несколько изменной форме: стандартными средвами можно сделать только 'post' and 'get' запросы, однако запросы, изменяющие объект принято отправлять с помощью 'put' запроса, по этой причине пришлось отправить данные полученные из формы с помощью Fetch Api.

Во всем проекте использовался преимущественно Fetch Api для взаимодействия клиентской части приложения с серверной. 
## Иные моменты, о которых стоит рассказать
Не использовал jwt-токены, хотя стоило бы

На стринцах 'Login', 'Logout' пришлось использовать нестандартный Redirect, а именно 

    window.location.replace(`${window.location.origin}/`);
  
Это связанно со структурой приложения. По большей части, в приложении меняется только основная часть (content). При использовании Redirect меню не обновляется. Узнать больше об этой проблеме можно изучив файл App.js.

Возникли проблемы с Dto. Их слишком много, пришлось использовать некоторые по несколько раз. Не считаю это хорошей практикой. Раньше не сталкивался с этой проблемой, но переходить на GraphQL было поздно.

Данные вводимые на страницах Product(Create/Change) никак не контролируются. 
