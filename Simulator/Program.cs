// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;
using Simulator;

var listListCustomer = new List<List<Customer>>();
listListCustomer.Add(new List<Customer>() { new Customer() { firstName = "Leia", lastName = "Harrison", age = 25, id = 1 }, new Customer() { firstName = "Lukas", lastName = "Anderson", age = 28, id = 2 } });
listListCustomer.Add(new List<Customer>() { new Customer() { firstName = "Tomas", lastName = "Ronan", age = 25, id = 3 }, new Customer() { firstName = "Sara", lastName = "Chan", age = 19, id = 4 } });
listListCustomer.Add(new List<Customer>() { new Customer() { firstName = "Dewey", lastName = "Ray", age = 25, id = 5 }, new Customer() { firstName = "Dewey", lastName = "Drew", age = 18, id = 6 }, new Customer() { firstName = "Tomas", lastName = "Larsen", age = 25, id = 7 }, new Customer() { firstName = "Joel", lastName = "Powell", age = 25, id = 8 } });
listListCustomer.Add(new List<Customer>() { new Customer() { firstName = "Lukas", lastName = "Liberty", age = 57, id = 9 }, new Customer() { firstName = "Carlos", lastName = "Lane", age = 25, id = 10 }, new Customer() { firstName = "Jose", lastName = "Harrison", age = 82, id = 11 } });
listListCustomer.Add(new List<Customer>() { new Customer() { firstName = "Frank", lastName = "Powell", age = 25, id = 12 }, new Customer() { firstName = "Sadie", lastName = "Larsen", age = 24, id = 13 }, new Customer() { firstName = "Leia", lastName = "Drew", age = 25, id = 14 } });

var url = "https://localhost:44355/Customer";
HttpClient client = new HttpClient();
var requests = new List<HttpRequestMessage>();
requests.Add(new HttpRequestMessage(HttpMethod.Get, url));
foreach (var listCustomer in listListCustomer)
{
    var postRequest = new HttpRequestMessage(HttpMethod.Post, url);
    var body = JsonConvert.SerializeObject(listCustomer);
    var content = new StringContent(body, null, "application/json");
    postRequest.Content = content;
    requests.Add(postRequest);
}
requests.Add(new HttpRequestMessage(HttpMethod.Get, url));

var tasks = new List<Task<HttpResponseMessage>>();
foreach (var request in requests)
{
    tasks.Add(client.SendAsync(request));
}

await Task.WhenAll(tasks);

// Get and print the responses
foreach (var task in tasks)
{
    HttpResponseMessage response = await task;
    if (response.RequestMessage.Method == HttpMethod.Post)
    {
        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine(response.StatusCode);
        }
        else
        {
            Console.WriteLine(await response.Content.ReadAsStringAsync());
        }
    }
    else
    {
        Console.WriteLine(await response.Content.ReadAsStringAsync());
    }
}
