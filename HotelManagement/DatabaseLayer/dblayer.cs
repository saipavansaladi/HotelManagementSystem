using HotelManagement.Models;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.CodeDom;
using System.Data.SqlClient;

namespace HotelManagement.DatabaseLayer
{
    public class dblayer
    {
        public static string connection = "Data Source=KARNA\\SQLEXPRESS;Initial Catalog=hotelmanagement;Integrated Security=true;";
        public static bool isDuplicateCustomer(string customerid)
        {
            try
            {
               
                SqlConnection con = new SqlConnection(connection);
                con.Open();
                SqlCommand command = new SqlCommand($"select * from customers where customerid='{customerid}'", con);
                SqlDataReader sdr = command.ExecuteReader();
                bool present = false;
                while (sdr.Read())
                {
                    present = true;
                }
                sdr.Close();
                con.Close();
                if(present)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool validatePassword(string customerid,string password)
        {
            try
            {

                SqlConnection con = new SqlConnection(connection);
                con.Open();
                SqlCommand command = new SqlCommand($"select * from customers where customerid='{customerid}' and password='{password}'", con);
                SqlDataReader sdr = command.ExecuteReader();
                bool present = false;
                while (sdr.Read())
                {
                    present = true;
                }
                sdr.Close();
                con.Close();
                if (present)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }



        public static bool roomExists(int roomnumber)
        {
            try
            {

                SqlConnection con = new SqlConnection(connection);
                con.Open();
                SqlCommand command = new SqlCommand($"select * from rooms where roomnumber={roomnumber}", con);
                SqlDataReader sdr = command.ExecuteReader();
                bool present = false;
                while (sdr.Read())
                {
                    present = true;
                }
                sdr.Close();
                con.Close();
                if (present)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static List<RoomsModel> getAllRooms()
        {
            try
            {
                List<RoomsModel> allRooms = new List<RoomsModel>();
                SqlConnection con = new SqlConnection(connection);
                con.Open();
                SqlCommand command = new SqlCommand($"select * from rooms", con);
                SqlDataReader sdr = command.ExecuteReader();              
                while (sdr.Read())
                {
                    RoomsModel room = new RoomsModel();
                    room.roomid = int.Parse(sdr[0].ToString());
                    room.roomnumber = int.Parse(sdr[1].ToString());
                    room.roomtype = sdr[2].ToString();
                    room.imagepath = sdr[3].ToString();
                    room.facilities = sdr[4].ToString();
                    room.cost = int.Parse(sdr[5].ToString());
                    allRooms.Add(room);
                }
                sdr.Close();
                con.Close();
                return allRooms;
               
            }
            catch (Exception)
            {
                return null;
            }
        }



        public static List<bookingModel> getBookings(string userid)
        {
            try
            {
                List<bookingModel> allBookings = new List<bookingModel>();
                SqlConnection con = new SqlConnection(connection);
                con.Open();
                SqlCommand command = new SqlCommand($"select * from bookings where customerid='{userid}'", con);
                SqlDataReader sdr = command.ExecuteReader();
                while (sdr.Read())
                {
                    bookingModel currentBooking = new bookingModel();
                    currentBooking.bookingid = int.Parse(sdr[0].ToString());
                    currentBooking.roomid = int.Parse(sdr[2].ToString());
                    currentBooking.bookingstartdate = DateTime.Parse(sdr[3].ToString());
                    currentBooking.bookingenddate = DateTime.Parse(sdr[4].ToString());
                    currentBooking.totalcost = float.Parse(sdr[5].ToString());
                    currentBooking.status = sdr[6].ToString();
                    allBookings.Add(currentBooking);
                }
                sdr.Close();
                con.Close();
                return allBookings;

            }
            catch (Exception)
            {
                return null;
            }
        }
        public static List<employeeLoginModel> getStaffs()
        {
            try
            {
                List<employeeLoginModel> staffs = new List<employeeLoginModel>();
                SqlConnection con = new SqlConnection(connection);
                con.Open();
                SqlCommand command = new SqlCommand($"select * from staffs", con);
                SqlDataReader sdr = command.ExecuteReader();
                while (sdr.Read())
                {
                    employeeLoginModel staff = new employeeLoginModel() { staffid = sdr[0].ToString() };
                    staffs.Add(staff);
                }
                sdr.Close();
                con.Close();
                return staffs;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public static List<customerModel> getAllCustomers()
        {
            try
            {
                List<customerModel> customers = new List<customerModel>();
                SqlConnection con = new SqlConnection(connection);
                con.Open();
                SqlCommand command = new SqlCommand($"select * from customers", con);
                SqlDataReader sdr = command.ExecuteReader();
                while (sdr.Read())
                {
                    customerModel customer = new customerModel();
                    customer.customerid = sdr[0].ToString();
                    customer.firstname = sdr[2].ToString();
                    customer.lastname = sdr[3].ToString();
                    customer.gender = sdr[4].ToString();
                    customer.email = sdr[5].ToString();
                    customer.phone = sdr[6].ToString();
                    customers.Add(customer);
                }
                sdr.Close();
                con.Close();
                return customers;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public static List<bookingModel> getAllCheckins()
        {
            try
            {
                List<bookingModel> bookings = new List<bookingModel>();
                SqlConnection con = new SqlConnection(connection);
                con.Open();
                SqlCommand command = new SqlCommand($"select * from bookings where status='checkedin'", con);
                SqlDataReader sdr = command.ExecuteReader();
                while (sdr.Read())
                {
                    bookingModel booking = new bookingModel();
                    booking.bookingid = int.Parse(sdr[0].ToString());
                    booking.customerid = sdr[1].ToString();
                    booking.roomid = int.Parse(sdr[2].ToString());
                    booking.bookingstartdate = DateTime.Parse(sdr[3].ToString());
                    booking.bookingenddate = DateTime.Parse(sdr[4].ToString());
                    booking.totalcost = int.Parse(sdr[5].ToString());
                    booking.status = sdr[6].ToString();
                    bookings.Add(booking);
                }
                sdr.Close();
                con.Close();
                return bookings;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public static List<topCustomersModel> gettopcustomers()
        {
            try
            {
                List<topCustomersModel> customers = new List<topCustomersModel>();
                SqlConnection con = new SqlConnection(connection);
                con.Open();
                SqlCommand command = new SqlCommand($"select top 10 customerid,count(*) as totalbookings from bookings group by customerid order by count(*) desc;", con);
                SqlDataReader sdr = command.ExecuteReader();
                while (sdr.Read())
                {
                    topCustomersModel customer = new topCustomersModel();
                    customer.customerid = int.Parse(sdr[0].ToString());
                    customer.totalbookings = int.Parse(sdr[1].ToString());
                    customers.Add(customer);
                }
                sdr.Close();
                con.Close();
                return customers;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public static void addCustomer(customerModel customer)
        {
            try
            {
                SqlConnection con = new SqlConnection(connection);
                con.Open();
                string query = "insert into customers values(" + formatQuotes(customer.customerid) + formatQuotes(customer.password) + formatQuotes(customer.firstname) + formatQuotes(customer.lastname) +
                    formatQuotes(customer.gender) +  formatQuotes(customer.email) + "'"+customer.phone+ "')";
                SqlCommand cmd = new SqlCommand(query, con);
                int rows = cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception)
            {

            }
        }

        public static void saveFeedback(string userid,string comments)
        {
            try
            {
                SqlConnection con = new SqlConnection(connection);
                con.Open();
                string query = "insert into feedbacks(customerid,comments) values(" + formatQuotes(userid) + "'" + comments + "')"; 
                SqlCommand cmd = new SqlCommand(query, con);
                int rows = cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception)
            {

            }
        }

        public static int bookRoom(string customerid, int roomid,DateTime startDate, DateTime endDate, double totalcost,string status, paymentModel payment)
            {
            try
            {
                float totcost = (float)totalcost;
                SqlConnection con = new SqlConnection(connection);
                con.Open();
                string query = "insert into bookings(customerid,roomid,bookingstartdate,bookingenddate,totalcost,status) values(" + formatQuotes(customerid) + roomid +","+ formatQuotes(startDate) + formatQuotes(endDate) +
                    totalcost +",'"+ status+ "')";
                SqlCommand cmd = new SqlCommand(query, con);
                int rows = cmd.ExecuteNonQuery();
                con.Close();
                int bookingid = getLastBookingID();
                savePaymentDetails(bookingid, payment);
                return bookingid;
            }
            catch (Exception)
            {
                return 0;
            }
            

            }

        public static int getLastBookingID()
        {
           try
            {
                SqlConnection con = new SqlConnection(connection);
                con.Open();
                string query = "select top 1 bookingid from bookings order by bookingid desc";
                SqlCommand cmd = new SqlCommand(query, con);
                int bookingid = (int)cmd.ExecuteScalar();
                con.Close();
                return bookingid;
            }
            catch(Exception)
            {
                return 0;
            }
        }

        

        public static void savePaymentDetails(int bookingid, paymentModel payment)
        {
            try
            {
                SqlConnection con = new SqlConnection(connection);
                con.Open();
                string query = "insert into paymentdetails(bookingid,cardnumber,streetaddress,city,state,zipcode) values(" + bookingid +
                "," + formatQuotes(payment.cardnumber) + formatQuotes(payment.streetaddress) + formatQuotes(payment.city) +
                formatQuotes(payment.state) + "'" + payment.zipcode + "')";
                SqlCommand cmd = new SqlCommand(query, con);
                int rows = cmd.ExecuteNonQuery();
                con.Close();
            }
            catch(Exception)
            {

            }

        }
        public static void addStaff(employeeLoginModel staff)
        {
            try
            {
                SqlConnection con = new SqlConnection(connection);
                con.Open();
                string query = "insert into staffs values(" + formatQuotes(staff.staffid) + "'"+staff.password+ "')";
                SqlCommand cmd = new SqlCommand(query, con);
                int rows = cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception)
            {

            }
        }
        public static bool validateCustomer(customerlogin customer)
        {
            try
            {

                SqlConnection con = new SqlConnection(connection);
                con.Open();
                SqlCommand command = new SqlCommand($"select * from customers where customerid='{customer.customerid}' and password='{customer.password}'", con);
                SqlDataReader sdr = command.ExecuteReader();
                bool present = false;
                while (sdr.Read())
                {
                    present = true;
                }
                sdr.Close();
                con.Close();
                if (present)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool isduplicateStaff(employeeLoginModel  staff)
        {
            try
            {

                SqlConnection con = new SqlConnection(connection);
                con.Open();
                SqlCommand command = new SqlCommand($"select * from staffs where staffid='{staff.staffid}'", con);
                SqlDataReader sdr = command.ExecuteReader();
                bool present = false;
                while (sdr.Read())
                {
                    present = true;
                }
                sdr.Close();
                con.Close();
                if (present)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool validateAdmin(adminModel admin)
        {
            try
            {

                SqlConnection con = new SqlConnection(connection);
                con.Open();
                SqlCommand command = new SqlCommand($"select * from admins where userid='{admin.userid}' and password='{admin.password}'", con);
                SqlDataReader sdr = command.ExecuteReader();
                bool present = false;
                while (sdr.Read())
                {
                    present = true;
                }
                sdr.Close();
                con.Close();
                if (present)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static string formatQuotes(string value)
        {
            return "'" + value + "',";
        }
        public static string formatQuotes(DateTime value)
        {
            return "'" + value + "',";
        }

        public static void deleteroom(int roomid)
        {
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            string deletequery = "delete from rooms where roomid=" + roomid;
            SqlCommand cmd = new SqlCommand(deletequery, con);
            int rows = cmd.ExecuteNonQuery();
            con.Close();
        }

        public static void changePassword(string userid,string newpassword)
        {
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            string updatequery = $"update customers set password='{newpassword}' where customerid='{userid}'";
            SqlCommand cmd = new SqlCommand(updatequery, con);
            int rows = cmd.ExecuteNonQuery();
            con.Close();
        }

        public static void cancelBooking(int bookingid)
        {
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            string updatequery = $"update bookings set status='cancelled' where bookingid={bookingid}";
            SqlCommand cmd = new SqlCommand(updatequery, con);
            int rows = cmd.ExecuteNonQuery();
            con.Close();

        }

        public static void checkin(int bookingid)
        {
            SqlConnection con = new SqlConnection( connection);
            con.Open();
            string updatequery = $"update bookings set status='checkedin' where bookingid={bookingid}";
            SqlCommand cmd = new SqlCommand( updatequery, con);
            int rows = cmd.ExecuteNonQuery();
            con.Close();
        }

        public static void checkout(int bookingid)
        {
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            string updatequery = $"update bookings set status='checkedout' where bookingid={bookingid}";
            SqlCommand cmd = new SqlCommand(updatequery, con);
            int rows = cmd.ExecuteNonQuery();
            con.Close();
        }

        public static void deleteStaff(string staffid)
        {
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            string deletequery = "delete from staffs where staffid='" + staffid + "'";
            SqlCommand cmd = new SqlCommand(deletequery, con);
            int rows = cmd.ExecuteNonQuery();
            con.Close();
        }

        public static void addRoom(RoomsModel room)
        {
            try
            {
                SqlConnection con = new SqlConnection(connection);
                con.Open();
                string query = "insert into rooms(roomnumber,roomtype,imagepath,facilities,cost) values(" + room.roomnumber + ","+formatQuotes(room.roomtype) + formatQuotes(room.imagepath) + formatQuotes(room.facilities) + room.cost+")";
                SqlCommand cmd = new SqlCommand(query, con);
                int rows = cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception)
            {

            }
        }

        public static bool getExistingConflictBookings(int roomid,DateTime startdate,DateTime enddate)
        {
            string query1 = $"select * from bookings where roomid={roomid} and bookingstartdate<'{startdate}' and bookingenddate>='{startdate}' and status='booked'";
            string query2 = $"select * from bookings where roomid={roomid} and bookingstartdate='{startdate}'  and status='booked'";
            string query3 = $"select * from bookings where roomid={roomid} and bookingstartdate>'{startdate}' and bookingenddate<='{enddate}' and status='booked'";
            string query4 = $"select * from bookings where roomid={roomid} and bookingenddate='{enddate}'  and status='booked'";
            string query5 = $"select * from bookings where roomid={roomid} and bookingstartdate>'{startdate}' and bookingstartdate<'{enddate}'  and status='booked'";

            string finalquery = query1 + " UNION " + query2 + " UNION " + query3 + " UNION " + query4+" UNION "+query5;
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand command = new SqlCommand(finalquery, con);
            SqlDataReader sdr = command.ExecuteReader();
            int count = 0;
            while (sdr.Read())
            {
                count++;
                break;
            }
            sdr.Close();
            con.Close();
            if(count>0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static int getoldBookingsCount(string userid)
        {
            try
            {
                SqlConnection con = new SqlConnection(connection);
                con.Open();
                SqlCommand command = new SqlCommand($"select count(*) from bookings where customerid='{userid}'", con);
                int count = (int)command.ExecuteScalar();
                con.Close();
                return count;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static int getActiveBookingsCount(string userid)
        {
            try
            {
                SqlConnection con = new SqlConnection(connection);
                con.Open();
                SqlCommand command = new SqlCommand($"select count(*) from bookings where status='booked' and customerid='{userid}'", con);
                int count = (int)command.ExecuteScalar();
                con.Close();
                return count;
            }
            catch (Exception)
            {
                return 0;
            }
        }

    }
}
