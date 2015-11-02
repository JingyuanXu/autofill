// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Public License.
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// All other rights reserved.


using System;
using System.Collections.Generic;
using System.Web.Services;
using System.Data.SqlClient;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]
public class AutoComplete : WebService
{
    public AutoComplete()
    {
    }

    [WebMethod]
    public string[] GetCompletionList(string prefixText, int count)
    {
        if (count == 0)
        {
            count = 10;
        }

        if (prefixText.Equals("xyz"))
        {
            return new string[0];
        }

        Random random = new Random();
        List<string> items = new List<string>(count);
        //for (int i = 0; i < count; i++)
        //{
        //    char c1 = (char) random.Next(65, 90);
        //    char c2 = (char) random.Next(97, 122);
        //    char c3 = (char) random.Next(97, 122);

        //    items.Add(prefixText + c1 + c2 + c3);
        //}
       // List<string> items = new List<string>(count);//���� 
        string ConStr = System.Configuration.ConfigurationManager.ConnectionStrings["wyglConnectionString"].ConnectionString;

        //SqlConnection myCon = new SqlConnection("Server=192.168.1.245;uid=sa;pwd=123123;Database=use02");//���ݿ����� 
        SqlConnection myCon = new SqlConnection(ConStr);//���ݿ����� 
        myCon.Open();//�����ݿ����� 
        SqlCommand myCmd = new SqlCommand("select top " + count + " mc from use02.dbo.khb where mc like '" + prefixText + "%'group by mc order by mc ", myCon);
       SqlDataReader myDR = myCmd.ExecuteReader();
        while (myDR.Read())
        {
            items.Add(myDR["mc"].ToString());
        }
        myCon.Close();//�ر����ݿ����� 
        return items.ToArray();
      
    }
    /*  public string[] GetCompleteList(string prefixText, int count)
    {
        if (count == 0)
        {
            count = 10;
        }
        Random random = new Random();
        List<string> items = new List<string>(count);
        for (int i = 0; i < count; i++)
        {
            char c1 = (char)random.Next(65, 90);
            char c2 = (char)random.Next(97, 122);
            char c3 = (char)random.Next(97, 122);

            items.Add(prefixText + c1 + c2 + c3);
        }

        return items.ToArray();
        //List<string> items = new List<string>(count);//���� 
        //SqlConnection myCon = new SqlConnection("Server=192.168.1.245;uid=sa;pwd=123123;Database=use02");//���ݿ����� 
        //myCon.Open();//�����ݿ����� 
        //SqlCommand myCmd = new SqlCommand("select top " + count + " mc from khb where mc like '" + prefixText + "%'group by mc order by mc ", myCon);
        //SqlDataReader myDR = myCmd.ExecuteReader();
        //while (myDR.Read())
        //{
        //    items.Add(myDR["mc"].ToString());
        //}
        //myCon.Close();//�ر����ݿ����� 
        //return items.ToArray();
    } */
    
}