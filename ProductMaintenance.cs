using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using static System.Console;
using static System.Convert;
using System.Configuration;
using BusinessObject;
using BusinessLayer;

namespace Order_Entry_System
{
    class ProductMaintenance
    {
        public void ProductMaintenanceModule()
        {
            WriteLine("\n");
            WriteLine("-----------------Product Maintenance----------------");

            WriteLine("Press any one of the Options as 1 or 2 or 3 or ");
            WriteLine("1.Insert new Product details");
            WriteLine("2.Modify the price or quantity in hand ");
            WriteLine("3.Delete ");
            WriteLine("4.Fetch  Product details ");
            WriteLine("5.Fetch  a specific product based on the product id");
            WriteLine("6.Exit");
            int option1 = ToInt32(ReadLine());
            
                switch (option1)
                {
                    case 1:
                        AddProduct();
                        break;
                    case 2:
                        UpdateProductDetails();
                        break;
                    case 3:
                        DeleteProduct();
                        break;
                    case 4:
                        ShowProductDetails();
                        break;
                    case 5:
                        ShowSpecificProductDetails();
                        break;
                    case 6:
                        break;
                    default:
                        WriteLine("Invalid Option");
                        break;
           
                }
        }
        static void AddProduct()
        {
            Product proD = new Product();
            WriteLine("Enter Product ID");
            proD.Product_ID = ReadLine();
            WriteLine("Enter Product Name");
            proD.Product_Name = ReadLine();
            WriteLine("Enter Product description");
            proD.Product_Description = ReadLine();
            WriteLine("Enter Listing Price");
            proD.Listing_Price= ToDecimal(ReadLine());
            WriteLine("Enter Quantity in Hand");
            proD.Quantity_in_hand = ToInt32(ReadLine());
            WriteLine("Enter Reorder level");
            proD.Reorder_Level =ToInt32(ReadLine());

            ProductBL proBL = new ProductBL();
            int n = proBL.SaveProductBL(proD);
            if (n != 0)
            {
                WriteLine("Successfully Added");
            }
            else
            {
                WriteLine("Failed to add");
            }
        }
        static void DeleteProduct()
        {
            
            
            WriteLine("1.Delete Product by id");
            WriteLine("2.Delete the product which does not have any consumer preference");
            WriteLine("3.Exit");
            WriteLine("Choose option");
            int op = ToInt32(ReadLine());
            
                switch (op)
                {
                    case 1:
                        Product proD1 = new Product();
                        WriteLine("Enter Product ID");
                        proD1.Product_ID = ReadLine();
                        ProductBL pro = new ProductBL();
                        int n = pro.DeleteProductBL(proD1);
                        if (n != 0)
                        {
                            WriteLine("Successfully Deleted");
                        }
                        else
                        {
                            WriteLine("Failed to Delete");
                        }
                        break;
                    case 2:
                        //WriteLine("Does product have any consumer preference?(y/n)");
                        //string res = ReadLine();
                        //if (res == "y" || res == "Y")
                        //{
                        //    WriteLine("Okay proceed");
                        //}
                        //else if (res == "n" || res == "N")
                        //{
                        ProductBL proD = new ProductBL();
                        int nn = proD.DeleteProductWithNoPreferenceBL();
                        if (nn != 0)
                        {
                            WriteLine("Successfully Deleted");
                        }
                        else
                        {
                            WriteLine("Failed to Delete as Every product present now has preference");
                        }
                        //}
                        //else
                        //{
                        //    WriteLine("Invalid response ");
                        //}
                        break;
                    case 3:
                        
                            break;
                    default:
                        WriteLine("Wrong option");
                        break;
                }
            
            
        }

        static void ShowProductDetails()
        {

            ProductBL pro = new ProductBL();
            DataSet ds = pro.FetchAllProductBL();
            WriteLine("Product Details");
            WriteLine("{0,-5} {1,-15} {2,-15} {3,-15} {4,-15} {5,-15}\n", "proID", "proName", "proDes", "Listing_Price", "Quantity", "Reorder_level");
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                WriteLine("{0,-5} {1,-15} {2,-15} {3,-15} {4,-15} {5,-15}\n", dr[0], dr[1], dr[2], dr[3], dr[4], dr[5]);
            }
        }
        static void UpdateProductDetails()
        {
            Product proD5 = new Product();

            WriteLine("1.Update Price");
            WriteLine("2.Update Quantity in hand");
            WriteLine("3.Exit");
            int op = ToInt32(ReadLine());
            ProductBL pro1 = new ProductBL();
            
                switch (op)
                {
                    case 1:
                        WriteLine("Enter Product ID");
                        proD5.Product_ID = ReadLine();
                        WriteLine("Enter Product Price to be updated");
                        proD5.Listing_Price = ToDecimal(ReadLine());
                        int n=pro1.UpdateProductPriceBL(proD5);
                         if (n != 0)
                         {
                            WriteLine("Successfully Updated");
                         }
                         else
                         {
                            WriteLine("Failed to Update");
                         }
                         break;
                    case 2:
                        WriteLine("Enter Product ID");
                        proD5.Product_ID = ReadLine();
                        WriteLine("Enter Product quantity  to be updated");
                        proD5.Quantity_in_hand = ToInt32(ReadLine());
                        int mm=pro1.UpdateProductQuantityBL(proD5);
                         if (mm != 0)
                         {
                           WriteLine("Successfully Updated");
                         }
                         else
                         {
                           WriteLine("Failed to Update");
                         }
                        break;
                    case 3:
                        //if (true)
                            break;
                    default:
                        WriteLine("Invalid Option");
                        break;
                
                }
        }
        static void ShowSpecificProductDetails()
        {
            Product proD3 = new Product();
            ProductBL pro1 = new ProductBL();
            WriteLine("Enter Product ID");
            proD3.Product_ID = ReadLine();
            DataSet ds1 = pro1.FetchSpecificProductByIDBL(proD3);
            if (ds1.Tables.Count > 0)
            {
                
                WriteLine("Product Details");
                WriteLine("{0,-5} {1,-15} {2,-15} {3,-15} {4,-15} {5,-15}\n", "proID", "proName", "proDes", "Listing_Price", "Quantity", "Reorder_level");
                foreach (DataRow dr in ds1.Tables[0].Rows)
                {
                    WriteLine("{0,-5} {1,-15} {2,-15} {3,-25} {4,-15} {5,-10} {6,-10}\n", dr[0], dr[1], dr[2], dr[3], dr[4], dr[5]);
                }
            }
            else
            {
                WriteLine("No records found.");
            }

                
        }
        public void FetchProductDetailAndUpdateQuantity_in_hand()
        {
            {

                ProductBL pro = new ProductBL();
                DataSet ds = pro.FetchAllProductBL();
                if (ds.Tables.Count > 0)
                {
                    WriteLine("Product Details");
                    WriteLine("{0,-5} {1,-15} {2,-15} {3,-15} {4,-15} {5,-15}\n", "proID", "proName", "proDes", "Listing_Price", "Quantity", "Reorder_level");
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        WriteLine("{0,-5} {1,-15} {2,-15} {3,-15} {4,-15} {5,-15}\n", dr[0], dr[1], dr[2], dr[3], dr[4], dr[5]);
                    }
                }
                else
                {
                    WriteLine("No records found.");
                }
            }

        }
    }
}
