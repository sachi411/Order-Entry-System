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
namespace Order_Entry_System
{
    //Main
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Enter AdminID");
            string AdminID = ReadLine();
            WriteLine("Enter password");
            string pass = ReadLine();
            if (AdminID == "sachi1" && pass == "abc123")
            {
                WriteLine("Login Successful!!");
                ReadLine();
                while (true)
                    {
                        Clear();
                 
                        WriteLine("********************");
                        WriteLine("|Order Entry System|");
                        WriteLine("********************");
                        OrderMaintenance upStatusAnDQuan = new OrderMaintenance();
                        upStatusAnDQuan.FetchOrderDetailAndUpdateStatus();
                        WriteLine("Press any one of the Options as 1 or 2 or 3");
                        WriteLine("1.Product Maintenance");
                        WriteLine("2.Customer Maintenance");
                        WriteLine("3.Order Maintenance");
                        WriteLine("4.Exit ");
                        int option = ToInt32(ReadLine());
                        try
                        {
                        
                            switch (option)
                            {
                                case 1:
                                    ProductMaintenance pm = new ProductMaintenance();
                                    pm.ProductMaintenanceModule();
                                    break;
                                case 2:
                                    CustomerMaintenance cm = new CustomerMaintenance();
                                    cm.CustomerMaintenanceModule();
                                    break;
                                case 3:
                                    OrderMaintenance om = new OrderMaintenance();
                                    om.OrderMaintenanceModule();
                                    break;
                                case 4:
                                    WriteLine("Closing the application..");

                                    break;
                       
                                default:
                                    WriteLine("Enter a valid option");
                                    break;
                            }

                        WriteLine("Press any key  to continue ");
                        ReadLine();

                    }
                        catch (Exception ex)
                        {
                            WriteLine(ex.Message);
                            WriteLine(ex.StackTrace);
                            WriteLine(ex.InnerException);
                            ReadLine();
                        }

                    }
                }
            else
            {
                WriteLine("Wrong ID or Password");
                ReadLine();
            }
        }
    }
}

