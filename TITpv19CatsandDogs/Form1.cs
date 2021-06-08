using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TITpv19CatsandDogs
{
    public partial class Form1 : Form
    {
        string connectionString;
        SqlConnection connection;

        public Form1()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["TITpv19CatsandDogs.Properties.Settings.PetsConnectionString"].ConnectionString;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PopulatePetTypeTable();
        }

        private void PopulatePetTypeTable()
        {
            using (connection = new SqlConnection(connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter("Select * from PetType", connection))
            {
                DataTable petTypeTable = new DataTable();
                adapter.Fill(petTypeTable);

                types.DisplayMember = "PetTypeName";
                types.ValueMember = "id";
                types.DataSource = petTypeTable;


            }

        }

        private void types_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulatePetNames();
        }


        private void PopulatePetNames()
        {
            string query = "Select Pet.Name, PetType.PetTypeName from Pet INNER JOIN PetType ON Pet.TypeId = PetType.Id Where PetType.Id = @Typeid";
            using(connection = new SqlConnection(connectionString))
            using(SqlCommand command = new SqlCommand(query, connection))
            using(SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                command.Parameters.AddWithValue("@Typeid", types.SelectedValue);
                DataTable petNameTable = new DataTable();
                adapter.Fill(petNameTable);

                names.DisplayMember = "Name";
                names.ValueMember = "Id";
                names.DataSource = petNameTable;

            }
        }
    }
}
