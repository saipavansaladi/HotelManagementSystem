using HotelManagement.DatabaseLayer;
using HotelManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Hosting;
using System.Text.RegularExpressions;

namespace HotelManagement.Controllers
{
    public class HotelController : Controller
    {
        public IActionResult Home()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            List<genderModel> genders = new List<genderModel>()
            {
                new genderModel(){gendername="Male"},
                new genderModel(){gendername="Female"},

            };
            ViewBag.genders = genders;
            
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(customerModel customer)
        {
            List<genderModel> genders = new List<genderModel>()
            {
                new genderModel(){gendername="Male"},
                new genderModel(){gendername="Female"},

            };
            ViewBag.genders = genders;
            if (!ModelState.IsValid)
            {
                return View();
            }
            else if(dblayer.isDuplicateCustomer(customer.customerid))
            {
                ModelState.AddModelError("customerid", "This ID is already present");
                return View();
            }
            else
            {
                dblayer.addCustomer(customer);
                return RedirectToAction("Home");
            }
        }

        [HttpGet]
        public IActionResult customersignin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult customersignin(customerlogin customer)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            else if(!dblayer.validateCustomer(customer))
            {
                ModelState.AddModelError("customerid", "Either Customer ID or Password is incorrect");
                return View();
            }
            else
            {
                HttpContext.Session.SetString("userid", customer.customerid);
                HttpContext.Session.SetString("usertype", "customer");

                return RedirectToAction("Home");
            }
        }

