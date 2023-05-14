using Newtonsoft.Json;

public class PrintGuiaDTO
{
    public Data Data { get; set; }
}

public class Data
{
    [JsonProperty("getPrintGuia")]
    public PrintGuia PrintGuia { get; set; }
}

public class PrintGuia
{
    [JsonProperty("isError")]
    public bool IsError { get; set; }

    [JsonProperty("error")]
    public string Error { get; set; }

    [JsonProperty("filesExistent")]
    public string[] FilesExistent { get; set; }

    [JsonProperty("inCreation")]
    public InCreation[] InCreation { get; set; }
}

public class InCreation
{
    [JsonProperty("base64Data")]
    public string Base64Data { get; set; }

    [JsonProperty("error")]
    public string Error { get; set; }

    [JsonProperty("folio")]
    public string Folio { get; set; }

    [JsonProperty("isError")]
    public bool IsError { get; set; }

    [JsonProperty("message")]
    public string Message { get; set; }

    [JsonProperty("url")]
    public string Url { get; set; }
}
