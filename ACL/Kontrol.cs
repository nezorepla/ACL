using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace ACL
{
    public class Kontrol
    {

      

        public static Boolean IsNumeric(String s)
        {
            float f;

            return float.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out f);
            //float.TryParse(s, out output);
        }
  




      public static Boolean IsNull(String t)
        {
            bool rv;

            if (t == null)
            { rv = true; }
            else { rv = false; }

            return rv;
        }
        public static string MimeType(string Extension)
        {
            string mime = "application/octetstream";
            if (string.IsNullOrEmpty(Extension))
                return mime;
            string ext = Extension.ToLower();
            Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
            if (rk != null && rk.GetValue("Content Type") != null)
                mime = rk.GetValue("Content Type").ToString();
            return mime;
        }


  


        public static string KillAllDBConn()
        {
            string Rv = "Use Master Go Declare @dbname sysname;  Set @dbname ='CCOPS'; Declare @spid int; Select @spid =min(spid) from master.dbo.sysprocesses where dbid = db_id(@dbname)   While @spid is not NULL  Begin Execute ('Kill ' + @spid)  Select @spid = min(spid) from master.dbo.sysprocesses where dbid =db_id(@dbname) and spid > @spid End Print 'All connection DROPPED!' ";


            return Rv;
        }












    }
}