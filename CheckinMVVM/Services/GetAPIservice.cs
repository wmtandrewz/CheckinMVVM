using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CheckinMVVM.Globals;
using CheckinMVVM.Helpers;

namespace CheckinMVVM.Services
{
    public static class GetAPIservice
    {
        public static async Task<string> GetHotelCode()
        {
            try
            {
                using (HttpClient apiClient = new HttpClient())
                {
                    apiClient.BaseAddress = new Uri(Settings.BaseUri);
                    apiClient.DefaultRequestHeaders.Add("AuthToken", Constants.AccessToken);
                    apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var apiResult = await apiClient.GetAsync($"retrive/GetHotelCode");

                    if (apiResult.IsSuccessStatusCode)
                    {
                        using (HttpContent responceContent = apiResult.Content)
                        {
                            string hotelCodeResult = await responceContent.ReadAsStringAsync();

                            return hotelCodeResult;
                        }
                    }
                    else
                    {
                        return await apiResult.Content.ReadAsStringAsync();
                    }
                }
            }
            catch(Exception e)
            {
                return e.Message;
            }
        }

        public static async Task<string> GetHotelDate(string hotelCode)
        {
            try
            {
                using (HttpClient apiClient = new HttpClient())
                {
                    apiClient.BaseAddress = new Uri(Settings.BaseUri);
                    apiClient.DefaultRequestHeaders.Add("AuthToken", Constants.AccessToken);
                    apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var apiResult = await apiClient.GetAsync($"retrive/GetHotelDate?hotelCode={hotelCode}");

                    if (apiResult.IsSuccessStatusCode)
                    {
                        using (HttpContent responceContent = apiResult.Content)
                        {
                            string hotelDateResult = await responceContent.ReadAsStringAsync();
                            return hotelDateResult;
                        }
                    }
                    else
                    {
                        return await apiResult.Content.ReadAsStringAsync();
                    }
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public static async Task<string> GetReservationsList(string hotelCode,string hotelDate)
        {
            try
            {
                using (HttpClient apiClient = new HttpClient())
                {
                    apiClient.BaseAddress = new Uri(Settings.BaseUri);
                    apiClient.DefaultRequestHeaders.Add("AuthToken", Constants.AccessToken);
                    apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var apiResult = await apiClient.GetAsync($"retrive/GetReservationsList?hotelCode={hotelCode}&hotelDate={hotelDate}");

                    if (apiResult.IsSuccessStatusCode)
                    {
                        using (HttpContent responceContent = apiResult.Content)
                        {
                            string reservationsResult = await responceContent.ReadAsStringAsync();
                            return reservationsResult;
                        }
                    }
                    else
                    {
                        return await apiResult.Content.ReadAsStringAsync();
                    }
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
