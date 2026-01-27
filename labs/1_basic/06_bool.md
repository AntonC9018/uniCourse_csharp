# Лабораторная работа 9

- Тема: **Логические выражения и операторы**.
- [Видео (до `if`)](https://www.youtube.com/watch?v=mj9E9BhOAhU&list=PL4sUOB8DjVlVVw9Yx_tUO7fRPDYeaACXD&index=18)

## Концепты

- Логические значения (`true`, `false`)
- Логические выражения
- Операторы сравнения чисел `>`, `<`, `>=`, `<=`, `==`, `!=`
- Логические операторы `&&`, `||`, `==`, `!`, `!=`
- Lazy evaluation операндов операторов `&&` и `||`

## Вопросы на понимание

Что произойдет в программе? Что выведется?

1. ```csharp
   bool a = true;
   Console.WriteLine(a);
   ```
   
   <details>
   <summary>Ответ</summary>
 
   Тип `bool` принимает только два значения: `true` и `false`.  
   При выводе в консоль они преобразуются в текстовую форму.
   </details>

2. ```csharp
   bool a = null;
   ```

   <details>
   <summary>Ответ</summary>
 
   Ошибка компиляции.
 
   `bool` — это тип-значение, не ссылочный тип.  
   Он не может содержать `null`.  
   </details>



3. ```csharp
   bool a = 1 == 2;
   Console.WriteLine(a);
   ```

   <details>
   <summary>Ответ</summary>
 
   Оператор `==` применяется к 2 выражениям и возвращает результат проверки на равенство, как логическое значение.
   Результат `false`.
   </details>


4. ```csharp
   int x = 3;
   int y = 4;
   bool b = x == y;
   Console.WriteLine(b);
   ```

   <details>
   <summary>Ответ</summary>
 
   Здесь, оператор работает не с числами напрямую, а со значениями переменных.
   При выполнении, вместо `x` как бы вставится значение из `x` (как с выражениями), и так далее.
   </details>
  
4. ```csharp
   int x = 3;
   int y = 4;
   bool b = x * 2 == y + 4;
   Console.WriteLine(b);
   ```

   <details>
   <summary>Ответ</summary>
 
   Пример использования более сложных выражений как операнд.
 
   `bool b = x * 2 == y + 4` воспринимается как `bool b = ((x * 2) == (y + 4))`.
   Дальше вычисления происходят согласно правилам выражений.
   </details>


5. ```csharp
   bool a = 1 > 2;
   a = 3 == 3;
   Console.WriteLine(a);
   ```

   <details>
   <summary>Ответ</summary>
 
   Здесь, `a` перезапишется с `false` на другое значение (`true`).
   </details>


6. ```csharp
   bool a = true;
   F(a);

   static void F(bool x)
   {
       Console.WriteLine(x);
   }
   ```

7. ```csharp
   F(5 > 3);
   
   static void F(bool flag)
   {
       Console.WriteLine(flag);
   }
   ```

   <details>
   <summary>Ответ</summary>
 
   Аргумент `5 > 3` вычисляется перед вызовом функции.  
   В `flag` попадет результат `true`.
   </details>

8. ```csharp
   bool result = F();
   Console.WriteLine(result);
   
   static bool F()
   {
       return true;
   }
   ```

9. ```csharp
   bool result = IsGreater(5, 3);
   Console.WriteLine(result);
   
   static bool IsGreater(int a, int b)
   {
       return a > b;
   }
   ```

10. ```csharp
    bool a = true;
    bool b = false;
    bool c = a == b;
    Console.WriteLine(c);
    ```
 
    <details>
    <summary>Ответ</summary>
  
    Оператор равенства `==` можно применять к выражениям типа `bool`.
    Сравнение `true == false` даёт `false`.
    </details>


1. ```csharp
   bool a = false;
   bool b = !a;
   Console.WriteLine(b);
   ```

   <details>
   <summary>Ответ</summary>
  
   Оператор `!` делает из `false` `true` (и наоборот).
   </details>

2. ```csharp
   bool a = true;
   bool b = false;
   bool c = a && b;
   Console.WriteLine(c);
   ```

   <details>
   <summary>Ответ</summary>

   `a && b` -> `true && false` -> `false`, потому что оба операнда должны быть `true`.
   </details>

3. ```csharp
   bool result = A() && B();
    
   static bool A()
   {
       Console.WriteLine("A");
       return true;
   }
    
   static bool B()
   {
       Console.WriteLine("B");
       return true;
   }
   ```

   <details>
   <summary>Ответ</summary>

   Для того, чтобы удостоверится, что `A()` и `B()` оба вернут `true`, программе необходимо их обоих вызвать.

   ```
   A
   B
   ```
   </details>

4. ```csharp
   bool result = A() && B();
    
   static bool A()
   {
       Console.WriteLine("A");
       return true;
   }
    
   static bool B()
   {
       Console.WriteLine("B");
       return false;
   }
   ```

   <details>
   <summary>Ответ</summary>
  
   Первая функция возвращает `true`, поэтому вторая тоже выполняется.  
   Вторая вернет `false`, и результат выражения будет `A() && B()` -> `true && false` -> `false`.

   ```
   A
   B
   ```
   </details>

5. ```csharp
   bool result = A() && B();

   static bool A()
   {
       Console.WriteLine("A");
       return false;
   }

   static bool B()
   {
       Console.WriteLine("B");
       return true;
   }
   ```

   <details>
   <summary>Ответ</summary>

   При `&&`, если первый операнд `false`, второй не вычисляется.  
   Это называется lazy evaluation (ленивое вычисление).

   ```
   A
   ```
   </details>

6. ```csharp
   bool result = A() || B();

   static bool A()
   {
       Console.WriteLine("A");
       return true;
   }

   static bool B()
   {
       Console.WriteLine("B");
       return true;
   }
   ```

   <details>
   <summary>Ответ</summary>

   При `||`, если первый операнд `true`, второй не вычисляется.  
   Это тоже ленивое вычисление.
    
   ```
   A
   ```
   </details>
