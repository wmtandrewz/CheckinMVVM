using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using CheckinMVVM.Globals;
using CheckinMVVM.Helpers;
using CheckinMVVM.Models.Payloads;
using Newtonsoft.Json;

namespace CheckinMVVM.Services
{
    public static class PostAPIservice
    {
        public static async Task<string> CreateUpdateGuest(GuestPayloadModel guestModel)
        {
            try
            {
                var payload = JsonConvert.SerializeObject(guestModel);

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("AuthToken", Constants.AccessToken);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = await client.PostAsync(Settings.BaseUri + "guest/post/createUpdateGuest", new StringContent(payload, Encoding.UTF8, "application/json"));
                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        return await response.Content.ReadAsStringAsync();
                    }
                }

            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

        public static async Task<string> AssignRoom(RoomPayloadModel roomPayload)
        {
            try
            {
                var payload = JsonConvert.SerializeObject(roomPayload);

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("AuthToken", Constants.AccessToken);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = await client.PostAsync(Settings.BaseUri + "posting/AssignRoom", new StringContent(payload, Encoding.UTF8, "application/json"));
                    if (response.IsSuccessStatusCode)
                    {
                        return "success";
                    }
                    else
                    {
                        return await response.Content.ReadAsStringAsync();
                    }
                }

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
