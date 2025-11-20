# Создание DI контейнера

Сделайте реализацию DI контейнера:

1. Создание "сырой" реализации, не привязанной к конструкторам, генерикам и рефлексии.
   Для начала реализуйте вариант, не реализуя lifetime-ы объектов.

   <details>
   <summary>Подробнее</summary>
   
   Цель - получить реестр, используя который можно будет создавать объекты, 
   автоматически передавая им и при необходимости создавая их зависимости.
   
   Например, если есть вот такая схема:
   ```csharp
   sealed class DependencyOfDependency
   {
   }
   
   interface IDependency1
   
   sealed class Dependency : IDependency1
   {
       public required DependencyOfDependency Dep { get; init; }
   }
   
   interface Service
   {
       void Print();
   }
   
   sealed class Implementation : Service
   {
       public required IDependency1 Dep { get; init; }
       public void Print()
       {
           Console.WriteLine("Hello");
       }
   }
   ```
   
   DI container будет позволять создание сходу `Implementation`, создавая зависимости автоматически.
   Один из возможных подходов:
   ```csharp
   var services = new DIContainer();
   services.Add(
       serviceType: typeof(DependencyOfDependency),
       dependencies: [],
       factory: static (ReadOnlySpan<object> dependencies) => new DependencyOfDependency());
   services.Add(
       serviceType: typeof(IDependency1),
       dependencies: [typeof(DependencyOfDependency)],
       factory: static (ReadOnlySpan<object> dependencies) => new Dependency
       {
           Dep = (DependencyOfDependency) dependencies[0],
       });
   services.Add(
       serviceType: typeof(Service),
       dependencies: [typeof(IDependency1)],
       factory: static (ReadOnlySpan<object> dependencies) => new Implementation
       {
           Dep = (IDependency1) dependencies[0],
       });
   
   var service = (Service) services.Create(typeof(Service));
   service.Print(); // Hello
   ```
   </details>

2. Создание вспомогательных generic оберток для функций.

   <details>
   <summary>Пример интерфейса</summary>
   
   Предыдущий пример с generic обертками:
   ```csharp
   var services = new DIContainer();
   services.Add<DependencyOfDependency, DependencyOfDependency>(
       dependencies: [],
       factory: static (ReadOnlySpan<object> dependencies) => new());
   services.Add<IDependency1, Dependency>(
       dependencies: [typeof(DependencyOfDependency)],
       factory: static (ReadOnlySpan<object> dependencies) => new()
       {
           Dep = (DependencyOfDependency) dependencies[0],
       });
   services.Add<Service, Implementation>(
       dependencies: [typeof(IDependency1)],
       factory: static (ReadOnlySpan<object> dependencies) => new()
       {
           Dep = (IDependency1) dependencies[0],
       });
   
   Service service = services.Create<Service>();
   service.Print(); // Hello
   ```
   
   С таким интерфейсом можно пойти на шаг дальше и сделать поддержку перегрузок 
   (в том числе с разным количеством параметров):
   ```csharp
   var services = new DIContainer();
   services.Add<DependencyOfDependency, DependencyOfDependency>(
       static () => new());
   services.Add<IDependency1, Dependency, DependencyOfDependency>(
       static (/*DependencyOfDependency*/ dep) => new()
       {
           Dep = dep,
       });
   services.Add<Service, Implementation, IDependency1>(
       static (/*IDependency1*/ dep) => new()
       {
           Dep = dep,
       });
   
   Service service = services.Create<Service>();
   service.Print(); // Hello
   ```
   </details>

