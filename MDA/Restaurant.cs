using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MDA
{
    public class Restaurant
    {
        private readonly List<Table> _tables=new List<Table>();

        public bool change=true;
        

        public Restaurant()
        {
            for (ushort i = 1; i <= 10; i++)
            {
                _tables.Add(new Table(i));
            }
        }

        /// <summary>
        /// Бронирование по звонку
        /// </summary>
        /// <param name="countOfPersons"></param>
        public void BookFreeTable(int countOfPersons)
        {
            Console.WriteLine("Добрый день! Подождите секунду я подберу столик и подтвержу вашу бронь, оставайтесь на линии");

            var table=_tables.FirstOrDefault(t=>t.SeatsCount>countOfPersons&& t.State==State.Free);

            Thread.Sleep(5000);
            Console.WriteLine(table is null
                ? $"К сожалению, сейчас все столики заняты"
                : $"Готово! Ваш столик номер {table.Id}") ;
            
            table?.SetState(State.Booked);
            
        }
        /// <summary>
        /// Бронирование по SMS
        /// </summary>
        /// <param name="countOfPersons"></param>
        public void BookFreeTableAsync(int countOfPersons)
        {
            Console.WriteLine("Добрый день! Подождите секунду я подберу столик и подтвержу вашу бронь, Вам придет уведомление");
            Task.Run(async () =>
            {
                var table = _tables.FirstOrDefault(t => t.SeatsCount > countOfPersons && t.State == State.Free);

                await Task.Delay(5000);

                table?.SetState(State.Booked);

                Console.WriteLine(table is null
                    ? $"УВЕДОМЛЕНИЕ: К сожалению, сейчас все столики заняты"
                    : $"УВЕДОМЛЕНИЕ: Готова! Ваш столик номер {table.Id}");
            });
        }

        /// <summary>
        /// Вывод состояния всех столиков
        /// </summary>
        public void GetAllTable()
        {
            foreach (var i in _tables)
            {
                Console.WriteLine($"Id - {i.Id}, Статус - {i.State} ");
            }
        }

        /// <summary>
        /// Отмена брони определённого столика
        /// </summary>
        /// <param name="id"></param>
        public void TableSetFree(int id)
        {
            foreach (var i in _tables)
            {
                if (i.Id==id)
                {
                    i.State = State.Free;
                }
            }
        }

        /// <summary>
        /// Oтмена всех забронированных столиков
        /// </summary>
        public async Task CancelAllTable()
        {

            if (change == true)
            {
                change = false;
                while (change == false)
                {
                    await Task.Delay(20000);
                    foreach (var i in _tables)
                    {
                        i.State = State.Free;

                    }
                    Console.WriteLine("Все брони отменены!");
                }
            }
            else
            {
                change = true;
            }
        }

    }
}
