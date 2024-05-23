using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Business.Exceptions
{
    public class ImageFileNotFoundException : Exception
    {
        public ImageFileNotFoundException(string? message) : base(message)
        {

        }
    }
}
