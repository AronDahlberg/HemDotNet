using Blazored.LocalStorage;
using HemDotNetBlazorClient.Services.Base;

namespace HemDotNetBlazorClient.Services
{
    // Allan
    public class RealEstateAgencyService : BaseHttpService, IRealEstateAgencyService
    {
        private readonly IClient _client;

        public RealEstateAgencyService(ILocalStorageService localStorage, IClient client)
            : base(localStorage, client)
        {
            _client = client;
        }

        // Author: Allan
        public async Task<Response<List<AgencyNameDto>>> GetAllAgencies()
        {
            Response<List<AgencyNameDto>> response;

            try
            {
                var data = await _client.RealEstateAgencyAsync();
                response = new Response<List<AgencyNameDto>>
                {
                    Data = data.ToList(),
                    Success = true
                };
            }
            catch (ApiException ex)
            {
                response = ConvertApiExceptions<List<AgencyNameDto>>(ex);
            }

            return response;
        }
        
        // Author: Allan
        public async Task<Response<List<AgencyDto>>> GetAllAgenciesPartial()
        {
            Response<List<AgencyDto>> response;

            try
            {
                var data = await _client.PartialAgenciesAsync();
                response = new Response<List<AgencyDto>>
                {
                    Data = data.ToList(),
                    Success = true
                };
            }
            catch (ApiException ex)
            {
                response = ConvertApiExceptions<List<AgencyDto>>(ex);
            }

            return response;
        }

        
        
        // Allan
        public async Task<Response<int>> CreateAgency(AgencyCreateDto dto)
        {
            Response<int> response;

            try
            {
                await GetBearerToken();

                var data = await _client.CreateAgencyAsync(dto);
                response = new Response<int>
                {
                    Data = data,
                    Success = true
                };
            }
            catch (ApiException ex)
            {
                response = ConvertApiExceptions<int>(ex);
            }

            return response;
        }
        
        // Allan
        public async Task<Response<AgencyImageUrlDto>> UploadAgencyImage(int agencyId, StreamContent fileContent)
        {
            Response<AgencyImageUrlDto> response;

            try
            {
                await GetBearerToken();

                var formData = new MultipartFormDataContent();

                formData.Add(new StringContent(agencyId.ToString()), "AgencyId");

                formData.Add(fileContent, "imageFile", "image" + Path.GetExtension(fileContent.Headers.ContentDisposition?.FileName ?? ".jpg"));
                var fileParameter = new FileParameter(await fileContent.ReadAsStreamAsync(), "image.jpg", fileContent.Headers.ContentType?.MediaType);

                AgencyImageUrlDto dto = await _client.AgencyImageAsync(agencyId, fileParameter);

                response = new Response<AgencyImageUrlDto>
                {
                    Data = dto,
                    Success = true
                };
            }
            catch (ApiException ex)
            {
                response = ConvertApiExceptions<AgencyImageUrlDto>(ex);
            }

            return response;
        }
        
        // Allan
        public async Task<Response<bool>> DeleteAgency(int id)
        {
            Response<bool> response;

            try
            {
                await GetBearerToken();

                await _client.DeleteAsync(id);
                response = new Response<bool>
                {
                    Data = true,
                    Success = true
                };
            }
            catch (ApiException ex)
            {
                response = ConvertApiExceptions<bool>(ex);
            }

            return response;
        }
    }
}