        public IActionResult logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Home");
        }

        [HttpGet]
        public IActionResult adminsignin()
        {
            return View();
        }
        [HttpPost]
        public IActionResult adminsignin(adminModel admin)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            else if (!dblayer.validateAdmin(admin))
            {
                ModelState.AddModelError("userid", "Either Customer ID or Password is incorrect");
                return View();
            }
            else
            {
                HttpContext.Session.SetString("userid", admin.userid);
                HttpContext.Session.SetString("usertype", "admin");

                return RedirectToAction("Home");
            }
        }

        [HttpGet]
        public IActionResult managerooms()
        {
            List<RoomsModel> rooms = dblayer.getAllRooms();
            return View(rooms);

        }

        [HttpGet]
        public IActionResult deleteroom(int roomid)
        {
            dblayer.deleteroom(roomid);
            return RedirectToAction("managerooms");
        }

        [HttpGet]
        public IActionResult addroom()
        {
           List<RoomTypesModel> types = new List<RoomTypesModel>();
           types.Add(new RoomTypesModel(){type = "Standard"});            
           types.Add(new RoomTypesModel(){type = "Deluxe"});
           types.Add(new RoomTypesModel() { type = "Suite" });
           types.Add(new RoomTypesModel() { type = "Executive" });
            ViewBag.RoomTypes = types;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> addroom(RoomsModel room,IFormFile imagefile)
        {
            List<RoomTypesModel> types = new List<RoomTypesModel>();
            types.Add(new RoomTypesModel() { type = "Standard" });
            types.Add(new RoomTypesModel() { type = "Deluxe" });
            types.Add(new RoomTypesModel() { type = "Suite" });
            types.Add(new RoomTypesModel() { type = "Executive" });
            ViewBag.RoomTypes = types;
            if(room.wifi==false && room.minibar == false && room.workspace == false && room.roomservice == false)
            {
                ModelState.AddModelError("facilities", "Please Select Atleast one facility");
                return View();
            }
           
            if (imagefile==null || imagefile.Length<=0)
            {
                ModelState.AddModelError("imagepath", "Please Upload Image");
                return View();
            }
            else if (!ModelState.IsValid)
            {
                return View();
            }
            else if(dblayer.roomExists(room.roomnumber))
            {
                ModelState.AddModelError("roomnumber", "This Room Number already exists");
                return View();
            }
            else
            {
                var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "roomimages");
                var fileName = imagefile.FileName;

              
                var filePath = Path.Combine(uploads, fileName);

                // Save the uploaded image to the specified path
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imagefile.CopyToAsync(stream);
                }
                room.imagepath = fileName;
                if(room.roomtype== "Standard")
                {
                    room.cost = 100;
                }
                else if(room.roomtype== "Deluxe")
                {
                    room.cost = 120;
                }
                else if (room.roomtype == "Executive")
                {
                    room.cost = 140;
                }
                else if(room.roomtype== "Suite")
                {
                    room.cost = 200;
                }
                string facilities = "";
                if (room.wifi) facilities = format(facilities, "WiFi");
                if (room.minibar) facilities = format(facilities, "Mini- Bar");
                if (room.workspace) facilities = format(facilities, "WorkSpace");
                if (room.roomservice) facilities = format(facilities, "24*7 Room Service");
                if(facilities.EndsWith(","))
                    {
                    facilities=facilities.Remove(facilities.Length - 1, 1);
                }
                room.facilities = facilities;
                    dblayer.addRoom(room);
                return RedirectToAction("managerooms");
            }

        }

        [HttpGet]
        public IActionResult managestaff()
        {
            List<employeeLoginModel> staffs = dblayer.getStaffs();
            return View(staffs);
        }

        [HttpPost]
        public IActionResult managestaff(employeeLoginModel login)
        {
            return View();
        }

        public IActionResult getallcustomers()
        {
            List<customerModel> customers = dblayer.getAllCustomers();
            return View(customers);
        }
        
        public IActionResult getactivebookings()
        {
            List<bookingModel> bookings = dblayer.getAllCheckins();
            return View(bookings);
        }

        public IActionResult gettopcustomers()
        {
            List<topCustomersModel> customers = dblayer.gettopcustomers();
            return View(customers);
        }

        public IActionResult reports()
        {
            return View();
        }

        [HttpPost]
        public IActionResult reports(List<customerModel> customers)
        {
            return View(customers);
        }

        public string format(string faclist,string curfac)
        {
            if (faclist == "") return curfac+",";
            else return faclist + curfac+",";
        }

        public IActionResult deletestaff(string staffid)
        {
            dblayer.deleteStaff(staffid);
            return RedirectToAction("managestaff");
        }

        [HttpGet]
        public IActionResult addstaff()
        {
            return View();
        }
        [HttpPost]
        public IActionResult addstaff(employeeLoginModel login)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            else if(dblayer.isduplicateStaff(login))
            {
                ModelState.AddModelError("staffid", "This ID is already present");
                return View();
            }
            else
            {
                dblayer.addStaff(login);
                return RedirectToAction("Home");
            }
        }

        [HttpGet]
        public IActionResult changepassword()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult changepassword(passwordModel details)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            else if(!dblayer.validatePassword(HttpContext.Session.GetString("userid"), details.currentpassword))
            {
                ModelState.AddModelError("currentpassword", "Please enter correct password");
                return View();
            }
            else
            {
                string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z0-9]).{8,}$";
                int count = 0;
                if(HttpContext.Session.GetString("invalidattempts")==null)
                {
                    HttpContext.Session.SetString("invalidattempts", "0");
                }

                if (!Regex.IsMatch(details.newpassword, pattern))
                {
                    count = int.Parse(HttpContext.Session.GetString("invalidattempts"));
                    count++;
                    HttpContext.Session.SetString("invalidattempts", count.ToString());
                    if(count==3)
                    {
                        HttpContext.Session.Clear();
                        return RedirectToAction("Home");

                    }
                    ModelState.AddModelError("newpassword", "You should have atleast one lowercase,uppercase,digit and special character");
                    return View();

                }
                else
                {
                    dblayer.changePassword(HttpContext.Session.GetString("userid"), details.newpassword);
                    return RedirectToAction("Home");
                }
            }
        }

        public IActionResult bookRooms()
        {
            List<RoomsModel> rooms = dblayer.getAllRooms();
            return View(rooms);
        }

        [HttpGet]
        public IActionResult bookroom(int roomid,int cost)
        {
            //get total bookings for the user
            int count = dblayer.getActiveBookingsCount(HttpContext.Session.GetString("userid"));
            if (count > 3)
            {
                return RedirectToAction("ExceededLimit");
            }
            else
            {
                HttpContext.Session.SetString("bookingroomid", roomid.ToString());
                HttpContext.Session.SetString("bookingcost", cost.ToString());
                return View();
            }
        }

        public IActionResult ExceededLimit()
        {
            return View();
        }

        [HttpPost]
        public IActionResult bookroom(DateTime startDate,DateTime endDate)
        {
            int roomid = int.Parse(HttpContext.Session.GetString("bookingroomid"));
            
            if(dblayer.getExistingConflictBookings(roomid,startDate, endDate))
            {
                ViewBag.error = "Please chose different date as there are already bookings on the selected schedule";
                return View();
            }
            else
            {
                int discount = 0;
                int count=dblayer.getoldBookingsCount(HttpContext.Session.GetString("userid"));
               
                if(count>5)
                {
                    discount = 20;
                }
                else if (count > 3)
                {
                    discount = 10;
                }
                TimeSpan diff = endDate - startDate;
                double totaldays = diff.TotalDays + 1;
                int costperday = int.Parse(HttpContext.Session.GetString("bookingcost"));
                double totalcost = costperday * totaldays;
                if(discount==10)
                {
                    double discountedprice = totalcost * 0.1;
                    totalcost = totalcost - discountedprice;
                }
                else if(discount==20)
                {
                    double discountedprice = totalcost * 0.2;
                    totalcost = totalcost - discountedprice;
                }

                //apply tax to the total cost
                double roomcost = totalcost;
                double tax = totalcost * 0.1;
                totalcost = totalcost + (totalcost * 0.1);
                HttpContext.Session.SetString("startDate", startDate.ToString());
                HttpContext.Session.SetString("endDate", endDate.ToString());
                HttpContext.Session.SetString("totalcost", totalcost.ToString());

                //dblayer.bookRoom(HttpContext.Session.GetString("userid"), roomid, startDate, endDate, totalcost, "booked");
                ViewBag.cost = totalcost;
                return RedirectToAction("payment",new {cost=totalcost,roomcost=roomcost,tax=tax});
            }
        }

        [HttpGet]
        public IActionResult payment(double cost,double roomcost,double tax)
        {
            ViewBag.cost = cost;
            ViewBag.roomcost = roomcost;
            ViewBag.tax = tax;
            return View();
        }
        [HttpPost]
        public IActionResult payment(paymentModel payment)
        {
            double totalcost = Double.Parse(HttpContext.Session.GetString("totalcost"));
            ViewBag.cost = totalcost;
            if (!ModelState.IsValid)
            {
                return View();
            }
            int roomid = int.Parse(HttpContext.Session.GetString("bookingroomid"));
            DateTime startDate = DateTime.Parse(HttpContext.Session.GetString("startDate"));
            DateTime endDate = DateTime.Parse(HttpContext.Session.GetString("endDate"));
            
            int bookingid=dblayer.bookRoom(HttpContext.Session.GetString("userid"), roomid, startDate, endDate, totalcost, "booked",payment);
            
            return RedirectToAction("bookingconfirmation",new {bookingid=bookingid});            
        }

        [HttpGet]
        public IActionResult viewbookings()
        {
            string userid = HttpContext.Session.GetString("userid");
            List<bookingModel> allbookings = dblayer.getBookings(userid);
            return View(allbookings);
        }

        [HttpGet]
        public IActionResult cancelbooking(int bookingid,DateTime bookingstartdate,float totalcost)
        {
            double dayscount = (bookingstartdate - DateTime.Now.Date).TotalDays;
            float cancelcharges = 0;
            if(dayscount<5)
            {
                cancelcharges = totalcost * 0.1f;
            }
            else
            {
                cancelcharges = totalcost * 0.05f;

            }
           
            dblayer.cancelBooking(bookingid);
            return RedirectToAction("cancelled", new { cancelcharges = cancelcharges });
        }

        [HttpGet]
        public IActionResult checkin(int bookingid)
        {
            dblayer.checkin(bookingid);
            return RedirectToAction("viewbookings");
        }

        [HttpGet]

        public IActionResult checkout(int bookingid)
        {
            dblayer.checkout(bookingid);
            return RedirectToAction("viewbookings");
        }

        public IActionResult submitfeedback()
        {
            string userid = HttpContext.Session.GetString("userid");
            int totalBookings = dblayer.getoldBookingsCount(userid);
            ViewBag.totalBookings = totalBookings;
            return View();
        }

        [HttpPost]
        public IActionResult submitfeedback(feedbackModel feedback)
        {
            string userid = HttpContext.Session.GetString("userid");
            int totalBookings = dblayer.getoldBookingsCount(userid);
            ViewBag.totalBookings = totalBookings;
            if (!ModelState.IsValid)
            {
                return View();
            }
            
            dblayer.saveFeedback(userid, feedback.comments);
            return RedirectToAction("Home");
        }

        public IActionResult bookingconfirmation(int bookingid)
        {
            ViewBag.bookingid = bookingid;
            return View();
        }

        public IActionResult cancelled(float cancelcharges)
        {
            ViewBag.cancelcharges = cancelcharges;
            return View();
        }
    }
}
