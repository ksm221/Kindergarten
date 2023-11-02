using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Kindergarten.Pages.Kindergartens
{
    public class IndexModel : PageModel
    {
        public List<KindergartenInfo> listKindergartens = new List<KindergartenInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=DESKTOP-OG5O3FO\\SQLEXPRESS;Initial Catalog=Kindergarten2;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Kindergartens";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                KindergartenInfo kindergarteninfo = new KindergartenInfo();
                                kindergarteninfo.Id = "" + reader.GetInt32(0);
                                kindergarteninfo.GroupName = reader.GetString(1);
                                kindergarteninfo.ChildrenCount = "" + reader.GetInt32(2);
                                kindergarteninfo.KindergartenName = reader.GetString(3);
                                kindergarteninfo.Teacher = reader.GetString(4);
                                kindergarteninfo.CreatedAt = reader.GetDateTime(5).ToString();

                                listKindergartens.Add(kindergarteninfo);
                            }
                        }                        
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }

    public class KindergartenInfo
    {
        public string Id;
        public string GroupName;
        public string ChildrenCount;
        public string KindergartenName;
        public string Teacher;
        public string CreatedAt;
    }
}
