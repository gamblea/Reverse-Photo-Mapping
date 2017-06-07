using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.Drawing;

namespace Reverse_Geotag
{
    static class ExtensionClasses
    {
        public static Bitmap SetMetaValue(this Bitmap SourceBitmap, MetaProperty property, string value)
        {
            PropertyItem prop = SourceBitmap.PropertyItems[0];

            int iLen = value.Length + 1;
            byte[] bTxt = new Byte[iLen];
            for (int i = 0; i < iLen - 1; i++)
                bTxt[i] = (byte)value[i];
            bTxt[iLen - 1] = 0x00;
            prop.Id = (int)property;
            prop.Type = 2;
            prop.Value = bTxt;
            prop.Len = iLen;
            return SourceBitmap;
        }

        public static string GetMetaValue(this Bitmap SourceBitmap, MetaProperty property)
        {
            PropertyItem[] propItems = SourceBitmap.PropertyItems;
            var prop = propItems.FirstOrDefault(p => p.Id == (int)property);
            if (prop != null)
            {
                return Encoding.UTF8.GetString(prop.Value);
            }
            else
            {
                return null;
            }
        }

    }
}
