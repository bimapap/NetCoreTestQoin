using AutoMapper;
using NetCoreTest.BusinessLogic;
using NetCoreTest.DataAccess.Context.Models;
using NetCoreTest.DataAccess.ViewModel;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreTest.Helper
{
    public class RabbitMqHelper
    {
        public IConnection _connection;
        public IModel _channel;
        private readonly Test01BL BusinessLogic;
        public readonly string QueueName = "qtest1";
        public readonly string ExchangeName = "exchange.qtest1";

        public RabbitMqHelper()
        {
            InitRabbitMQ();
            BusinessLogic = new Test01BL();
        }

        private void InitRabbitMQ()
        {
            _connection = InitializeConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(ExchangeName, ExchangeType.Direct);
            _channel.QueueDeclare(QueueName, true, false, false, null);
            _channel.QueueBind(QueueName, ExchangeName, "directexchange_key");
            _channel.BasicQos(0, 1, false);
        }
        private IConnection InitializeConnection()
        {
            ConnectionFactory ConnectionFactory = new ConnectionFactory()
            {
                HostName = "localhost",
                Port = 5672,
                UserName = "guest",
                Password = "guest"
            };
            return ConnectionFactory.CreateConnection();
        }
        public void Dispose()
        {
            _channel.Close();
            _connection.Close();
        }

        public void HandleMessage(WorkerServiceModel Model)
        {
            var Config = new MapperConfiguration(cfg => cfg.CreateMap<Test01ViewModel, Test01>());
            var Data = new Mapper(Config).Map<Test01>(Model.Data);

            if (Model.Command == "add")
            {
                BusinessLogic.Create(Data);
            }
            else if (Model.Command == "update")
            {
                BusinessLogic.Update(Data);
            }
            else if (Model.Command == "delete")
            {
                BusinessLogic.Delete(Data.Id);
            }
        }

        public void SendMessage(WorkerServiceModel message) 
        {
            _channel.QueueDeclare(QueueName, true, false, false);

            var properties = _channel.CreateBasicProperties();
            properties.Persistent = false;

            var output = JsonConvert.SerializeObject(message);
            _channel.BasicPublish(string.Empty, QueueName, null, Encoding.UTF8.GetBytes(output));
        }

    }
}
