using System;
using System.Data.SqlClient;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using Common;

namespace Service
{
    public class TestService
    {
        public static void TestConnection()
        {
            try
            {
                using (var context = new SqlConnection(Parameters.ConnectionString))
                {
                    context.Open();
                    Console.WriteLine("Connection Successful");
                }
            }
            catch(Exception e)
            {
                Console.WriteLine($"SQL SERVER: {e.Message}");
            }
            
        }
    }
}
