

using MDA;
using System.Diagnostics;

Console.OutputEncoding=System.Text.Encoding.UTF8;
var rest = new Restaurant();

while (true)
{
    Console.WriteLine("Привет! Желаете забронировать столик?" +
        "\n1 - мы уведомим Вас по смс (асинхронно)"+
        "\n2 - подождите на линии, мы Вас оповестим (синхронно)"+
        "\n3 - вывод состояния всех столиков"+
        "\n4 - отменить резервирование"+
        "\n5 - установить авто отмену/отключить авто отмену (брони  через каждые 20 секунд)!"
        );
    if (!int.TryParse(Console.ReadLine(),out var choice) && choice is not(1 or 2 or 3 or 4))
    {
        Console.WriteLine("Введите, пожалуйста 1,2,3 или 4");
        continue;
    }

    var stopWatch = new Stopwatch();

    stopWatch.Start();

    switch (choice)
    {
        case 1:
            rest.BookFreeTableAsync(1);
            break;
        case 2:
            rest.BookFreeTable(1);
           
            break;
        case 3:
            rest.GetAllTable();
            break;

        case 4:
            Console.Write("Введите номер стола для отмены резервирования: ");
            int.TryParse(Console.ReadLine(), out var num);
            rest.TableSetFree(num);
            break;

        case 5:
            rest.CancelAllTable();
            break;

        default:
            break;
    }


    stopWatch.Stop();
    var ts = stopWatch.Elapsed;
    Console.WriteLine($"{ts.Seconds:00}:{ts.Milliseconds:00}");
}
