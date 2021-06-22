using System;
using System.Collections.Generic;
using System.Text;

namespace lab2
{
	public class PriorityQueue
	{
		public List<Table> PriorQueue = new List<Table>(); //список з найбільшим значенням евристики

		public Table Dequeue()
		{
			if (PriorQueue.Count == 0)
			{
				return null;
			}

			Table table = PriorQueue[0];
			PriorQueue.RemoveAt(0);

			return table;
		}

		public void Enqueue(Table table)
		{
			int i = 0;
			foreach (var element in PriorQueue)
			{
				if (element.CompareTo(table) < 0) //якщо новий варіант має більшу евристику
				{
					break;
				}
				i++;
			}
			PriorQueue.Insert(i, table); //додати до списку 
		}

		public bool IsEmpty()
		{
			if (PriorQueue.Count == 0) return true;
			else return false;
		}
	}
}
