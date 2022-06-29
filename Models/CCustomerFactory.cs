using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace prjDemo.Models
{
    public class CCustomerFactory
    {
        public void insert(CCustomer p)
        {
            List<SqlParameter> paras = new List<SqlParameter>();

            string sql = "insert into tCustomer(";
            if (!string.IsNullOrEmpty(p.name))
                sql += "tName,";
            if (!string.IsNullOrEmpty(p.phone))
                sql += "tPhone,";
            if (!string.IsNullOrEmpty(p.address))
                sql += "tAddress,";
            if (!string.IsNullOrEmpty(p.password))
                sql += "tPassword,";

            sql = sql.Trim();
            if (sql.Substring(sql.Length - 1, 1) == ",")
                sql = sql.Substring(0, sql.Length - 1);

            sql += ")values(";

            if (!string.IsNullOrEmpty(p.name))
            {
                sql += "@name,";
                paras.Add(new SqlParameter("name", p.name));
            }
            if (!string.IsNullOrEmpty(p.phone))
            {
                sql += "@phone,";
                paras.Add(new SqlParameter("phone", p.phone));
            }
            if (!string.IsNullOrEmpty(p.address))
            {
                sql += "@address,";
                paras.Add(new SqlParameter("address", p.address));
            }
            if (!string.IsNullOrEmpty(p.password))
            {
                sql += "@password,";
                paras.Add(new SqlParameter("password", p.password));
            }

            sql = sql.Trim();
            if (sql.Substring(sql.Length - 1, 1) == ",")
                sql = sql.Substring(0, sql.Length - 1);
            sql += ")";
            excutesql(sql, paras);

        }

        internal CCustomer queryByPhone(string txtAccount)
        {
            string sql = "select * from tCustomer where tphone=@phone";
            List<SqlParameter> paras = new List<SqlParameter>();
            paras.Add(new SqlParameter("phone", txtAccount));
            var list = queryBySql(sql, paras);
            if (list.Count == 0)
                return null;
            return list[0];
        }

        internal List<CCustomer> queryByKeyword(string keyword)
        {
            string sql = "select * from tCustomer where tName LIKE @keyword ";
            sql += " OR tPhone LIKE @keyword ";
            sql += " OR tAddress LIKE @keyword ";


            List<SqlParameter> paras = new List<SqlParameter>();
            paras.Add(new SqlParameter("keyword", "%" + keyword + "%"));
            return queryBySql(sql, paras);
        }

        public void delete(int? c)
        {
            string sql = "DELETE FROM tCustomer where tid=@id";
            List<SqlParameter> paras = new List<SqlParameter>();
            paras.Add(new SqlParameter("id", c));
            queryBySql(sql, paras);
        }

        public void update(CCustomer p)
        {
            List<SqlParameter> paras = new List<SqlParameter>();

            string sql = "update tCustomer set ";

            if (!string.IsNullOrEmpty(p.name))
            {
                sql += "tName=@name,";
                paras.Add(new SqlParameter("name", p.name));
            }
            if (!string.IsNullOrEmpty(p.phone))
            {
                sql += "tPhone=@phone,";
                paras.Add(new SqlParameter("phone", p.phone));
            }
            if (!string.IsNullOrEmpty(p.address))
            {
                sql += "tAddress=@address,";
                paras.Add(new SqlParameter("address", p.address));
            }
            if (!string.IsNullOrEmpty(p.password))
            {
                sql += "tPassword=@password,";
                paras.Add(new SqlParameter("password", p.password));
            }

            sql = sql.Trim();
            if (sql.Substring(sql.Length - 1, 1) == ",")
                sql = sql.Substring(0, sql.Length - 1);

            sql += " where tid=@id";
            paras.Add(new SqlParameter("id", p.id));
            excutesql(sql, paras);

        }

        private static void excutesql(string sql, List<SqlParameter> sqlParameter)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=.;Initial Catalog=dbDemo;Integrated Security=True";

            using (con)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                if (sqlParameter != null)
                    foreach (SqlParameter p in sqlParameter)
                        cmd.Parameters.Add(p);
                cmd.ExecuteNonQuery();
            }
        }

        public CCustomer queryById(int id)
        {
            string sql = "select * from tCustomer where tid=@id";
            List<SqlParameter> paras = new List<SqlParameter>();
            paras.Add(new SqlParameter("id", id));
            var list = queryBySql(sql, paras);
            if (list.Count == 0)
                return null;
            return list[0];

        }

        public List<CCustomer> queryAll()
        {
            return queryBySql("select * from tCustomer", null);
        }

        private List<CCustomer> queryBySql(string sql, List<SqlParameter> sqlParameter)
        {
            List<CCustomer> list = new List<CCustomer>();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=.;Initial Catalog=dbDemo;Integrated Security=True";

            using (con)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);

                if (sqlParameter != null)
                    foreach (SqlParameter p in sqlParameter)
                        cmd.Parameters.Add(p);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new CCustomer()
                    {
                        id = (int)reader["tid"],
                        name = reader["tName"].ToString(),
                        phone = reader["tPhone"].ToString(),
                        address = reader["tAddress"].ToString(),
                        password = reader["tPassword"].ToString()
                    });
                }

                return list;
            }
        }
    }
}