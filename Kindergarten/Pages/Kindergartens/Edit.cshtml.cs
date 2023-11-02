using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Kindergarten.Pages.Kindergartens
{
    public class EditModel : PageModel
    {
        public KindergartenInfo kindergartenInfo = new KindergartenInfo();
        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
            string Id = Request.Query["Id"];

            try
            {
                String connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=Kindergarten2;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Kindergartens WHERE Id=@Id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("Id", Id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                kindergartenInfo.Id = "" + reader.GetInt32(0);
                                kindergartenInfo.GroupName = reader.GetString(1);
                                kindergartenInfo.ChildrenCount = "" + reader.GetInt32(2);
                                kindergartenInfo.KindergartenName = reader.GetString(3);
                                kindergartenInfo.Teacher = reader.GetString(4);
                                kindergartenInfo.UpdatedAt = reader.GetDateTime(6).ToString();


                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }
        public void OnPost()
        {
            kindergartenInfo.Id = Request.Form["Id"];
            kindergartenInfo.GroupName = Request.Form["GroupName"];
            kindergartenInfo.ChildrenCount = Request.Form["ChildrenCount"];
            kindergartenInfo.KindergartenName = Request.Form["KindergartenName"];
            kindergartenInfo.Teacher = Request.Form["Teacher"];
            kindergartenInfo.UpdatedAt = Request.Form["UpdatedAt"];

            if (kindergartenInfo.Id.Length == 0 ||
                kindergartenInfo.GroupName.Length == 0 ||
                kindergartenInfo.KindergartenName.Length == 0 ||
                kindergartenInfo.ChildrenCount.Length == 0 ||
                kindergartenInfo.Teacher.Length == 0 ||
                kindergartenInfo.UpdatedAt.Length == 0)
            {
                errorMessage = "All fields are required";
                return;
            }
            try
            {
                String connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=Kindergarten2;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE Kindergartens" +
                        " SET GroupName=@GroupName, ChildrenCount=@ChildrenCount, KindergartenName=@KindergartenName, Teacher=@Teacher, UpdatedAt=@UpdatedAt" +
                        " WHERE Id=@Id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Id", kindergartenInfo.Id);
                        command.Parameters.AddWithValue("@GroupName", kindergartenInfo.GroupName);
                        command.Parameters.AddWithValue("@ChildrenCount", kindergartenInfo.ChildrenCount);
                        command.Parameters.AddWithValue("@KindergartenName", kindergartenInfo.KindergartenName);
                        command.Parameters.AddWithValue("@Teacher", kindergartenInfo.Teacher);
                        command.Parameters.AddWithValue("@UpdatedAt", kindergartenInfo.UpdatedAt);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Kindergartens/Index");
        }
    }
}
