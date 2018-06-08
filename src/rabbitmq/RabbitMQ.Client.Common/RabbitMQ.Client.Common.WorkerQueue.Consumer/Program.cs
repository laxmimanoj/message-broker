using RabbitMQ.Client.Common.Models;
using RabbitMQ.Client.Events;
using System;

namespace RabbitMQ.Client.Common.WorkerQueue.Consumer
{
    class Program
    {
        private const string QueueName = "WorkerQueue_Example";
        private static IConnectionFactory _factory;
        private static IConnection _connection;
        private static IModel _model;

        static void Main(string[] args)
        {
            _factory = new ConnectionFactory() { HostName = "localhost", UserName = "guest", Password = "guest" };

            using (_connection = _factory.CreateConnection())
            {
                using (_model = _connection.CreateModel())
                {
                    _model.QueueDeclare(QueueName, true, false, false, null);
                    _model.BasicQos(0, 1, false);
                    Console.WriteLine("[x] Waiting for messages..");

                    var consumer = new EventingBasicConsumer(_model);
                    consumer.Received += (model, ea) =>{
                        var message = (Payment) ea.Body.DeSerializeToObject(typeof(Payment));
                        Console.WriteLine($"WorkerQueue.Consumer.{ea.DeliveryTag} -- Payment Message received : {message.CardNumber} {message.AmountToPay} {message.Name}");

                        _model.BasicAck(ea.DeliveryTag, false);
                        Console.WriteLine($"WorkerQueue.Consumer.{ea.DeliveryTag} -- Acknowledged.");
                    };

                    _model.BasicConsume(QueueName, false, consumer);
                    Console.ReadLine();
                }
            }
        }       
    }
}
