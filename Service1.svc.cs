using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Scrap_DAL;

namespace Scrap_Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public void email(string subj, string body)
        {
            EmailLibrary.Email e = new EmailLibrary.Email();
            e.SendEmail("akotov4991@student.gwinnetttech.edu", subj, body);
        }

        public List<DTO_Offer> GetOffers()
        {
            DB_111206_scrapEntities db = new DB_111206_scrapEntities();
            List<DTO_Offer> returnOffers = new List<DTO_Offer>();
            var offersTable = db.Offers.ToList();
            foreach (var o in offersTable)
            {
                DTO_Offer tempO = new DTO_Offer
                {
                    DT = o.offerDate,
                    Id = o.offerID,
                    Ostat = Convert.ToInt32(o.ostat),
                    PartId = Convert.ToInt32(o.partsID),
                    Price = Convert.ToDouble(o.price),
                    SellerId = Convert.ToInt32(o.sellerID)
                };
                returnOffers.Add(tempO);
            }
            return returnOffers;
        }

        public List<DTO_Manufacture> GetManufactures(DTO_Manufacture m)
        {
            //DB Connection
            DB_111206_scrapEntities db = new DB_111206_scrapEntities();
            //get table contents
            var listOfSQLObjects = db.Manufacturers.ToList().OrderBy(c=>c.manName).ToList();
            //create list to fill with sql objects
            List<DTO_Manufacture> listOfLocalObjects = new List<DTO_Manufacture>();
            foreach (var sqlobj in listOfSQLObjects)
            {
                DTO_Manufacture o = new DTO_Manufacture();
                o.Id = sqlobj.manID;
                o.Name = sqlobj.manName;
                listOfLocalObjects.Add(o);
            }
            return listOfLocalObjects;
        }

        public List<DTO_Manufacture> GetManufacturesById(DTO_Manufacture m)
        {
            //DB Connection
            DB_111206_scrapEntities db = new DB_111206_scrapEntities();
            //get table contents
            var manufacturer = db.Manufacturers.Where(x => x.manID == m.Id).FirstOrDefault();
            List<DTO_Manufacture> listOfLocalObjects = new List<DTO_Manufacture>();
            DTO_Manufacture o = new DTO_Manufacture();
            if (manufacturer != null)
            {
                o.Id = manufacturer.manID;
                o.Name = manufacturer.manName;
                listOfLocalObjects.Add(o);
            }
            else
            {
                //insert record
                Manufacturer sc = new Manufacturer();
                sc.manName = m.Name;
                db.Manufacturers.Add(sc);
                db.SaveChanges();
                //DTO_Manufacture newMan = new Manufacturer();
            }
            return listOfLocalObjects;
        }
        public List<DTO_Manufacture> GetManufacturesByPartial(DTO_Manufacture m)
        {
            //DB Connection
            DB_111206_scrapEntities db = new DB_111206_scrapEntities();
            //get table contents
            var manufacturer = db.Manufacturers.Where(x => x.manName.StartsWith(m.Name)).ToList();
            List<DTO_Manufacture> listOfLocalObjects = new List<DTO_Manufacture>();
            foreach (var man in manufacturer)
            {
                DTO_Manufacture o = new DTO_Manufacture
                {
                    Name = man.manName,
                    Id = man.manID
                };
                listOfLocalObjects.Add(o);
            }
            return listOfLocalObjects;
        }

        public List<DTO_User> GetUsers()
        {
            //DB Connection
            DB_111206_scrapEntities db = new DB_111206_scrapEntities();
            //get table contents
            var usersTable = db.Users.ToList();
            //create list to fill with sql objects
            List<DTO_User> users = new List<DTO_User>();
            foreach (var user in usersTable)
            {
                DTO_User tempU = new DTO_User
                {
                    id = user.userID,
                    Email = user.email,
                    FirstName = user.fName,
                    LastName = user.lName,
                    Password = user.pwd,
                    Phone = user.phone
                };
                users.Add(tempU);
            }
            return users;
        }

        public List<DTO_User> GetIndUser(DTO_User u)
        {
            //DB Connection
            DB_111206_scrapEntities db = new DB_111206_scrapEntities();
            //get table contents
            var userCred = db.Users.Where(x => x.userID == u.id || x.fName == u.FirstName || x.lName == u.LastName).FirstOrDefault();
            //create list to fill with sql objects
            List<DTO_User> user = new List<DTO_User>();
            if (userCred != null)
            {
                DTO_User tempU = new DTO_User
                {
                    id = userCred.userID,
                    Email = userCred.email,
                    FirstName = userCred.fName,
                    LastName = userCred.lName,
                    Password = userCred.pwd,
                    Phone = userCred.phone
                };
                user.Add(u);
            }
            return user;
        }

        public List<DTO_Offer> GetOffersByPartDesc(DTO_Part p)
        {
            //string partDesc = p.partDesc;
            DB_111206_scrapEntities db = new DB_111206_scrapEntities();
            var listOffers = db.Offers.ToList();
            var matches = listOffers.Where(x => x.Part.partDesc.Contains(p.Description)).ToList();

            List<DTO_Offer> offers = new List<DTO_Offer>();
            foreach (var offer in matches)
            {
                var o = new DTO_Offer
                {
                    Id = offer.offerID,
                    /* Parts = offer.partsID,// partsID),
                    Seller = Convert.ToInt32(offer.sellerID),
                    */
                    Price = Convert.ToInt32(offer.price),
                    Ostat = Convert.ToInt32(offer.ostat),
                    DT = offer.offerDate

                };
                offers.Add(o);
            }


            return offers;
        }

        public List<DTO_User> LoginUser(DTO_Login login)
        {
            DTO_User user = new DTO_User();
            List<DTO_User> users = new List<DTO_User>();

            using (DB_111206_scrapEntities db = new DB_111206_scrapEntities())
            {

                var match = db.Users.Where(u => u.email == login.Email && u.pwd == login.Password).FirstOrDefault();

                if (match != null)
                {
                    user.id = match.userID;
                    user.LastName = match.lName;
                    user.FirstName = match.fName;
                    user.Email = match.email;
                    user.Password = match.pwd;
                    user.Phone = match.phone;
                    users.Add(user);

                    UserLogin userlogin = new UserLogin();
                    userlogin.userID = user.id;
                    userlogin.lat = login.Latutude;
                    userlogin.lon = login.Longitude;
                    userlogin.logInDateTime = DateTime.Now;
                    db.UserLogins.Add(userlogin);
                    db.SaveChanges();
                    // email("New user login", "Hello, World!");
                }
            }
            return users;
        }

        public DTO_User VerifyUserLogin(DTO_Login uL)
        {
            //DB Connection
            DB_111206_scrapEntities db = new DB_111206_scrapEntities();
            //get table contents
            var listUsers = db.Users.ToList();
            var tempUser = listUsers.Where(x => x.email == uL.Email && x.pwd == uL.Password).FirstOrDefault();
            //create list to return with sql objects
            DTO_User user = new DTO_User();
            if (tempUser != null)
            {
                DTO_User verifiedUser = new DTO_User
                {
                    id = tempUser.userID,
                    FirstName = tempUser.fName,
                    LastName = tempUser.lName,
                    Phone = tempUser.phone,
                    Password = tempUser.pwd
                };
                //Add user to list for returning
                user = verifiedUser;
                //create odbject for UserLogins
                var loginItem = new Scrap_DAL.UserLogin
                {
                    userID = tempUser.userID,
                    lat = Convert.ToDouble(tempUser.lat),
                    lon = Convert.ToDouble(tempUser.lon),
                    logInDateTime = DateTime.Now

                };
                db.UserLogins.Add(loginItem);
                db.SaveChanges();
            }
            return user;
        }

        public List<DTO_User> RegisterUser(DTO_User u)
        {
            //DB Connection
            DB_111206_scrapEntities db = new DB_111206_scrapEntities();
            List<DTO_User> tempList = new List<DTO_User>();
            tempList.Add(u);
            var temp = new Scrap_DAL.User
            {
                //no userid because system creates it
                email = u.Email,
                fName = u.FirstName,
                lName = u.LastName,
                phone = u.Phone,
                pwd = u.Password,
            };
            db.Users.Add(temp);

            var loginItem = new Scrap_DAL.UserLogin
            {
                userID = temp.userID,
                lat = Convert.ToSingle(temp.lat),
                lon = Convert.ToSingle(temp.lon),
                logInDateTime = DateTime.Now
            };
            db.UserLogins.Add(loginItem);
            db.SaveChanges();
            return tempList;
        }

        public List<DTO_Transaction> GetTransactions(DTO_User user)
        {
            List<DTO_Transaction> transactions = new List<DTO_Transaction>();
            using (DB_111206_scrapEntities db = new DB_111206_scrapEntities())
            {

                var match = db.Users.Where(u => u.email == user.Email && u.pwd == user.Password).FirstOrDefault();

                if (match != null)
                {
                    var all_trans = match.Transactions.ToList();
                    {
                        foreach (var t in all_trans)
                        {
                            DTO_Transaction tran = new DTO_Transaction();
                            tran.Id = t.tranId;
                            tran.Price = (double)t.salePrice;
                            tran.DT = t.tranDate;
                            transactions.Add(tran);
                        }
                    }
                }
            }

            return transactions;
        }

        public List<DTO_WishListItems> GetWishListItems(DTO_User user)
        {
            List<DTO_WishListItems> wishList = new List<DTO_WishListItems>();
            using (DB_111206_scrapEntities db = new DB_111206_scrapEntities())
            {
                var match = db.Users.Where(u => u.email == user.Email && u.pwd == user.Password).FirstOrDefault();
                if (match != null)
                {
                    var itemsWanted = match.wishlistItems.ToList();
                    foreach (var i in itemsWanted)
                    {
                        DTO_WishListItems items = new DTO_WishListItems();
                        i.offerID = items.Offer;
                        i.wilID = items.Id;
                        i.userID = items.User;
                        wishList.Add(items);
                    }
                }

            }

            return wishList;
        }

        public List<DTO_Register> RegisterUser_Bekka(DTO_Register newUser)
        {
            DB_111206_scrapEntities db = new DB_111206_scrapEntities();
            User user = new User();
            List<DTO_Register> listOfLocalObjects = new List<DTO_Register>();
            var sucessfulPassword = db.Users.Where(c => c.pwd == newUser.Password).FirstOrDefault();

            if (sucessfulPassword == null)
            {
                user.pwd = sucessfulPassword.pwd;
                user.fName = newUser.FirstName;
                user.lName = newUser.LastName;
                user.email = newUser.Email;
                user.phone = newUser.Phone;
                user.lat = newUser.Latutude;
                user.lon = newUser.Longitude;

                listOfLocalObjects.Add(newUser);
                db.Users.Add(user);
            }
            return listOfLocalObjects;
        }

        public List<DTO_Offer> GetOffersByUser(DTO_User user)
        {
            List<DTO_Offer> offers = new List<DTO_Offer>();
            using (DB_111206_scrapEntities db = new DB_111206_scrapEntities())
            {
                var match = db.Users.Where(u => u.email == user.Email && u.pwd == user.Password).FirstOrDefault();
                if (match != null)
                {
                    var offers_ = match.Offers.ToList();
                    foreach (var i in offers_)
                    {
                        DTO_Offer bid = new DTO_Offer();
                        i.offerID = bid.Id;
                        i.offerDate = bid.DT;
                        i.price = (decimal)bid.Price;
                        offers.Add(bid);
                    }
                }

            }

            return offers;
        }

        /* public List<DTO_Transaction> PurchaseOffer_Checkout(DTO_Offer o, DTO_User u)
        {
            DB_111206_scrapEntities db = new DB_111206_scrapEntities();
            List<DTO_Transaction> returnOffers = new List<DTO_Transaction>();

            var offersTable = db.Offers.ToList();
            //offer to be purchased/edited in db
            var currentO = offersTable.Find(x => x.offerID == o.Id);

            var usersTable = db.Users.ToList();
            var tranTime = DateTime.Now.ToString("h:mm:ss tt");

            var tempTrans = new Scrap_DAL.Transaction
            {
                buyerID = Convert.ToInt32(u.id),
                offerID = Convert.ToInt32(o.Id),
                tranDate = Convert.ToDateTime(tranTime),
                salePrice = Convert.ToDecimal(o.Price * o.Quantity)
                //tranid is created as it is inserted in db
            };
            //add transaction to table
            db.Transactions.Add(tempTrans);
            db.SaveChanges();

            //modify table to correct parts in offer and ostat
            if (currentO != null)
            {
                currentO.partQuantity = currentO.partQuantity - o.Quantity;
                //if no more in quantity, alter ostat
                if (currentO.partQuantity == 0)
                {
                    currentO.ostat = 0;
                    //save changes to columns
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
            }
            return returnOffers;

        } */

    }

}
