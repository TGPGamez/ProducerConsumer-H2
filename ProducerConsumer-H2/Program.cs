using System;
using System.Threading;

namespace ProducerConsumer_H2
{
    class Program
    {
        static void Main(string[] args)
        {
            Bakery bakery = new Bakery();
            bakery.BakeryInfo += BakeryInfoEvent;
        }

        private static void BakeryInfoEvent(string message)
        {
            Console.WriteLine(message);
        }
    }
}
