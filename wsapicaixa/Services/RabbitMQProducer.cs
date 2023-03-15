using RabbitMQ.Client;
using System.Text;
using Newtonsoft.Json;

namespace wsapicaixa.Services;

public class RabbitMQProducer : IMessageProducer
{
    public void SendMessage<T>(T message)
    {
        var factory = new ConnectionFactory { HostName = "192.168.18.19", UserName= "wscaixa", Password = "wscaixa" , Port = 5674 };
        var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
        channel.ExchangeDeclare("fornecedore_ex", "direct", true);

        var json = JsonConvert.SerializeObject(message);
        var body = Encoding.UTF8.GetBytes(json);

        channel.BasicPublish(exchange: "fornecedore_ex", routingKey: "orders", body: body);

        Task.Delay(100);

        channel.Close();
        connection.Close();
    }
}
