//hacked by rti
/*
 * Copyright (c) rti    おーるらいとりざーぶ
 * 
 * $Id$
 */
using System;
using System.Xml;
using System.IO;
using System.Drawing;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Security.Cryptography;

using Travis.Storage;

using System.Data;
using System.Data.OleDb;

namespace Zanetti.CodeExtendData{

    //情報
    abstract class DataInterface : IComparable
    {
        public abstract int getDate();
        public abstract void Read(XmlNode inX);
        public abstract XmlNode Write(XmlDocument ioDocument);

        public int CompareTo(object obj)
        {
            if (this.getDate() > ((DataInterface)obj).getDate())
            {
                return 1;
            }
            else if (this.getDate() < ((DataInterface)obj).getDate())
            {
                    return -1;
            }
            return 0;
        }

        public virtual int Compare(object x, object y)
        {
            if (((DataInterface)x).getDate() < ((DataInterface)y).getDate())
            {
                return -1;
            }
            else if (((DataInterface)x).getDate() > ((DataInterface)y).getDate())
            {
                return 1;
            }
            return 0;
        }
    }

    //ファンダメンタルな情報をまとめて返します。
    class DataBasket
    {
        public int Kettusan = 0;       //決算日 yyyymmdd
        public int Uriage = 0;         //売上(百万円)
        public int Keijyourieki = 0;   //経常利益(百万円)
        public int Rieki = 0;          //利益(百万円)
        public int Hitoikabueki = 0;   //一株益(銭)
        public int Haitoukin = 0;      //配当金(銭)
        public int Hitokabujyunsisan = 0;   //一株純資産
        public int ZenkiKettusan = 0;   //前期決算日 yyyymmdd
    };

    //決算情報
    class DataAccounts : DataInterface
    {
        public int Code;
        public int Date;
        public int Kubun;               //決算区分 0:1/4四半期    1:2/4中間決算   2:3/4四半期     3:4/4本決算
        public int ZenkiKettusan;       //前期決算日
//        public int ZenkiUriage;         //前期売上(百万円)
//        public int ZenkiKeijyourieki;   //前期経常利益(百万円)
//        public int Zenkirieki;          //前期利益(百万円)
//        public int ZenkiHitoikabueki;   //前期一株益(銭)
//        public int ZenkiHaitoukin;      //前期配当金(銭)
        public int KonkiKettusan;       //今期決算
        public int KonkiUriage;         //今期売上(百万円)
        public int KonkiKeijyourieki;   //今期経常利益(百万円)
        public int Konkirieki;          //今期利益(百万円)
        public int KonkiHitoikabueki;   //今期一株益(銭)
        public int KonkiHaitoukin;      //今期配当金(銭)
        public int JikiKettusan;        //次期決算
        public int JikiUriage;          //次期売上(百万円)
        public int JikiKeijyourieki;    //次期経常利益(百万円)
        public int Jikirieki;           //次期利益(百万円)
        public int JikiHitoikabueki;    //次期一株益(銭)
        public int JikiHaitoukin;       //次期配当金(銭)
        public int Hitokabujyunsisan;   //一株純資産



        public override int getDate()
        {
            return this.Date;
        }

