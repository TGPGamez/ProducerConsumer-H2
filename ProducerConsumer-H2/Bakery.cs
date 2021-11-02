using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProducerConsumer_H2
{
    public delegate void ActionMessageEvent(string message);

    class Bakery
    {
        private static Random rand = new Random();
        private Queue<int> breadTray = new Queue<int>();

        public Bakery()
        {
            Thread consumerThread = new Thread(Consumer);
            Thread producerThread = new Thread(Producer);
            producerThread.Start();
            consumerThread.Start();
        }


        public void Producer()
        {
            while (true)
            {
                lock (breadTray)
                {
                    while (breadTray.Count != 0)
                    {
                        Monitor.Wait(breadTray);
                    }
                    for (int i = 0; i < rand.Next(1,4); i++)
                    {
                        breadTray.Enqueue(i);
                        Console.WriteLine("Produced: " + (i + 1));
                    }
                    Monitor.PulseAll(breadTray);
                    Thread.Sleep(600);
                }
            }
        }

        public void Consumer()
        {
            while (true)
            {
                lock (breadTray)
                {
                    while (breadTray.Count == 0)
                    {
                        Monitor.PulseAll(breadTray);
                        Console.WriteLine("Consumer waits..");
                        Monitor.Wait(breadTray);
                    }
                    Console.WriteLine("Consumed: " + (breadTray.Dequeue() + 1));
                    Thread.Sleep(600);
                }
            }
        }
    }
}
