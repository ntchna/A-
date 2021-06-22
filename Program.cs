using System;
using System.Collections.Generic;

namespace lab2
{
	internal class Program
	{
		static void Main(string[] args)
		{
			AStar();
		}


		static void AStar()
		{
			PriorityQueue queue = new PriorityQueue();
			List<Table> closed = new List<Table>();
			char[] start = new char[8] { 'b', 'b', 'b', 'b', '_', 'w', 'w', 'w' }; //стартова комбінація
			Table board = new Table(start); //стартова дошка з розміщенням куль
			queue.Enqueue(board); //додання до пріоритетного списку
			closed.Add(board); //додання до пройдених вершин
			int n = 0;

			while (!queue.IsEmpty()) //допоки список пріоритетів не пустий
			{
				board = queue.Dequeue(); //вибарти перший елемент з PriorityQueue та видалити його зі списку
				for (int i = 1; i <= 4; i++)
				{
					Table child = board.GetChild(i); //знаходження похідний варіацій комбінацій з поточного

					if (child is null)
					{
						continue;
					}

					if (child.HeuristicFunc() == 8) //якщо ціль досягнута
					{

						child.Path(ref n); //повертаємо шлях рекурсивно по батьківському об'єкту
						Console.WriteLine($"\nCount of steps: {n}"); //кількість кроків для досягнення оптимального шляху
						Environment.Exit(0);
					}
					else if (!closed.Contains(child)) //якщо комбінація нова і не вивчена
					{
						queue.Enqueue(child); //додавання в PriorityQueue
						closed.Add(child);    //додавання в закриті варіації
					}
				}
				if (queue.IsEmpty()) //якщо список пріоритетів пустий
				{
					Console.WriteLine("No solutions!");
					Environment.Exit(0);
				}
			}
		}
	}
}