        //読み込み
        public override void Read(XmlNode inX)
        {
            if (inX.Name != "決算") return;

            this.Date = int.Parse(inX.Attributes["日付"].InnerText);
            this.Code = int.Parse(inX.Attributes["コード"].InnerText);
            this.Kubun = int.Parse(inX["区分"].InnerText);
            this.ZenkiKettusan = int.Parse(inX["前期決算日"].InnerText);
//            this.ZenkiUriage = int.Parse(inX["前期売上"].InnerText);
//            this.ZenkiKeijyourieki = int.Parse(inX["前期経常利益"].InnerText);
//            this.Zenkirieki = int.Parse(inX["前期利益"].InnerText);
//            this.ZenkiHitoikabueki = int.Parse(inX["前期一株益"].InnerText);
//            this.ZenkiHaitoukin = int.Parse(inX["前期配当金"].InnerText);
            this.KonkiKettusan = int.Parse(inX["今期決算日"].InnerText);
            this.KonkiUriage = int.Parse(inX["今期売上"].InnerText);
            this.KonkiKeijyourieki = int.Parse(inX["今期経常利益"].InnerText);
            this.Konkirieki = int.Parse(inX["今期利益"].InnerText);
            this.KonkiHitoikabueki = int.Parse(inX["今期一株益"].InnerText);
            this.KonkiHaitoukin = int.Parse(inX["今期配当金"].InnerText);
            this.JikiKettusan = int.Parse(inX["次期決算日"].InnerText);
            this.JikiUriage = int.Parse(inX["次期売上"].InnerText);
            this.JikiKeijyourieki = int.Parse(inX["次期経常利益"].InnerText);
            this.Jikirieki = int.Parse(inX["次期利益"].InnerText);
            this.JikiHitoikabueki = int.Parse(inX["次期一株益"].InnerText);
            this.JikiHaitoukin = int.Parse(inX["次期配当金"].InnerText);
            this.Hitokabujyunsisan = int.Parse(inX["一株純資産"].InnerText);
        }


        public override XmlNode Write(XmlDocument ioDocument)
        {
            XmlNode element = ioDocument.CreateElement("決算");
            element.Attributes.Append(UtilX.CreateAttribute(ioDocument, "日付", this.Date.ToString()));
            element.Attributes.Append(UtilX.CreateAttribute(ioDocument, "コード", this.Code.ToString()));
            element.AppendChild(UtilX.CreateElemnt(ioDocument, "区分", this.Kubun.ToString()));
            element.AppendChild(UtilX.CreateElemnt(ioDocument, "前期決算日", this.ZenkiKettusan.ToString()));
//            element.AppendChild(UtilX.CreateElemnt(ioDocument, "前期売上", this.ZenkiUriage.ToString()));
//            element.AppendChild(UtilX.CreateElemnt(ioDocument, "前期経常利益", this.ZenkiKeijyourieki.ToString()));
//            element.AppendChild(UtilX.CreateElemnt(ioDocument, "前期利益", this.Zenkirieki.ToString()));
//            element.AppendChild(UtilX.CreateElemnt(ioDocument, "前期一株益", this.ZenkiHitoikabueki.ToString()));
//            element.AppendChild(UtilX.CreateElemnt(ioDocument, "前期配当金", this.ZenkiHaitoukin.ToString()));
            element.AppendChild(UtilX.CreateElemnt(ioDocument, "今期決算日", this.KonkiKettusan.ToString()));
            element.AppendChild(UtilX.CreateElemnt(ioDocument, "今期売上", this.KonkiUriage.ToString()));
            element.AppendChild(UtilX.CreateElemnt(ioDocument, "今期経常利益", this.KonkiKeijyourieki.ToString()));
            element.AppendChild(UtilX.CreateElemnt(ioDocument, "今期利益", this.Konkirieki.ToString()));
            element.AppendChild(UtilX.CreateElemnt(ioDocument, "今期一株益", this.KonkiHitoikabueki.ToString()));
            element.AppendChild(UtilX.CreateElemnt(ioDocument, "今期配当金", this.KonkiHaitoukin.ToString()));
            element.AppendChild(UtilX.CreateElemnt(ioDocument, "次期決算日", this.JikiKettusan.ToString()));
            element.AppendChild(UtilX.CreateElemnt(ioDocument, "次期売上", this.JikiUriage.ToString()));
            element.AppendChild(UtilX.CreateElemnt(ioDocument, "次期経常利益", this.JikiKeijyourieki.ToString()));
            element.AppendChild(UtilX.CreateElemnt(ioDocument, "次期利益", this.Jikirieki.ToString()));
            element.AppendChild(UtilX.CreateElemnt(ioDocument, "次期一株益", this.JikiHitoikabueki.ToString()));
            element.AppendChild(UtilX.CreateElemnt(ioDocument, "次期配当金", this.JikiHaitoukin.ToString()));
            element.AppendChild(UtilX.CreateElemnt(ioDocument, "一株純資産", this.Hitokabujyunsisan.ToString()));

            return element;
        }
    };


