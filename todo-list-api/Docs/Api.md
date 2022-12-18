# Todo List API

- [Buber Breakfast API](#buber-breakfast-api)
  - [Create Breakfast](#create-breakfast)
    - [Create Breakfast Request](#create-breakfast-request)
    - [Create Breakfast Response](#create-breakfast-response)
  - [Get Breakfast](#get-breakfast)
    - [Get Breakfast Request](#get-breakfast-request)
    - [Get Breakfast Response](#get-breakfast-response)
  - [Update Breakfast](#update-breakfast)
    - [Update Breakfast Request](#update-breakfast-request)
    - [Update Breakfast Response](#update-breakfast-response)
  - [Delete Breakfast](#delete-breakfast)
    - [Delete Breakfast Request](#delete-breakfast-request)
    - [Delete Breakfast Response](#delete-breakfast-response)

## Create Breakfast

### Create Breakfast Request

```js
POST /todo
```

```json
{
    "name": "Ajouter un nouveau service",
    "description": "Abstraire les données de la classe via un service...",
    "startDateTime": "2022-04-08T08:00:00",
    "endDateTime": "2022-04-08T11:00:00",
}
```

### Create Breakfast Response

```js
201 Created
```

```yml
Location: {{host}}/todo/{{id}}
```

```json
{
    "id": "00000000-0000-0000-0000-000000000000",
    "name": "Ajouter un nouveau service",
    "description": "Abstraire les données de la classe via un service...",
    "startDateTime": "2022-04-08T08:00:00",
    "endDateTime": "2022-04-08T11:00:00",
    "lastModifiedDateTime": "2022-04-06T12:00:00",
}
```

## Get Breakfast

### Get Breakfast Request

```js
GET /todo/{{id}}
```

### Get Breakfast Response

```js
200 Ok
```

```json
{
    "id": "00000000-0000-0000-0000-000000000000",
    "name": "Ajouter un nouveau service",
    "description": "Abstraire les données de la classe via un service...",
    "startDateTime": "2022-04-08T08:00:00",
    "endDateTime": "2022-04-08T11:00:00",
    "lastModifiedDateTime": "2022-04-06T12:00:00",
}
```

## Update Breakfast

### Update Breakfast Request

```js
PUT /todo/{{id}}
```

```json
{
    "name": "Ajouter un nouveau service",
    "description": "Abstraire les données de la classe via un service...",
    "startDateTime": "2022-04-08T08:00:00",
    "endDateTime": "2022-04-08T11:00:00",
}
```

### Update Breakfast Response

```js
204 No Content
```

or

```js
201 Created
```

```yml
Location: {{host}}/todo/{{id}}
```

## Delete Breakfast

### Delete Breakfast Request

```js
DELETE /todo/{{id}}
```

### Delete Breakfast Response

```js
204 No Content
```