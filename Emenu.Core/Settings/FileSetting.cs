using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emenu.Core.Settings
{
    public class FileSetting
    {
        public const string ImagePath = "/assets/images/products";
        public const string AllowedExtensions = ".jpg,.jpeg,.png";
        public const int MaxFilleSizeInMg = 1;
        public const int MaxFilleSizeInByte = MaxFilleSizeInMg * 1024 * 1024;
    }
}
