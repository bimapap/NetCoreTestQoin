using AutoMapper;
using NetCoreTest.BusinessLogic;
using NetCoreTest.DataAccess.Context.Models;
using NetCoreTest.DataAccess.ViewModel;
using NetCoreTest.Helper;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace NetCoreTest2
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly RabbitMqHelper Helper; 

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
            Helper = new RabbitMqHelper();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            var _channel = Helper._channel;
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ch, ea) =>
            {
                try
                {
                    var content = System.Text.Encoding.UTF8.GetString(ea.Body.ToArray());
                    var data = JsonConvert.DeserializeObject<WorkerServiceModel>(content);
                    Helper.HandleMessage(data);
                    _logger.LogInformation($"Process data with command = {data.Command} & Id = {data.Data.Id}");
                    _channel.BasicAck(ea.DeliveryTag, false);
                }
                catch (Exception Ex)
                {
                    _logger.LogError(Ex.Message);
                }
            };
            _channel.BasicConsume(Helper.QueueName, false, consumer);
            await Task.CompletedTask;
        }

        public override void Dispose()
        {
            Helper.Dispose();
            base.Dispose();
        }
    }
}