3. Использование рефлексии на одном из уровней:
   
   > Для C# и Java, в других может быть неосуществимо.
   
   <details>
   <summary>На уровне конструктора (классика)</summary>

   Идея в поиске конструктора и его вызова, используя рефлексию (`Activator.CreateInstance`).
   Для этого, в классах нужны будут так же конструкторы:
   ```csharp
   sealed class DependencyOfDependency
   {
   }
   
   sealed class Dependency : IDependency1
   {
       private readonly DependencyOfDependency _dep;
       public Dependency(DependencyOfDependency dep)
       {
           _dep = dep;
       }
   }
   
   sealed class Implementation : Service
   {
       private readonly IDependency1 _dep;
       public Dependency(IDependency1 dep)
       {
           _dep = dep;
       }
       public void Print()
       {
           Console.WriteLine("Hello");
       }
   }
   ```
   
   Методы DI контейнера должны сами определить конструктор, используя рефлексию (`Type.Constructors`).
   
   ```csharp
   var services = new DIContainer();
   services.Add<DependencyOfDependency>();
   services.Add<IDependency1, Dependency>();
   services.Add<Service, Implementation>();
   Service service = services.Create<Service>();
   service.Print(); // Hello
   ```
   </details>

   <details>
   <summary>На уровне фабричных функций (реже)</summary>
   
   Идея, чтобы `Add` принимал `Delegate` и находил его возвращаемый тип и зависимости, используя рефлексию.
   Затем, при создании имплементации, чтобы он вызывал этот `Delegate` используя рефлексию (`DynamicInvoke`),
   или создав обертку, принимающую `ReadOnlySpan<object>`, используя `System.Linq.Expression` и `Compile`.

   ```csharp
   var services = new DIContainer();
   services.Add/*<DependencyOfDependency>*/(static () => new DependencyOfDependency());
   services.Add<IDependency1>(
       static (DependencyOfDependency dep) => new Dependency()
       {
           Dep = dep,
       });
   services.Add<Service>(
       static (IDependency1 dep) => new Implementation()
       {
           Dep = dep,
       });
   
   Service service = services.Create<Service>();
   service.Print(); // Hello
   ```
   
   Сделайте также вспомогательный метод для подобного:

   ```csharp
   var services = new DIContainer();
   services.AddAll<ContainerClass>();
   Service service = services.Create<Service>();
   service.Print(); // Hello
   
   static class ContainerClass
   {
       public static DependencyOfDependency CreateDependencyOfDependency()
       {
           return new();
       }
       
       public static IDependency1 CreateDependency(DependencyOfDependency dep)
       {
           return new Dependency()
           {
               Dep = dep,
           };
       }
         
       public static Service CreateImpl(IDependency1 dep)
       {
           return new Implementation()
           {
               Dep = dep,
           };
       }
   }
   ```
   </details>

   <details>
   <summary>На уровне свойств (реже)</summary>

   Совершите поиск `readonly` свойств, у которых есть публичный сеттер.
   Найдите информацию про `System.Runtime.CompilerServices.IsExternalInit`.
   Возможно так же нужно будет использовать `System.Linq.Expressions` или `Activator.CreateInstance`
   (а может даже `FormatterServices.GetUninitializedObject`, я не пробовал это делать).
   
   Использование:
   ```csharp
   var services = new DIContainer();
   services.Add<DependencyOfDependency>();
   services.Add<IDependency1, Dependency>();
   services.Add<Service, Implementation>();
   Service service = services.Create<Service>();
   service.Print(); // Hello
   ```
   </details>

4. Максимально добавьте валидацию:
   - тип не наследует от типа сервиса? - ошибка;
   - зависимости нет в контейнере при создании? - ошибка;
   - 2 раза зарегистрирован тот же тип сервиса? - ошибка;
   - когда тип запрашивает сам себя напрямую или через другие зависимости (цикличная зависимость) - ошибка;
   - и т.д.

5. Добавьте билдер под создание графа зависимостей. 
   Добавьте валидацию графа зависимостей на циклы и наличие зависимостей 
   (проверка при создании графа, а не при запросе объекта).
   
   ```csharp
   var services = new DIContainerBuilder();
   services.Add<DependencyOfDependency>();
   services.Add<IDependency1, Dependency>();
   services.Add<Service, Implementation>();
   ServiceProvider serviceProvider = services.Build();
   Service service = serviceProvider.Create<Service>();
   service.Print(); // Hello
   ```
   
6. Добавьте (или не убирайте) возможность получать зависимости динамическим образом,
   путем указания `ServiceProvider` как зависимости и использовании его в дальнейшем в коде.

7. Под финальный вариант сделайте unit тесты.

**Дополнительно:**
- Реализация lifetime-ов (transient, singleton).
- Реализация scope-ов и scoped lifetime-а.
- Дополнительные функции для регистрации всех типов в assembly, или типов с определенным аттрибутом.
- Аттрибуты для указания свойств или полей для property injection.
