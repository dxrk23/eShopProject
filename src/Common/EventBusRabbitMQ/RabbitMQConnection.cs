using System;
using RabbitMQ.Client;

namespace EventBusRabbitMQ
{
    public class RabbitMQConnection : IRabbitMQConnection
    {
        private readonly IConnectionFactory _connectionFactory;
        private IConnection _connection;
        private bool _disposed;

        public RabbitMQConnection(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
            if (!IsConnected) TryConnect();
        }

        public bool IsConnected => _connection != null && _connection.IsOpen && !_disposed;

        public bool TryConnect()
        {
            try
            {
                _connection = _connectionFactory.CreateConnection();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            if (IsConnected)
            {
                Console.WriteLine("RabbitMQ connection acquired");
                return true;
            }

            Console.WriteLine("Error: RabbitMQ connection failed");
            return false;
        }

        public IModel CreateModel()
        {
            if (!IsConnected) throw new InvalidOperationException("Error: RabbitMQ Connection is not open");

            return _connection.CreateModel();
        }

        public void Dispose()
        {
            if (_disposed) return;

            try
            {
                _connection.Dispose();
                _disposed = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}