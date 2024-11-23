using ApiApplication1.Commands;
using ApiApplication1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Xml;

namespace ApiApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class XmlContentsController : ControllerBase
    {
        [HttpPost("Upload")]
        public IActionResult Upload(string xmlContent)
        {
            using var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(xmlContent));
            var xmlReader = XmlReader.Create(memoryStream);

            var xmlDocument = new XmlDocument();
            xmlDocument.XmlResolver = new XmlUrlResolver();
            //xmlDocument.LoadXml(xmlContent);
            xmlDocument.Load(xmlReader);

            return Ok(xmlDocument.InnerText);  
        }
    }
}
