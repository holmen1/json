
```
dotnet tool install --global dotnet-ef

dotnet ef migrations add DetailedModelCreate
dotnet ef database update
```

```
$ docker compose up -d
[+] Running 3/3
 ✔ Network json_default         Created                                                                                                                                                            0.1s 
 ✔ Volume "json_postgres_data"  Created                                                                                                                                                            0.0s 
 ✔ Container postgres           Started                                                                                                                                                            0.4s 

$ docker exec -it postgres psql -U sa -d assumptions -c "\l"
                                                  List of databases
    Name     | Owner | Encoding | Locale Provider |  Collate   |   Ctype    | Locale | ICU Rules | Access privileges 
-------------+-------+----------+-----------------+------------+------------+--------+-----------+-------------------
 assumptions | sa    | UTF8     | libc            | en_US.utf8 | en_US.utf8 |        |           | 
 postgres    | sa    | UTF8     | libc            | en_US.utf8 | en_US.utf8 |        |           | 
 template0   | sa    | UTF8     | libc            | en_US.utf8 | en_US.utf8 |        |           | =c/sa            +
             |       |          |                 |            |            |        |           | sa=CTc/sa
 template1   | sa    | UTF8     | libc            | en_US.utf8 | en_US.utf8 |        |           | =c/sa            +
             |       |          |                 |            |            |        |           | sa=CTc/sa

```