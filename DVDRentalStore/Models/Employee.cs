using DVDRentalStore.Config;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVDRentalStore.Models
{
	public class Employee
	{
		public static readonly string table = "employees";

		public int Id { get; private set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string City { get; set; }
		public double Salary { get; set; }

		public Employee(int id, string firstname, string lastname, string city, double salary)
		{
			this.Id = id;
			this.FirstName = firstname;
			this.LastName = lastname;
			this.City = city;
			this.Salary = salary;
		}

		public void Delete()
		{
			using (NpgsqlConnection conn = new NpgsqlConnection(Configuration.CONNECTION_STRING))
			{
				conn.Open();
				using (var command = new NpgsqlCommand($"DELETE FROM {table} WHERE employee_id = @Id", conn))
				{
					command.Parameters.AddWithValue("@Id", Id);
					command.ExecuteNonQuery();
				}
			}
		}

		public static IEnumerable<Employee> GetAll()
		{
			List<Employee> Employees = new List<Employee>();

			using (NpgsqlConnection conn = new NpgsqlConnection(Configuration.CONNECTION_STRING))
			{
				conn.Open();
				using (var command = new NpgsqlCommand($"SELECT * FROM {table}", conn))
				{
					NpgsqlDataReader reader = command.ExecuteReader();
					while (reader.Read())
						Employees.Add(new Employee((int)reader["employee_id"], (string)reader["first_name"], (string)reader["last_name"], (string)reader["city"], (float)reader["salary"]));

					if (Employees.Count() != 0)
						return Employees;
				}
			}
			return null;
		}

		public static Employee GetByID(int id)
		{
			using (NpgsqlConnection conn = new NpgsqlConnection(Configuration.CONNECTION_STRING))
			{
				conn.Open();
				using (var command = new NpgsqlCommand($"SELECT * FROM {table} WHERE employee_id = @Id", conn))
				{
					command.Parameters.AddWithValue("@Id", id);

					NpgsqlDataReader reader = command.ExecuteReader();
					if (reader.HasRows)
					{
						reader.Read();
						return new Employee(id, (string)reader["first_name"], (string)reader["last_name"], (string)reader["city"], (float)reader["salary"]);
					}
				}
			}
			return null;
		}

		public void Save()
		{
			using (NpgsqlConnection conn = new NpgsqlConnection(Configuration.CONNECTION_STRING))
			{
				conn.Open();

				using (var command = new NpgsqlCommand($"INSERT INTO {table}(employee_id, first_name, last_name, city, salary) " +
					"VALUES (@Id, @FirstName, @LastName, @City, @Salary) " +
					"ON CONFLICT (employee_id) DO UPDATE " +
					"SET first_name = @FirstName, last_name = @LastName, city = @City, salary = @Salary", conn))
				{
					command.Parameters.AddWithValue("@Id", Id);
					command.Parameters.AddWithValue("@FirstName", FirstName);
					command.Parameters.AddWithValue("@LastName", LastName);
					command.Parameters.AddWithValue("@City", City);
					command.Parameters.AddWithValue("@Salary", Salary);

					command.ExecuteNonQuery();
				}
			}
		}
	}
}
