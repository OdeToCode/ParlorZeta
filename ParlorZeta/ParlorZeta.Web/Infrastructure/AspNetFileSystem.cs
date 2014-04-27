using System.Collections.Generic;
using System.IO;
using System.Web;
using ParlorZeta.Azure.FileSystem;

namespace ParlorZeta.Web.Infrastructure
{
    public class AspNetFileSystem : IFileSystem
    {
        private readonly HttpContext _context;

        public AspNetFileSystem(HttpContext context)
        {
            _context = context;
        }

        public Stream OpenWritableFileStream(string relativePath, string fileName)
        {
            var destination = Path.Combine(_context.Server.MapPath("~/"), relativePath, fileName);            
            return File.OpenWrite(destination);
        }

        public IEnumerable<string> GetFileNames(string relativePath)
        {
            var destination = Path.Combine(_context.Server.MapPath("~/"), relativePath);
            if (!Directory.Exists(destination))
            {
                Directory.CreateDirectory(destination);
            }
            return Directory.GetFiles(destination);
        }

        public void Delete(string fileName)
        {
            File.Delete(fileName);
        }
    }
}