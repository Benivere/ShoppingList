using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using ShoppingList.Application.Api.CommandHandlers;
using ShoppingList.Application.Api.Sql;
using ShoppingList.Application.Common;
using ShoppingList.Application.DatabaseModel;

namespace ShoppingList.Application.Api
{
    public class PictureApiController : ApiController
    {
        [Route("api/PictureApi/GetById/")]
        [HttpGet]
        public HttpResponseMessage GetById(int id)
        {
            var picture = new PictureSqlQueries().GetById(id);
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(new MemoryStream(picture.PictureContent))
            };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpg");
            return response;
        }

        [Route("api/PictureApi/AddUpdate/")]
        [HttpPut]
        public async Task<HttpResponseMessage> Put()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            var uploadPath = HttpContext.Current.Server.MapPath("~/App_Data");
            var provider = new MultipartFormDataStreamProvider(uploadPath);
            var uploadedPictures = new List<Picture>();

            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);

                foreach (var file in provider.FileData)
                {
                    var picture = new Picture
                    {
                        Id = 0,
                        PictureContent = File.ReadAllBytes(file.LocalFileName)
                    };
                    File.Delete(file.LocalFileName);

                    var updatedPicture = new PictureTransitionHandler().AddUpdate(picture);
                    uploadedPictures.Add(updatedPicture);
                }
            }
            catch (Exception e)
            {
                if (e is IOException && e.InnerException is HttpException) // e.InnerException.WebEventCode == 3004
                {
                    if (e.InnerException.Message == "Maximum request length exceeded.") // e.InnerException.WebEventCode == 3004
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.RequestEntityTooLarge, "Image file is too large");
                    }

                    return Request.CreateErrorResponse(HttpStatusCode.UnsupportedMediaType, e);
                }

                // ReSharper disable once InvertIf
                var exception = e as VerificationFailedException;
                if (exception != null)
                {
                    // ReSharper disable once UseObjectOrCollectionInitializer
                    var validationErrors = new List<KeyValuePair<string, string>>();
                    validationErrors.Add(new KeyValuePair<string, string>("ValidationException", exception.GetType().ToString()));
                    validationErrors.Add(new KeyValuePair<string, string>("ValidationMessage", exception.Message));
                    // ReSharper disable once LoopCanBeConvertedToQuery
                    foreach (var failedValidation in exception.FailedVerifications)
                    {
                        validationErrors.Add(new KeyValuePair<string, string>("Validation", failedValidation.ErrorMessageOnFailure));
                    }
                    var response = Request.CreateResponse(HttpStatusCode.ExpectationFailed, validationErrors);
                    return response;
                }

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }

            var pictureToReturn = new Picture()
            {
                Id = uploadedPictures[0].Id
            };
            return Request.CreateResponse(HttpStatusCode.OK, pictureToReturn);
        }

        [Route("api/PictureApi/Delete/")]
        [HttpDelete]
        public bool Delete(int id)
        {
            return new PictureTransitionHandler().Delete(id);
        }
    }
}
