using RestSharp;

public class RestSharpConnector
{
    const string baseUrl = "https://api.exchangeratesapi.io/v1/";
    RestClient client = new RestClient(baseUrl);

    public IRestResponse SendRequest(string requestUrl)
    {
        RestRequest request = new RestRequest(requestUrl, Method.GET);
        request.RequestFormat = DataFormat.Json;
        IRestResponse response = client.Execute(request);
        return response;
    }
}
