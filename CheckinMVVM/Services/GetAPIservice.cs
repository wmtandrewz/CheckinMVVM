using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CheckinMVVM.Globals;
using CheckinMVVM.Helpers;
using Newtonsoft.Json.Linq;

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

        public static async Task<string> GetReservationDetails(string hotelCode, string reservationId)
        {
            try
            {
                using (HttpClient apiClient = new HttpClient())
                {
                    apiClient.BaseAddress = new Uri(Settings.BaseUri);
                    apiClient.DefaultRequestHeaders.Add("AuthToken", Constants.AccessToken);
                    apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var apiResult = await apiClient.GetAsync($"retrive/GetReservationDetails?hotelCode={hotelCode}&reservationId={reservationId}");

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

        public static async Task<string> GetRoomStatus(string hotelCode, string roomNumber)
        {
            try
            {
                using (HttpClient apiClient = new HttpClient())
                {
                    apiClient.BaseAddress = new Uri(Settings.BaseUri);
                    apiClient.DefaultRequestHeaders.Add("AuthToken", Constants.AccessToken);
                    apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var apiResult = await apiClient.GetAsync($"retrive/GetRoomsStatus?hotelCode={hotelCode}&roomNumber={roomNumber}");

                    if (apiResult.IsSuccessStatusCode)
                    {
                        using (HttpContent responceContent = apiResult.Content)
                        {
                            string roomStatusResult = await responceContent.ReadAsStringAsync();
                            return roomStatusResult;
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

        public static async Task<string> GetRoomDetails(string hotelCode,string reservationId, string roomStatusType)
        {
            try
            {
                using (HttpClient apiClient = new HttpClient())
                {
                    apiClient.BaseAddress = new Uri(Settings.BaseUri);
                    apiClient.DefaultRequestHeaders.Add("AuthToken", Constants.AccessToken);
                    apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var apiResult = await apiClient.GetAsync($"retrive/GetRoomsDetails?hotelCode={hotelCode}&reservationId={reservationId}&roomStatus={roomStatusType}");

                    if (apiResult.IsSuccessStatusCode)
                    {
                        using (HttpContent responceContent = apiResult.Content)
                        {
                            string roomList = await responceContent.ReadAsStringAsync();
                            return roomList;
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

        public static async Task<string> FindGuestByID(string hotelCode, string identificationMethod, string identificationNumber)
        {
            try
            {
                using (HttpClient apiClient = new HttpClient())
                {
                    apiClient.BaseAddress = new Uri(Settings.BaseUri);
                    apiClient.DefaultRequestHeaders.Add("AuthToken", Constants.AccessToken);
                    apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var apiResult = await apiClient.GetAsync($"retrive/GetGuestDetailsByIDNUmber?hotelCode={hotelCode}&identificationMethod={identificationMethod}&identificationNumber={identificationNumber}");

                    if (apiResult.IsSuccessStatusCode)
                    {
                        using (HttpContent responceContent = apiResult.Content)
                        {
                            string roomList = await responceContent.ReadAsStringAsync();
                            return roomList;
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
