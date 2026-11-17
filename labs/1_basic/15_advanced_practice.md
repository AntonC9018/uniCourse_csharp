---
slug: ru/labs/basic/advanced-practice
---
<!-- course-site-backlink:start -->
[Этот урок на сайте](https://AntonC9018.github.io/uniCourse_csharp/ru/labs/basic/advanced-practice/)
<!-- course-site-backlink:end -->
# Продвинутая практика

> Эта лаба добавлена для практики. После неё предполагается, что вы умеете решать подобные задачи.

## Что нужно развить в данной работе

- Более продвинутое моделирование предметной области;
- Хранение фиксированного количества элементов в массивах, включая массив внутри типа;
- Работа с пустыми местами: пустая ссылка означает отсутствие объекта;
- Изменение объектов через ссылки: изменение видно через любую ссылку на тот же объект;
- Написание маленьких функций, возвращающих `bool` или количество,
  с использованием `if` с ранними возвратами (guard clauses) и `switch` по `enum`;
- Различение предусловий и обычных результатов:
  первые проверяются `Debug.Assert`, вторые возвращаются;
- Моделирование результата операции и планирование мутации как значений;
- Перебор объектов в цикле, подсчёт пустых мест, поиск первого пустого места.

## Предусловия и результаты

Операция может не удаться по двум совершенно разным причинам.

**Нарушенное предусловие** означает, что вызывающий код нарушил контракт:
индекс за пределами массива, `null` там, где его быть не может,
значение `enum`, которое не обрабатывает ни одна ветка.
В программе ошибка, поэтому осмысленного значения для возврата нет.
`Debug.Assert` останавливает программу ровно там, где произошла ошибка.

**Обычный результат** означает, что данные говорят «нет»:
слот уже занят, очередь полна, пиццы нет в холодильнике.
С программой всё в порядке, поэтому функция сообщает, что произошло,
а вызывающий код решает, что делать дальше.

Не превращайте нарушенное предусловие в возвращённый `false`.
Вызывающий код, который проигнорирует результат, продолжит работать
с некорректным состоянием, и ошибка уедет дальше от своего источника.
Не проверяйте через `Debug.Assert` и обычные данные: занятая полка — не ошибка.

| Ситуация | Вид | Реакция |
| --- | --- | --- |
| индекс за пределами массива | нарушенное предусловие | `Debug.Assert` |
| `null` там, где он недопустим | нарушенное предусловие | `Debug.Assert` |
| значение `enum` без подходящей ветки | нарушенное предусловие | `Debug.Assert` |
| слот уже занят | обычный результат | вернуть его |
| очередь полна | обычный результат | вернуть его |
| элемент не найден | обычный результат | вернуть его |

`Debug.Assert` находится в пространстве имён `System.Diagnostics`.

Все решения ниже следуют этому правилу.

## Задачи для практики

- **Книжная полка.** Полка вмещает 5 книг. У каждой книги есть название.
  Положите «Атлас» в слот 2 и уберите книгу из слота 0.
  Посчитайте, сколько слотов пустует.

  ![Полка на пять книг: книга уезжает с места 0, «Атлас» встаёт на место 2, места 1 и 4 пусты](./images/21_bookshelf.svg)

  <details>
  <summary>
  Возможное решение
  </summary>

  Слот полки либо хранит книгу, либо пуст, поэтому тип слота — `Book?`:
  пустой слот — это `null`.
  Сами классы нужны лишь для группировки, как в прошлой работе:

  ```csharp
  class Book
  {
      public string title;
  }

  class Bookshelf
  {
      public Book?[] slots = new Book?[5];
  }
  ```

  Полка и книга — классы, поэтому при передаче параметром они не копируются:
  изменения в слотах видит вызывающий код.

  Допустим, слоты 0 и 3 хранят книги, а остальные пусты.
  Чтобы положить «Атлас» в слот 2, нужны две проверки.
  Индекс должен быть в пределах массива — это контракт с вызывающим кодом,
  поэтому нарушение контракта ловит `Debug.Assert`, а не возвращаемое значение.
  В слоте 2 не должно быть книги — это обычные данные,
  поэтому занятый слот сообщается через `false`.
  В слоте 0 слот занят, поэтому книгу не кладём и ничего не меняем:

  ```csharp
  static bool ShelveBook(Bookshelf shelf, int index, Book book)
  {
      Debug.Assert(index >= 0 && index < shelf.slots.Length);
      if (shelf.slots[index] != null)
      {
          return false;
      }
      shelf.slots[index] = book;
      return true;
  }
  ```

  Взятие книги — зеркальный случай: индекс снова контракт,
  в слоте 0 есть книга, поэтому очищаем его (`null`) и возвращаем `true`;
  пустой слот — это обычные данные, поэтому возвращаем `false`:

  ```csharp
  static bool TakeBook(Bookshelf shelf, int index)
  {
      Debug.Assert(index >= 0 && index < shelf.slots.Length);
      if (shelf.slots[index] == null)
      {
          return false;
      }
      shelf.slots[index] = null;
      return true;
  }
  ```

  Чтобы посчитать пустые, просто обходим каждый слот по очереди в цикле `for`
  и прибавляем 1 за каждый слот без книги:

  ```csharp
  static int EmptyCount(Bookshelf shelf)
  {
      int count = 0;
      for (int i = 0; i < shelf.slots.Length; i++)
      {
          if (shelf.slots[i] == null)
          {
              count++;
          }
      }
      return count;
  }
  ```

  Использование в основной функции:

  ```csharp
  Bookshelf shelf = new();
  shelf.slots[0] = new Book
  {
      title = "Справочник",
  };
  shelf.slots[3] = new Book
  {
      title = "Словарь",
  };
  ShelveBook(shelf, 2, new Book
  {
      title = "Атлас",
  });
  TakeBook(shelf, 0);
  int free = EmptyCount(shelf);
  ```
  </details>

- **Из холодильника в морозилку.** Холодильник вмещает 3 продукта, морозилка вмещает 2 продукта.
  У каждого продукта есть название.
  Переместите пиццу с полки 1 холодильника на полку 0 морозилки.
  Старое место при этом освобождается.

  ![Холодильник с яблоком, пиццей и молоком; морозилка с пустой полкой и мороженым; пицца переезжает на пустую полку морозилки](./images/21_fridge_freezer.svg)

  <details>
  <summary>
  Возможное решение
  </summary>

  Тот же принцип с полками, только контейнеров два: у холодильника 3 полки, у морозилки 2,
  каждая полка либо хранит продукт, либо пуста:

  ```csharp
  class Food
  {
      public string name;
  }

  class Fridge
  {
      public Food?[] shelves = new Food?[3];
  }

  class Freezer
  {
      public Food?[] shelves = new Food?[2];
  }
  ```

  Допустим, на полке 1 холодильника лежит пицца, а полка 0 морозилки пуста.
  Оба индекса — это контракт с вызывающим кодом, поэтому их проверяет `Debug.Assert`.
  Есть ли продукт в источнике и свободна ли цель — это обычные данные,
  поэтому эти проверки возвращают `false`.
  Затем два шага: переносим ссылку на продукт на полку 0 морозилки,
  очищаем полку 1 холодильника.
  С этого момента пицца живёт ровно в одном месте — в этом и состоит идея владения.
  Неудачная проверка данных возвращает `false` и ничего не трогает:

  ```csharp
  static bool MoveFood(Fridge from, int fromIndex, Freezer to, int toIndex)
  {
      Debug.Assert(fromIndex >= 0 && fromIndex < from.shelves.Length);
      Debug.Assert(toIndex >= 0 && toIndex < to.shelves.Length);
      if (from.shelves[fromIndex] == null)
      {
          return false;
      }
      if (to.shelves[toIndex] != null)
      {
          return false;
      }
      to.shelves[toIndex] = from.shelves[fromIndex];
      from.shelves[fromIndex] = null;
      return true;
  }
  ```

  Использование в основной функции:

  ```csharp
  Fridge fridge = new();
  fridge.shelves[1] = new Food
  {
      name = "pizza",
  };
  Freezer freezer = new();
  bool moved = MoveFood(fridge, 1, freezer, 0);
  ```
  </details>

- **Разогрев супа.** Микроволновка либо вмещает одно блюдо, либо пуста.
  У каждого блюда есть название и состояние нагрева: холодное или горячее.
  Положите холодный суп внутрь, разогрейте до горячего состояния, затем достаньте.

  ![Миска холодного супа отправляется в микроволновку и выходит оттуда горячей](./images/21_microwave.svg)

  <details>
  <summary>
  Возможное решение
  </summary>

  Здесь всего один слот — поддон — плюс новый параметр, за которым нужно следить:
  состояние нагрева, которое бывает холодным или горячим:

  ```csharp
  enum Warmth
  {
      Cold,
      Hot,
  }

  class Meal
  {
      public string name;
      public Warmth warmth;
  }

  class Microwave
  {
      public Meal? tray;
  }
  ```

  Поддон изначально пуст. Когда кладём холодный суп внутрь, поддон свободен,
  поэтому сохраняем блюдо и возвращаем `true`;
  при второй попытке поддон уже занят, поэтому возвращаем `false`:

  ```csharp
  static bool PutMeal(Microwave m, Meal meal)
  {
      if (m.tray != null)
      {
          return false;
      }
      m.tray = meal;
      return true;
  }
  ```

  При разогреве внутри есть блюдо, поэтому меняем его состояние на горячее.
  При разогреве пустой микроволновки внутри ничего нет, поэтому возвращаем `false`:

  ```csharp
  static bool HeatMeal(Microwave m)
  {
      if (m.tray == null)
      {
          return false;
      }
      m.tray.warmth = Warmth.Hot;
      return true;
  }
  ```

  При доставании возвращаем горячий суп вызывающему коду,
  очищаем поддон и возвращаем `null`, если поддон пуст:

  ```csharp
  static Meal? TakeMeal(Microwave m)
  {
      if (m.tray == null)
      {
          return null;
      }
      Meal? result = m.tray;
      m.tray = null;
      return result;
  }
  ```

  Использование в основной функции:

  ```csharp
  Microwave mw = new();
  PutMeal(mw, new Meal
  {
      name = "soup",
      warmth = Warmth.Cold,
  });
  HeatMeal(mw);
  Meal? lunch = TakeMeal(mw);
  ```
  </details>

- **Выпить зелье.** У героя есть здоровье и щит.
  Зелье бывает красным, синим или испорченным.
  Дайте герою 1 красное зелье.
  Красное добавляет 20 здоровья, синее добавляет 10 щита, испорченное ничего не делает.

  ![Герой со здоровьем и щитом, а также красное, синее и испорченное зелья, помеченные сердцем, щитом и черепом; выбрано одно красное зелье](./images/21_potions.svg)

  <details>
  <summary>
  Возможное решение
  </summary>

  Вид зелья определяет эффект, поэтому оформляем его как `enum`.
  Герой и зелье — классы, нужные лишь для группировки данных:

  ```csharp
  enum PotionKind
  {
      Red,
      Blue,
      Spoiled,
  }

  class Potion
  {
      public PotionKind kind;
  }

  class Hero
  {
      public int health;
      public int shield;
  }
  ```

  Разберём случай красного зелья для героя с 50 единицами здоровья и 0 щита:
  ветка `switch` для `Red` добавляет 20 здоровья
  и возвращает `true` — теперь у героя 70 здоровья.
  Синее зелье идёт в следующую ветку и добавляет 10 щита.
  Испорченное зелье идёт в последнюю ветку и возвращает `false`,
  не меняя героя.
  Все значения `PotionKind` обработаны, поэтому выход из `switch`
  означает испорченное значение — его ловит `Debug.Assert`:

  ```csharp
  static bool DrinkPotion(Hero hero, Potion potion)
  {
      switch (potion.kind)
      {
          case PotionKind.Red:
              hero.health += 20;
              return true;
          case PotionKind.Blue:
              hero.shield += 10;
              return true;
          case PotionKind.Spoiled:
              return false;
      }
      Debug.Assert(false); // все значения PotionKind обработаны выше
      return false;        // недостижимо
  }
  ```

  Использование в основной функции:

  ```csharp
  Hero hero = new()
  {
      health = 50,
      shield = 0,
  };
  DrinkPotion(hero, new Potion
  {
      kind = PotionKind.Red,
  });
  ```
  </details>

- **Замачивание белья.** Корзина вмещает 3 вещи.
  В прачечной 2 корзины.
  У каждой вещи есть метка и состояние: грязная, замоченная или чистая.
  Достаньте каждую чистую рубашку из корзины 0, затем замочите каждую грязную рубашку в корзине 0.
  Посчитайте, сколько достали и сколько замочили.

  ![Две корзины для белья: в корзине 0 замачиваются грязные рубашки, в корзине 1 одна чистая рубашка и два пустых места](./images/21_laundry.svg)

  <details>
  <summary>
  Возможное решение
  </summary>

  Новая идея здесь — вложенность: `Basket` хранит массив,
  а `Laundry` хранит корзины:

  ```csharp
  enum ClothState
  {
      Dirty,
      Soaked,
      Clean,
  }

  class Cloth
  {
      public string label;
      public ClothState state;
  }

  class Basket
  {
      public Cloth?[] slots = new Cloth?[3];
  }

  class Laundry
  {
      public Basket[] baskets = [new Basket(), new Basket()];
  }
  ```

  Допустим, слоты 0 и 1 корзины 0 хранят грязные рубашки, а слот 2 хранит чистую рубашку.
  Сначала достаём чистые: цикл перебирает слоты по очереди
  и очищает каждый слот с чистой вещью, увеличивая счётчик на 1 (в примере счётчик станет равен 1).
  Затем замачиваем грязные: второй цикл по тем же слотам
  помечает каждую грязную вещь замоченной, увеличивая счётчик на 1 (в примере счётчик дойдёт до 2).
  Пустой слот пропускается в обоих циклах.

  ```csharp
  static int TakeCleanShirts(Basket basket)
  {
      int taken = 0;
      for (int i = 0; i < basket.slots.Length; i++)
      {
          Cloth? cloth = basket.slots[i];
          if (cloth == null)
          {
              continue;
          }
          if (cloth.state != ClothState.Clean)
          {
              continue;
          }
          basket.slots[i] = null;
          taken++;
      }
      return taken;
  }

  static int SoakBasket(Basket basket)
  {
      int soaked = 0;
      for (int i = 0; i < basket.slots.Length; i++)
      {
          Cloth? cloth = basket.slots[i];
          if (cloth == null)
          {
              continue;
          }
          if (cloth.state != ClothState.Dirty)
          {
              continue;
          }
          cloth.state = ClothState.Soaked;
          soaked++;
      }
      return soaked;
  }
  ```

  Использование в основной функции:

  ```csharp
  Laundry laundry = new();
  laundry.baskets[0].slots[0] = new Cloth
  {
      label = "shirt",
      state = ClothState.Dirty,
  };
  laundry.baskets[0].slots[1] = new Cloth
  {
      label = "shirt",
      state = ClothState.Dirty,
  };
  laundry.baskets[0].slots[2] = new Cloth
  {
      label = "shirt",
      state = ClothState.Clean,
  };
  int taken = TakeCleanShirts(laundry.baskets[0]);
  int soaked = SoakBasket(laundry.baskets[0]);
  ```
  </details>

- **Тюнинг машины.** Гараж вмещает 4 машины. У каждой машины есть модель, мощность и цена.
  Тюнингуйте машину 2: увеличьте мощность на 10, а цену — на 1000 за каждую добавленную единицу мощности.

  ![Четыре машины в гараже: машина 2 (Supra) получает +10 к мощности при цене +$1000 за единицу мощности, а итоговые мощность и цена неизвестны](./images/21_tune_car.svg)

- **Мойка машин.** Мойка вмещает 3 машины. Каждая машина бывает чистой или грязной.
  Помойте каждую грязную машину. Посчитайте, сколько было помыто.

  ![Три поста автомойки с двумя грязными машинами под струями воды, одной чистой машиной и неизвестным числом помытых машин](./images/21_wash_cars.svg)

- **Парковка.** На парковке 2 этажа по 4 места.
  Каждое место либо хранит машину, либо пусто.
  Поставьте новую машину на первое пустое место.

  ![Двухэтажная парковка: места просматриваются по индексам от 0 до 5, и новая машина встаёт на первое пустое место 5](./images/21_parking_lot.svg)

- **Двигатель машины.** В гараже 4 машины. У каждой машины свой двигатель. У двигателя есть износ от 0 до 100.
  Проверьте двигатель машины 1 и отремонтируйте его до износа 0.

  ![Четыре машины в гараже со шкалами износа двигателей; двигатель машины 1 проверяют и чинят до износа 0](./images/21_car_engine.svg)

- **Рейлинг для чашек.** Рейлинг рассчитан на 4 чашки. Некоторые крючки пусты.
  Повесьте новую чашку на первый пустой крючок и снимите чашку с крючка 1.
  При перемещении владение передаётся: скопируйте ее, затем очистите старое место.

  ![Рейлинг с четырьмя крючками: чашка с крючка 1 снимается, а новая вешается на первый пустой крючок 2](./images/21_cup_rail.svg)

- **Отряд героев.** Отряд вмещает 3 героев. У каждого героя есть здоровье и щит.
  Полечите героя 1 на 20 единиц здоровья. Посчитайте, скольким героям все еще нужно лечение.

  ![Три героя со шкалами здоровья и щита, лечение героя 1 на +20 и неизвестное число героев, которым всё ещё нужно лечение](./images/21_hero_party.svg)

- **Активный бафф.** Игрок хранит ссылку на активный бафф. Отсутствие баффа означает пустую ссылку.
  Дайте игроку 1 бафф на щит до боя и заберите его после боя.

  ![Ссылка activeBuff игрока перед боем указывает на бафф +1 к щиту, а после боя сбрасывается в null](./images/21_active_buff.svg)

- **Класс.** В комнате 2 ряда по 3 парты. Каждая парта либо хранит ученика, либо пуста.
  Посчитайте пустые парты. Посадите новичка за первую пустую парту.

  ![Класс с двумя рядами по три парты: четыре заняты, две пусты, новичок направляется за первую пустую парту, число пустых парт неизвестно](./images/21_classroom.svg)

- **Найти пиццу.** Холодильник вмещает 3 продукта с названиями.
  Найдите пиццу и переместите ее на полку 1 морозилки. Верните признак, была ли она найдена.

  ![Холодильник с яблоком, пиццей и молоком, показанный через массив products; морозилка с мороженым и пустой полкой; пицца переезжает в морозилку](./images/21_find_pizza.svg)

  <details>
  <summary>
  Возможное решение
  </summary>

  Те же холодильник и морозилка, что и раньше, но поиск выполняется по массиву полок,
  а параметры «что найти» и «куда положить» сгруппированы в отдельный класс запроса.
  Имя функции — то же, что у предыдущего `MoveFood`, но параметры другие (перегрузка):

  ```csharp
  class Food
  {
      public string name;
  }

  class Fridge
  {
      public Food?[] shelves = new Food?[3];
  }

  class Freezer
  {
      public Food?[] shelves = new Food?[2];
  }

  class FoodMove
  {
      public string name;
      public int toIndex;
  }
  ```

  Допустим, на полке 1 холодильника лежит пицца, а полка 1 морозилки пуста.
  Вызывающий код упаковывает `"pizza"` и `1` в `FoodMove`.
  Целевой индекс — это контракт, поэтому его проверяет `Debug.Assert`;
  свободна ли целевая полка — это обычные данные, поэтому проверка возвращает `false`.
  Цикл перебирает полки холодильника по очереди: на полке 0 пиццы нет, поэтому пропускаем её;
  на полке 1 есть пицца, поэтому переносим ссылку в морозилку и очищаем старую полку,
  затем сигнализируем об успехе, возвращая `true`.
  Если ни на одной полке нет продукта с таким именем, возвращаем `false` и ничего не меняем:

  ```csharp
  static bool MoveFood(Fridge fridge, Freezer to, FoodMove move)
  {
      Debug.Assert(move.toIndex >= 0 && move.toIndex < to.shelves.Length);
      if (to.shelves[move.toIndex] != null)
      {
          return false;
      }
      for (int i = 0; i < fridge.shelves.Length; i++)
      {
          Food? slot = fridge.shelves[i];
          if (slot == null)
          {
              continue;
          }
          if (slot.name != move.name)
          {
              continue;
          }
          to.shelves[move.toIndex] = slot;
          fridge.shelves[i] = null;
          return true;
      }
      return false;
  }
  ```

  Изменения в массиве полок видны снаружи, потому что массивы — ссылочный тип:
  функция получает не копию полок, а ссылку на тот же массив.

  Использование в основной функции:

  ```csharp
  Fridge fridge = new();
  fridge.shelves[1] = new Food
  {
      name = "pizza",
  };
  Freezer freezer = new();
  bool moved = MoveFood(fridge, freezer, new FoodMove
  {
      name = "pizza",
      toIndex = 1,
  });
  ```
  </details>

- **Замочить посуду.** На кухне 2 сушилки по 3 тарелки. Каждая тарелка бывает грязной или замоченной.
  Замочите каждую грязную тарелку в сушилке 0.

  ![Две кухонные сушилки по три тарелки; каждую грязную тарелку в сушилке 0 замачивают водой](./images/21_soak_dishes.svg)

- **Комнаты библиотеки.** В библиотеке 2 комнаты по 3 слота. Каждый слот либо хранит книгу, либо пуст.
  Положите «Атлас» в первый пустой слот комнаты 1.

  ![Две комнаты библиотеки по три слота; книгу «Атлас» кладут в первый пустой слот 1 комнаты 1](./images/21_library_rooms.svg)

- **Светофор.** На перекрестке 3 светофора. Каждый светофор бывает красным, желтым или зеленым.
  Переключите светофор 2 на следующий цвет через `switch`.

  ![Три светофора, где жёлтый светофор 2 переключается по циклу красный–зелёный–жёлтый с неизвестным новым цветом](./images/21_traffic_light.svg)

- **Аптечка.** Шкафчик вмещает 4 коробки. Каждая коробка бывает свежей или просроченной.
  Верните признак, можно ли использовать коробку 1. Выбросите коробку 1, если она просрочена.

  ![Аптечка с четырьмя свежими и просроченными коробками: просроченную коробку 1 проверяют на пригодность и выбрасывают](./images/21_medicine_cabinet.svg)

- **Столовая.** Столовая хранит 3 супа и 3 хлеба.
  Подайте суп 1 с хлебом 1. При подаче оба забираются.

  ![Ряды из трёх супов и трёх буханок хлеба в столовой: суп 1 и хлеб 1 забирают и ставят на поднос](./images/21_canteen.svg)

- **Горячая цепочка.** На кухне есть холодильник, морозилка и микроволновка.
  Положите пиццу с полки 0 холодильника в микроволновку, разогрейте ее,
  затем переместите на полку 1 морозилки. Каждый шаг возвращает признак, получилось ли его выполнить.

  ![Цепочка из холодильника, микроволновки и морозилки: пицца с полки 0 греется и переходит на полку 1 морозилки, каждый шаг возвращает неизвестный результат](./images/21_hot_chain.svg)

- **Барбершоп.** В салоне есть место для 4 ожидающих клиента и 2 мастера.
  У каждого клиента есть метка и остаток стрижки в минутах — сколько ещё нужно состричь.
  Клиенты создаются отдельно в основной функции, а салон хранит только ссылки на них.
  Добавьте каждого клиента на первое свободное место в очереди.
  Назначьте свободных мастеров ожидающим клиентам.
  Каждый тик каждый занятый мастер состригает 1 минуту.
  Когда клиент полностью пострижен, мастер освобождается.
  Симулируйте через цикл `while`, пока все клиенты не будут обслужены.

  ![Очередь из четырёх стульев с alice и bob, два мастера и тик часов на одну минуту](./images/21_barbershop.svg)

  <details>
  <summary>
  Возможное решение
  </summary>

  Клиенты живут в основной функции, а салон хранит только ссылки на них —
  поэтому и очередь, и мастера хранят ссылки, а не копии.
  Изменения через ссылку отражаются на самом клиенте: один и тот же клиент
  сначала доступен через очередь, затем через мастера.

  ```csharp
  struct MinutesLeft
  {
      public int value;
  }

  class Client
  {
      public string label;
      public MinutesLeft hairCompletion;
  }

  class Worker
  {
      public Client? assignedClient;
  }

  class Shop
  {
      public Client?[] queue = new Client?[4];
      public Worker[] workers = [new Worker(), new Worker()];
  }
  ```

  Добавление кладёт ссылку на первое свободное место в очереди.
  Поиск этого места — отдельная функция:
  она отдаёт индекс слота или `-1`, когда очередь полна.
  Добавление просто заполняет этот слот.
  Параметр типа `Client` не может быть пустой ссылкой, поэтому клиент не проверяется;
  переполненная очередь — это штатная ситуация, о ней сообщается возвращаемым значением:

  ```csharp
  enum AddClientResult
  {
      Added,
      FailedQueueFull,
  }

  static int FindFreeSlot(Shop shop)
  {
      for (int i = 0; i < shop.queue.Length; i++)
      {
          if (shop.queue[i] == null)
          {
              return i;
          }
      }
      return -1;
  }

  static AddClientResult AddClient(Shop shop, Client client)
  {
      int slot = FindFreeSlot(shop);
      if (slot < 0)
      {
          return AddClientResult.FailedQueueFull;
      }
      shop.queue[slot] = client;
      return AddClientResult.Added;
  }
  ```

  Взятие первого ожидающего клиента — отдельная функция:
  поиск первого занятого места отдаёт его индекс,
  взятие очищает этот слот и возвращает клиента или `null`.
  Назначение просто раздаёт каждому свободному мастеру то, что вернёт эта функция, —
  `null` просто оставляет мастера свободным:

  ```csharp
  static int FindTakenSlot(Shop shop)
  {
      for (int i = 0; i < shop.queue.Length; i++)
      {
          if (shop.queue[i] != null)
          {
              return i;
          }
      }
      return -1;
  }

  static Client? TakeFirstWaiting(Shop shop)
  {
      int slot = FindTakenSlot(shop);
      if (slot < 0)
      {
          return null;
      }
      Client? found = shop.queue[slot];
      shop.queue[slot] = null;
      return found;
  }

  static void AssignWorkers(Shop shop)
  {
      for (int i = 0; i < shop.workers.Length; i++)
      {
          Worker worker = shop.workers[i];
          if (worker.assignedClient != null)
          {
              continue;
          }
          worker.assignedClient = TakeFirstWaiting(shop);
      }
  }
  ```

  Стрижка одной минуты и проверка готовности клиента — тоже отдельные функции:

  ```csharp
  static void CutOneMinute(Client client)
  {
      Debug.Assert(client.hairCompletion.value > 0); // должно остаться что стричь
      client.hairCompletion.value--;
  }

  static bool ClientDone(Client client)
  {
      return client.hairCompletion.value == 0;
  }

  enum TickResult
  {
      Unassigned,
      StillWorking,
      Done,
  }

  static TickResult TickWorker(Worker worker)
  {
      Client? client = worker.assignedClient;
      if (client == null)
      {
          return TickResult.Unassigned;
      }
      if (client.hairCompletion.value > 0)
      {
          CutOneMinute(client);
      }
      if (ClientDone(client))
      {
          worker.assignedClient = null;
          return TickResult.Done;
      }
      return TickResult.StillWorking;
  }

  static void TickAllWorkers(Shop shop)
  {
      for (int i = 0; i < shop.workers.Length; i++)
      {
          TickWorker(shop.workers[i]);
      }
  }

  static bool HasWaitingClients(Shop shop)
  {
      for (int i = 0; i < shop.queue.Length; i++)
      {
          if (shop.queue[i] != null)
          {
              return true;
          }
      }
      return false;
  }

  static bool HasBusyWorkers(Shop shop)
  {
      for (int i = 0; i < shop.workers.Length; i++)
      {
          if (shop.workers[i].assignedClient != null)
          {
              return true;
          }
      }
      return false;
  }

  static bool ShopDone(Shop shop)
  {
      return !HasWaitingClients(shop) && !HasBusyWorkers(shop);
  }
  ```

  Симуляция — это обычный цикл `while`: сначала назначаем мастеров,
  затем вызываем тик всех мастеров, пока все клиенты не будут обслужены.
  Клиенты должны существовать дольше салона, потому что салон их никогда не копирует:

  ```csharp
  Client alice = new()
  {
      label = "alice",
      hairCompletion = new()
      {
          value = 2,
      },
  };
  Client bob = new()
  {
      label = "bob",
      hairCompletion = new()
      {
          value = 3,
      },
  };

  Shop shop = new();
  AddClient(shop, alice);
  AddClient(shop, bob);

  while (!ShopDone(shop))
  {
      AssignWorkers(shop);
      TickAllWorkers(shop);
  }
  ```
  </details>

- **Дроны-барберы.** Работники теперь дроны-автоматы:
  несколько дронов могут обрабатывать одного клиента одновременно,
  каждый состригает по 1 минуте за тик.
  Когда клиент полностью пострижен, освобождается каждый занятый им дрон.
  Симулируйте, пока все клиенты не будут обслужены.

  ![Три автоматических дрона-барбера: дроны 0 и 1 одновременно стригут bob, а дрон 2 — alice, состригая по 1 минуте за тик](./images/21_barbershop_drones.svg)

- **Парк аттракционов.** В парке 3 аттракциона.
  Каждый посетитель упорядочивает все 3 аттракциона по предпочтению,
  не посещает один и тот же аттракцион дважды подряд
  и может посетить аттракционы всего 4 раза.
  Каждый тик обслуженные посетители сходят с аттракциона —
  или покидают парк, когда квота исчерпана —
  а посетители без места занимают первый свободный аттракцион в порядке своих предпочтений.
  Если всё занято, они ждут, пока что-нибудь освободится.
  Симулируйте, пока все не исчерпают квоту.

  ![Три посетителя со списками предпочтений и квотами посещений выбирают среди американских горок, карусели и колеса обозрения, не повторяя один аттракцион подряд](./images/21_amusement_park.svg)

- **Рейс грузовика.** Грузовику нужно проехать заданное количество часов.
  В списке 3 водителя.
  Про каждого известно, сколько он может ехать без отдыха
  и сколько отдыха ему нужно за час езды.
  Напишите функцию, которая генерирует события рейса:
  кто сколько проехал и сколько времени грузовик простоял.
  Водитель едет до своего лимита (или пока не закончится рейс), а затем отдыхает
  столько, сколько положено: часы езды × норму отдыха. Пока он отдыхает,
  за руль садится другой водитель, а после отдыха снова готов ехать.
  Грузовик простаивает, только пока отдыхают все водители одновременно, —
  до тех пор, пока первый из них не отдохнёт.

  ![Грузовик с тремя водителями, у каждого свой предел вождения и норма отдыха, и лента из пронумерованных отрезков вождения в цветах водителей, которые чередуются с серыми отрезками сна и продолжаются в неизвестность](./images/21_truck_trip.svg)

  <details>
  <summary>
  Возможное решение
  </summary>

  Состояние рейса — это сколько ещё осталось ехать
  и сколько отдыха ещё должен каждый водитель.
  Оставшийся отдых — это состояние самого водителя,
  как остаток стрижки у клиента в барбершопе, поэтому он хранится в его классе:

  ```csharp
  class Driver
  {
      public string name;
      public double maxDriveHours;    // сколько водитель может ехать без отдыха
      public double restPerDriveHour; // сколько отдыха нужно за час езды
      public double restLeft;         // сколько отдыха ещё осталось
  }
  ```

  Событие — это вид, имя водителя и длительность.
  У события простоя водителя нет, поэтому имя остаётся пустым:

  ```csharp
  enum EventKind
  {
      Drive,
      Idle,
  }

  struct Event
  {
      public EventKind kind;
      public string? driver; // у события Idle имя пустое
      public double hours;
  }
  ```

  Генератор работает в цикле, пока не закончится езда.
  Сначала он ищет водителя, который уже отдохнул.
  Такой водитель проезжает один отрезок пути — до своего лимита или до конца рейса, —
  а затем ему положен отдых за часы езды.
  Если не готов никто, грузовик простаивает, пока не закончится самый короткий
  оставшийся отдых; на это время отдых сокращается у всех.
  Каждый шаг записывает ровно одно событие.
  Непустой список водителей, неотрицательное время рейса и место под событие —
  это контракты, поэтому их проверяет `Debug.Assert`:

  ```csharp
  // Записывает события рейса в events, возвращает их количество.
  static int GenerateEvents(Driver[] drivers, double tripHours, Event[] events)
  {
      Debug.Assert(drivers.Length > 0);   // у грузовика всегда есть хотя бы один водитель
      Debug.Assert(tripHours >= 0.0);     // рейс не может длиться отрицательное время
      for (int i = 0; i < drivers.Length; i++)
      {
          Debug.Assert(drivers[i].maxDriveHours > 0.0); // водитель без запаса хода остановит рейс
          Debug.Assert(drivers[i].restPerDriveHour >= 0.0);
      }
      int count = 0;
      while (tripHours > 0.0)
      {
          int freeIndex = drivers.Length;
          for (int i = 0; i < drivers.Length; i++)
          {
              if (drivers[i].restLeft <= 0.0)
              {
                  freeIndex = i;
                  break;
              }
          }

          if (freeIndex < drivers.Length)
          {
              Driver driver = drivers[freeIndex];
              double hours = driver.maxDriveHours;
              if (hours > tripHours)
              {
                  hours = tripHours;
              }
              tripHours -= hours;
              driver.restLeft = driver.restPerDriveHour * hours;

              Debug.Assert(count < events.Length);
              events[count] = new Event
              {
                  kind = EventKind.Drive,
                  driver = driver.name,
                  hours = hours,
              };
              count++;
              continue;
          }

          // Все отдыхают: грузовик ждёт первого освободившегося.
          double idleHours = drivers[0].restLeft;
          for (int i = 0; i < drivers.Length; i++)
          {
              if (drivers[i].restLeft < idleHours)
              {
                  idleHours = drivers[i].restLeft;
              }
          }
          Debug.Assert(idleHours > 0.0); // иначе кто-то из водителей был бы готов
          for (int i = 0; i < drivers.Length; i++)
          {
              drivers[i].restLeft -= idleHours;
          }

          Debug.Assert(count < events.Length);
          events[count] = new Event
          {
              kind = EventKind.Idle,
              driver = null,
              hours = idleHours,
          };
          count++;
      }
      return count;
  }
  ```

  Функция печати выбирает ветку по виду через `switch`, а у события простоя нет имени:

  ```csharp
  static void PrintEvent(Event e)
  {
      switch (e.kind)
      {
          case EventKind.Drive:
              Console.WriteLine($"{e.driver} drove {e.hours} h");
              break;
          case EventKind.Idle:
              Console.WriteLine($"truck idle for {e.hours} h");
              break;
      }
  }
  ```

  Использование в основной функции:

  ```csharp
  Driver[] drivers =
  [
      new Driver
      {
          name = "alice",
          maxDriveHours = 4.0,
          restPerDriveHour = 0.5,
      },
      new Driver
      {
          name = "bob",
          maxDriveHours = 3.0,
          restPerDriveHour = 1.0,
      },
      new Driver
      {
          name = "cid",
          maxDriveHours = 2.0,
          restPerDriveHour = 2.0,
      },
  ];

  Event[] events = new Event[32];
  int count = GenerateEvents(drivers, 12.0, events);
  for (int i = 0; i < count; i++)
  {
      PrintEvent(events[i]);
  }
  ```

  Первой по списку едет alice: 4 часа, после чего ей положено 2 часа отдыха.
  Дальше едет bob: 3 часа и 3 часа отдыха, затем cid: 2 часа и 4 часа отдыха.
  Теперь отдыхают все, и грузовик простаивает 2 часа — это самый короткий отдых.
  Оставшиеся 3 часа проезжает alice:

  ```text
  alice drove 4 h
  bob drove 3 h
  cid drove 2 h
  truck idle for 2 h
  alice drove 3 h
  ```
  </details>

- **Объятия в семье.** В семье 5 человек.
  Каждый хочет обнять каждого другого ровно один раз.
  Каждый человек хранит ссылки на тех, кого уже обнял.
  Обнимите всех.

  ![Пять карточек членов семьи со слотами ссылок на уже обнятых родственников, дугами между парами и оставшимися пустыми слотами](./images/21_family_hugs_references.svg)

- **Объятия в семье по индексам.** Те же 5 человек,
  но каждый хранит индексы обнятых вместо ссылок —
  теперь объятия имеют смысл только вместе с массивом семьи, на который указывают индексы.
  Обнимите всех по разу.

  ![Массив семьи из пяти элементов с индексами 0–4 и пять карточек участников, каждая из которых хранит массив из четырёх индексов обнятых родственников](./images/21_family_hugs_indices.svg)

## Моделирование результатов операций

`bool` говорит только, получилось ли выполнить операцию.
Часто вызывающему коду нужно знать, *что* произошло:
какой снек куплен, сколько осталось сдачи, почему автомат отказал.
Оформите результат отдельным типом, а печать пусть реагирует на него.

В автомате со снеками есть слоты со снеком и ценой:

```csharp
using System.Diagnostics;

enum Snack
{
    Chips,
    Chocolate,
    Water,
}

static string SnackName(Snack snack)
{
    switch (snack)
    {
        case Snack.Chips:
            return "chips";
        case Snack.Chocolate:
            return "chocolate";
        case Snack.Water:
            return "water";
    }
    Debug.Assert(false); // все значения Snack обработаны выше
    return "";
}

class VendingSlot
{
    public Snack? snack;
    public int price;
}

class VendingMachine
{
    public VendingSlot[] slots = new VendingSlot[3];
}
```

Покупка сообщает один из нескольких исходов.
Индекс слота и число монет приходят от покупателя,
поэтому неверное значение — это обычный результат, а не нарушенное предусловие:
контракт принимает любой ввод и сообщает, что произошло.
Единственное утверждение касается состояния самого автомата:
у каждого слота должна быть положительная цена.
Успешный результат — отдельный класс,
поэтому результат не может содержать половину покупки:

```csharp
enum PurchaseStatus
{
    Bought,
    InvalidSelection,
    InvalidPayment,
    EmptySlot,
    NotEnoughCoins,
}

class BoughtSnack
{
    public Snack snack;
    public int change;
}

class Purchase
{
    public PurchaseStatus status;
    public BoughtSnack? bought; // есть ровно при status == Bought
}

static Purchase BuySnack(VendingMachine machine, int slotIndex, int coins)
{
    if (slotIndex < 0 || slotIndex >= machine.slots.Length)
    {
        return new Purchase
        {
            status = PurchaseStatus.InvalidSelection,
        };
    }
    if (coins < 0)
    {
        return new Purchase
        {
            status = PurchaseStatus.InvalidPayment,
        };
    }

    VendingSlot slot = machine.slots[slotIndex];
    Debug.Assert(slot.price > 0); // у каждого слота должна быть положительная цена
    if (slot.snack == null)
    {
        return new Purchase
        {
            status = PurchaseStatus.EmptySlot,
        };
    }
    if (coins < slot.price)
    {
        return new Purchase
        {
            status = PurchaseStatus.NotEnoughCoins,
        };
    }

    Purchase purchase = new()
    {
        status = PurchaseStatus.Bought,
        bought = new BoughtSnack
        {
            snack = slot.snack.Value,
            change = coins - slot.price,
        },
    };
    slot.snack = null; // автомат меняется только после успешной покупки
    return purchase;
}
```

Печать выбирает ветку по статусу и реагирует на то, что покупка действительно сделала:

```csharp
static void PrintPurchase(Purchase purchase)
{
    switch (purchase.status)
    {
        case PurchaseStatus.Bought:
            Debug.Assert(purchase.bought != null); // у Bought всегда есть снек
            Console.WriteLine($"bought {SnackName(purchase.bought.snack)}, change {purchase.bought.change}");
            break;
        case PurchaseStatus.InvalidSelection:
            Console.WriteLine("no such slot");
            break;
        case PurchaseStatus.InvalidPayment:
            Console.WriteLine("the payment is invalid");
            break;
        case PurchaseStatus.EmptySlot:
            Console.WriteLine("the slot is empty");
            break;
        case PurchaseStatus.NotEnoughCoins:
            Console.WriteLine("not enough coins");
            break;
    }
}
```

Использование в основной функции:

```csharp
VendingMachine machine = new()
{
    slots =
    [
        new VendingSlot
        {
            snack = Snack.Chips,
            price = 3,
        },
        new VendingSlot
        {
            snack = null,
            price = 2,
        },
        new VendingSlot
        {
            snack = Snack.Water,
            price = 2,
        },
    ],
};

PrintPurchase(BuySnack(machine, 9, 5)); // no such slot
PrintPurchase(BuySnack(machine, 0, 5)); // bought chips, change 2
PrintPurchase(BuySnack(machine, 0, 5)); // the slot is empty
PrintPurchase(BuySnack(machine, 2, 1)); // not enough coins
```

Второй вызов — единственный, который меняет автомат.
Остальные два оставляют все слоты точно такими, какими они были.

## Планирование мутации как значения

Ещё один способ разделить обязанности — сделать изменение самостоятельным значением.
Одна функция читает текущее состояние и вычисляет изменение;
другая печатает, что сейчас произойдёт;
третья применяет изменение.
Состояние меняется в одном месте, а план можно рассмотреть до применения.

В гараже перекрашивают машины. План хранит, какую машину красят, новый цвет,
оценку времени и цену; его вычисление ничего не меняет:

```csharp
enum Paint
{
    Red,
    Blue,
    Green,
}

static string PaintName(Paint paint)
{
    switch (paint)
    {
        case Paint.Red:
            return "red";
        case Paint.Blue:
            return "blue";
        case Paint.Green:
            return "green";
    }
    Debug.Assert(false); // все значения Paint обработаны выше
    return "";
}

class Car
{
    public string model;
    public Paint paint;
}

class Repaint
{
    public int carIndex;
    public Paint newPaint;
    public int hours;
    public int price;
}

static Repaint PlanRepaint(Car[] cars, int carIndex, Paint newPaint)
{
    Debug.Assert(carIndex >= 0 && carIndex < cars.Length); // план должен указывать на существующую машину
    Car car = cars[carIndex];
    int hours = car.paint == newPaint ? 1 : 3;
    return new Repaint
    {
        carIndex = carIndex,
        newPaint = newPaint,
        hours = hours,
        price = hours * 1000,
    };
}

static void PrintRepaint(Repaint repaint)
{
    Console.WriteLine($"car {repaint.carIndex} will be repainted {PaintName(repaint.newPaint)} in {repaint.hours} h for {repaint.price}");
}

static void ApplyRepaint(Car[] cars, Repaint repaint)
{
    Debug.Assert(repaint.carIndex >= 0 && repaint.carIndex < cars.Length); // план должен указывать на существующую машину
    cars[repaint.carIndex].paint = repaint.newPaint;
}
```

Использование в основной функции: план печатается до применения,
поэтому вывод показывает, что именно сейчас произойдёт:

```csharp
Car[] cars =
[
    new Car
    {
        model = "Supra",
        paint = Paint.Red,
    },
    new Car
    {
        model = "Civic",
        paint = Paint.Green,
    },
    new Car
    {
        model = "Miata",
        paint = Paint.Red,
    },
];

Repaint repaint = PlanRepaint(cars, 0, Paint.Blue);
PrintRepaint(repaint);
ApplyRepaint(cars, repaint);
```

План — обычное значение: его можно напечатать, сохранить или отбросить,
не трогая машины.