     //業績修正
     class DataRevised : DataInterface
     {
         public int Code;
         public int Date;
         public int SyuseimaeUriage;    //修正前売上
         public int Uriage;             //売上
         public int SyuseimaeKeijyourieki;    //修正前経常利益
         public int Keijyourieki;             //経常利益
         public int SyuseimaeZeibikieki;    //修正前税引益
         public int Zeibikieki;             //税引益
         public int SyuseimaeHaitou;    //修正前配当
         public int Haitou;             //配当

         public override int getDate()
         {
             return this.Date;
         }

         //読み込み
         public override void Read(XmlNode inX)
         {
             if (inX.Name != "業績修正") return;

             this.Date = int.Parse(inX.Attributes["日付"].InnerText);
             this.Code = int.Parse(inX.Attributes["コード"].InnerText);
             this.SyuseimaeUriage = int.Parse(inX["修正前売上"].InnerText);
             this.Uriage = int.Parse(inX["売上"].InnerText);
             this.SyuseimaeKeijyourieki = int.Parse(inX["修正前経常利益"].InnerText);
             this.Keijyourieki = int.Parse(inX["経常利益"].InnerText);
             this.SyuseimaeZeibikieki = int.Parse(inX["修正前税引益"].InnerText);
             this.Zeibikieki = int.Parse(inX["税引益"].InnerText);
             this.SyuseimaeHaitou = int.Parse(inX["修正前配当"].InnerText);
             this.Haitou = int.Parse(inX["配当"].InnerText);
         }

         public override XmlNode Write(XmlDocument ioDocument)
         {
             XmlNode element = ioDocument.CreateElement("業績修正");
             element.Attributes.Append(UtilX.CreateAttribute(ioDocument, "日付", this.Date.ToString()));
             element.Attributes.Append(UtilX.CreateAttribute(ioDocument, "コード", this.Code.ToString()));
             element.AppendChild(UtilX.CreateElemnt(ioDocument, "修正前売上", this.SyuseimaeUriage.ToString()));
             element.AppendChild(UtilX.CreateElemnt(ioDocument, "売上", this.Uriage.ToString()));
             element.AppendChild(UtilX.CreateElemnt(ioDocument, "修正前経常利益", this.SyuseimaeKeijyourieki.ToString()));
             element.AppendChild(UtilX.CreateElemnt(ioDocument, "経常利益", this.Keijyourieki.ToString()));
             element.AppendChild(UtilX.CreateElemnt(ioDocument, "修正前税引益", this.SyuseimaeZeibikieki.ToString()));
             element.AppendChild(UtilX.CreateElemnt(ioDocument, "税引益", this.Zeibikieki.ToString()));
             element.AppendChild(UtilX.CreateElemnt(ioDocument, "修正前配当", this.SyuseimaeHaitou.ToString()));
             element.AppendChild(UtilX.CreateElemnt(ioDocument, "配当", this.Haitou.ToString()));

             return element;
         }
     };


    //コード拡張を束ねるボス
    //子分とお話したいときは、ボスに話を通すというのがスジというもの
    //コイツはコード(銘柄)単位で存在する.
    class Data
    {
        private ArrayList AccountsHash; //決算情報
        private ArrayList RevisedHash;  //業績修正

        public Data()
        {
            this.AccountsHash = new ArrayList();
            this.RevisedHash = new ArrayList();
        }

