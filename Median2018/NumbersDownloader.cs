using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Unicode;
using System.Threading;

namespace Median2018
{
    public class NumbersDownloader
    {
        private string _address;
        private int _port;

        private static ConcurrentStack<int> _numbersForUpload;
        private static ConcurrentStack<long> _resultNumbers;

        private List<Thread> _threads;
        private int _threadsCount;

        private long getResultForNumber(int number)
        {
            using var client = new TcpClient(_address, _port);           
            using var clientStream = client.GetStream();

            string result = "";

            var bytesWithNumber = new MemoryStream();          
            
            clientStream.Write(Encoding.UTF8.GetBytes(NumberHelper.GetStringFromNumber(number)));
            do
            {
                Thread.Sleep(10);
                var buf = new byte[128];//Много памяти если, много потоков, но пока взял с запасом.

                int countOfReadedBytes = clientStream.Read(buf);//Считываем байты
                if (countOfReadedBytes < buf.Length) Array.Resize(ref buf, countOfReadedBytes);

                bytesWithNumber.Write(buf);
                result = (Encoding.ASCII.GetString(bytesWithNumber.ToArray()));
                if (result[result.Length - 1] == '\n') break;//Проверяем считали ли мы до конца строки
            } while (true);

            return NumberHelper.GetNumberFromString(result);
        }

        private void makeRequest()
        {            
            while (_numbersForUpload.Count!=0)
            {
                int number = 0;
                long result;
                if (!_numbersForUpload.TryPop(out number))
                {
                    continue;
                }
                try
                {
                    result = getResultForNumber(number);
                }catch(Exception ex)
                {
                    //Console.WriteLine($"{ex.Message}, trace: {ex.StackTrace}");
                    _numbersForUpload.Push(number);                    
                    continue;
                }
                _resultNumbers.Push(result);
                Console.WriteLine($"num: {number}, result: {result}, forUpl: {_numbersForUpload.Count}, resNum: {_resultNumbers.Count}");
            }

        }
        public NumbersDownloader(string address, int port, int threadsCount = 5)
        {
            _address = address;
            _port = port;
            _threadsCount = threadsCount;
        }

        public ConcurrentStack<long> GetResultNumbers()
        {
            _numbersForUpload = new ConcurrentStack<int>();
            _resultNumbers = new ConcurrentStack<long>();

            for (int i = 1; i <= 2018; i++)
                _numbersForUpload.Push(i);
            _threads = new List<Thread>();

            for (int i = 0; i < _threadsCount; i++) _threads.Add(new Thread(makeRequest));
            foreach (var th in _threads) th.Start();
            foreach (var th in _threads) th.Join();

            return _resultNumbers;
        }
    }
}
