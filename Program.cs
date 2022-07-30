using CSharpConsoleHttpRequestDemo.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace CSharpConsoleHttpRequestDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Connecting!");
            CallWebAPIAsync().Wait();
            Console.WriteLine("Finished!");
        }

        static async Task CallWebAPIAsync()
        {
            using (var client = new HttpClient())
            {
                //Send HTTP requests from here to https://dog.ceo/api/breeds/image/random.

                client.BaseAddress = new Uri("https://crudcrud.com/api/e397d7d0d60e4c86a0a231632a6ea592");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                
                //GET Method  
                HttpResponseMessage responseFromGETRequest = await client.GetAsync("dogs");
                if (responseFromGETRequest.IsSuccessStatusCode)
                {
                    List<DogPicture> obj = await responseFromGETRequest.Content.ReadFromJsonAsync<List<DogPicture>>();
                    foreach(DogPicture objd in obj)
                      Console.WriteLine("Dog picture is {0} and its status is {1}", objd.message, objd.status);
                }
                else
                {
                    Console.WriteLine("Internal server Error");
                }
                
                //POST Method
                var dogPicture = new DogPicture() { message = "Blado, blado, blado", status = "OK" };
                HttpResponseMessage responseFromPOSTRequest = await client.PostAsJsonAsync("dogs/", dogPicture);
                if (responseFromPOSTRequest.IsSuccessStatusCode)
                {
                    // Get the URI of the created resource.  
                    Uri returnUrl = responseFromPOSTRequest.Headers.Location;
                    Console.WriteLine(returnUrl);
                }
                else
                {
                    Console.WriteLine("Internal server Error");
                }

                //PUT Method
                string msg = "610ce2c2dc46c203e8b3b087";
                var anotherDogPicture = new DogPicture() { message = "Bla, bla, bla", status = "success" };
                HttpResponseMessage responseFromPUTRequest = await client.PutAsJsonAsync("dogs/" + msg, anotherDogPicture);
                if (responseFromPUTRequest.IsSuccessStatusCode)
                {
                    Console.WriteLine("Success");
                }
                else
                {
                    Console.WriteLine("Internal server Error");
                }

                //DELETE Method  
                string mesg = "610ce19ddc46c203e8b3b086";
                HttpResponseMessage responseFromDELETERequest = await client.DeleteAsync("dogs/" + mesg);
                if (responseFromDELETERequest.IsSuccessStatusCode)
                {
                    Console.WriteLine("Success");
                }
                else
                {
                    Console.WriteLine("Internal server Error");
                }
                
            }
        }
    }
}