        //読み込み
        public void Read(int inCode)
        {
            string filename = Util.GetExtDataFileName(inCode);

            if (!File.Exists(filename))
            {
                return;
            }
            XmlDocument document = new XmlDocument();
            document.Load(filename);
            XmlNodeList nodeList = document.DocumentElement.ChildNodes;
            foreach (XmlNode nd in nodeList)
            {
                if (nd.Name == "決算")
                {
                    DataAccounts p = new DataAccounts();
                    p.Read(nd);
                    this.AccountsHash.Add(p);
                }
                else if (nd.Name == "業績修正")
                {
                    DataRevised p = new DataRevised();
                    p.Read(nd);
                    this.RevisedHash.Add(p);
                }
            }

        }
        //書き込み
        public void Write(int inCode)
        {
            bool change = false;

            string filename = Util.GetExtDataFileName(inCode);
            XmlDocument document = new XmlDocument();
            document.AppendChild(document.CreateXmlDeclaration("1.0", "UTF-8", null));
            document.AppendChild(document.CreateElement("コード拡張"));

            foreach (DataAccounts p in this.AccountsHash)
            {
                XmlNode element = p.Write(document);
                document.DocumentElement.AppendChild(element);
                change = true;
            }
            foreach (DataRevised p in this.RevisedHash)
            {
                XmlNode element = p.Write(document);
                document.DocumentElement.AppendChild(element);
                change = true;
            }

            if (change)
            {
                document.Save(filename);
            }
        }

        //決算情報の修正
        public void Update( DataAccounts inAccounts)
        {
            UpdateLaw(inAccounts, this.AccountsHash);
        }
        //業績修正
        public void Update(DataRevised inRevised)
        {
            UpdateLaw(inRevised, this.RevisedHash);
        }

        //修正を行う実際の部分
        private void UpdateLaw(DataInterface inData, ArrayList inArrayList)
        {
            foreach (DataInterface p in inArrayList)
            {
                if (p.getDate() == inData.getDate() )
                {
                    inArrayList.Remove(p);
                    inArrayList.Add(inData);
                    inArrayList.Sort();
                    return;
                }
            }
            //ない場合は追加する.
            inArrayList.Add(inData);
            inArrayList.Sort();
        }

        //近いデータを見つけ出す
        public DataAccounts FindAccounts(int inDate )
        {
            return (DataAccounts)FindLaw(inDate, this.AccountsHash);
        }
        //近いデータを見つけ出す
        public DataRevised FindRevised(int inDate )
        {
            return (DataRevised)FindLaw(inDate,this.RevisedHash);
        }

        //一番合理的だと思われる決算、業績修正を見つけ出す
        private DataBasket FindFundamentalSub(int inDate)
        {
            //近場の決算と近場の業績修正を求める
            DataAccounts accounts = FindAccounts(inDate);
            DataRevised revised = FindRevised(inDate);

            DataBasket  ret = new DataBasket();

            if (accounts == null && revised == null)
            {
                return ret; //情報がない!!
            }
            if (accounts == null)
            {//業績修正を返す
                ret.Kettusan = revised.Date;
                ret.Uriage = revised.Uriage;
                ret.Keijyourieki = revised.Keijyourieki;
                ret.Rieki = revised.Zeibikieki;
                ret.Hitoikabueki = 0;
                ret.Haitoukin = revised.Haitou;
                ret.Hitokabujyunsisan = 0;
                ret.ZenkiKettusan = 0;
                return ret;
            }
            if (revised == null)
            {//決算情報を返す
                ret.Kettusan = accounts.JikiKettusan;
                ret.Uriage = accounts.JikiUriage;
                ret.Keijyourieki = accounts.JikiKeijyourieki;
                ret.Rieki = accounts.Jikirieki;
                ret.Hitoikabueki = accounts.JikiHitoikabueki;
                ret.Haitoukin = accounts.JikiHaitoukin;
                ret.Hitokabujyunsisan = accounts.Hitokabujyunsisan;
                ret.ZenkiKettusan = accounts.ZenkiKettusan;
                return ret;
            }

            if (accounts.getDate() >= revised.getDate() )
            {
                if (accounts.JikiUriage == 0 && accounts.JikiKeijyourieki == 0 && accounts.Jikirieki == 0)
                {
                    //時期決算情報が白紙!!!
                    //仕方ないので、業績修正を返す
                }
                else
                {
                    //決算情報を返す
                    ret.Kettusan = accounts.JikiKettusan;
                    ret.Uriage = accounts.JikiUriage;
                    ret.Keijyourieki = accounts.JikiKeijyourieki;
                    ret.Rieki = accounts.Jikirieki;
                    ret.Hitoikabueki = accounts.JikiHitoikabueki;
                    ret.Haitoukin = accounts.JikiHaitoukin;
                    ret.Hitokabujyunsisan = accounts.Hitokabujyunsisan;
                    ret.ZenkiKettusan = accounts.ZenkiKettusan;

                    return ret;
                }
            }
            //業績修正を返す
            ret.Kettusan = revised.Date;
            ret.Uriage = revised.Uriage;
            ret.Keijyourieki = revised.Keijyourieki;
            ret.Rieki = revised.Zeibikieki;
            ret.Hitoikabueki = (accounts.JikiHitoikabueki * revised.Zeibikieki) / accounts.Konkirieki;
            ret.Haitoukin = revised.Haitou;
            //一株資産が業績修正にないので、決算情報から逆算して求めます。
            //途中で増資をやられた場合、数字がおかしくなります。
            ret.Hitokabujyunsisan = (accounts.Hitokabujyunsisan * revised.Zeibikieki) / accounts.Konkirieki;
            ret.ZenkiKettusan = accounts.ZenkiKettusan;

            return ret;
        }
        //指定年数前の一番合理的だと思われる決算、業績修正を見つけ出す
        public DataBasket FindFundamental(int inDate,int inSkip )
        {
            DataBasket NullBasket = new DataBasket();
            DataBasket basket = FindFundamentalSub(inDate);

            for (int i = 0; i < inSkip; i++)
            {
                if (basket.ZenkiKettusan == 0)
                {
                    return NullBasket;  //譲歩がないので空を返します
                }
                basket = FindFundamentalSub( basket.ZenkiKettusan );
            }
            return basket;
        }

