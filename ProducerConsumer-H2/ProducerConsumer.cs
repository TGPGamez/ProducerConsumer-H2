using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProducerConsumer_H2
{
    class ProducerConsumer
    {
        private Queue<int> breadTray = new Queue<int>();

        public ActionMessageEvent ProducerConsumerInfo { get; set; }

        public int GetNumber()
        {
            lock (breadTray)
            {
                while (breadTray.Count == 0)
                {
                    Monitor.PulseAll(breadTray);
                    ProducerConsumerInfo.Invoke("Forbruger venter..");
                    Monitor.Wait(breadTray);
                }
                return breadTray.Dequeue();
            }
        }

        public void RefillTray(int[] numbers)
        {
            lock (breadTray)
            {
                for (int i = 0; i < numbers.Length; i++)
                {
                    breadTray.Enqueue(numbers[i]);
                }
                Monitor.PulseAll(breadTray);
            }
        }
    }
}
