Console.WriteLine("Please click Enter");
Console.ReadKey();

var requestUri = "https://localhost:3000/api/patients/postpatients";
HttpClient client = new HttpClient();

var request = new HttpRequestMessage(HttpMethod.Post, requestUri);
var response = client.Send(request);

Console.WriteLine(response?.EnsureSuccessStatusCode());

Console.WriteLine("The patients are loaded");
