using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ZennoLab.Api.Data;
using ZennoLab.Api.Model;

namespace ZennoLab.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataSetController : ControllerBase
    {
        private readonly DataSetUploadOptions options;
        private readonly SqlContext context;
        private readonly IMapper mapper;

        public DataSetController(DataSetUploadOptions dataSetUploadOptions, SqlContext context, IMapper mapper)
        {
            this.options = dataSetUploadOptions;
            this.context = context;
            this.mapper = mapper;
            Directory.CreateDirectory(options.PathFileFolder);
        }

        [HttpPost]
        [Route("upload")]
        [RequestFormLimits(MultipartBodyLengthLimit = 209715200)]
        public async Task<IActionResult> Upload([FromForm] DataSet dataSet)
        {
            if (!ModelState.IsValid)            
                return BadRequest(ModelState);

            var uploadedFile = dataSet.Archive;
            var fileName = Path.GetRandomFileName();
            var saveToPath = Path.Combine(options.PathFileFolder, fileName);
            await SaveFile(saveToPath, uploadedFile);

            var entity = mapper.Map<DataSetEntity>(dataSet);
            entity.ArchivePath = fileName;
            context.Add(entity);
            await context.SaveChangesAsync();

            return Ok();
        }

        private async Task SaveFile(string path, IFormFile file)
        {
            using var fileStream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(fileStream);
        }
    }
}
