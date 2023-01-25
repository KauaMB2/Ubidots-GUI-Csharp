using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ubidots{
    public class commands{
        static string TOKEN="BBFF-VxU3d3ooEv0ZbJFYvUbiOSimw4i2rY";
        public static void PostDATA(Dictionary<string,int> payload){
            // Creates the headers for the HTTP requests
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://industrial.api.ubidots.com");//Get the url
            client.DefaultRequestHeaders.Add("X-Auth-Token", TOKEN);//Informing the TOKEN
            StringContent content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");//Convert the JSON data in a binary data and inform that the data that will be send is in JSON format

            // Makes the HTTP requests
            int status = 400;
            int attempts = 0;
            while (status >= 400 && attempts <= 5){
                if(attempts>=1){
                    Console.WriteLine($"[ERRO] Tentando novamente... {attempts}");
                }
                string DEVICE="teste";
                string url = $"/api/v1.6/devices/{DEVICE}";//LINK to Post
                var result = client.PostAsync(url, content).Result;//Post the info in Ubidots
                status = (int)result.StatusCode;//Return the requestion code
                Console.WriteLine($"Response code:{status}");
                attempts += 1;
                Thread.Sleep(1000);//Wait 1 second
            }
            Console.WriteLine("[INFO] Solicitação feita corretamente, seu device está atualizado");
            return;
        }
        public static async Task<string[]> GetDATA(){//Define a async function that will return a double var
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("X-Auth-Token", TOKEN);//Inform the TOKEN
            string[] vars={"velocidade","aceleracao","umidade","temperatura"};
            HttpResponseMessage[] data=new HttpResponseMessage[4];
            string[] dataFinal=new string[4];
            for(int i=0;i<4;i++){
                data[i] = await client.GetAsync($"https://industrial.api.ubidots.com/api/v1.6/devices/teste/{vars[i]}/lv");//Link to get
                if(data[i].IsSuccessStatusCode){//If the request was good
                    dataFinal[i]=await data[i].Content.ReadAsStringAsync();
                }
            }
            return dataFinal;
        }
    }
}

