using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using AForge;
//using AForge.Controls;
//using AForge.Imaging;
//using AForge.Video;
//using AForge.Video.DirectShow;
namespace FaceAzureWinform
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

      
    }

    public class Users
    {
        //编号
        public long id { get; set; }
        //姓名
        public string name { get; set; }
        //密码
        public string password { get; set; }
        //年龄
        public int age { get; set; }
        //电话
        public string phone { get; set; }
        //地址
        public string address { get; set; }
        //脸
        public string picture { get; set; }
    }

    public class SqlHelper
    {
        #region 获取数据库连接
        private static string GetConnectionString
        {
            get
            {
                return "Data Source=.;Initial Catalog=TestFaceDB;Persist Security Info=True;User ID=sa;Password=171268"; //转换成string类型
            }
        }
        #endregion

        #region 查询多条记录
        /// <summary>
        /// 查询多条记录
        /// params SqlParameter  param  表示既可以传过来数组  也可以传过来单个值
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="type"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static SqlDataReader ExcuteReader(string sql, CommandType type, params SqlParameter[] param)
        {
            SqlConnection conn = new SqlConnection(GetConnectionString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            PreaPareCommand(sql, conn, cmd, type, param);
            //参数是关闭连接
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }
        #endregion


        #region DataSet
        public static DataSet ExexuteDataset(string sql, CommandType type, params SqlParameter[] param)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                PreaPareCommand(sql, conn, cmd, type, param);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                return ds;
            }
        }
        #endregion


        #region 查询返回一条记录
        /// <summary>
        /// 查询返回一条记录
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="type"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static Object ExecuteScalar(string sql, CommandType type, params SqlParameter[] param)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                PreaPareCommand(sql, conn, cmd, type, param);
                return cmd.ExecuteScalar();
            }
        }
        #endregion

        #region 命令对象装配
        //命令对象装配
        private static void PreaPareCommand(string sql, SqlConnection conn, SqlCommand cmd, CommandType type, params SqlParameter[] param)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Close();
                conn.Open();
            }
            cmd.CommandType = type;
            if (param != null)
            {
                foreach (SqlParameter p in param)
                {
                    cmd.Parameters.Add(p);
                }
            }
        }
        #endregion

        #region 增删改
        public static int ExecuteNonQuery(string sql, CommandType type, params SqlParameter[] param)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                PreaPareCommand(sql, conn, cmd, type, param);
                return cmd.ExecuteNonQuery();
            }
        }
        #endregion

    }
}
