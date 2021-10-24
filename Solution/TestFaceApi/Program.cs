using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace TestFaceApi
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var imagePath = @"oscars-2021-red-carpet-r-1619395051043.jpg";
            var urlAddress = "http://localhost:3000/api/faces";
            ImageUtility imUtil = new();
            var bytes = await imUtil.ConvertToByteArrayAsync(imagePath);
            var faceList = new List<byte[]>();
            ByteArrayContent byteContent = new(bytes);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            using (HttpClient httpClient = new())
            {
                using (var response = await httpClient.PostAsync(urlAddress, byteContent))
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var apiResponse = await response.Content.ReadAsStringAsync();
                        faceList = JsonConvert.DeserializeObject<List<byte[]>>(apiResponse);
                    }
                }
            }
            if (faceList.Count > 0)
            {
                for (var i = 0; i < faceList.Count; i++)
                {
                    imUtil.FromBytesToImage(faceList[i], "face" + i);
                }
            }
        }
    }
}
