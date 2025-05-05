
using Blazored.LocalStorage;
using HemDotNetBlazorClient.Services.Base;

namespace HemDotNetBlazorClient.Services
{
    public class PropertyImageService : BaseHttpService, IPropertyImageService
    {
        private readonly IClient _client;

        public PropertyImageService(ILocalStorageService localStorage, IClient client)
            : base(localStorage, client)
        {
            _client = client;
        }

        public async Task<Response<List<PropertyImageDto>>> GetPropertyImages(int marketPropertyId)
        {
            Response<List<PropertyImageDto>> response;

            try
            {
                await GetBearerToken();

                var data = await _client.PropertyImageAllAsync(marketPropertyId);
                
                response = new Response<List<PropertyImageDto>>
                {
                    Data = data.ToList(),
                    Success = true
                };
            }
            catch (ApiException ex)
            {
                response = ConvertApiExceptions<List<PropertyImageDto>>(ex);
            }

            return response;
        }

        public async Task<Response<PropertyImageDto>> AddPropertyImage(int marketPropertyId, StreamContent fileContent)
        {
            Response<PropertyImageDto> response;

            try
            {
                await GetBearerToken();

                
                var formData = new MultipartFormDataContent();

                formData.Add(new StringContent(marketPropertyId.ToString()), "MarketPropertyId");

                formData.Add(fileContent, "imageFile", "image" + Path.GetExtension(fileContent.Headers.ContentDisposition?.FileName ?? ".jpg"));

                var fileParameter = new FileParameter(await fileContent.ReadAsStreamAsync(), "image.jpg", fileContent.Headers.ContentType?.MediaType);
                var data = await _client.PropertyImagePOSTAsync(marketPropertyId, fileParameter);

                response = new Response<PropertyImageDto>
                {
                    Data = data,
                    Success = true
                };
            }
            catch (ApiException ex)
            {
                response = ConvertApiExceptions<PropertyImageDto>(ex);
            }

            return response;
        }

        public async Task<Response<bool>> DeletePropertyImage(int imageId)
        {
            Response<bool> response;

            try
            {
                await GetBearerToken();

                await _client.PropertyImageDELETEAsync(imageId);

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