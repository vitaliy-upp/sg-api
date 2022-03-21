using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Domain.BusinessLogic.Settings;
using Domain.BusinessLogic.ServiceInterfaces;
using FileManagement.Utilities.AzureBlob;

namespace NoLimitTech.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IUserService _userApplicationService;
        private readonly ILogger<ImagesController> _logger;

        private readonly ImageSettings _imageSettings;
        private readonly BlobStorageSettings _blobStorageSettings;

        public ImagesController(IUserService userApplicationService
            , IConfiguration configuration
            , ILogger<ImagesController> logger)
        {
            _userApplicationService = userApplicationService;
            _logger = logger;

            _blobStorageSettings = configuration.GetSection(nameof(BlobStorageSettings)).Get<BlobStorageSettings>();
            _imageSettings = configuration.GetSection(nameof(ImageSettings)).Get<ImageSettings>();
        }

        ///// <summary>
        ///// Upload a user image
        ///// </summary>
        ///// <param name="model">Upload Image Model</param>
        ///// <returns>Image path</returns>
        //[HttpPost]
        //public async Task<IActionResult> Post([FromForm] UploadImageModel model)
        //{
        //    var eventUser = _eventUserApplicationService.FindDetailedByIdentity(User.Identity);
        //    string imgName = "";
        //    try
        //    {
        //        imgName = ImageUtils.GenerateName(model.Image);
        //        await StorageUtils.UploadFileToStorage(model.Image.OpenReadStream(), imgName, _blobStorageSettings);
        //        // Updating image of online user
        //        _userApplicationService.UpdateUserOnlineImage(eventUser.Id, imgName);
        //        // Updating image of registered user
        //        if (eventUser.UserId.HasValue)
        //        { _userApplicationService.UpdateUserImage(eventUser.UserId.Value, imgName); }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error ocurred during saving an image");
        //        return Problem(ex.Message, null, 500, "Error ocurred during saving an image", null);
        //    }
            
        //    return Ok(Path.Combine(_imageSettings.Path, imgName));
        //}

    }
}
