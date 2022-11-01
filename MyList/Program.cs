// See https://aka.ms/new-console-template for more information
using MyList;

MyList<int> list = new MyList<int> { -1, 122, 4, 5, 6, 9, 10, 8 };
MyList<int> list2 = new MyList<int>(list);

Console.WriteLine($"Capacity: {list.Capacity} | Count: {list.Count} Items: {list}");
list.Add(100);
Console.WriteLine($"Capacity: {list.Capacity} | Count: {list.Count} Items: {list}");
list.Remove(9);
Console.WriteLine($"Capacity: {list.Capacity} | Count: {list.Count} Items: {list}");
list.RemoveAt(0);
Console.WriteLine($"Capacity: {list.Capacity} | Count: {list.Count} Items: {list}");