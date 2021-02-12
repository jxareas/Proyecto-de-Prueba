using System;
using Service;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var orderService = new OrderService();
            var rs = orderService.GetAll();

        }
    }
}
