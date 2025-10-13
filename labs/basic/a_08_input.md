# Лабораторная работа 8

- Тема: **Ввод данных с консоли**.
- [Видео](https://www.youtube.com/watch?v=SSTFFX5heuY&list=PL4sUOB8DjVlVVw9Yx_tUO7fRPDYeaACXD&index=10)

## Концепты

- Блокирование программы
- Конвертирование из строки в число (парсинг)
- Цикл ввода данных
- `out` параметры
- Валидация (больше про методы будет позже)
- Инструкция `continue`, `if` (будет больше в одной из следующих лаб)

## Вопросы на понимание

Что будет, если пользователь не введет строковое представление числа?

1. ```csharp
   string a = Console.ReadLine()!;
   int b = int.Parse(a);
   Console.WriteLine(b);
   ```
   
2. ```csharp
   string a = Console.ReadLine()!;
   int b;
   bool success = int.TryParse(a, out b);
   Console.WriteLine(success);
   Console.WriteLine(b);
   ```
   
Как работает этот код? Что выведется в консоль?

1. ```csharp
   int a = 0;
   F(a);
   Console.WriteLine(a);
   
   static void F(int a)
   {
       a = 1;
   }
   ```
   
   <details>
   <summary>Ответ</summary>
   
   Здесь при вызове `F` создается локальная переменная `a` во временной памяти,
   существующая только на время вызова `F`.
   Она получает копию значения из локальной переменной `a`.
   </details>
   
2. ```csharp
   int a = 0;
   F(out a);
   Console.WriteLine(a);
   
   static void F(out int b)
   {
       b = 1;
   }
   ```
   
   <details>
   <summary>Ответ</summary>
   
   Здесь `F` передается адрес `a` во временной памяти (еще ее называют "ссылка на `a`") для инициализации.
   `b = 1` вписывает по этому адресу в переменную `a`.
   </details>
   
3. ```csharp
   int a = 0;
   F(out a);
   Console.WriteLine(a);
   
   static void F(out int a)
   {
       a = 1;
   }
   ```
   
   <details>
   <summary>Ответ</summary>
   
   Идентично с предыдущей ситуацией.
   Эти переменные независимы друг от друга, их имена не играют роли.
   </details>

4. ```csharp
   A a;
   F(out a);
   Console.WriteLine(a.f1);
   Console.WriteLine(a.f2);
   
   static void F(out int b)
   {
       b = 1;
   }
   
   struct A
   {
       int f1;
       int f2;
   }
   ```
   
   <details>
   <summary>Ответ</summary>
   
   Этот код не скомпилируется, потому что `F` принимает не ссылки на `A`, а ссылки на `int`.
   </details>

5. ```csharp
   A a;
   F(out a);
   Console.WriteLine(a.f1);
   Console.WriteLine(a.f2);
   
   static void F(out int a)
   {
       a = 1;
   }
   
   struct A
   {
       int f1;
       int f2;
   }
   ```
   
   <details>
   <summary>Ответ</summary>
   
   Та же проблема.
   </details>

6. ```csharp
   A a;
   F(out a);
   Console.WriteLine(a.f1);
   Console.WriteLine(a.f2);
   
   static void F(out A b)
   {
       b = 1;
   }
   
   struct A
   {
       int f1;
       int f2;
   }
   ```
   
   <details>
   <summary>Ответ</summary>
   
   Не скомпилируется, поскольку переменной типа `A` нельзя присвоить значение типа `int`.
   </details>

7. ```csharp
   A a;
   F(out a);
   Console.WriteLine(a.f1);
   Console.WriteLine(a.f2);
   
   static void F(out A b)
   {
       b.f1 = 1;
   }
   
   struct A
   {
       int f1;
       int f2;
   }
   ```
   
   <details>
   <summary>Ответ</summary>
   
   Не скомпилируется, потому что `a` не полностью инициализирован по завершению функции `F`.
   </details>

8. ```csharp
   A a;
   F(out a);
   Console.WriteLine(a.f1);
   Console.WriteLine(a.f2);
   
   static void F(out A b)
   {
       b.f1 = 1;
       b.f2 = 2;
   }
   
   struct A
   {
       int f1;
       int f2;
   }
   ```
   
   <details>
   <summary>Ответ</summary>
   
   Выведется 1 и 2.
   </details>

9. ```csharp
   A a;
   F(out a);
   Console.WriteLine(a.f1);
   Console.WriteLine(a.f2);
   
   static void F(out A b)
   {
       b = new()
       {
           f1 = 1,
           f2 = 2,
       };
   }
   
   struct A
   {
       int f1;
       int f2;
   }
   ```
   
   <details>
   <summary>Ответ</summary>
   
   Выведется 1 и 2.
   </details>

10. ```csharp
    int a = 0;
    F(out a);
    Console.WriteLine(a);
   
    static int F(out int b)
    {
        b = 1;
        return 2;
    }
    ```
   
    <details>
    <summary>Ответ</summary>
   
    Возвращаемое значение никуда не сохраняется.
    Здесь в `a` (переданное как адрес) лишь запишется значение.
    </details>

11. ```csharp
    int a = 0;
    int b = 0;
    b = F(out a);
    Console.WriteLine(a);
    Console.WriteLine(b);
   
    static int F(out int c)
    {
        c = 1;
        return 2;
    }
    ```
   
    <details>
    <summary>Ответ</summary>
   
    `a` будет 1, `b` будет 2.
    </details>

12. ```csharp
    int a = 0;
    int b = 0;
    b = F(out a);
    Console.WriteLine(a);
    Console.WriteLine(b);
   
    static int F(out int c)
    {
        c = 1;
        return 2;
    }
    ```
   
    <details>
    <summary>Ответ</summary>
   
    `a` будет 1, `b` будет 2.
    </details>

13. ```csharp
    int a;
    int b;
    F(out a, out b);
    Console.WriteLine(a);
    Console.WriteLine(b);
   
    static void F(out int c, out int d)
    {
        c = 1;
        d = 2;
    }
    ```
   
    <details>
    <summary>Ответ</summary>
   
    `a` будет 1, `b` будет 2.
    Можно передавать несколько `out` параметров.
    </details>

## Практика

Создайте программу, где вводятся 3 числа больше 2, считается их произведение и выводится на экран.

- Если пользователь вводит не число, или недопустимое значение, запрашивайте снова.
- Используйте бесконечный цикл для ввода корректных чисел.

<details>
<summary>Не знаю с чего начать</summary>

Можно разбить задание на 2 части:
- Ввод 3 чисел;
- Вычисление и вывод ответа.

Ввод 3 чисел - это просто ввод одного числа 3 раза.

О том, как сделать ввод числа, есть в видео.
</details>
