using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;
using System.Globalization;


namespace WeatherClassLibrary
{
    public class GlobalWeatherDetails
    {
        public void WeatherDLL()
        {
            //string TodayDate = DateTime.Today.ToShortDateString();
            string Baltimore, NY, NJ, DU, SJ, IN, London, Boston, SouthPort, Halifax, ErrorMessage;

            WeatherRefrence.GlobalWeather weatherObject = new WeatherRefrence.GlobalWeather();

            string xmlBaltimore = weatherObject.GetWeather("Baltimore", "United States");
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlBaltimore);
            XmlNode tempNode = xmlDoc.SelectSingleNode("CurrentWeather/Temperature");
            Baltimore = tempNode.InnerText;

            string xmlNewYork = weatherObject.GetWeather("New York", "United States");
            XmlDocument xmlDocNY = new XmlDocument();
            xmlDocNY.LoadXml(xmlNewYork);
            XmlNode tempNodeNY = xmlDocNY.SelectSingleNode("CurrentWeather/Temperature");
            NY = tempNodeNY.InnerText;

            string xmlNewJersey = weatherObject.GetWeather("Morristown", "United States");
            XmlDocument xmlDocNJ = new XmlDocument();
            xmlDocNJ.LoadXml(xmlNewJersey);
            XmlNode tempNodeNJ = xmlDocNJ.SelectSingleNode("CurrentWeather/Temperature");
            NJ = tempNodeNJ.InnerText;

            string xmlDublin = weatherObject.GetWeather("Dublin", "Ireland");
            XmlDocument xmlDocDU = new XmlDocument();
            xmlDocDU.LoadXml(xmlDublin);
            XmlNode tempNodeDU = xmlDocDU.SelectSingleNode("CurrentWeather/Temperature");
            DU = tempNodeDU.InnerText;

            string xmlSanJose = weatherObject.GetWeather("San Jose", "United States");
            XmlDocument xmlDocSJ = new XmlDocument();
            xmlDocSJ.LoadXml(xmlSanJose);
            XmlNode tempNodeSJ = xmlDocSJ.SelectSingleNode("CurrentWeather/Temperature");
            SJ = tempNodeSJ.InnerText;

            string xmlPune = weatherObject.GetWeather("Bombay", "India");
            XmlDocument xmlDocIN = new XmlDocument();
            xmlDocIN.LoadXml(xmlPune);
            XmlNode tempNodeIN = xmlDocIN.SelectSingleNode("CurrentWeather/Temperature");
            IN = tempNodeIN.InnerText;

            string xmlLondon = weatherObject.GetWeather("London / Heathrow Airport", "United Kingdom");
            XmlDocument xmlDocUK = new XmlDocument();
            xmlDocUK.LoadXml(xmlLondon);
            XmlNode tempNodeUK = xmlDocUK.SelectSingleNode("CurrentWeather/Temperature");
            London = tempNodeUK.InnerText;

            string xmlBoston = weatherObject.GetWeather("BOSTON", "United States");
            XmlDocument xmlDocBO = new XmlDocument();
            xmlDocBO.LoadXml(xmlBoston);
            XmlNode tempNodeBO = xmlDocBO.SelectSingleNode("CurrentWeather/Temperature");
            Boston = tempNodeBO.InnerText;

            string xmlLiverpool = weatherObject.GetWeather("Liverpool", "United Kingdom");
            XmlDocument xmlDocLI = new XmlDocument();
            xmlDocLI.LoadXml(xmlLiverpool);
            XmlNode tempNodeLI = xmlDocLI.SelectSingleNode("CurrentWeather/Temperature");
            SouthPort = tempNodeLI.InnerText;

            string xmlHA = weatherObject.GetWeather("Halifax", "Canada");
            XmlDocument xmlDocHA = new XmlDocument();
            xmlDocHA.LoadXml(xmlHA);
            XmlNode tempNodeHA = xmlDocHA.SelectSingleNode("CurrentWeather/Temperature");
            Halifax = tempNodeHA.InnerText;

            try
            {

                string ConnectionString = "Data Source=HIB-PUNEsp2010\\sharepoint;Initial Catalog=CRMFeedback;Persist Security Info=True;User ID=sa;Password=reset@123";

                SqlConnection sqlcon = new SqlConnection(ConnectionString);
                sqlcon.Open();

                SqlCommand sqlcmd = new SqlCommand();
                sqlcmd.Connection = sqlcon;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.CommandText = "Insert_WeatherForecast";
                sqlcmd.Parameters.Add("@Baltimore", SqlDbType.NVarChar, 255).Value = Baltimore;
                sqlcmd.Parameters.Add("@Boston", SqlDbType.NVarChar, 255).Value = Boston;
                sqlcmd.Parameters.Add("@Dublin", SqlDbType.NVarChar, 255).Value = DU;
                sqlcmd.Parameters.Add("@Halifax", SqlDbType.NVarChar, 255).Value = Halifax;
                sqlcmd.Parameters.Add("@London", SqlDbType.NVarChar, 255).Value = London;
                sqlcmd.Parameters.Add("@New_Jersey", SqlDbType.NVarChar, 255).Value = NJ;
                sqlcmd.Parameters.Add("@New_York", SqlDbType.NVarChar, 255).Value = NY;
                sqlcmd.Parameters.Add("@Pune", SqlDbType.NVarChar, 255).Value = IN;
                sqlcmd.Parameters.Add("@SanJose", SqlDbType.NVarChar, 255).Value = SJ;
                sqlcmd.Parameters.Add("@Southport_LI", SqlDbType.NVarChar, 255).Value = SouthPort;
                //sqlcmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = TodayDate;
                sqlcmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 255).Value = "Pankaj Sapkal";

                sqlcmd.Parameters.Add("@Retout", SqlDbType.Int).Direction = ParameterDirection.Output;

                sqlcmd.ExecuteNonQuery();

                int RetOut = Convert.ToInt32(sqlcmd.Parameters["@Retout"].Value.ToString());
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message.ToString();
            }
        }
    }
}
