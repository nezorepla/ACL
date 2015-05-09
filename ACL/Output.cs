using System;

using System.Collections.Generic;

using System.Linq;

using System.Text;

using System.Data;

using System.Text.RegularExpressions;



namespace ACL
{

    public class Output
    {

        /// <summary>

        /// Executes a command object. Use SQLException to check any error occured during execution.

        /// </summary>
        /// <param name="id">tablonun Id'si</param>
        /// <param name="css">tablonun Css Class'ı</param>

        public static string HTMLTableString(DataTable dt, string id, string css)
        {
            // string strM = "EXEC KKBSITE_SP_FINANSAL " + ViewState["base"].ToString();
            String RVl = "";
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("<table id=\"" + id + "\" border=\"0\" cellpadding=\"0\" cellspacing=\"1\"  class=\"" + css + "\"  ><thead><tr>");
                foreach (DataColumn c in dt.Columns)
                {
                    sb.AppendFormat("<th>{0}</th>", c.ColumnName);
                }
                sb.AppendLine("</tr></thead><tbody>");
             
                foreach (DataRow dr in dt.Rows)
                {
                    sb.Append("<tr>"); foreach (object o in dr.ItemArray)
                    {
                        sb.AppendFormat("<td>{0}</td>", o.ToString());
                        //System.Web.HttpUtility.HtmlEncode());
                    } sb.AppendLine("</tr>");
                } sb.AppendLine("</tbody></table>");
                RVl = sb.ToString();
            }
            catch (Exception ex)
            {
                RVl = "HATA @ConvertDataTable2HTMLString: " + ex;//  Page.ClientScript.RegisterStartupScript(typeof(Page), "bisey3", "alert('bunu Alper Özen e gönderiniz\n" + strM + "\n" + ex.ToString() + "');", true);
            }
            return RVl;
        }

        /// <summary>

        /// Executes a command object. Use SQLException to check any error occured during execution.

        /// </summary>

        public static string HTMLTableString(DataTable dt)
        {
            // string strM = "EXEC KKBSITE_SP_FINANSAL " + ViewState["base"].ToString();
            String RVl = "";
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"1\" ><thead><tr>");
                foreach (DataColumn c in dt.Columns)
                {
                    sb.AppendFormat("<th>{0}</th>", c.ColumnName);
                }
                sb.AppendLine("</tr></thead><tbody>");
           