        //すでに確定している一番合理的だと思われる決算を見つけ出す
        private DataBasket FindFixedFundamentalSub(int inDate)
        {
            //近場の決算と近場の業績修正を求める
            DataAccounts accounts = FindAccounts(inDate);

            DataBasket ret = new DataBasket();

            if (accounts == null)
            {
                return ret; //情報がない!!
            }
            //決算情報を返す
            ret.Kettusan = accounts.KonkiKettusan;
            ret.Uriage = accounts.KonkiUriage;
            ret.Keijyourieki = accounts.KonkiKeijyourieki;
            ret.Rieki = accounts.Konkirieki;
            ret.Hitoikabueki = accounts.KonkiHitoikabueki;
            ret.Haitoukin = accounts.KonkiHaitoukin;
            ret.Hitokabujyunsisan = accounts.Hitokabujyunsisan;
            ret.ZenkiKettusan = accounts.ZenkiKettusan;

            return ret;
        }

        //すでに確定している指定年数前の一番合理的だと思われる決算を見つけ出す
        public DataBasket FindFixedFundamental(int inDate, int inSkip)
        {
            DataBasket NullBasket = new DataBasket();
            DataBasket basket = FindFixedFundamentalSub(inDate);

            for (int i = 0; i < inSkip; i++)
            {
                if (basket.ZenkiKettusan == 0)
                {
                    return NullBasket;  //譲歩がないので空を返します
                }
                basket = FindFixedFundamentalSub(basket.ZenkiKettusan);
            }
            return basket;
        }

        //近いデータを見つけ出す
        private DataInterface FindLaw(int inDate, ArrayList inArrayList)
        {
            DataInterface last = null;
            //配列は昇順にソートされているため、自分より大きいものが見つかるまで比較する.
            foreach (DataInterface p in inArrayList)
            {
                if (inDate <= p.getDate() )
                {
                    return last;
                }
                last = p;
            }
            //近いデータはない
            return last;
        }
    };

    //共通部分かな、かな
    public class UtilX
    {
        public static XmlElement CreateElemnt(XmlDocument ioDocument, string inName, string inValue)
        {
            XmlElement e = ioDocument.CreateElement(inName);
            XmlText text = ioDocument.CreateTextNode(inValue);
            e.AppendChild(text);
            return e;
        }
        public static XmlAttribute CreateAttribute(XmlDocument ioDocument, string inName, string inValue)
        {
            XmlAttribute e = ioDocument.CreateAttribute(inName);
            e.Value = inValue;
            return e;
        }
        
