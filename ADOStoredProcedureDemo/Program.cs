using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;

internal class Program
{
    static string connectionstring = "data source=LAPTOP-BQF0DTHQ\\SQLEXPRESS;initial catalog=samp2024;integrated security=true;";
    private static void Main(string[] args)
    {
        Display();
       // Insert();
       // Update();
        Console.WriteLine("Enter the year after which the number of students need to be found:");
        int year = Convert.ToInt32(Console.ReadLine());
        int count = CountStudent(year);
        Console.WriteLine($"Number of students joined after {year}: {count}");
        
    }
    public static void Display()
    {
        using (SqlConnection con = new SqlConnection(connectionstring))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("spGetAll", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataReader sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {
                Console.WriteLine(sdr["id"] + " " + sdr["name"] + " " + sdr["email"] + " " + sdr["join_date"]);
            }
        }
    }
    public static void Insert()
    {
        using (SqlConnection con = new SqlConnection(connectionstring))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("spAddStudent", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            Console.WriteLine("Enter Student id:");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Student Name and Email:");
            string? name = Console.ReadLine();
            string? email = Console.ReadLine();
            Console.WriteLine("Enter the joining date:");
            DateTime date = DateTime.Parse(Console.ReadLine()??string.Empty);
            cmd.Parameters.AddWithValue("@stid", id);
            cmd.Parameters.AddWithValue("@stname", name);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@join_date", date);

            cmd.ExecuteNonQuery();
            Console.WriteLine("REcord Inserted");
        }
    }
    public static void Update()
    {
        using (SqlConnection con = new SqlConnection(connectionstring))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("spUpdateStudent", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            Console.WriteLine("Enter Student id which need to be updated:");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Student Name and Email:");
            string name = Console.ReadLine() ?? string.Empty;
            string email = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Enter the joining date:");
            DateTime date = DateTime.Parse(Console.ReadLine() ?? string.Empty);
            cmd.Parameters.AddWithValue("@stid", id);
            cmd.Parameters.AddWithValue("@stname", name);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@join_date", date);

            cmd.ExecuteNonQuery();
            Console.WriteLine("REcord Updated");

        }
    }
    public static int CountStudent(int year)
    {
        using (SqlConnection con = new SqlConnection(connectionstring))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("spCountStudent", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            //Input Parameter
            cmd.Parameters.AddWithValue("@year", year);
          //  cmd.Parameters.Add(new SqlParameter("@year", year));
            //Output Parameter
            SqlParameter outputParameter = new SqlParameter
            {
                ParameterName = "@count",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(outputParameter);
            cmd.ExecuteNonQuery();
            int count = (int)outputParameter.Value;
            return count;
        }
    }        
    
}