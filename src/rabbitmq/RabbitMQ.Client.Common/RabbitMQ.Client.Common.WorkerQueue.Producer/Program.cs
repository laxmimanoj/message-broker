using RabbitMQ.Client.Common.Models;
using System;

namespace RabbitMQ.Client.Common.WorkerQueue.Producer
{
    class Program
    {
        private const string QueueName = "WorkerQueue_Example";

        private static IConnectionFactory _factory;
        private static IConnection _connection;
        private static IModel _model;

        static void Main(string[] args)
        {
            #region data
            var payment1 = new Payment
            {
                AmountToPay = 25.0m,
                CardNumber = "1234567885858858",
                Name = "test1"
            };
            var payment2 = new Payment
            {
                AmountToPay = 30.0m,
                CardNumber = "23456566643634633",
                Name = "test2"
            };
            var payment3 = new Payment
            {
                AmountToPay = 35.0m,
                CardNumber = "4234567885858858",
                Name = "test3"
            };
            var payment4 = new Payment
            {
                AmountToPay = 40.0m,
                CardNumber = "5234567885858858",
                Name = "test4"
            };
            #endregion

            CreateQueue();

            SendMessage(payment1);
            SendMessage(payment2);
            SendMessage(payment3);
            SendMessage(payment4);

            Console.ReadLine();
        }

        private static void SendMessage(Payment message)
        {
            var props = _model.CreateBasicProperties();
            props.Persistent = true;

            _model.BasicPublish("", QueueName, props, message.SerializeToBytes());
            Console.WriteLine($"WorkerQueue.Producer -- Payment Message sent : {message.CardNumber} {message.AmountToPay} {message.Name}");
        }

        private static void CreateQueue()
        {
            _factory = new ConnectionFactory() { HostName = "localhost", UserName = "guest", Password = "guest" };
            _connection = _factory.CreateConnection();
            _model = _connection.CreateModel();

            _model.QueueDeclare(QueueName, true, false, false, null);
        }
    }
}