                foreach (DataRow dr in dt.Rows)
                {
                    sb.Append("<tr>"); foreach (object o in dr.ItemArray)
                    {
                        sb.AppendFormat("<td>{0}</td>", o.ToString());
                        //System.Web.HttpUtility.HtmlEncode());
                    } sb.AppendLine("</tr>");
                } sb.AppendLine("</tbody></table>");
                RVl = sb.ToString();
            }
            catch (Exception ex)
            {
                RVl = "HATA @ConvertDataTable2HTMLString: " + ex;//  Page.ClientScript.RegisterStartupScript(typeof(Page), "bisey3", "alert('bunu Alper Özen e gönderiniz\n" + strM + "\n" + ex.ToString() + "');", true);
            }
            return RVl;
        }
        public static DataTable HTMLTransposedTable(DataTable inputTable)
        {
            DataTable outputTable = new DataTable();
            // Add columns by looping rows
            // Header row's first column is same as in inputTable
            outputTable.Columns.Add(inputTable.Columns[0].ColumnName.ToString());
            // Header row's second column onwards, 'inputTable's first column taken
            foreach (DataRow inRow in inputTable.Rows)
            {
                string newColName = inRow[0].ToString();
                outputTable.Columns.Add(newColName);
            }
            // Add rows by looping columns       
            for (int rCount = 1; rCount <= inputTable.Columns.Count - 1; rCount++)
            {
                DataRow newRow = outputTable.NewRow();
                // First column is inputTable's Header row's second column
                newRow[0] = inputTable.Columns[rCount].ColumnName.ToString();
                for (int cCount = 0; cCount <= inputTable.Rows.Count - 1; cCount++)
                {
                    string colValue = inputTable.Rows[cCount][rCount].ToString();
                    newRow[cCount + 1] = colValue;
                }
                outputTable.Rows.Add(newRow);
            }
            return outputTable;

        }



        public static string RGX(String inputString)
        {

            // Regex regex = new Regex(@"(\.|[a-z]|[A-Z]|[0-9])*@(\.|[a-z]|[A-Z]|[0-9])*");

            Regex regex = new Regex(@"(\.|[a-zA-Z0-9_.+-])*@(\.|[a-z]|[A-Z]|[0-9])*");

            //   Regex regex = new Regex(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$");

            foreach (Match match in regex.Matches(inputString))
            {



                string val = match.Value;

                // match.Value == "xx@yahoo.com.my"

                // string name = match.Groups[1]; // "xx"

                // string domain = match.Groups[2]; // "yahoo.com.my"

                int end = val.IndexOf('@');

                string name = val.Substring(0, end);





                inputString = inputString.Replace(val, "<a href=\"mailto:" + val + "\">" + name + "</a>");

            }



            /*     return inputString;

 

             String pattern = @"(\S*)@\S*\.\S*";

             String output = Regex.Replace(inputString, pattern, "<a href=#>$1</a>");

 

     */



            // string output = Regex.Match(inputString, @"gwtHash:""(.*?)""").Value;

            return inputString;

        }

        public static string JSONString(DataTable Dt)
        {



            string[] StrDc = new string[Dt.Columns.Count];

            string HeadStr = string.Empty;

            StringBuilder Sb = new StringBuilder();

            if (Dt.Rows.Count > 0)
            {

                if (Dt.Columns.Count > 0)
                {

                    for (int i = 0; i < Dt.Columns.Count; i++)
                    {



                        StrDc[i] = Dt.Columns[i].Caption;



                        HeadStr += "\"" + StrDc[i] + "\" : \"" + StrDc[i] + i.ToString() + "¾" + "\",";

                    }



                    HeadStr = HeadStr.Substring(0, HeadStr.Length - 1);





                    // Sb.Append("{\"" + Dt.TableName + "\" : [");

                    Sb.Append("[");



                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {



                        string TempStr = HeadStr;

                        Sb.Append("{");



                        for (int j = 0; j < Dt.Columns.Count; j++)
                        {



                            TempStr = TempStr.Replace("<br>", Environment.NewLine).Replace(Dt.Columns[j] + j.ToString() + "¾", Dt.Rows[i][j].ToString());

                        }



                        Sb.Append(TempStr + "},");

                    }



                    Sb = new StringBuilder(Sb.ToString().Substring(0, Sb.ToString().Length - 1));

                    //Sb.Append("]}");

                    Sb.Append("]");

                }

                else
                {

                    Sb.Append("[]");

                }

            }

            else { Sb.Append("[]"); }

            return Sb.ToString();

        }

        public static string ToFormattedThousands(string number)
        {

            string val = "0";

            if (Kontrol.IsNumeric(number))//!String.IsNullOrEmpty(number))
            {

                decimal d = decimal.Parse(number);

                val = d.ToString("N0");//0.00");





                // val = Convert.Decimal(number).ToString("N0");

            }

            else
            {

                val = number;

            }

            return val;



        }

        public static string vericek(string StrData, string StrBas, string StrSon)
        {

            try
            {

                int IntBas = StrData.IndexOf(StrBas) + StrBas.Length;

                int IntSon = StrData.IndexOf(StrSon, IntBas + 1);

                return StrData.Substring(IntBas, IntSon - IntBas);

            }

            catch
            {

                return "";

            }

        }

        public static string turkce(string format)
        {

            /*

            format = format.Replace("İ", "I");

            format = format.Replace("Ş", "S");

            format = format.Replace("Ç", "C");

            format = format.Replace("Ö", "O");

            format = format.Replace("Ü", "U");

            format = format.Replace("Ğ", "G");

            format = format.Replace("ı", "i");

            format = format.Replace("ş", "s");

            format = format.Replace("ç", "c");

            format = format.Replace("ö", "o");

            format = format.Replace("ü", "u");

            format = format.Replace("ğ", "g");

            //format = Replace("%D6", "*") 'ö

 

            format = format.Replace("%FC", "u");

            //ü

            format = format.Replace("%dc", "u");

            //ü

            format = format.Replace("%c7", "c");

            //ç

            format = format.Replace("%d0", "g");

            //ğ

            format = format.Replace("&#252;", "u");

            format = format.Replace("&#220;", "U");

            format = format.Replace("&#304;", "I");

            format = format.Replace("&#305;", "i");

            format = format.Replace("&#351;", "s");

            format = format.Replace("&#350;", "S");

            format = format.Replace("&#199;", "C");

            format = format.Replace("&#231;", "c");

            format = format.Replace("&#287;", "g");

            format = format.Replace("&#214;", "O");

            format = format.Replace("&#246;", "o");

 

 

            format = format.Replace("Åz", "S");

            format = format.Replace("Åz", "S");

            format = format.Replace("Å°", "S");

    

            format = format.Replace("ÄY", "g");

            format = format.Replace("ÄŸ", "g");

 

            format = format.Replace("Äz", "G");

 


            format = format.Replace("Ã§", "c");

            format = format.Replace("a§", "c");

            format = format.Replace("Ã‡", "C");

 

            format = format.Replace("Ã?", "C");

            format = format.Replace("a‡", "C");

 

            format = format.Replace("Ã¶", "o");

            format = format.Replace("Ã¶s", "O");

            format = format.Replace("Ã–", "O");

 

            format = format.Replace("Ã¼", "u");

            format = format.Replace("Ão", "U");

            format = format.Replace("Ãœ", "U");

             */

            format = format.Replace("Ä", "Ğ");

            format = format.Replace("Å", "Ş");

            format = format.Replace("Ã–", "Ö");

            format = format.Replace("Ãœ", "Ü");

            format = format.Replace("Ã‡", "Ç");

            format = format.Replace("Ä°", "İ");

            format = format.Replace("Ä±", "ı");

            format = format.Replace("ÅŸ", "ş");

            //format = Replace("},{", "}, ")





            return format;

        }





        //private static  string GenerateUL(DataRow[] menu, DataTable table, StringBuilder sb)

        //{

        //    sb.AppendLine("<ul>");



        //    if (menu.Length > 0) {

        //        foreach (DataRow dr in menu) {

        //            //Dim handler As String = dr("Handler").ToString()

        //            string menuText = dr["NameA"].ToString();

        //            string line = String.Format("<li><a href=\"#\">{0}</a>", menuText);

        //            sb.Append(line);



        //            string pid = dr["Code"].ToString();



        //            DataRow[] subMenu = table.Select(String.Format("ParentId = {0}", pid));

        //            if (subMenu.Length > 0) {

        //                StringBuilder subMenuBuilder = new StringBuilder();

        //                sb.Append(GenerateUL(subMenu, table, subMenuBuilder));

        //            }

        //            sb.Append("</li>");

        //        }

        //    }



        //    sb.Append("</ul>");

        //    return sb.ToString();

        //}



        public static string makeul(DataTable dt, string css)
        {



            DataSet ds = new DataSet();

            ds.Tables.Add(dt);



            DataTable table = ds.Tables[0];

            DataRow[] parentMenus = table.Select("PID = 0");



            StringBuilder sb = new StringBuilder();

            string unorderedList = GenerateUL(parentMenus, table, sb, css);

            return unorderedList;

        }

        public static string makeul(DataTable dt)
        {



            DataSet ds = new DataSet();

            ds.Tables.Add(dt);



            DataTable table = ds.Tables[0];

            DataRow[] parentMenus = table.Select("PID = 0");



            StringBuilder sb = new StringBuilder();

            string unorderedList = GenerateUL(parentMenus, table, sb);

            return unorderedList;

        }

        private static string GenerateUL(DataRow[] menu, DataTable table, StringBuilder sb)
        {

            sb.AppendLine("<ul>");



            if (menu.Length > 0)
            {

                foreach (DataRow dr in menu)
                {

                    string handler = dr["HANDLER"].ToString();

                    string menuText = dr["MENU"].ToString();

                    string line = String.Format(@"<li><a href=""{0}"">{1}</a>", handler, menuText);

                    sb.Append(line);



                    string pid = dr["ID"].ToString();

                    string parentId = dr["PID"].ToString();



                    DataRow[] subMenu = table.Select(String.Format("PID = {0}", pid));

                    if (subMenu.Length > 0 && !pid.Equals(parentId))
                    {

                        var subMenuBuilder = new StringBuilder();

                        sb.Append(GenerateUL(subMenu, table, subMenuBuilder));

                    }

                    sb.Append("</li>");

                }

            }

            sb.Append("</ul>");

            return sb.ToString();

        }



        private static string GenerateUL(DataRow[] menu, DataTable table, StringBuilder sb, string css)
        {

            sb.AppendLine("<ul class=\"" + css + "\">");



            if (menu.Length > 0)
            {

                foreach (DataRow dr in menu)
                {

                    string handler = dr["HANDLER"].ToString();

                    string menuText = dr["MENU"].ToString();

                    string line = String.Format(@"<li><a href=""{0}"">{1}</a>", handler, menuText);

                    sb.Append(line);



                    string pid = dr["ID"].ToString();

                    string parentId = dr["PID"].ToString();



                    DataRow[] subMenu = table.Select(String.Format("PID = {0}", pid));

                    if (subMenu.Length > 0 && !pid.Equals(parentId))
                    {

                        var subMenuBuilder = new StringBuilder();

                        sb.Append(GenerateUL(subMenu, table, subMenuBuilder, ""));

                    }

                    sb.Append("</li>");

                }

            }

            sb.Append("</ul>");

            return sb.ToString();

        }







    }





}




