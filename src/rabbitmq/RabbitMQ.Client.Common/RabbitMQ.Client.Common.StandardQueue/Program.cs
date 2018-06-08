using RabbitMQ.Client.Common.Models;
using System;

namespace RabbitMQ.Client.Common.StandardQueue
{
    class Program
    {    
        private static ConnectionFactory _factory;
        private static IConnection _connection;
        private static IModel _model;

        private const string QueueName = "StandardQueue_Example1";
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
            var payment5 = new Payment
            {
                AmountToPay = 45.0m,
                CardNumber = "6234567885858858",
                Name = "test5"
            };
            var payment6 = new Payment
            {
                AmountToPay = 50.0m,
                CardNumber = "7234567885858858",
                Name = "test6"
            };
            var payment7 = new Payment
            {
                AmountToPay = 55.0m,
                CardNumber = "8234567885858858",
                Name = "test7"
            };
            var payment8 = new Payment
            {
                AmountToPay = 60.0m,
                CardNumber = "9234567885858858",
                Name = "test8"
            };
            var payment9 = new Payment
            {
                AmountToPay = 65.0m,
                CardNumber = "10234567885858858",
                Name = "test9"
            };
            var payment10 = new Payment
            {
                AmountToPay = 70.0m,
                CardNumber = "11234567885858858",
                Name = "test10"
            };
            #endregion

            CreateQueue();

            SendMessage(payment1);
            SendMessage(payment2);
            SendMessage(payment3);
            SendMessage(payment4);
            SendMessage(payment5);
            SendMessage(payment6);
            SendMessage(payment7);
            SendMessage(payment8);
            SendMessage(payment9);
            SendMessage(payment10);

            Receive();
        }

        private static void Receive()
        {
            //find the number of messages in the queue
            var msgcount = GetMessageCount(_model, QueueName);
            var count = 0;
            //if there are any messages in the queue, proceed to process the messages
            while (count < msgcount)
            {
                // get one message at a time and do not send ack 
                var result = _model.BasicGet(QueueName, false);
                // retrive the message
                var message = (Payment)result.Body.DeSerializeToObject(typeof(Payment));
                // log/record the message
                Console.WriteLine($"--Received {message.CardNumber} : {message.AmountToPay} : {message.Name}");
                //now ack the message
                _model.BasicAck(result.DeliveryTag, false);

                count++;
            }
        }

        private static long GetMessageCount(IModel model, string queueName)
        {
            // idempotent operation - to get the contents of the queue
            var results = model.QueueDeclare(queueName, true, false, false, null);

            Console.WriteLine($"Message count is {results.MessageCount}");
            return results.MessageCount;
        }

        private static void SendMessage(Payment message)
        {
            _model.BasicPublish("", QueueName, null, message.SerializeToBytes());
            Console.WriteLine($"[X] Payment Message sent : {message.CardNumber} {message.AmountToPay} {message.Name}");
        }

        private static void CreateQueue()
        {
            _factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };
            _connection = _factory.CreateConnection();
            _model = _connection.CreateModel();
            _model.QueueDeclare(QueueName, true, false, false, null);
        }
    }    
}
