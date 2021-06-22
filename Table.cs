using System;
using System.Collections.Generic;
using System.Text;

namespace lab2
{
    public class Table : IComparable<Table>
    {
        public Table Parent { get; set; } //батьківський об'єкт
        public char[] PresentState { get; set; } = new char[8]; //поточна комбінація розташування куль
        private char[] goal = new char[8] { 'w', 'w', 'w', '_', 'b', 'b', 'b', 'b' }; //goal - цільова комбінація

        public Table(char[] table)
        {
            PresentState = (char[])table.Clone();
        }

        public int HeuristicFunc() //евристична функція
        {
            int f = 0;
            for (int i = 0; i < 8; i++)
            {
                if (PresentState[i] == goal[i]) //якщо положення поточного елемента збігається з цільовим - збільшуємо значення евристики
                {
                    f++;
                }
            }
            return f;
        }

        public Table GetChild(int variation)
        {
            int space = FindSpace();    //положення пустої клітинки
            char[] newState = (char[])PresentState.Clone();

            if (variation == 1)
            {
                if (space == 0 || PresentState[space - 1] == 'w') //якщо пуста клітинка на початку або зліва знаходиться біла кулька
                {
                    return null;
                }
                Swap(ref newState, space - 1, space); //в іншому випадку міняємо пусту клітинку і ліву сусідню 
            }
            else if (variation == 2)
            {
                if (space == 7 || PresentState[space + 1] == 'b') //якщо пуста клітинка вкінці або спрва знаходиться чорна кулька
                {
                    return null;
                }
                Swap(ref newState, space + 1, space); //в іншому випадку міняємо пусту клітинку і праву сусідню 
            }
            else if (variation == 3)
            {
                if (space <= 1 || PresentState[space - 2] == 'w') //якщо пуста клітинка на початку (або на другому місці) або зліва через одного знаходиться біла кулька
                {
                    return null;
                }
                Swap(ref newState, space - 2, space); //в іншому випадку міняємо пусту клітинку і ліву через одну
            }
            else
            {
                if (space >= 6 || PresentState[space + 2] == 'b') //якщо пуста клітинка вкінці (або на передостанньому місці) або справа через одного знаходиться чорна кулька
                {
                    return null;
                }
                Swap(ref newState, space + 2, space); //в іншому випадку міняємо пусту клітинку і праву через одну
            }

            return new Table(newState, this);
        }

        private int FindSpace()
        {
            for (int i = 0; i < 8; i++)
            {
                if (PresentState[i] == '_')
                {
                    return i;
                }
            }
            return default;
        }
        private void Swap(ref char[] newState, int ball, int space)
        {
            char temp = newState[ball];
            newState[ball] = newState[space];
            newState[space] = temp;
        }

        public void Path(ref int n)
        {
            n++;
            if (Parent != null)
            {
                Parent.Path(ref n);
            }
            Console.WriteLine();
            for (int i = 0; i < PresentState.Length; i++)
            {
                Console.Write(PresentState[i] + "  ");
            }
            Console.WriteLine();
        }


        public int CompareTo(Table neighbor)
        {
            int presentHeuristic = HeuristicFunc();
            int neighboringHeuristic = neighbor.HeuristicFunc();
            return presentHeuristic - neighboringHeuristic;
        }

        public Table(char[] table, Table parent)
        {
            PresentState = (char[])table.Clone();
            Parent = parent;
        }
    }
}
