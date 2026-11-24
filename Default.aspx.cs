
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace PlacementDashboard
{
    public partial class Default : System.Web.UI.Page
    {
        string cs = ConfigurationManager
            .ConnectionStrings["PlacementDB"]
            .ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDashboard();
                BindStudents();
                BindCompanies();
            }
        }

        // Display dashboard statistics
        private void BindDashboard()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();

                string query = @"
                    SELECT
                    COUNT(*) AS Total,
                    SUM(CASE WHEN PlacementStatus = 'Placed'
                        THEN 1 ELSE 0 END) AS Placed,
                    SUM(CASE WHEN PlacementStatus = 'Not Placed'
                        THEN 1 ELSE 0 END) AS NotPlaced
                    FROM Students";

                using (SqlCommand cmd = new SqlCommand(query, con))
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        int total = Convert.ToInt32(dr["Total"]);
                        int placed = Convert.ToInt32(dr["Placed"]);
                        int notPlaced = Convert.ToInt32(dr["NotPlaced"]);

                        lblTotal.Text = total.ToString();
                        lblPlaced.Text = placed.ToString();
                        lblNotPlaced.Text = notPlaced.ToString();

                        double percentage = total > 0
                            ? (placed * 100.0) / total
                            : 0;

                        lblPercentage.Text =
                            percentage.ToString("0.00") + "%";
                    }
                }
            }
        }

        // Display all student records using GridView
        private void BindStudents()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "SELECT * FROM Students";

                SqlDataAdapter da =
                    new SqlDataAdapter(query, con);

                DataTable dt = new DataTable();
                da.Fill(dt);

                gvStudents.DataSource = dt;
                gvStudents.DataBind();
            }
        }

        // Display selected student using DetailsView
        protected void gvStudents_SelectedIndexChanged(
            object sender, EventArgs e)
        {
            int id = Convert.ToInt32(
                gvStudents.SelectedDataKey.Value);

            using (SqlConnection con = new SqlConnection(cs))
            {
                string query =
                    "SELECT * FROM Students WHERE StudentID=@id";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", id);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                da.Fill(dt);

                dvStudent.DataSource = dt;
                dvStudent.DataBind();
            }
        }

        // Display company information using Repeater
        private void BindCompanies()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = @"
                    SELECT CompanyName,
                    COUNT(*) AS TotalStudents
                    FROM Students
                    WHERE PlacementStatus = 'Placed'
                    GROUP BY CompanyName";

                SqlDataAdapter da =
                    new SqlDataAdapter(query, con);

                DataTable dt = new DataTable();
                da.Fill(dt);

                rptCompanies.DataSource = dt;
                rptCompanies.DataBind();
            }
        }
    }
}
