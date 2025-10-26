# Лабораторная работа 10

- Тема: **Flow Control**.
- [Видео](https://www.youtube.com/watch?v=LLfU1a7pTxg&list=PL4sUOB8DjVlVVw9Yx_tUO7fRPDYeaACXD&index=15)

## Концепты

- `if`
- `else`
- `else if`
- `while (true)`
- `continue`
- `break`
- `do ... while`

## Примеры на понимание

1. ```csharp
   if (true)
   {
       Console.WriteLine("Hello");
   }
   ```

   <details>
   <summary>Ответ</summary>

   `true` в условии заставляет тело `if`-а выполняться всегда.
   </details>

1. ```csharp
   if (false)
   {
       Console.WriteLine("Hello");
   }
   ```

   <details>
   <summary>Ответ</summary>

   При копмиляции, данный код покажет warning, что код является *unreachable*.
   Unreachable означает, что этот код гарантировано никогда не выполнится.
   </details>


1. ```csharp
   bool execute = true;
   if (execute)
   {
       Console.WriteLine("Hello");
   }

   bool notExecute = !execute;
   if (notExecute)
   {
       Console.WriteLine("Not executed");
   }
   ```

   <details>
   <summary>Ответ</summary>

   Здесь, warning-а при компиляции не будет.

   При запуске, выведется только Hello.
   </details>

1. В чем *функциональное* и *семантическое* различие функций `F1`, `F2`, `F3`?
   ```csharp
   static void F1()
   {
       if (A())
       {
           if (B())
           {
               Console.WriteLine("Hello");
           }
       }
   }

   static void F2()
   {
       if (A() && B())
       {
           Console.WriteLine("Hello");
       }
   }

   static void F3()
   {
       bool a = A();
       bool b = B();
       bool ok = a && b;
       if (ok)
       {
           Console.WriteLine("Hello");
       }
   }

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
   <summary>Что значит <i>функциональное</i> и <i>семантическое</i>?</summary>

   Разница может быть:
   - эстетическая — одно субъективно выглядит красивее другого;
   - синтактическая — функции записаны, используя различный код, но совершают то же действие;
   - семантическая — различие в том, какие именно операции используются, как достигается цель;
   - функциональная — функции производят разный видимый результат (печать в консоль, возвращаемое значение).
   - нефункциональная — например, малозаметная разница в скорости выполнения.
   </details>

   <details>
   <summary>Ответ</summary>

   `F1` и `F2` семантически и функционально идентичны:
   при их выполнении выполнятся те же самые действия, и в консоль напишется `A` и `B`.

   `F3` функционально не отличается от `F1` и `F2`, потому что результат выполнения будет тот же,
   но есть семантическое отличие, поскольку ранее `B` бы не выполнилась, если `A` бы вернула `false`,
   из-за механизма работы вложенного `if` и `&&`.

   Можно сделать `F3` функционально различной от `F1` и `F2`, если вернуть `false` из `A`.
   В этом случае, `B` не напечатается.
   </details>

1. ```csharp
   if (1)
   {
       Console.WriteLine("Hello");
   }
   ```

   <details>
   <summary>Ответ</summary>

   `if` применим только на выражения типа `bool` (или неявно конвертимые в `bool`), 
   тогда как `int` — это числовой тип.

   Данный код не скомпилируется.
   </details>

1. ```csharp
   if (false)
   {
       Console.WriteLine("A");
       Console.WriteLine("B");
   }
   ```

   <details>
   <summary>Ответ</summary>

   Ничего не напечатается.
   </details>

1. ```csharp
   if (false)
       Console.WriteLine("A");
       Console.WriteLine("B");
   ```

   <details>
   <summary>Ответ</summary>

   Напечатается только `B`, потому что лишь инструкция `A` прикреплена к `if`-у.

   Эквивалентный код:
   ```csharp
   if (false)
   {
       Console.WriteLine("A");
   }
   Console.WriteLine("B");
   ```
   </details>

1. ```csharp
   if (false)
   {
       Console.WriteLine("A");
   }
   else
   {
       Console.WriteLine("B");
   }
   ```

   <details>
   <summary>Ответ</summary>

   Данный код выведет "B", поскольку блок `else` выполняется тогда, когда не выполняется условие.
   </details>

1. ```csharp
   bool a = true;
   if (a)
   {
       a = false;
   }
   else
   {
       Console.WriteLine("B");
   }
   ```

   <details>
   <summary>Ответ</summary>

   "B" не напечатается, потому что то, выполнится ли `else`, определяется на момент проверки `a` в `if`,
   а это происходит до его изменения.
   </details>

1. ```csharp
   F();

   static void F()
   {
       if (true)
       {
           return;
       }
       else
       {
           Console.WriteLine("B");
       }
   }
   ```

   <details>
   <summary>Ответ</summary>

   Здесь "B" не напечатается. То, выполнится ли блок `else`, зависит лишь от условия в `if`.
   </details>

1. ```csharp
   if (true)
       Console.WriteLine("A");
   else
       Console.WriteLine("B");
   Console.WriteLine("C");
   ```

   <details>
   <summary>Ответ</summary>

   "B" не напечатается. Напечатаются "A" и "C".
   </details>

1. ```csharp
   if (true)
       Console.WriteLine("A");
   else
   {
       Console.WriteLine("B");
   }
   ```

   <details>
   <summary>Ответ</summary>

   Допустимо комбинировать прилепление инструкции и явный блок.
   </details>

1. Как обычно записывают данный код, используя цепочку `if`-`else`?
   ```csharp
   if (a)
   {
       Console.WriteLine("A");
   }
   else
   {
       if (b)
       {
           Console.WriteLine("B");
       }
       else
       {
           if (c)
           {
               Console.WriteLine("C");
           }
       }
   }
   ```

   <details>
   <summary>Ответ</summary>

   `if` обычно цепляют как инструкцию, сопровождающую `else`:

   ```csharp
   if (a)
   {
       Console.WriteLine("A");
   }
   else if (b)
   {
       Console.WriteLine("B");
   }
   else if (c)
   {
       Console.WriteLine("C");
   }
   </details>

1. Попытайтесь представить данный код как цепочку `if`-`else`, семантически ему идентичную.
   Как сделать этот код через early return / guard clause?

   ```csharp
   if (a)
   {
       Console.WriteLine("A");
   }
   else
   {
       Console.WriteLine("After A");

       if (b)
       {
           Console.WriteLine("B");
       }
       else
       {
           Console.WriteLine("After B");

           if (c)
           {
               Console.WriteLine("C");
           }
           else
           {
               Console.WriteLine("After C");
           }
       }
   }
   ```

   <details>
   <summary>Ответ (цепочка)</summary>

   Этот код невозможно представить как цепочку.
   Некуда поставить "After B" и "After C" так, чтобы они выполнялись по тем же правилам.
   Можно попробовать их продублировать, но тогда они не будут семантически эквивалентны:

   ```csharp
   if (a)
   {
       Console.WriteLine("A");
   }
   else if (b)
   {
       Console.WriteLine("After A");
       Console.WriteLine("B");
   }
   else if (c)
   {
       Console.WriteLine("After A");
       Console.WriteLine("After B");
       Console.WriteLine("C");
   }
   else
   {
       Console.WriteLine("After A");
       Console.WriteLine("After B");
       Console.WriteLine("After C");
   }
   ```
   </details>

   <details>
   <summary>Ответ (guard clause / early return)</summary>

   1. Создается функция для этого кусочка кода;
   2. Внутри каждого `if` прописывается `return`;
   3. `else` и блоки пропадают.

   ```csharp
   F(a, b, c);

   static void F(bool a, bool b, bool c)
   {
       if (a)
       {
           Console.WriteLine("A");
           return;
       }
       Console.WriteLine("After A");

       if (b)
       {
           Console.WriteLine("B");
           return;
       }
       Console.WriteLine("After B");

       if (c)
       {
           Console.WriteLine("C");
           return;
       }
       Console.WriteLine("After C");
   }
   ```
   </details>

   <details>
   <summary>Зачем этот guard clause / early return?</summary>

   - Чтобы поднять обработку ошибок вверх функции, а основную логику опустить вниз.
     Это делает очевидным тот факт, что логика зависит от корректности данных,
     которая проверялась на момент обработки ошибок (контракт).
   - Убирает лишнюю вложенность условий;
   - Способствует локальности кода проверки ошибки и ее обработки.

   Пример кода без применения guard clause / early return:
   ```csharp
   static void SendWelcomeEmail(User user)
   {
       // Условия перечислены с увеличением вложенности.
       if (user != null)
       {
           if (user.IsActive)
           {
               if (user.EmailConfirmed)
               {
                   // Код с самим действием спрятан в середине функции.
                   Console.WriteLine($"Sending email to {user.Email}");
               }
               else
               {
                   Console.WriteLine("Email not confirmed.");
               }
           }
           else
           {
               Console.WriteLine("User is not active.");
           }
       }
       // Не соблюдается локальность:
       // обработка удалена в исходном коде от связанной проверки.
       else
       {
           Console.WriteLine("User not found.");
       }
   }
   ```

   Тот же код, с его применением:
   ```csharp
   static void SendWelcomeEmail(User user)
   {
       // Можно блоком разграничить контракт 
       // (необходимые условия для выполнения основного действия), 
       // или вынести его в свою функцию.
       {
           // Соблюдена локальность: условия рядом с их обработкой.
           if (user == null)
           {
               Console.WriteLine("User not found.");
               return;
           }
       
           if (!user.IsActive)
           {
               Console.WriteLine("User is not active.");
               return;
           }
       
           if (!user.EmailConfirmed)
           {
               Console.WriteLine("Email not confirmed.");
               return;
           }
       }
   
       // Основной код находится после всех проверок, а не в середине.
       Console.WriteLine($"Sending email to {user.Email}");
   }
   ```
   </details>

1. Выполните в голове данный код:
   ```csharp
   int i = 0;
   while (true)
   {
      if (i == 4)
      {
          Console.WriteLine("ERROR: Should not happen");
          break;
      }
      if (i == 3)
      {
          Console.WriteLine("Exit");
          break;
      }
      if (i == 0)
      {
          Console.WriteLine("Increase by 2 on first iter");
          i += 2;
          continue;
      }

      Console.WriteLine("Increase by 1 normally");
      i++;

      // Implicit continue.
      // continue;
   }
   ```
   
   <details>
   <summary>Что делают <code>break</code> и <code>continue</code></summary>

   `break` прекращает выполнение цикла (переходит на первую инструкцию после цикла).

   `continue` переходит в начало цикла (дальнейшие инструкции из тела цикла не выполняются для этой итерации).
   </details>

   <details>
   <summary>Ответ</summary>

   "Increase by 2 on first iter" напечатается в первой итерации цикла, `i++` не выполнится из-за `continue`.

   "Increase by 1 normally" напечатается во второй итерации цикла, 
   после прохождения с неудачей всех проверок `if`-ов.

   "Exit" напечатается в третьей итерации, тогда как проверка `i == 0` и инструкция `i++` не выполнится,
   поскольку `break` прервет выполнение цикла.

   ```
   Increase by 2 on first iter
   Increase by 1 normally
   Exit
   ```
   </details>

1. Что вернет эта функция, если ее вызвать?
   ```csharp
   static int F()
   {
       while (true)
       {
          if (true)
          {
              return 0;
          }
          break;
       }
       return 1;
   }
   ```
   
   <details>
   <summary>Ответ</summary>

   При выполнении `return 0`, прерывается не только цикл, 
   но и последующее выполнение оставшегося кода функции.

   `break` и `return 1` никогда не выполнятся.
   </details>
   
