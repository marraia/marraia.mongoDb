# Utilização do Mongodb em aplicações em .Net Core

Adapter para conexão com o Mongodb  


## Arquivo de configuração - String de conexão

Primeiro passo é adicionar a string de conexão com o mongodb em seu arquivo de configuração **appsettings.json**

```
"MongoSettings": {
    "Connection": "mongodb://192.168.0.18:17017/",
    "Database": "TesteMarraia"
  }
```


## Injetar o uso do MongoDb em sua aplicação

No arquivo Startup.cs de sua aplicação adicione no método **ConfigureServices** o middleware em específico:
```
        public void ConfigureServices(IServiceCollection services)
        {
            ..
            ..
            services.AddMongoDb();
        }
```
## Herança na classe de domínio

Nas classes de domínio que serão collections no MongoDb, realizar a herança da classe **Entity < PrimaryKey >** 
Onde **PrimaryKey**, é o tipo de dado que será a identificação do objeto no MongoDb.

```
public class Person : Entity<Guid>
    {
        public Person(string name, string surname)
        {
            Id = Guid.NewGuid();
            Name = name;
            Surname = surname;
        }


        public string Name { get; private set; }
        public string Surname { get; private set; }
    }
```  

## Herança de Repositório Base
Em seu repositório use a herança da classe **MongoDbRepositoryBase<ClassDomain, PrimaryKey>**.
Onde **ClassDomain** informe sua classe de domínio
Onde **PrimaryKey** informe o tipo da identificação do objeto no MongoDb

````
public class PersonRepository : MongoDbRepositoryBase<Person, Guid>, IPersonRepositorio
{
   public PersonRepositorio(IMongoContext context) 
     : base (context)
   {
           
   }
}
````

Com essa herança, você terá os métodos:
- InsertAsync(ClassDomain)
- UpdateAsync(ClassDomain)
- DeleteAsync(PrimaryKey)
- GetByIdAsync(PrimaryKey)
- GetAllAsync(PrimaryKey)

## Sobrescrita dos métodos base

Caso necessite você poderá fazer a sobrescrita dos métodos da classe base **MongoDbRepositoryBase<ClassDomain, PrimaryKey>**
```
public class PersonRepositorio : MongoDbRepositoryBase<Person, Guid>, IPersonRepositorio
{
	public PersonRepositorio(IMongoContext context) 
		: base (context)
	{
		
	}

	public override async Task InsertAsync(Person entity)
	{
		await Collection
				.InsertOneAsync(entity);
	}

	public override async Task<Person> GetByIdAsync(Guid id)
	{
		return await Collection
					   .AsQueryable()
					   .Where(a => a.Id == id)
					   .FirstOrDefaultAsync();
	}
}
```
Perceba, que para obter o contexto da conexão com o MongoDb, existe a propriedade **Collection**, que já está injetada com a sua classe de domínio.
