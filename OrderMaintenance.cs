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
    public class OrderMaintenance
    {
        public void OrderMaintenanceModule()
        {
            WriteLine("\n");
            WriteLine("------------------Order Maintenance----------------");

            WriteLine("Press any one of the Options as 1 or 2 or 3 or ");
            WriteLine("1.Insert new  order");
            WriteLine("2.Update the order before shipment.");
            WriteLine("3.Delete the order before shipment before unpaid.");
            WriteLine("4.Fetch  Order details");
            WriteLine("5.Exit");
            int option2 = ToInt32(ReadLine());
            
                switch (option2)
                {
                    case 1:
                        AddOrderDetail();
                        break;
                    case 2:
                        UpdateOrderDetail();
                        break;
                    case 3:
                        DeleteOrderDetail();
                        break;
                    case 4:
                    WriteLine("1.Order Detail And OrderLine Detail by id");
                    WriteLine("2.All Order Details And OrderLine Details");
                    int od = ToInt32(ReadLine());
                    switch (od)
                    {
                        case 1:
                            Order om = new Order();
                            WriteLine("Enter Order ID");
                            om.Order_ID = ToInt32(ReadLine());
                            FetchOrderDetailbyID(om.Order_ID);
                            FetchOrderLineDetailbyID(om.Order_ID);
                            break;
                        case 2:
                            FetchOrderDetail();
                            FetchOrderLineDetail();
                            break;
                    }
                    break;
                    case 5:
                        
                            break;
                    default:
                        WriteLine("Invalid option");
                        break;

                }
            
        }
        static void AddOrderDetail()
        {
            Order ordD = new Order();
            OrderLine lin = new OrderLine();
            //FetchOrderDetailAndUpdateStatus();
            WriteLine("Enter Customer ID");
            ordD.Customer_ID = ReadLine();
            WriteLine("Enter Order ID");
            ordD.Order_ID = ToInt32(ReadLine());
            //WriteLine("Enter Order Date");
            //string ordate = ReadLine();
            //ordD.Order_Date = DateTime.Parse(ordate);
            DateTime CurDate = DateTime.Today;
            ordD.Order_Date = CurDate;
            ordD.Shippment_Date = ordD.Order_Date.AddDays(14);
            ordD.Delivered_Date = ordD.Shippment_Date.AddDays(7);
            //WriteLine("Enter Order Status");
            ordD.Order_status = "pending";
            //WriteLine("Enter Paid Status");
            ordD.Paid_status = "pending";

            //ordD.Order_Amount = sum;
            //if (ordD.Order_Date == CurDate)
            //{
            //    ordD.Order_status = "pending";
            //    ordD.Paid_status = "pending";
            //}
            //else
            //{
            //if (CurDate < (ordD.Shippment_Date))
            //{
            //    ordD.Order_status = "pending";
            //    ordD.Paid_status = "pending";
            //}
            //else if (CurDate >= (ordD.Shippment_Date) && CurDate <= (ordD.Delivered_Date))
            //{
            //    ordD.Order_status = "shipped";
            //    ordD.Paid_status = "pending";
            //}
            //else if (CurDate >= (ordD.Delivered_Date))
            //{
            //    ordD.Order_status = "completed";
            //    ordD.Paid_status = "paid";
            //}
            //}
            OrderBL ordBL = new OrderBL();
            int nn = ordBL.SaveOrderBL(ordD);
            if (nn != 0)
            {
                WriteLine("Order successfully Added");
            }
            else
            {
                WriteLine("Failed to add");
            }
            WriteLine("Ente no. of diff products that customer wants to order");
            int n = ToInt32(ReadLine());
            //decimal sum = 0;
            for (int i = 0; i < n; i++)
            {
                WriteLine("For " + (i + 1) + "Item ");
                WriteLine("Enter Product ID");
                lin.Product_ID = ReadLine();
                lin.Order_ID = ordD.Order_ID;

                WriteLine("Enter Selling Price");
                lin.Selling_Price = ToDecimal(ReadLine());

                WriteLine("Enter Quantity ordered.");
                lin.Quantity_ordered = ToInt32(ReadLine());
                
                //lin.Amount = (lin.Selling_Price) * (lin.Quantity_ordered);
                OrderLineBL linBL = new OrderLineBL();
                int m = linBL.SaveOrderLineBL(lin);
                if (m != 0)
                {
                    WriteLine("OrderLine " + (i + 1) + " successfully Added");
                    WriteLine("\n");
                }
                else
                {
                    WriteLine("Failed to add");
                }
                //sum += lin.Amount;
                
                   updateProductQuan(lin.Order_ID,lin.Product_ID, lin.Quantity_ordered);
                
            }
            updateAmount(ordD.Order_ID);
        }
        static void updateProductQuan(int ordID,string proID,int quan)
        {
            
            
            OrderLine lin = new OrderLine();
            lin.Product_ID = proID;
            lin.Order_ID = ordID;
            lin.Quantity_ordered = quan;
            OrderLineBL proBL = new OrderLineBL();
                    int n = proBL.UpdateProductQuan_in_handBL(lin);
                    if (n != 0)
                    {
                        WriteLine("Successfully updated Quan in hand");
                    }
                    else
                    {
                        WriteLine("Failed to update Quan in hand");
                    }
        }
        
        static void updateAmount(int ordID)
        {
            Order ordD = new Order();
            ordD.Order_ID = ordID;
            OrderBL ord = new OrderBL();
            int n = ord.UpdateOrderAmountBL(ordD);
            if (n != 0)
            {
                WriteLine("Successfully updated amount");
            }
            else
            {
                WriteLine("Failed to update amount");
            }

        }
        static void UpdateOrderDetail()
        {
            Order ordD = new Order();
            WriteLine("Enter Customer ID");
            ordD.Customer_ID = ReadLine();
            WriteLine("Enter Order ID");
            ordD.Order_ID = ToInt32(ReadLine());
            OrderBL ord = new OrderBL();
            DataSet ds = ord.FetchAllOrderBL();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                ordD.Shippment_Date = (DateTime)dr[3];
                ordD.Paid_status = (string)dr[6];
                DateTime CurDate = DateTime.Today;
                if (ordD.Shippment_Date <= CurDate && ordD.Customer_ID==(string)dr[7] && ordD.Order_ID==(int)dr[0])
                {

                    int n = ord.DeleteOrderBL(ordD);
                    if (n != 0)
                    {
                        WriteLine("This order has been deleted ,to update it again type the order");
                    }
                    else
                    {
                        WriteLine("Failed to Delete");
                    }
                    AddOrderDetail();

                }
            }

        }
        static void DeleteOrderDetail()
        {
            Order ordD = new Order();
            WriteLine("Enter Customer ID");
            ordD.Customer_ID = ReadLine();
            WriteLine("Enter Order ID");
            ordD.Order_ID = ToInt32(ReadLine());
          
            OrderBL ord = new OrderBL();
            
                    int n = ord.DeleteOrderBL(ordD);
                    if (n != 0)
                    {
                        WriteLine("Successfully Deleted");
                    }
                    else
                    {
                        WriteLine("Failed to Delete");
                    }
      
            
           
        }
        public  void FetchOrderDetailAndUpdateStatus()
        {
            Order ordD = new Order();
            OrderBL ord = new OrderBL();
            DataSet ds=ord.FetchAllOrderBL();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                ordD.Shippment_Date = (DateTime)dr[3];
                ordD.Delivered_Date = (DateTime)dr[4];
                
                        int n = ord.UpdateStatusBL(ordD);
                        if (n != 0)
                        {
                            ;
                        }
                        else
                        {
                            WriteLine("Failed to update previous record");
                        }
            }
        }
        static void FetchOrderDetail()
        {
            OrderBL ord = new OrderBL();
            DataSet ds = ord.FetchAllOrderBL();
            WriteLine("Order Details");
            WriteLine("{0,-5} {1,-15} {2,-15} {3,-15} {4,-15} {5,-10} {6,-15}{7,-15}\n", "OrdID", "Ord_date", "Amount", "Ship_date", "Del_date", "Ord_status","Paid_status","CusID");
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                WriteLine("{0,-5} {1,-15} {2,-15} {3,-15} {4,-15} {5,-15} {6,-15}{7,-15}\n", dr[0], ((DateTime)dr[1]).ToString("dd-MM-yyyy"), dr[2], ((DateTime)dr[3]).ToString("dd-MM-yyyy"), ((DateTime)dr[4]).ToString("dd-MM-yyyy"), dr[5], dr[6],dr[7]);
            }

        }

        static void FetchOrderDetailbyID(int ord1)
        {
            Order ot = new Order();
            ot.Order_ID = ord1;
            OrderBL ord = new OrderBL();
            DataSet ds = ord.FetchOrderbyidBL(ot);
            WriteLine("Order Details");
            WriteLine("{0,-5} {1,-15} {2,-15} {3,-15} {4,-15} {5,-10} {6,-15}{7,-15}\n", "OrdID", "Ord_date", "Amount", "Ship_date", "Del_date", "Ord_status", "Paid_status", "CusID");
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                WriteLine("{0,-5} {1,-15} {2,-15} {3,-15} {4,-15} {5,-15} {6,-15}{7,-15}\n", dr[0], ((DateTime)dr[1]).ToString("dd-MM-yyyy"), dr[2], ((DateTime)dr[3]).ToString("dd-MM-yyyy"), ((DateTime)dr[4]).ToString("dd-MM-yyyy"), dr[5], dr[6], dr[7]);
            }

        }
        static void FetchOrderLineDetailbyID(int ord2)
        {
            OrderLine ot = new OrderLine();
            ot.Order_ID = ord2;
            
            OrderLineBL lin = new OrderLineBL();
            DataSet ds = lin.FetchOrderLinebyidBL(ot);
            WriteLine("OrderLine Details");
            WriteLine("{0,-5} {1,-15} {2,-15} {3,-15} {4,-15}\n", "ProID", "OrdID", "Selling_Price", "Quan_ordered", "Line_amount");
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                WriteLine("{0,-5} {1,-15} {2,-15} {3,-15} {4,-15}\n", dr[0], dr[1], dr[2], dr[3], dr[4]);
            }

        }
        static void FetchOrderLineDetail()
        {
            OrderLineBL lin = new OrderLineBL();
            DataSet ds = lin.FetchAllOrderLineBL();
            WriteLine("OrderLine Details");
            WriteLine("{0,-5} {1,-15} {2,-15} {3,-15} {4,-15}\n", "ProID", "OrdID", "Selling_Price", "Quan_ordered", "Line_amount");
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                WriteLine("{0,-5} {1,-15} {2,-15} {3,-15} {4,-15}\n", dr[0], dr[1], dr[2], dr[3], dr[4]);
            }
        }
    }
}
