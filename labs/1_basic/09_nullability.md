# Лабораторная работа 9

- Тема: **Nullability**.
- [Видео](https://www.youtube.com/watch?v=zeuNRQBdwuY&list=PL4sUOB8DjVlVVw9Yx_tUO7fRPDYeaACXD&index=7), 

## Вопросы

- Что такое `null`?
  <details>
  <summary>Ответ</summary>
  
  `null` - это значение, указывающее на отсутствие ссылки (адреса памяти).
  Хранится в памяти как 0.
  </details>

- Почему тип `int` не позволяет значение `null`?
  <details>
  <summary>Ответ</summary>
  
  `int` - это целочисленный тип, а не ссылочный тип.
  0 как `int` это число 0, а не пустая ссылка.
  </details>

- Что означает `string?`?
  <details>
  <summary>Ответ</summary>
  
  `string` сам по себе означает ссылку на строку.
  
  `string?` также допускает значение `null`.
  </details>

- Почему `int?` и `string?` работают по-разному? Как вообще работает `int?`?
  <details>
  <summary>Это не объяснял более подробно</summary>
  
  Я в видео не объяснял, но `?` на самом деле можно применять и к типам-значение.
  Тогда они тоже принимают `null`, как одно из значений.
  На самом деле, это работает используя структуру `Nullable<int>`,
  которая имеет сам `int`, а так же `bool` флаг, который определяет,
  если значение `null` или нет.
  
  В случае со `string?`, тип `Nullable<string>` не используется, поскольку
  `string` это ссылочный тип, для которого уже существует специальное значение,
  а именно нулевой адрес, указывающий отсутствие значения.
  </details>

- Что делает оператор `!`? 
  <details>
  <summary>Ответ</summary>
  
  `!` позволяет присвоить результат выражения, который возмножно `null`,
  переменной, которая не поддерживает `null`.
  Пример:
  ```
  static string? MaybeString(int num)
  {
      if (num == 0)
      {
          return "Hello";
      }
      else
      {
          return null;
      }
  }
  
  // Мы знаем, что функция по факту вернет не `null`, 
  // но компилятор это не способен определить.
  string s = MaybeString(0)!;
  ```
  </details>

- Приведите пример, как с помощью оператора `!` можно обойти гарантии nullability компилятора.  

  <details>
  <summary>Пример</summary>
  
  ```
  static string? MaybeString(int num)
  {
      if (num == 0)
      {
          return "Hello";
      }
      else
      {
          return null;
      }
  }
  
  // Функция вернет `null`, который мы без ошибок сохраняем в переменную,
  // тип которой не поддерживает `null`.
  string s = MaybeString(1)!;
  ```
  </details>

  <details>
  <summary>Еще пример</summary>
  
  ```
  string s = null!;
  ```
  </details>


- Чего позволяет достичь `required`?
  <details>
  <summary>Ответ</summary>
  
  `required` позволяет гарантировать, 
  что гарантии nullability компилятора будут всегда соблюдены.
  </details>

## Задание

Убедитесь, что nullability включен в вашей программе из предыдущей лабы.

<details>
<summary>Как?</summary>

Файл проекта должен содержать строчку:
```xml
<Nullable>enable</Nullable>
```
</details>

Добавьте также следующую строчку, чтобы 
вы не могли скомпилировать программу за наличием каких бы то ни было проблем в ней.
Рекомендую никогда не закрывать глаза на предупреждения от компилятора,
лог при компилировании оставляйте максимально чистым.
```xml
<TreatWarningsAsErrors>true</TreatWarningsAsErrors>
```

<details>
<summary>Пример файла проекта с этой настройкой:</summary>

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net9.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <TreatWarningsAsErrors>true</TreatWarningsAsErrors>
    <Nullable>enable</Nullable>
  </PropertyGroup>
</Project>
```
</details>

Исправьте проблемы, появившиеся в программе, если были таковые.
