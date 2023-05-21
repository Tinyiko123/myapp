using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using WebApplication11.Controllers;
using WebApplication11.Models;
using WebApplication11v2;

namespace WebApplication11.Controllers
{
    public class ProductController : Controller
    {
        string connectString = @"Data Source=NOZIPHO_SITHEBE\SQLEXPRESS;Initial Catalog=farmcentralSchema;Integrated Security=True";
        
        // GET: ProductController

        private readonly ApplicationDbContext _dbContext;

        public ProductController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        // GET: FarmerController1
        public ActionResult Index()
        {
            var dbProducts = _dbContext.Product.ToList();

            var dbProductsTable = new DataTable();

            var properties = typeof(Product).GetProperties();
            foreach(var property in properties)
            {
                Type propertyType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;

                dbProductsTable.Columns.Add(property.Name, propertyType);
            }

            foreach(var product in dbProducts)
            {
                var row = dbProductsTable.NewRow();

                foreach(var property in properties)
                {
                    object value = property.GetValue(product);
                    if(value != null && Nullable.GetUnderlyingType(property.PropertyType) != null)
                    {
                        value = Convert.ChangeType(value, Nullable.GetUnderlyingType(property.PropertyType));
                    }

                    row[property.Name] = value;
                }

                dbProductsTable.Rows.Add(row);
            }

            return View(dbProductsTable);
        }

        //public ActionResult Index()
        //{
        //    DataTable dt = new DataTable();
        //    using (SqlConnection sql = new SqlConnection(connectString))
        //    {
        //        sql.Open();
        //        SqlDataAdapter sd = new SqlDataAdapter("Select * from Product", sql);
        //        sd.Fill(dt);
        //    }

        //    return View(dt);
        //}
        [HttpGet]
        public ActionResult Create()
        {
            return View(new Product());
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            using (SqlConnection sql = new SqlConnection(connectString))
            {
                sql.Open();
                string query = "INSERT INTO Product VALUES(@ProductName,@IncomingorOutgoing,@Type,@Dateacquired)";
                SqlCommand sqlCmd = new SqlCommand(query, sql);
                sqlCmd.Parameters.AddWithValue("@ProductName", product.productName);
                sqlCmd.Parameters.AddWithValue("@IncomingorOutgoing", product.incomingorOutgoing);
                sqlCmd.Parameters.AddWithValue("@Type", product.type);
                sqlCmd.Parameters.AddWithValue("@Dateacquired", product.dateAcquired);
                sqlCmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            Product pM = new Product();
            DataTable dtblProduct = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectString))
            {
                sqlCon.Open();
                string query = "SELECT * FROM Product Where ProductCode = @ProductCode";
                SqlDataAdapter sqlDa = new SqlDataAdapter(query, sqlCon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@ProductCode", id);
                sqlDa.Fill(dtblProduct);
            }
            if (dtblProduct.Rows.Count == 1)
            {
                pM.productCode = Convert.ToInt32(dtblProduct.Rows[0][0].ToString());
                pM.productName = dtblProduct.Rows[0][1].ToString();
                pM.incomingorOutgoing = dtblProduct.Rows[0][2].ToString();
                pM.type = dtblProduct.Rows[0][3].ToString();
                pM.dateAcquired = DateTime.Parse(dtblProduct.Rows[0][4].ToString());
                return View(pM);
            }
            else
                return RedirectToAction("Index");
        }


        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product productModel)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectString))
            {
                sqlCon.Open();
                string query = "UPDATE Product SET ProductName = @ProductName , IncomingorOutgoing=@IncomingorOutgoing , Type = @Type , DateAcquired=@dateAcquired WHere ProductCode = @ProductCode";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@ProductCode", productModel.productCode);
                sqlCmd.Parameters.AddWithValue("@ProductName", productModel.productName);
                sqlCmd.Parameters.AddWithValue("@IncomingorOutgoing", productModel.incomingorOutgoing);
                sqlCmd.Parameters.AddWithValue("@Type", productModel.type);
                sqlCmd.Parameters.AddWithValue("@Dateacquired", productModel.dateAcquired);
                sqlCmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectString))
            {
                sqlCon.Open();
                string query = "DELETE FROM Product WHere ProductCode = @ProductCode";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@ProductCode", id);
                sqlCmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index"); ;

        }
    }
}


