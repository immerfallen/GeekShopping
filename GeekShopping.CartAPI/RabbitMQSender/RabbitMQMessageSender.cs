﻿using GeekShopping.MessageBus;
using RabbitMQ.Client;

namespace GeekShopping.CartAPI.RabbitMQSender
{
    public class RabbitMQMessageSender : IRabbitMQMessageSender
    {
        private readonly string _hostName;
        private readonly string _userName;
        private readonly string _password;
        private IConnection _connection;

        public RabbitMQMessageSender()
        {
            _hostName = "localhost";
            _userName = "guest";
            _password = "guest";
        }

        public void SendMessage(BaseMessage baseMessage, string queueName)
        {
            var factory = new ConnectionFactory() 
            { 
            HostName = _hostName,
            UserName = _userName,
            Password = _password,
            };

            _connection = factory.CreateConnection();

            using var channel = _connection.CreateModel();
            channel.QueueDeclare(queue: queueName, false, false, false, arguments: null);

            byte[] body = GetMessageAsByteArray(baseMessage);

            channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);
            
        }

        private byte[] GetMessageAsByteArray(BaseMessage baseMessage)
        {
            throw new NotImplementedException();
        }
    }
}