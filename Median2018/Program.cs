using System;
using System.Text;

namespace Median2018
{
    class Program
    {
        static void Main(string[] args)
        {
            NumbersDownloader downloader = new NumbersDownloader("88.212.241.115", 2012, 50);
            Console.WriteLine(NumberHelper.GetMedian(downloader.GetResultNumbers().ToArray()));
           
        }
    }
}
