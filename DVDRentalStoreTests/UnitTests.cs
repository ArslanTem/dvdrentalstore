using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DVDRentalStore.Models;
using Npgsql;
using System.Linq;

namespace DVDRentalStoreTests
{
	[TestClass]
	public class UnitTests
	{
		[TestMethod]
		public void TestActor()
		{
			Assert.IsNull(Actor.GetByID(1000));
			Assert.IsNotNull(Actor.GetByID(1));
			Assert.IsNotNull(Actor.GetAll());

			Actor item = Actor.GetByID(1);
			string field = item.FirstName;
			item.FirstName += "1";
			item.Save();

			Assert.AreEqual(field + "1", item.FirstName);
		}

		[TestMethod]
		public void TestClient()
		{
			Assert.IsNull(Client.GetByID(1000));
			Assert.IsNotNull(Client.GetByID(1));
			Assert.IsNotNull(Client.GetAll());

			Client item = Client.GetByID(1);
			string field = item.FirstName;
			item.FirstName += "1";
			item.Save();

			Assert.AreEqual(field + "1", item.FirstName);
		}

		[TestMethod]
		public void TestCopy()
		{
			Assert.IsNull(Copy.GetByID(1000));
			Assert.IsNotNull(Copy.GetByID(1));
			Assert.IsNotNull(Copy.GetAll());

			Copy item = Copy.GetByID(1);
			bool field = item.Available;
			item.Available = item.Available == true ? false : true;
			item.Save();

			Assert.AreEqual(field == true ? false : true, item.Available);
		}

		[TestMethod]
		public void TestEmployee()
		{
			Assert.IsNull(Employee.GetByID(1000));
			Assert.IsNotNull(Employee.GetByID(1));
			Assert.IsNotNull(Employee.GetAll());

			Employee item = Employee.GetByID(1);
			string field = item.FirstName;
			item.FirstName += "1";
			item.Save();

			Assert.AreEqual(field + "1", item.FirstName);
		}

		[TestMethod]
		public void TestMovie()
		{
			Assert.IsNull(Movie.GetByID(1000));
			Assert.IsNotNull(Movie.GetByID(1));
			Assert.IsNotNull(Movie.GetAll());

			Movie item = Movie.GetByID(1);
			string field = item.Title;
			item.Title += "1";
			item.Save();

			Assert.AreEqual(field + "1", item.Title);
		}

		[TestMethod]
		public void TestRentals()
		{
			Assert.IsNotNull(Rental.GetAll());

			var item1 = Rental.GetAll().FirstOrDefault(obj => obj.CopyId == 1 && obj.ClientId == 1);
			var item2 = Rental.GetAll().FirstOrDefault(obj => obj.CopyId == 4 && obj.ClientId == 1);

			Assert.IsNotNull(item1);
			Assert.IsNull(item2);
		}

		[TestMethod]
		public void TestStarring()
		{
			Assert.IsNotNull(Starring.GetAll());

			var item1 = Starring.GetAll().FirstOrDefault(obj => obj.ActorId == 2 && obj.MovieId == 1);
			var item2 = Starring.GetAll().FirstOrDefault(obj => obj.ActorId == 2 && obj.MovieId == 2);

			Assert.IsNotNull(item1);
			Assert.IsNull(item2);
		}
	}
}