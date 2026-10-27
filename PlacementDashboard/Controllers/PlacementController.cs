using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Mvc;
using PlacementDashboard.Models;

namespace PlacementDashboard.Controllers
{
    public class PlacementController : Controller
    {
        string connectionString =
            @"Data Source=prince\sqlexpress;Initial Catalog=PlacementDB;Integrated Security=True";


        // GET: Placement
        public ActionResult Index()
        {
            List<Placement> placements = new List<Placement>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT
                        s.StudentId,
                        s.EnrollmentNo,
                        s.StudentName,
                        s.Department,
                        s.PassingYear,
                        s.IsPlaced,
                        p.CompanyName,
                        p.JobRole,
                        p.PackageLPA
                    FROM Students s
                    LEFT JOIN Placements p
                    ON s.StudentId = p.StudentId";

                SqlCommand cmd = new SqlCommand(query, con);

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Placement placement = new Placement();

                    placement.StudentId =
                        (int)reader["StudentId"];

                    placement.EnrollmentNo =
                        reader["EnrollmentNo"].ToString();

                    placement.StudentName =
                        reader["StudentName"].ToString();

                    placement.Department =
                        reader["Department"].ToString();

                    placement.PassingYear =
                        (int)reader["PassingYear"];

                    placement.IsPlaced =
                        (bool)reader["IsPlaced"];

                    if (reader["CompanyName"] != System.DBNull.Value)
                    {
                        placement.CompanyName =
                            reader["CompanyName"].ToString();
                    }

                    if (reader["JobRole"] != System.DBNull.Value)
                    {
                        placement.JobRole =
                            reader["JobRole"].ToString();
                    }

                    if (reader["PackageLPA"] != System.DBNull.Value)
                    {
                        placement.PackageLPA =
                            (decimal)reader["PackageLPA"];
                    }

                    placements.Add(placement);
                }
            }

            return View(placements);
        }
    }
}