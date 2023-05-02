using Microsoft.Net.Http.Headers;
using Myhotel.Objects.Models;
using Myhotel.Objects.Views;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Myhotel.Services;

public class HouseService
{
    private readonly HttpClient httpClient;
    private string basePath = "https://localhost:7120/api/House";
    private string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjQ5ZTEyZTY0LTFhZWItNDRlYi1hODZkLTY0ZTNiOTAyZjNkMSIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6ImNyZWF0b3IiLCJleHAiOjE2ODMwNDM3NjYsImlzcyI6IlNoYWJsb24udXoiLCJhdWQiOiJTaGFibG9uQXBpIn0.IZLmvN1vmq8OUiz-LYM9QPQ7Vbur4yEu6vxxm54kU38";
    public HouseService(HttpClient httpClient)
    {
        this.httpClient = httpClient;

    }

    public async Task<List<HouseView>> GetHouseViewsAsync()
    {

        httpClient.DefaultRequestHeaders.Clear();
        httpClient.DefaultRequestHeaders.Add(HeaderNames.Authorization, token);
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        httpClient.BaseAddress = new Uri(basePath);
        httpClient.Timeout = TimeSpan.FromSeconds(60);

        HttpResponseMessage response = await httpClient.GetAsync("/all");
        response.EnsureSuccessStatusCode();

        string json = await response.Content.ReadAsStringAsync();
        var HouseViews = JsonConvert.DeserializeObject<List<HouseView>>(json);
        return HouseViews;
    }

    public async Task<HouseView> GetHouseViewAsync(int id)
    {
        var HouseView = await httpClient.GetFromJsonAsync<HouseView>(basePath);
        return HouseView;
    }

    public async Task<HttpResponseMessage> AddHouseViewAsync(CreateHouseDto createHouseDto)
    {
        var response = await httpClient.PostAsJsonAsync(basePath, createHouseDto);
        return response;
    }

    public async Task<HttpResponseMessage> UpdateHouseViewAsync(UpdateHouseDto updateHouseDto)
    {
        var response = await httpClient.PutAsJsonAsync(basePath, updateHouseDto);
        return response;
    }

    public async Task<HttpResponseMessage> DeleteHouseViewAsync(Guid id)
    {
        var response = await httpClient.DeleteAsync(basePath);
        return response;
    }
}