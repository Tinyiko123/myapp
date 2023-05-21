using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using WebApplication11.Models;

namespace WebApplication11v2.Controllers
{

    public class FarmerController1 : Controller
    {
        string connectString = @"Data Source=NOZIPHO_SITHEBE\SQLEXPRESS;Initial Catalog=farmcentralSchema;Integrated Security=True";

        private readonly ApplicationDbContext _dbContext;

        public FarmerController1(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: FarmerController1
        public ActionResult Index()
        {
            var dbFarmers = _dbContext.Farmer.ToList();

            return View(dbFarmers);
        }

        // GET: FarmerController1/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FarmerController1/Create
        public ActionResult Create()
        {
            return View(new Farmer());
        }

        // POST: FarmerController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Farmer farmer)
        {
            using (SqlConnection sql = new SqlConnection(connectString))
            {
                sql.Open();
                string query = "INSERT INTO Farmer VALUES(@Fname,@Lname,@Email,@Pw, @ProductCode)";
                SqlCommand sqlCmd = new SqlCommand(query, sql);
                sqlCmd.Parameters.AddWithValue("@Fname", farmer.fname);
                sqlCmd.Parameters.AddWithValue("@Lname", farmer.lname);
                sqlCmd.Parameters.AddWithValue("@Email", farmer.email);
                sqlCmd.Parameters.AddWithValue("@Pw", farmer.pw);
                sqlCmd.Parameters.AddWithValue("@ProductCode", farmer.productCode);
                sqlCmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }


            // GET: FarmerController1/Edit/5
            public ActionResult Edit(int id)
            {
                return View();
            }

            // POST: FarmerController1/Edit/5
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Edit(int id, IFormCollection collection)
            {
                try
                {
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }

            // GET: FarmerController1/Delete/5
            public ActionResult Delete(int id)
            {
                return View();
            }

            // POST: FarmerController1/Delete/5
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Delete(int id, IFormCollection collection)
            {
                try
                {
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
        }
    }