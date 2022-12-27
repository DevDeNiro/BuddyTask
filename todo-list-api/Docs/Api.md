# Exemple de docs pour l'API 

- [Buber Plants API](#buber-Plants-api)
  - [Create Plants](#create-Plants)
    - [Create Plants Request](#create-Plants-request)
    - [Create Plants Response](#create-Plants-response)
  - [Get Plants](#get-Plants)
    - [Get Plants Request](#get-Plants-request)
    - [Get Plants Response](#get-Plants-response)
  - [Update Plants](#update-Plants)
    - [Update Plants Request](#update-Plants-request)
    - [Update Plants Response](#update-Plants-response)
  - [Delete Plants](#delete-Plants)
    - [Delete Plants Request](#delete-Plants-request)
    - [Delete Plants Response](#delete-Plants-response)

## Create Plants

### Create Plants Request

```js
POST /todo
```

```json
{
    "name": "Monstera Deliciosa",
    "description": "Le monstera deliciosa est une des plantes d'intérieur les plus convoitées pour ses grandes feuilles atypiques.",
    "startDateTime": "2022-04-08T08:00:00",
    "endDateTime": "2022-04-08T11:00:00",
}
```

### Create Plants Response

```js
201 Created
```

```yml
Location: {{host}}/todo/{{id}}
```

```json
{
    "id": "00000000-0000-0000-0000-000000000000",
    "name": "Monstera Deliciosa",
    "description": "Le monstera deliciosa est une des plantes d'intérieur les plus convoitées pour ses grandes feuilles atypiques.",
    "startDateTime": "2022-04-08T08:00:00",
    "endDateTime": "2022-04-08T11:00:00",
    "lastModifiedDateTime": "2022-04-06T12:00:00",
}
```

## Get Plants

### Get Plants Request

```js
GET /todo/{{id}}
```

### Get Plants Response

```js
200 Ok
```

```json
{
    "id": "00000000-0000-0000-0000-000000000000",
    "name": "Monstera Deliciosa",
    "description": "Le monstera deliciosa est une des plantes d'intérieur les plus convoitées pour ses grandes feuilles atypiques.",
    "startDateTime": "2022-04-08T08:00:00",
    "endDateTime": "2022-04-08T11:00:00",
    "lastModifiedDateTime": "2022-04-06T12:00:00",
}
```

## Update Plants

### Update Plants Request

```js
PUT /todo/{{id}}
```

```json
{
    "name": "Monstera Deliciosa",
    "description": "Le monstera deliciosa est une des plantes d'intérieur les plus convoitées pour ses grandes feuilles atypiques.",
    "startDateTime": "2022-04-08T08:00:00",
    "endDateTime": "2022-04-08T11:00:00",
}
```

### Update Plants Response

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

## Delete Plants

### Delete Plants Request

```js
DELETE /todo/{{id}}
```

### Delete Plants Response

```js
204 No Content
```