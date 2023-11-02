using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Kindergarten.Pages.Kindergartens
{
    public class CreateModel : PageModel
    {
        public KindergartenInfo kindergartenInfo = new KindergartenInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            kindergartenInfo.GroupName = Request.Form["GroupName"];
            kindergartenInfo.ChildrenCount = Request.Form["ChildrenCount"];
            kindergartenInfo.KindergartenName = Request.Form["KindergartenName"];
            kindergartenInfo.Teacher = Request.Form["Teacher"];

            if (kindergartenInfo.GroupName.Length == 0 || kindergartenInfo.KindergartenName.Length == 0 ||
                kindergartenInfo.ChildrenCount.Length == 0 || kindergartenInfo.Teacher.Length == 0)
            {
                errorMessage = "All fields are required";
                return;
            }

            try
            {
                String connectionString = "Data Source=DESKTOP-OG5O3FO\\SQLEXPRESS;Initial Catalog=Kindergarten2;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO Kindergartens " +
                        "(GroupName, ChildrenCount, KindergartenName, Teacher) VALUES" +
                        "(@GroupName, @ChildrenCount, @KindergartenName, @Teacher);";

                    using(SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@GroupName", kindergartenInfo.GroupName);
                        command.Parameters.AddWithValue("@ChildrenCount", kindergartenInfo.ChildrenCount);
                        command.Parameters.AddWithValue("@KindergartenName", kindergartenInfo.KindergartenName);
                        command.Parameters.AddWithValue("@Teacher", kindergartenInfo.Teacher);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            //save new kindergarten to db
            kindergartenInfo.GroupName = ""; kindergartenInfo.ChildrenCount = ""; kindergartenInfo.KindergartenName = ""; kindergartenInfo.Teacher = "";
            successMessage = "New Kindergarten successfully added!";

            Response.Redirect("/Kindergartens/Index");
        }
    }
}
