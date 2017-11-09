using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DatatableDemo.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult IndexExt()
        {
            return View();
        }

        public ActionResult SetScore(string Id, string score)
        {
            int iScore;
            string result = string.Empty;
            object obj = null;
            if (int.TryParse(score, out iScore))
            {
                string sql = @"update dbo.uEvaluationScore set score={0} where id={1}";
                sql = string.Format(sql, score, Id);

                string sqlcon = System.Configuration.ConfigurationManager.ConnectionStrings["SqlCon"].ConnectionString;
                System.Data.DataTable dt = new System.Data.DataTable();
                using (SqlConnection con = new SqlConnection(sqlcon))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = sql;
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                obj = new { result = result };
            }
            else
            {
                obj = new { result = "Score is not interge type" };
            }
            return Json(obj);
        }




        [HttpPost]
        public ActionResult GetSubData(string AssessmentId, string UserId, string areaId)
        {
            string sql = @"
                            SELECT 
	                            c.title
	                            ,a.Id
	                            ,b.agentCode
	                            ,b.CompanyNameCh
	                            ,a.AssessmentId
	                            ,a.UserId
	                            ,a.Score
	                            ,a.Weighting 
                            FROM 
	                            uEvaluationScore a 
	                            inner join dbo.uAgent B on a.agentid=b.AgentId 
	                            inner join dbo.uArea c on b.areaid=c.areaid 
                            where a.AssessmentId ={0}
	                            and 
		                            a.UserId ={1}
	                            and 
		                            c.areaid={2}
                            ";
            sql = string.Format(sql, AssessmentId, UserId, areaId);

            string sqlcon = System.Configuration.ConfigurationManager.ConnectionStrings["SqlCon"].ConnectionString;
            System.Data.DataTable dt = new System.Data.DataTable();
            using (SqlConnection con = new SqlConnection(sqlcon))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = sql;
                    SqlDataAdapter adt = new SqlDataAdapter(cmd);
                    con.Open();

                    adt.Fill(dt);
                }
            }

            List<object> datas = new List<object>();

            foreach (DataRow dr in dt.Rows)
            {
                object data = new
                {
                    title = dr["title"],
                    Id = dr["Id"],
                    agentCode = dr["agentCode"],
                    CompanyNameCh = dr["CompanyNameCh"],
                    AssessmentId = dr["AssessmentId"],
                    UserId = dr["UserId"],
                    Score = dr["Score"],
                    Weighting = dr["Weighting"]
                };
                datas.Add(data);
            }

            return Json(new { status = 0, data = datas });

        }

        public ActionResult GetArea(string AssessmentId, string UserId)
        {

            string sql = @" SELECT 
	                            c.title as Area
                                ,c.AreaId
                            FROM 
	                            uEvaluationScore a inner join dbo.uAgent B on a.agentid=b.AgentId 
	                            inner join dbo.uArea c on b.areaid=c.areaid 
                            where 
	                            a.AssessmentId ={0}
	                            and 
	                            a.UserId ={1}
                            group by
	                            c.Title	
                                ,c.AreaId";
            sql = string.Format(sql, AssessmentId, UserId);
            string sqlcon = System.Configuration.ConfigurationManager.ConnectionStrings["SqlCon"].ConnectionString;
            System.Data.DataTable dt = new System.Data.DataTable();
            using (SqlConnection con = new SqlConnection(sqlcon))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = sql;
                    SqlDataAdapter adt = new SqlDataAdapter(cmd);
                    con.Open();

                    adt.Fill(dt);
                }
            }

            List<object> datas = new List<object>();

            foreach (DataRow dr in dt.Rows)
            {
                object data = new
                {
                    id = (int)dr["AreaId"],
                    text = dr["Area"]
                };
                datas.Add(data);
            }


            return Json(datas, JsonRequestBehavior.AllowGet);

            //  return Json(new { status = 0, data = datas });

        }

        [HttpPost]
        public ActionResult GetParentData()
        {
            string sql = @"select a.AssessmentId,c.HDescription,a.PermitGroupId,b.UserId,b.Area,c.TermsId 
                            from dbo.uPermitweight a  (nolock) INNER JOIN dbo.uuser b (nolock) 
                            on a.PermitGroupId=b.PermitGroupId INNER JOIN 
                            dbo.uAssessment c (nolock) on a.AssessmentId=c.AssessmentId 
                            where c.LCV=0 and b.UserId =135
                            and c.TermsId= 1";


            string sqlcon = System.Configuration.ConfigurationManager.ConnectionStrings["SqlCon"].ConnectionString;
            System.Data.DataTable dt = new System.Data.DataTable();
            using (SqlConnection con = new SqlConnection(sqlcon))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = sql;
                    SqlDataAdapter adt = new SqlDataAdapter(cmd);
                    con.Open();

                    adt.Fill(dt);
                }
            }

            List<object> datas = new List<object>();

            foreach (DataRow dr in dt.Rows)
            {
                object data = new
                {
                    AssessmentId = (int)dr["AssessmentId"],
                    HDescription = dr["HDescription"].ToString(),
                    PermitGroupId = (int)dr["PermitGroupId"],
                    UserId = (int)dr["UserId"],
                    Area = dr["Area"],
                    TermsId = (int)dr["TermsId"]
                };
                datas.Add(data);
            }

            return Json(new { status = 0, data = datas });


        }


        public ActionResult TestIndex()
        {
            return View();
        }
    }
}