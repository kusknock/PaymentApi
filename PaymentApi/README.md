# Payment API with JWT
## API для работы с платежными сервисами

### Много чего брал из этих репозиториев, уроков и статей:

### Источники (по поводу хранения в базе):
+ https://stackoverflow.com/questions/42763146/does-it-make-sense-to-store-jwt-in-a-database
+ https://betterprogramming.pub/should-we-store-tokens-in-db-af30212b7f22

### JWT:
+ https://www.youtube.com/watch?v=eVxzuOxWEiY
+ https://www.youtube.com/watch?v=bWA-pZJrOFE
+ https://github.com/codingdroplets/JwtDotNetCoreDemo/
+ https://github.com/Kraval/aspnetcore-webapi-jwt
+ https://medium.com/c-sharp-progarmming/asp-net-core-5-jwt-authentication-tutorial-with-example-api-aa59e80d02da
+ https://medium.com/geekculture/how-to-add-jwt-authentication-to-an-asp-net-core-api-84e469e9f019
+ https://habr.com/ru/post/468401/

### Банки:
+ https://github.com/payneteasy/php-library-payneteasy-api
+ https://doc.payneteasy.com/
+ https://acdn.tinkoff.ru/static/documents/merchant_api_protocoI_e2c_v2.pdf
+ https://acdn.tinkoff.ru/static/documents/merchant_api_protocoI_eacq_atop.pdf

Текущего функционала, уже достаточно, чтобы не пускать с левыми IP в методы с авторизацией. 
Ограничения по хостам можно задать в application.json (AllowedHosts)

### TODO: 
1) Добавить логи (по объективным причинам)
2) Направить нотификации на сервис, перенаправлять их на MainApp (нотификации оплат на ClientCabinet, нотификации привязок на ClientCabinet, SuccessUrl на colibridengi.ru/cabinet, FailUrl paymentapi/fail)
    > Обработка их в CallbackController'ах (сделать грамотно)
3) Встроит как-то Альфа-Банк (обсуждаемо)
4) Проверять на наличие токена в базе данных (может какой-то хэш сравнивать, хз), это скорее всего будет нужно, когда есть необходимость в отзыве токенов 
      Алгоритм может быть такой:
      > 1) Валидируем токен по реализованной схеме (делается в Middleware)
      > 2) Проверяем его наличие в базе,
      > 3) Если нет токена то возвращаем 400
5) Добавить контроллер ролей пользователей для разграничения прав (удаления и создания токенов для пользователей).
6) Добавить контроллер для белого списка IP




