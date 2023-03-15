namespace wsapicaixa.Services;

public interface IMessageProducer
{
    void SendMessage<T>(T message);

}
