using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using Common;
using Models;

namespace Service
{
    public class OrderService
    {
        public List<Invoice> GetAll()
        {
            var ResultSet = new List<Invoice>();

            using (var connection = new SqlConnection(Parameters.ConnectionString))
            {
                connection.Open();

                var command = new SqlCommand("SELECT * FROM Invoices", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var invoice = new Invoice()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            IVA = Convert.ToDecimal(reader["IVA"]),
                            Subtotal = Convert.ToDecimal(reader["Subtotal"]),
                            Total = Convert.ToDecimal(reader["Total"]),
                            ClientId = Convert.ToInt32(reader["ClientId"])
                        };
                        ResultSet.Add(invoice);
                    }

                }


                foreach (var invoice in ResultSet)
                {

                    //Client
                    setClient(invoice, connection);

                    //Detail
                    setDetail(invoice, connection);

                }

                return ResultSet;
            }
        }

        private void setClient(Invoice invoice, SqlConnection connection)
        {
            var command = new SqlCommand("SELECT * FROM CLIENT WHERE Id = @clientId");
            command.Parameters.AddWithValue("@clientId", invoice.ClientId);

            using (var reader = command.ExecuteReader())
            {
                reader.Read();

                invoice.Client = new Client
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString()
                };

            }

        }

        private void setProduct(InvoiceDetail detail, SqlConnection connection)
        {
            var command = new SqlCommand("SELECT * FROM PRODUCTS WHERE Id = @productId");
            command.Parameters.AddWithValue("@productId", detail.ProductId);

            using (var reader = command.ExecuteReader())
            {
                reader.Read();

                detail.Product = new Product
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Price = Convert.ToDecimal(reader["Price"]),
                    Name = reader["Name"].ToString()
                };

            }

        }


        private void setDetail(Invoice invoice, SqlConnection connection)
        {
            var command = new SqlCommand("SELECT * FROM CLIENT WHERE invoiceId = @invoiceId");
            command.Parameters.AddWithValue("@invoiceId", invoice.Id);

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    invoice.Detail.Add(new InvoiceDetail()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            ProductId = Convert.ToInt32(reader["ProductId"]),
                            Quantity = Convert.ToInt32(reader["Quantity"]),
                            IVA = Convert.ToDecimal(reader["IVA"]),
                            Subtotal = Convert.ToDecimal(reader["Subtotal"]),
                            Total = Convert.ToDecimal(reader["Total"]),

                        }
                    );
                }
            }

            foreach (var detail in invoice.Detail)
            {
                setProduct(detail, connection);                
            }
        }

    }
}
