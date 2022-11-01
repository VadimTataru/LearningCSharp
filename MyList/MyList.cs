using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyList
{
    /// <summary>
    /// Реализация списка
    /// </summary>
    /// <typeparam name="T">Тип элементов списка</typeparam>
    public class MyList<T> : IEnumerable<T>
    {
        private T[] items;
        private int itemCount;

        private int defaultCapacity = 4;
        private T[] emptyArray = new T[0];

        /// <summary>
        /// Инициализация пустого экземпляра MyList с начальной ёмкостью 0
        /// </summary>
        public MyList()
        {
            items = Array.Empty<T>();
            itemCount = 0;
        }

        /// <summary>
        /// Инициализация пустого экземпляра MyList с начальной ёмкостью size
        /// </summary>
        /// <param name="size">Ёмкость экземпляра MyList</param>
        /// <exception cref="ArgumentException"></exception>
        public MyList(int size)
        {
            if (size < 0)
                throw new ArgumentException();
            items = new T[size];
            itemCount = 0;
        }

        /// <summary>
        /// Инициализация экземпляра MyList, который будет содержать элементы существующей коллекции, указанной в параметрах
        /// </summary>
        /// <param name="collection">Коллеция, элементы которой будут скопированы в экзмепляр MyList</param>
        /// <exception cref="ArgumentNullException"></exception>
        public MyList(IEnumerable<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException();

            ICollection<T>? c = collection as ICollection<T>;
            if (c != null)
            {
                int count = c.Count;
                if (count == 0)
                {
                    items = emptyArray;
                }
                else
                {
                    items = new T[count];
                    c.CopyTo(items, 0);
                    itemCount = count;
                }
            }
            else
            {
                itemCount = 0;
                items = emptyArray;

                using (IEnumerator<T> en = collection.GetEnumerator())
                {
                    while (en.MoveNext())
                    {
                        Add(en.Current);
                    }
                }
            }
        }

        /// <summary>
        /// Возращает или задаёт элемент с указанным индексом
        /// </summary>
        /// <param name="index">Индекс элемента который нужно вернуть или задать</param>
        /// <exception cref="ArgumentOutOfRangeException">Исключение, срабатываемое если параметр больше либо равен размеру Count или меньше нуля</exception>
        public T this[int index]
        {
            get {
                if ((uint)index >= (uint)itemCount)
                    throw new ArgumentOutOfRangeException();
                return items[index];
            }
            set {
                if ((uint)index >= (uint)itemCount)
                    throw new ArgumentOutOfRangeException();
                items[index] = value;
            }
        }

        /// <summary>
        /// Возвращает количество элементов экзмепляра MyList
        /// </summary>
        public int Count => itemCount;

        /// <summary>
        /// Возвращает или задаёт общее количество элементов, которое может содержать экземпляр MyList
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Capacity задано меньше, чем Count.</exception>
        /// <exception cref="OutOfMemoryException">В системе недостаточно доступной памяти.</exception>
        public int Capacity
        {
            get { return items.Length; }
            set
            {
                if (value < itemCount)
                    throw new ArgumentOutOfRangeException();
                if (value != items.Length)
                {
                    if (value > 0)
                    {
                        T[] newItems = new T[value];
                        if (itemCount > 0)
                            Array.Copy(items, 0, newItems, 0, itemCount);
                        items = newItems;
                    } else
                    {
                        items = emptyArray;
                    }
                }
            }
        }

        /// <summary>
        /// Добавляет стурктуру данных T в конец списка MyList
        /// </summary>
        /// <param name="item">Добавляемая структура данных</param>
        public void Add(T item)
        {
            if (itemCount >= items.Length)
                Resize(itemCount + 1);
            items[itemCount] = item;
            itemCount++;
        }

        /// <summary>
        /// Удаляет элемент item из списка MyList
        /// </summary>
        /// <param name="item">Удаляемый элемент</param>
        /// <returns>
        /// true - удаление прошло успешно
        /// false - удаление не удалось или элемент не найден в списке
        /// </returns>
        public bool Remove(T item)
        {
            int index = IndexOf(item);
            if (index < 0)
                return false;
            RemoveAt(index);
            return true;
        }

        /// <summary>
        /// Удаление элемента из списка MyList на заданной позиции
        /// </summary>
        /// <param name="index">Позиция элемента в списке</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void RemoveAt(int index)
        {
            if ((uint)index >= (uint)itemCount)
                throw new ArgumentOutOfRangeException();

            itemCount--;
            if(index < itemCount)
                Array.Copy(items, index+1, items, index, itemCount - index);
        }

        /// <summary>
        /// Поиск позиции заданного элемента
        /// </summary>
        /// <param name="item">Элемент, по которому будет вестись поиск</param>
        /// <returns>Позиция элемента из списка</returns>
        public int IndexOf(T item) => Array.IndexOf(items, item, 0, itemCount);

        /// <summary>
        /// Очищение списка
        /// </summary>
        public void Clear()
        {
            items = Array.Empty<T>();
        }

        /// <summary>
        /// Изменение размера массива
        /// </summary>
        /// <param name="amount">Объём изменения массива</param>
        private void Resize(int amount)
        {
            if(items.Length < amount)
            {
                int newCapacity = items.Length == 0 ? defaultCapacity : items.Length * 2;

                if((uint)newCapacity > Array.MaxLength) newCapacity = Array.MaxLength;
                if (newCapacity < amount) newCapacity = amount;
                Capacity = newCapacity;
            }
        }

        /// <summary>
        /// Приведение списка к строковому значению
        /// </summary>
        /// <returns>Строка</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append('[');
            for (int i = 0; i < itemCount; i++)
            {
                if (i > 0)
                    sb.Append(", ");
                sb.Append(items[i]);
            }
            sb.Append(']');
            return sb.ToString();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return default;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return default;
        }
    }
}