        /*
         * //ADO 版
        //Hashtable[ArrayList(ArrayList)]って構造で返ってくる(w
        //Hashtable[表の名前][行][列] という構成かな、、、
        //極悪だな。
        public static Hashtable XLSReader(string inFilename)
        {
            //ADO オブジェクトくんを作成します。
            ADODB.Connection connect = new ADODB.Connection();
            ADOX.Catalog catalog = new ADOX.Catalog();
            //エクセルファイルに接続します。
            connect.Open("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + inFilename + ";" + " Extended Properties=Excel 8.0;","","",0);

            catalog.ActiveConnection = connect;
            Hashtable returnValue = new Hashtable();

            string tablename ; 
            for(int t = 0 ; t < catalog.Tables.Count ; t ++)
            {
                //テーブル(表)
                if ( catalog.Tables[t].Type != "TABLE" )
                {
                    continue;   //テーブルぢゃないよ。
                }
                //テーブルが見つかったので、そいつにクエリを放ちます。
                tablename = catalog.Tables[t].Name;

			    //SQLのようなもの を構築.
			    string sql = "SELECT * FROM `" + tablename + "`";

			    //SQLの　すごい 実行
                object temp;    //何か返したいらしい、適当に入れる
                ADODB.Recordset recordset = connect.Execute(sql, out temp, (int)ADODB.ExecuteOptionEnum.adOptionUnspecified);
    			
                //行
                ArrayList arrayY = new ArrayList();
                for(int line = 1 ;  ! recordset.EOF ; line ++ )
                {
                    //列
                    ArrayList arrayX = new ArrayList();
                    for (int fields = 0; fields < recordset.Fields.Count; fields++)
                    {
                        arrayX.Add( recordset.Fields[fields].Value.ToString()   );
                    }
                    
                    arrayY.Add(arrayX);

                    //次の行へ
                    recordset.MoveNext();
                }

                returnValue[tablename] = arrayY;
                //開放
                recordset.Close();
                recordset = null;
                temp = null;
            }
            connect.Close();
            connect = null;

            catalog = null;


            return returnValue;
        }
        */
        //ADO.NET版
        //Hashtable[ArrayList(ArrayList)]って構造で返ってくる(w
        //Hashtable[表の名前][行][列] という構成かな、、、
        //極悪だな。
        public static Hashtable XLSReader(string inFilename)
        {
            //これに結果を代入します.
            Hashtable returnValue = new Hashtable();

            OleDbConnection connect = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + inFilename + ";" + " Extended Properties=Excel 8.0;");
            //接続開始
            connect.Open();

            //テーブル名を求めます
            string tablename;
            DataTable schemaTable = connect.GetOleDbSchemaTable(OleDbSchemaGuid.Tables,
                          new Object[] { null, null, null, "TABLE" });
            foreach (DataRow schemaTableRow in schemaTable.Rows)
            {
                //テーブルの名前
                tablename = schemaTableRow["TABLE_NAME"].ToString();

                //SQLのようなもの を構築します.
                string sql = "SELECT * FROM `" + tablename + "`";
                OleDbCommand command = connect.CreateCommand();
                command.CommandText = sql;

                OleDbDataAdapter adapter = new OleDbDataAdapter(command);
                DataSet dts = new DataSet();
                adapter.Fill(dts);

                DataTable table = dts.Tables[0];

                //行
                ArrayList arrayY = new ArrayList();

                //ADOとの互換性を取るためにヘッダーも結果に含めます。
                {
                    ArrayList arrayX = new ArrayList();
                    foreach (DataColumn column in table.Columns)
                    {
                        arrayX.Add(column.ToString());
                    }
                    arrayY.Add(arrayX);
                }

                foreach (DataRow row in table.Rows)
                {
                    //列
                    ArrayList arrayX = new ArrayList();
                    foreach (DataColumn column in table.Columns)
                    {
                        arrayX.Add(row[column].ToString());
                    }
                    arrayY.Add(arrayX);
                }
                returnValue[tablename] = arrayY;
            }

            connect.Close();
            connect = null; //開放汁

            return returnValue;
        }
    };
}

//------------------