# Payment API with JWT
## API ��� ������ � ���������� ���������

### ����� ���� ���� �� ���� ������������, ������ � ������:

### ��������� (�� ������ �������� � ����):
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

### �����:
+ https://github.com/payneteasy/php-library-payneteasy-api
+ https://doc.payneteasy.com/
+ https://acdn.tinkoff.ru/static/documents/merchant_api_protocoI_e2c_v2.pdf
+ https://acdn.tinkoff.ru/static/documents/merchant_api_protocoI_eacq_atop.pdf

�������� �����������, ��� ����������, ����� �� ������� � ������ IP � ������ � ������������. 
����������� �� ������ ����� ������ � application.json (AllowedHosts)

### TODO: 
1) �������� ���� (�� ����������� ��������)
2) ��������� ����������� �� ������, �������������� �� �� MainApp (����������� ����� �� ClientCabinet, ����������� �������� �� ClientCabinet, SuccessUrl �� colibridengi.ru/cabinet, FailUrl paymentapi/fail)
    > ��������� �� � CallbackController'�� (������� ��������)
3) ������� ���-�� �����-���� (����������)
4) ��������� �� ������� ������ � ���� ������ (����� �����-�� ��� ����������, ��), ��� ������ ����� ����� �����, ����� ���� ������������� � ������ ������� 
      �������� ����� ���� �����:
      > 1) ���������� ����� �� ������������� ����� (�������� � Middleware)
      > 2) ��������� ��� ������� � ����,
      > 3) ���� ��� ������ �� ���������� 400
5) �������� ���������� ����� ������������� ��� ������������� ���� (�������� � �������� ������� ��� �������������).
6) �������� ���������� ��� ������ ������ IP




