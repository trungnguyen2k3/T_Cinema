using CinemaBE.Dtos.Images;
using CinemaBE.Models;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;

namespace CinemaBE.Areas.User.Controllers
{
    [Area("User")]
    [ApiController]
    [Route("api/image")]
    public class ImageController : Controller
    {
        private readonly DatabaseContext _db;
        private readonly Cloudinary _cloudinary;
        public ImageController(DatabaseContext db, Cloudinary cloudinary)
        {
            _db = db;
            _cloudinary = cloudinary;
        }
        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm] UploadImageRequest request)
        {
            try
            {
                if (request.File == null || request.File.Length == 0)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Chưa chọn file ảnh"
                    });
                }

                if (string.IsNullOrWhiteSpace(request.TableName))
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "TableName không được để trống"
                    });
                }

                var extension = Path.GetExtension(request.File.FileName).ToLowerInvariant();
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp", ".gif" };

                if (!allowedExtensions.Contains(extension))
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Chỉ cho phép upload file ảnh"
                    });
                }

                await using var stream = request.File.OpenReadStream();

                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(request.File.FileName, stream),
                    Folder = $"cinemabe/{request.TableName}",
                    UseFilename = true,
                    UniqueFilename = true,
                    Overwrite = false
                };

                var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                if (uploadResult.Error != null)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = uploadResult.Error.Message
                    });
                }

                var image = new TblImage
                {
                    TableName = request.TableName,
                    ObjectId = request.ObjectId,
                    Path = uploadResult.SecureUrl?.ToString() ?? "",
                    Name = string.IsNullOrWhiteSpace(request.Name)
                        ? request.File.FileName
                        : request.Name,
                    SortOrder = request.SortOrder
                };

                _db.TblImages.Add(image);
                await _db.SaveChangesAsync();

                return Ok(new
                {
                    success = true,
                    message = "Upload ảnh thành công",
                    id = image.Id,
                    path = image.Path,
                    name = image.Name,
                    tableName = image.TableName,
                    objectId = image.ObjectId,
                    sortOrder = image.SortOrder,
                    publicId = uploadResult.PublicId
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }
    }
}
