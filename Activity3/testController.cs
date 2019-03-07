using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Activity3.Models;

namespace Activity3
{
    public class testController : ApiController
    {
        TestEntities1 db = new TestEntities1();
        [System.Web.Mvc.Route("api/test/getSomeData")]
        [HttpGet]
        public HttpResponseMessage getSomeData()
        {
            List<userT> outObjects = db.userTs.ToList();
            List<dynamic> newObj = new List<dynamic>();

            foreach (var u in outObjects)
            {
                dynamic obj = new ExpandoObject();
                obj.Name = u.Name;
                obj.Surname = u.Surname;

                newObj.Add(obj);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK, newObj);
            response.Headers.Add("Access-Control-Allow-Origin", "*");

            return response;
        }
        [System.Web.Mvc.Route("api/test/getMoreData")]
        [HttpGet]
        public HttpResponseMessage getMoreData()
        {
            userT user = db.userTs.FirstOrDefault();
            userINF gender = db.userINFs.Where(uu=>uu.idOfUser == user.userID).FirstOrDefault();
            userWork work = db.userWorks.Where(ii => ii.idUser == user.userID).First();
            List<dynamic> newObj = new List<dynamic>();
           dynamic obj = new ExpandoObject();
            
                    obj.Name = user.Name;
                    obj.Surname = user.Surname;
                    obj.Gender = gender.gender;
                    obj.TypeOfWork = work.workType;
            newObj.Add(obj);
            var response = Request.CreateResponse(HttpStatusCode.OK, newObj);
            response.Headers.Add("Access-Control-Allow-Origin", "*");

            return response;
           // return obj;
        }
        [System.Web.Mvc.Route("api/test/getMoreData")]
        [HttpGet]
        public HttpResponseMessage getMoreData(string name)
        {
            userT user = db.userTs.Where(yy=>yy.Name == name).FirstOrDefault();
            userINF gender = db.userINFs.Where(uu => uu.idOfUser == user.userID).FirstOrDefault();
            userWork work = db.userWorks.Where(ii => ii.idUser == user.userID).First();
            List<dynamic> newObj = new List<dynamic>();
            dynamic obj = new ExpandoObject();

            obj.Name = user.Name;
            obj.Surname = user.Surname;
            obj.Gender = gender.gender;
            obj.TypeOfWork = work.workType;

            newObj.Add(obj);
            var response = Request.CreateResponse(HttpStatusCode.OK, newObj);
            response.Headers.Add("Access-Control-Allow-Origin", "*");


            return response;
            //return obj;
        }

        [System.Web.Mvc.Route("api/test/insertData")]
        [HttpGet]
        public HttpResponseMessage insertData(string name,string surname,string gender,string work)
        {
            //Get all user info
            userT user = new userT();
            userINF u = new userINF();
            userWork w = new userWork();

            user.Name = name;
            user.Surname = surname;
           
            db.userTs.Add(user);
            db.SaveChanges();
            //Get id of user that we just inserted
            userT uID = db.userTs.Where(uu=>uu.Name==user.Name).FirstOrDefault();

            //ADD gender and worktype inthe db
            u.idOfUser = uID.userID;
            u.gender = gender;

            db.userINFs.Add(u);
            db.SaveChanges();

            w.idUser = uID.userID;
            w.workType = work;
            db.userWorks.Add(w);
            db.SaveChanges();

            //Send back this data to showcase the info we just added to the db
            List<dynamic> newObj = new List<dynamic>();
            dynamic obj = new ExpandoObject();

            obj.Name = name;
            obj.Surname = surname;
            obj.gender = gender;
            obj.TypeOfWork = work;
            newObj.Add(obj);
            var response = Request.CreateResponse(HttpStatusCode.OK, newObj);
            response.Headers.Add("Access-Control-Allow-Origin", "*");

            return response;
            //return obj;
        }
        [System.Web.Mvc.Route("api/test/updateData")]
        [HttpPost]
        public HttpResponseMessage updateData(string nameFirst,string name, string surname)
        {
            //Get all user info
            userT user = db.userTs.Where(yy => yy.Name == nameFirst).FirstOrDefault();
            user.Name = name;
            user.Surname = surname;
            db.SaveChanges();
            //Get id of user that we just inserted
           
            //Send back this data to showcase the info we just added to the db
            List<dynamic> newObj = new List<dynamic>();
            dynamic obj = new ExpandoObject();

            obj.Name = name;
            obj.Surname = surname;
            newObj.Add(obj);
            var response = Request.CreateResponse(HttpStatusCode.OK, newObj);
            response.Headers.Add("Access-Control-Allow-Origin", "*");

            return response;
            //return obj;
        }
    }
}