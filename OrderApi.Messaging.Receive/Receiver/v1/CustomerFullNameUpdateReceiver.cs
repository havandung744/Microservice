using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using OrderApi.Messaging.Receive.Options.v1;
using OrderApi.Service.v1.Models;
using OrderApi.Service.v1.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace OrderApi.Messaging.Receive.Receiver.v1
{
    public class CustomerFullNameUpdateReceiver : BackgroundService
    {
        private IModel _channel;
        private IConnection _connection;
        private readonly ICustomerNameUpdateService _customerNameUpdateService;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly string? _hostname;
        private readonly string? _queueName;
        private readonly string? _username;
        private readonly string? _password;

        public CustomerFullNameUpdateReceiver(ICustomerNameUpdateService customerNameUpdateService, IServiceScopeFactory serviceScopeFactory, IOptions<RabbitMqConfiguration> rabbitMqOptions)
        {
            _hostname = rabbitMqOptions.Value.Hostname;
            _queueName = rabbitMqOptions.Value.QueueName;
            _username = rabbitMqOptions.Value.UserName;
            _password = rabbitMqOptions.Value.Password;
            _serviceScopeFactory = serviceScopeFactory;
            _customerNameUpdateService = customerNameUpdateService;
            InitializeRabbitMqListener();
        }

        private void InitializeRabbitMqListener()
        {
            var factory = new ConnectionFactory
            {
                HostName = _hostname,
                UserName = _username,
                Password = _password
            };

            _connection = factory.CreateConnection();
            //_connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                var updateCustomerFullNameModel = JsonConvert.DeserializeObject<UpdateCustomerFullNameModel>(content);

                await HandleMessage(updateCustomerFullNameModel);

                _channel.BasicAck(ea.DeliveryTag, false);
            };
            //consumer.Shutdown += OnConsumerShutdown;
            //consumer.Registered += OnConsumerRegistered;
            //consumer.Unregistered += OnConsumerUnregistered;
            //consumer.ConsumerCancelled += OnConsumerCancelled;

            _channel.BasicConsume(_queueName, false, consumer);

            return Task.CompletedTask;
        }

        private async Task HandleMessage(UpdateCustomerFullNameModel updateCustomerFullNameModel)
        {
            //await _customerNameUpdateService.UpdateCustomerNameInOrders(updateCustomerFullNameModel);
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var customerNameUpdateService = scope.ServiceProvider.GetRequiredService<ICustomerNameUpdateService>();
                await customerNameUpdateService.UpdateCustomerNameInOrders(updateCustomerFullNameModel);
            }
        }
    }
}
