using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Sol_Demo.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Sol_Demo.Controllers
{
    [Produces("application/json")]
    [Route("api/demo")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        private readonly IWebHostEnvironment environment = null;

        public DemoController(IWebHostEnvironment environment)
        {
            this.environment = environment;
        }

        #region Actions

        [HttpGet("readjson")]
        public IActionResult ReadJsonFileData()
        {
            var fileList = this.FetchAllFiles();

            UserModel userModel = GetReadFiles(fileList.FirstOrDefault().FileName);

            return base.Ok(userModel);
        }

        #endregion Actions

        #region Private Method

        private List<FileModel> FetchAllFiles()
        {
            List<FileModel> filePaths =
                Directory
                .GetFiles(Path.Combine(this.environment.WebRootPath, "Files/"))
                .Select((file) => new FileModel()
                {
                    FileName = file
                })
                ?.ToList();

            return filePaths;
        }

        private UserModel GetReadFiles(string filePath)
        {
            string allText = System.IO.File.ReadAllText(filePath);

            UserModel userModel = JsonConvert.DeserializeObject<UserModel>(allText);

            return userModel;
        }

        #endregion Private Method
    }
}