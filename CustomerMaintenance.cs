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
using System.Text.RegularExpressions;
using System.Configuration;
using BusinessObject;
using BusinessLayer;

namespace Order_Entry_System
{
    class CustomerMaintenance
    {
        public void CustomerMaintenanceModule()
        {
            WriteLine("\n");
            WriteLine("-----------------Customer Maintenance----------------");

            WriteLine("Press any one of the Options as 1 or 2 or 3 or ");
            WriteLine("1.Insert new Customer details");
            WriteLine("2.Modify the Customer Details ");
            WriteLine("3.Delete");
            WriteLine("4.Fetch  Customer details ");
            WriteLine("5.Fetch specific customer based on the customer id or phone number");
            WriteLine("6.Exit");
            int option2 = ToInt32(ReadLine());
            
                switch (option2)
                {
                    case 1:
                        AddCustomer();
                        break;
                    case 2:
                        UpdateCustomerDetails();
                        break;
                    case 3:
                        DeleteCustomer();
                        break;
                    case 4:
                        ShowCustomerDetails();
                        break;
                    case 5:
                        ShowSpecificCustomerDetails();
                        break;
                    case 6:
                        
                            break;
                    default:
                        WriteLine("Invalid Option");
                        break;
                }
            

        }
        public static int ValidEmailid(string email)
        {
            string pattern = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
            if (Regex.IsMatch(email, pattern))
            {
                return 1;

            }
            else
            {
                
                return 0;

            }
        }
        static void AddCustomer()
        {
            Customer cusD = new Customer();
            WriteLine("Enter Customer ID");
            cusD.Customer_ID = ReadLine();
            WriteLine("Enter Customer First Name");
            cusD.First_Name = ReadLine();
            WriteLine("Enter Middle name(if not then press enter)");
            cusD.Middle_Name = ReadLine();
            WriteLine("Enter Last Name");
            cusD.Last_Name = ReadLine();
            WriteLine("Enter Address");
            cusD.Address = ReadLine();
            WriteLine("Enter Email");
            cusD.Email = ReadLine();
            int v = ValidEmailid(cusD.Email);
            if (v != 1)
            {
                WriteLine("Not valid Email id");
            }
            else
            {
                WriteLine("Enter Phone no");
                cusD.Phone = ReadLine();

                CustomerBL cusBL = new CustomerBL();
                int n = cusBL.SaveCustomerBL(cusD);
                if (n != 0)
                {
                    WriteLine("Successfully Added");
                }
                else
                {
                    WriteLine("Failed to add");
                }
            }
        }
        static void DeleteCustomer()
        {
            WriteLine("1.Delete Customer By ID");
            WriteLine("2.Delete the customer if they are not reachable by phone or email");
            WriteLine("3.Exit");
            WriteLine("Choose option");
            int op1 = ToInt32(ReadLine());
            
                switch (op1)
                {
                    case 1:
                        Customer cusD1 = new Customer();
                        WriteLine("Enter Customer ID");
                        cusD1.Customer_ID = ReadLine();
                        CustomerBL cus1 = new CustomerBL();
                        int nn = cus1.DeleteCustomerBL(cusD1);
                        if (nn != 0)
                        {
                            WriteLine("Successfully Deleted");
                        }
                        else
                        {
                            WriteLine("Failed to Delete");
                        }
                        break;
                    case 2:
                        Customer cusD2 = new Customer();
                        WriteLine("Enter Customer ID");
                        cusD2.Customer_ID = ReadLine();
                        WriteLine("Is Customer reachable or not?(y/n)");
                        string res = ReadLine();
                        if (res == "y" || res == "Y")
                        {
                            WriteLine("Customer contacted");
                        }
                        else if (res == "n" || res == "N")
                        {
                            CustomerBL cus = new CustomerBL();
                            int n = cus.DeleteCustomerBL(cusD2);
                            if (n != 0)
                            {
                                WriteLine("Successfully Deleted");
                            }
                            else
                            {
                                WriteLine("Failed to Delete");
                            }
                        }
                        else
                        {
                            WriteLine("Invalid response ");
                        }
                        break;
                    case 3:
                        
                            break;
                    default:
                        WriteLine("Invalid Option");
                        break;
                }
            
            

        }
        static void ShowCustomerDetails()
        {
           
            CustomerBL cus = new CustomerBL();
            DataSet ds=cus.FetchAllCustomerBL();
            if (ds.Tables.Count > 0)
            {
                WriteLine("Customer Details");
                WriteLine("{0,-5} {1,-15} {2,-15} {3,-15} {4,-15} {5,-10} {6,-15}\n", "CusID", "FirstName", "MiddleName", "LastName", "Address", "Email", "Phone");
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    WriteLine("{0,-5} {1,-15} {2,-15} {3,-15} {4,-15} {5,-15} {6,-15}\n", dr[0], dr[1], dr[2], dr[3], dr[4], dr[5], dr[6]);
                }
            }
            else
            {
                WriteLine("No record found");
            }
        }
        static void UpdateCustomerDetails()
        {
            Customer cusD5 = new Customer();
            WriteLine("1.Update FirstName");
            WriteLine("2.Update MiddleName");
            WriteLine("3.Update LastName");
            WriteLine("4.Update Phone");
            WriteLine("5.Update Address");
            WriteLine("6.Update Email");
            WriteLine("7.Exit");
            int op = ToInt32(ReadLine());
            CustomerBL cus1 = new CustomerBL();
            
                switch (op)
                {
                    case 1:
                        WriteLine("Enter Customer ID");
                        cusD5.Customer_ID = ReadLine();
                        WriteLine("Enter Customer First Name to be updated");
                        cusD5.First_Name = ReadLine();
                        int n=cus1.UpdateCustomerFirstNameBL(cusD5);
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
                        WriteLine("Enter Customer ID");
                        cusD5.Customer_ID = ReadLine();
                        WriteLine("Enter Customer Middle Name to be updated");
                        cusD5.Middle_Name = ReadLine();
                        int m=cus1.UpdateCustomerMiddleNameBL(cusD5);
                    if (m != 0)
                    {
                        WriteLine("Successfully Updated");
                    }
                    else
                    {
                        WriteLine("Failed to Update");
                    }
                    break;
                    case 3:
                        WriteLine("Enter Customer ID");
                        cusD5.Customer_ID = ReadLine();
                        WriteLine("Enter Customer Last Name to be updated");
                        cusD5.Last_Name = ReadLine();
                        int nn=cus1.UpdateCustomerLastNameBL(cusD5);
                    if (nn != 0)
                    {
                        WriteLine("Successfully Updated");
                    }
                    else
                    {
                        WriteLine("Failed to Update");
                    }
                    break;
                    case 4:
                        WriteLine("Enter Customer ID");
                        cusD5.Customer_ID = ReadLine();
                        WriteLine("Enter Customer Phone no.to be updated");
                        cusD5.Phone = ReadLine();

                       int nm= cus1.UpdateCustomerPhoneBL(cusD5);
                    if (nm != 0)
                    {
                        WriteLine("Successfully Updated");
                    }
                    else
                    {
                        WriteLine("Failed to Update");
                    }
                    break;
                    case 5:
                        WriteLine("Enter Customer ID");
                        cusD5.Customer_ID = ReadLine();
                        WriteLine("Enter Customer Address to be updated");
                        cusD5.Address = ReadLine();
                       int p= cus1.UpdateCustomerAddressBL(cusD5);
                    if (p != 0)
                    {
                        WriteLine("Successfully Updated");
                    }
                    else
                    {
                        WriteLine("Failed to Update");
                    }
                    break;
                    case 6:
                        WriteLine("Enter Customer ID");
                        cusD5.Customer_ID = ReadLine();
                        WriteLine("Enter Customer Email to be updated");
                        cusD5.Email = ReadLine();
                        int v = ValidEmailid(cusD5.Email);
                        if (v != 1)
                        {
                            WriteLine("Not valid Email id");
                        }
                        else
                        {
                            int u=cus1.UpdateCustomerEmailBL(cusD5);
                             if (u != 0)
                             {
                               WriteLine("Successfully Updated");
                             }
                             else
                             {
                               WriteLine("Failed to Update");
                             }
                        }
                        break;
                    case 7:
                        
                           break;
                    default:
                        WriteLine("Invalid Option");
                        break;
                }
            
        }
        static void ShowSpecificCustomerDetails()
        {
            Customer cusD3 = new Customer();
            WriteLine("1.For CustomerDetails by ID");
            WriteLine("2.For CustomerDetails by Phone no.");
            WriteLine("3.Exit");
            WriteLine("Enter Option");
            int op = ToInt32(ReadLine());
            CustomerBL cus1 = new CustomerBL();
            
                switch (op)
                {
                    case 1:
                        WriteLine("Enter Customer ID");
                        cusD3.Customer_ID = ReadLine();
                        DataSet ds1 = cus1.FetchSpecificCustomerByIDBL(cusD3);
                    if (ds1.Tables.Count > 0)
                    {
                        WriteLine("Customer Details");
                        WriteLine("{0,-5}  {1,-15}  {2,-15}  {3,-15}  {4,-15}  {5,-15}  {6,-15}|\n", "CusID", "FirstName", "MiddleName", "LastName", "Address", "Email", "Phone");
                        foreach (DataRow dr in ds1.Tables[0].Rows)
                        {
                            WriteLine("{0,-5} {1,-15} {2,-15} {3,-15} {4,-15} {5,-15} {6,-15}\n", dr[0], dr[1], dr[2], dr[3], dr[4], dr[5], dr[6]);
                        }
                    }
                    else
                    {
                        WriteLine("No record found");
                    }
                        break;
                    case 2:
                        WriteLine("Enter Customer Phone no.");
                        cusD3.Phone = ReadLine();
                        DataSet ds2 = cus1.FetchSpecificCustomerByPhoneBL(cusD3);
                    if (ds2.Tables.Count > 0)
                    {
                        WriteLine("Customer Details");
                        WriteLine("{0,-5} {1,-15} {2,-15} {3,-25} {4,-15} {5,-10} {6,-10}\n", "CusID", "FirstName", "MiddleName", "LastName", "Address", "Email", "Phone");
                        foreach (DataRow dr in ds2.Tables[0].Rows)
                        {
                            WriteLine("{0,-5} {1,-15} {2,-15} {3,-25} {4,-15} {5,-10} {6,-10}\n", dr[0], dr[1], dr[2], dr[3], dr[4], dr[5], dr[6]);
                        }
                    }
                    else
                    {
                        WriteLine("No record Found");
                    }

                        break;
                    case 3:
                        
                            break;
                    default:
                        WriteLine("Invalid Option");
                        break;
                }
            
        }
    }
}
