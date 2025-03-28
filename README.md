# 🧩 Db.All – REST & GraphQL with MongoDB & Couchbase in .NET

Welcome to **Db.All** – a full-stack example showcasing how to build both **REST** and **GraphQL** APIs using **MongoDB** and **Couchbase** in .NET. Whether you're experimenting with modern document databases or testing flexible API layers, this repo is built to help you explore and learn.

## 📦 Projects Included

This solution includes five separate projects:

### 🧱 `Common`
- Shared contracts and models
- Contains `Product.cs` and `IProductService.cs` interface

### 🍃 `Mongo.Service`
- MongoDB-specific implementation
- Includes `MongoDbContext`, `MongoDbSettings`, and `MongoProductService`

### 🔵 `Couchbase.Service`
- Couchbase-specific implementation
- Includes `CouchbaseDbContext`, `CouchbaseSettings`, and `CouchbaseProductService`

### 🌐 `Rest.Api`
- A traditional REST API using ASP.NET Core
- Contains `ProductsController`
- Can use either Mongo or Couchbase based on DI registration

### 🚀 `GraphQL.Api`
- A GraphQL API powered by [HotChocolate](https://chillicream.com/docs/hotchocolate)
- Contains `Query`, `Mutation`, and GraphQL config
- Supports filtering, sorting, pagination

---

## 🛠️ Getting Started

### 1. Clone the Repo
```bash
git clone https://github.com/piyushdoorwar/dotnet-apis-db-connect.git
cd Db.All
```

### 2. Database Setup

#### MongoDB
- Use [MongoDB Atlas](https://www.mongodb.com/cloud/atlas) or local Mongo
- Update `appsettings.json` in `GraphQL.Api` and `Rest.Api`:
```json
"MongoDbSettings": {
  "ConnectionString": "your-mongodb-connection-string",
  "DatabaseName": "YourDatabaseName"
}
```

#### Couchbase
- Use [Couchbase Capella](https://cloud.couchbase.com) or a local cluster
- Update the same `appsettings.json`:
```json
"CouchbaseSettings": {
  "ConnectionString": "couchbases://your-endpoint.cloud.couchbase.com",
  "Username": "your-username",
  "Password": "your-password",
  "Bucket": "products"
}
```

### 3. Switch Between MongoDB and Couchbase
In `Program.cs`, choose which implementation to inject:
```csharp
builder.Services.AddSingleton<IProductService, MongoProductService>();
// or
builder.Services.AddSingleton<IProductService, CouchbaseProductService>();
```

### 4. Run the Projects
You can run any API via Visual Studio or CLI:

#### REST API
```bash
cd Rest.Api
dotnet run
```
- Swagger available at `/swagger`
- Example: `GET /api/products`

#### GraphQL API
```bash
cd GraphQL.Api
dotnet run
```
- GraphQL playground: `https://localhost:5001/graphql`

---

## 💡 Sample Queries

### 🔍 GraphQL – Fetch Products
```graphql
query {
  products(page: 1, pageSize: 5) {
    id
    name
    price
  }
}
```

### ➕ GraphQL – Add Product
```graphql
mutation {
  addProduct(input: {
    id: "5f1a7a5e1d1f362017403501",
    name: "Wireless Mouse",
    price: 29.99,
  }) {
    id
    name
    price
  }
}
```

---

## 💬 Why This Project?

I built this to demonstrate how a common interface can power multiple backends (MongoDB and Couchbase) and be exposed through both REST and GraphQL APIs. It’s an exploration of flexibility, composability, and clean architecture.

---

## 🤝 Contributions

Pull requests are welcome! Open an issue to discuss what you’d like to improve.

---

## 📚 Related Articles

> 👉 [Read the full guide on Medium ](https://medium.com/@piyushdoorwar/a-net-devs-guide-to-mongodb-step-by-step-setup-crud-apis-interview-prep-bca931ac39d5)) 

---

## 📜 License

MIT – use it freely for learning or production.
