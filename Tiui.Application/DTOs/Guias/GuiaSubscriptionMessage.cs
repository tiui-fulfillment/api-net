using System.Net.WebSockets;

public class SubscriptionMessage
{
    public string Type { get; set; }
    public string Payload { get; set; }
}

public class SubscriptionMessageGuiaInfo
{
    public string Type { get; set; }
    public GuiaInfoSuscription Payload { get; set; }
}

