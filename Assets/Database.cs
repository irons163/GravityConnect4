using System;
using Mono.Data.Sqlite;
using UnityEngine;
using System.Collections.Generic;
using System.Text;

public class Database
{
	#region Variable

	private SqliteConnection dbSqlConnection = null;//資料庫連線
	private SqliteCommand dbSqlCommand = null;//資料庫的命令功能
	private SqliteDataReader dbSqlReader = null;//讀取資料庫的資料

	#endregion

	#region Open、Connect、Close、Table、Execute、Release

	/// <summary>
	/// 資料庫內建的建構子，無任何程式碼
	/// </summary>
	public Database()
	{

	}

	/// <summary>
	/// 資料庫建構子，同時執行資料庫連線的動作
	/// </summary>
	/// <param name="databaseName">資料庫完整路徑和名稱</param>
	public Database(string databaseName)
	{
		databaseConnection(databaseName);
	}

	/// <summary>
	/// 資料庫連線
	/// </summary>
	/// <param name="databaseName">資料庫完整路徑和名稱</param>
	public void databaseConnection(string databaseName)
	{
		string connectString;

		#if UNITY_EDITOR
			//connectString = "URI=file:" + Application.persistentDataPath + "/";
		//Windows端的資料庫存放位置，位置三選一

		//1.此位置為C:\Users\使用者名稱\AppData\LocalLow\DefaultCompany\專案名稱
		connectString = @"Data Source=";

		//2.此位置若是在Unity執行，則會放置在專案資料夾內；若是輸出成exe後則是放置在輸出後的Data資料夾內
		//connectString = @"Data Source=" + Application.dataPath + "/";

		//3.程式人員自行指定位置
		//connectString = @"Data Source=" + ;
		#elif UNITY_ANDROID
		//若是在Android平台
		connectString = "URI=file:" + Application.persistentDataPath + "/";
		if (!System.IO.File.Exists(Application.persistentDataPath + "/" + databaseName))
		{
			WWW loadDB = new WWW("jar:file://" + Application.dataPath + "!/assets/" + databaseName);
			while (!loadDB.isDone)
			{}
			System.IO.File.WriteAllBytes(Application.persistentDataPath + "/" + databaseName, loadDB.bytes);
		}

		#elif UNITY_IPHONE
		//若是在iOS平台

		connectString = "Data Source=" + Application.persistentDataPath + "/";
		#else
		//Windows端的資料庫存放位置，位置三選一

		//1.此位置為C:\Users\使用者名稱\AppData\LocalLow\DefaultCompany\專案名稱
		connectString = @"Data Source=" + Application.persistentDataPath + "/";

		//2.此位置若是在Unity執行，則會放置在專案資料夾內；若是輸出成exe後則是放置在輸出後的Data資料夾內
		//connectString = @"Data Source=" + Application.dataPath + "/";

		//3.程式人員自行指定位置
		//connectString = @"Data Source=" + ;

		#endif
		dbSqlConnection = new SqliteConnection(connectString + databaseName);
		}

		/// <summary>
		/// 開啟資料庫
		/// </summary>
		public void openDatabase()
		{
		dbSqlConnection.Open();
		}

		/// <summary>
		/// 關閉資料庫連線，必須在呼叫releaseDatabaseAllResources()之前執行
		/// </summary>
		public void closeDatabaseConnecting()
		{
		dbSqlConnection.Close();
		}

		/// <summary>
		/// 釋放資料庫物件的全部資源，且全設為null
		/// </summary>
		public void releaseDatabaseAllResources()
		{
		if (dbSqlReader != null)
		{
		dbSqlReader.Dispose();
		dbSqlReader = null;
		}
		if (dbSqlCommand != null)
		{
		dbSqlCommand.Dispose();
		dbSqlCommand = null;
		}
		if (dbSqlConnection != null)
		{
		dbSqlConnection.Dispose();
		dbSqlConnection = null;
		}
		}

		/// <summary>
		/// 確認該資料庫是否有某資料表。若為true，代表資料表已存在；若為false，代表資料表不存在
		/// </summary>
		/// <param name="tableName">資料表名稱</param>
		/// <returns></returns>
		public bool isTableExists(string tableName)
		{
		dbSqlCommand = dbSqlConnection.CreateCommand();
		dbSqlCommand.CommandText = "SELECT COUNT(*) FROM sqlite_master where type='table' and name='" + tableName + "';";

		if (Convert.ToInt32(dbSqlCommand.ExecuteScalar()) == 1)
		{
		return true;
		}
		else
		{
		return false;
		}
		}

		/// <summary>
		/// 執行SQL的指令
		/// </summary>
		/// <param name="sqlQuery">SQL指令</param>
		/// <returns></returns>
		public SqliteDataReader executeQuery(string sqlQuery)
		{
		dbSqlCommand = dbSqlConnection.CreateCommand();
		dbSqlCommand.CommandText = sqlQuery;
		dbSqlReader = dbSqlCommand.ExecuteReader();

		return dbSqlReader;
		}

		/// <summary>
		/// 建立資料表
		/// </summary>
		/// <param name="tableName">資料表名稱</param>
		/// <param name="column">資料表的column名稱</param>
		/// <param name="columnType">資料表的column格式</param>
		/// <returns></returns>
		public SqliteDataReader createTable(string tableName, string[] column, string[] columnType)
		{
		if (column.Length != columnType.Length)
		{
		throw new SqliteException("column.Length != columnType.Length");
		}

		StringBuilder query = new StringBuilder();
		query.Append("CREATE TABLE " + tableName + " (" + column[0] + " " + columnType[0]);

		for (int i = 1; i < column.Length; i++)
		{
		query.Append(", " + column[i] + " " + columnType[i]);
		}
		query.Append(")");

		return executeQuery(query.ToString());
		}

		#endregion

		#region Insert And Update

		/// <summary>
		/// 將資料按順序寫入資料表內，一次限寫一row的資料
		/// </summary>
		/// <param name="tableName">資料表名稱</param>
		/// <param name="value">要寫入的資料</param>
		/// <returns></returns>
		public SqliteDataReader insertInto(string tableName, string[] value)
		{
		StringBuilder query = new StringBuilder();
		query.Append("INSERT INTO " + tableName + " VALUES (" + value[0]);

		for (int i = 1; i < value.Length; i++)
		{
		query.Append(", " + value[i]);
		}
		query.Append(")");

		return executeQuery(query.ToString());
		}

		/// <summary>
		/// 將資料寫入資料表的特定欄位，沒寫入欄位的將被設為null
		/// </summary>
		/// <param name="tableName">資料表名稱</param>
		/// <param name="column">寫入第column個</param>
		/// <param name="columnValue">寫入column的資料</param>
		/// <returns></returns>
		public SqliteDataReader insertIntoSpecific(string tableName, string[] column, string[] columnValue)
		{
		if (column.Length != columnValue.Length)
		{
		throw new SqliteException("column.Length != columnValue.Length");
		}

		StringBuilder query = new StringBuilder();
		query.Append("INSERT INTO " + tableName + "(" + column[0]);

		for (int i = 1; i < column.Length; ++i)
		{
		query.Append(", " + column[i]);
		}
		query.Append(") VALUES (" + columnValue[0]);

		for (int i = 1; i < columnValue.Length; ++i)
		{
		query.Append(", " + columnValue[i]);
		}
		query.Append(")");

		return executeQuery(query.ToString());
		}

		/// <summary>
		/// 修改資料表特定欄位的值
		/// </summary>
		/// <param name="tableName">資料表名稱</param>
		/// <param name="column">要修改的值所在的column名稱</param>
		/// <param name="columnValue">修改後的新值</param>
		/// <param name="selectKey">搜尋符合此名稱的column</param>
		/// <param name="selectValue">selectKey內符合該值的row將被修改</param>
		/// <returns></returns>
		public SqliteDataReader updateInto(string tableName, string[] column, string[] columnValue, string selectKey, string selectValue)
		{
		StringBuilder query = new StringBuilder();
		query.Append("UPDATE " + tableName + " SET " + column[0] + " = " + columnValue[0]);

		for (int i = 1; i < column.Length; i++)
		{
		query.Append(", " + column[i] + " = " + columnValue[i]);
		}
		query.Append(" WHERE " + selectKey + " = " + selectValue + " ");

		return executeQuery(query.ToString());
		}

		#endregion

		#region Delete

		/// <summary>
		/// 刪除資料表內符合判斷的全部資料(但資料表以及column欄位保留)
		/// </summary>
		/// <param name="tableName">資料表名稱</param>
		/// <param name="column">要搜尋的column名稱</param>
		/// <param name="columnValue">若該column內有符合此值的話，則將該值的row全部刪除</param>
		/// <returns></returns>
		public SqliteDataReader deleteAccordData(string tableName, string[] column, string[] columnValue)
		{
		StringBuilder query = new StringBuilder();
		query.Append("DELETE FROM " + tableName + " WHERE " + column[0] + " = " + columnValue[0]);

		for (int i = 1; i < column.Length; i++)
		{
		query.Append(" or " + column[i] + " = " + columnValue[i]);
		}

		return executeQuery(query.ToString());
		}

		/// <summary>
		/// 刪除資料表內的全部資訊(但資料表以及column欄位保留)
		/// </summary>
		/// <param name="tableName">要刪除資訊的資料表名稱</param>
		/// <returns></returns>
		public SqliteDataReader deleteAllData(string tableName)
		{
		string query = "DELETE  FROM " + tableName;

		return executeQuery(query);
		}

		/// <summary>
		/// 刪除資料表
		/// </summary>
		/// <param name="tableName">要被刪除的資料表名稱</param>
		/// <returns></returns>
		public SqliteDataReader deleteTable(string tableName)
		{
		string query = "DROP TABLE " + tableName;

		return executeQuery(query);
		}

		#endregion

		#region Search

		/// <summary>
		/// 搜尋資料表內符合的資訊，將符合的row整個取出
		/// </summary>
		/// <param name="tableName">資料表名稱</param>
		/// <param name="column">要搜尋的column欄位名稱</param>
		/// <param name="operation">比較運算符號</param>
		/// <param name="Value">被比較的數值</param>
		/// <returns></returns>
		public SqliteDataReader searchAccordData(string tableName, string column, string operation, string Value)
		{

		string query = "SELECT * FROM " + tableName + " WHERE " + column + operation + Value;

		return executeQuery(query);
		}

		/// <summary>
		/// 對指定的資料表進行模糊搜尋
		/// </summary>
		/// <param name="tableName">資料表名稱</param>
		/// <param name="column">要搜尋的column欄位名稱</param>
		/// <param name="Value">要搜尋的數值，且英文不分大小寫，請用陣列區隔每組數值。例如：new string[]{"'早'", "'安'"}，則會搜尋指定的column中擁有 "早" 和 "安" 的資料</param>
		/// <returns></returns>
		public SqliteDataReader searchBlurryData(string tableName, string column, string[] Value)
		{
		StringBuilder query = new StringBuilder();
		query.Append("SELECT * FROM " + tableName + " WHERE " + column + " LIKE " + "'%");

		for (int i = 0; i < Value.Length; i++ )
		{
		query.Append(Value[i] + "%");
		}
		query.Append("'");

		return executeQuery(query.ToString());
		}

		/// <summary>
		/// 讀取資料表內所有欄位的資料
		/// </summary>
		/// <param name="tableName">資料表名稱</param>
		/// <returns></returns>
		public SqliteDataReader searchFullTable(string tableName)
		{
		string query = "SELECT * FROM " + tableName;
		return executeQuery(query);
		}

		public SqliteDataReader searchFullTableByDESC(string tableName)
		{
		string query = "SELECT * FROM " + tableName + " ORDER BY Score DESC";
		return executeQuery(query);
		}

		#endregion

		#region Read

		/// <summary>
		/// 讀取SqliteDataReader內的string型態資料，將符合的數值全部回傳(string)
		/// </summary>
		/// <param name="reader">要被讀取的SqliteDataReader物件</param>
		/// <param name="search">該物件內要被讀取的column值</param>
		/// <returns></returns>
		public string[] readStringData(SqliteDataReader reader, string search)
		{
		List<string> list = new List<string>();

		while (reader.Read())
		{
		list.Add(reader.GetString(reader.GetOrdinal(search)));
		}

		return list.ToArray();
		}

		/// <summary>
		/// 讀取SqliteDataReader內的Int16型態資料(short)，將符合的數值全部回傳
		/// </summary>
		/// <param name="reader">要被讀取的SqliteDataReader物件</param>
		/// <param name="search">該物件內要被讀取的column值</param>
		/// <returns></returns>
		public Int16[] readShortData(SqliteDataReader reader, string search)
		{
		List<Int16> list = new List<Int16>();

		while (reader.Read())
		{
		list.Add(reader.GetInt16(reader.GetOrdinal(search)));
		}

		return list.ToArray();
		}

		/// <summary>
		/// 讀取SqliteDataReader內的Int32型態資料(int)，將符合的數值全部回傳
		/// </summary>
		/// <param name="reader">要被讀取的SqliteDataReader物件</param>
		/// <param name="search">該物件內要被讀取的column值</param>
		/// <returns></returns>
		public Int32[] readIntData(SqliteDataReader reader, string search)
		{
		List<Int32> list = new List<Int32>();

		while (reader.Read())
		{
		list.Add(reader.GetInt32(reader.GetOrdinal(search)));
		}

		return list.ToArray();
		}

		/// <summary>
		/// 讀取SqliteDataReader內的Int64型態資料(long)，將符合的數值全部回傳
		/// </summary>
		/// <param name="reader">要被讀取的SqliteDataReader物件</param>
		/// <param name="search">該物件內要被讀取的column值</param>
		/// <returns></returns>
		public Int64[] readLongData(SqliteDataReader reader, string search)
		{
		List<Int64> list = new List<Int64>();

		while (reader.Read())
		{
		list.Add(reader.GetInt64(reader.GetOrdinal(search)));
		}

		return list.ToArray();
		}

		/// <summary>
		/// 讀取SqliteDataReader內的float型態資料，將符合的數值全部回傳(float)
		/// </summary>
		/// <param name="reader">要被讀取的SqliteDataReader物件</param>
		/// <param name="search">該物件內要被讀取的column值</param>
		/// <returns></returns>
		public float[] readFloatData(SqliteDataReader reader, string search)
		{
		List<float> list = new List<float>();

		while (reader.Read())
		{
		list.Add(reader.GetFloat(reader.GetOrdinal(search)));
		}

		return list.ToArray();
		}

		/// <summary>
		/// 讀取SqliteDataReader內的double型態資料，將符合的數值全部回傳(double)
		/// </summary>
		/// <param name="reader">要被讀取的SqliteDataReader物件</param>
		/// <param name="search">該物件內要被讀取的column值</param>
		/// <returns></returns>
		public double[] readDoubleData(SqliteDataReader reader, string search)
		{
		List<double> list = new List<double>();

		while (reader.Read())
		{
		list.Add(reader.GetDouble(reader.GetOrdinal(search)));
		}

		return list.ToArray();
		}

		/// <summary>
		/// 讀取SqliteDataReader內的bool型態資料，將符合的數值全部回傳(bool)
		/// </summary>
		/// <param name="reader">要被讀取的SqliteDataReader物件</param>
		/// <param name="search">該物件內要被讀取的column值</param>
		/// <returns></returns>
		public bool[] readBoolData(SqliteDataReader reader, string search)
		{
		List<bool> list = new List<bool>();

		while (reader.Read())
		{
		list.Add(reader.GetBoolean(reader.GetOrdinal(search)));
		}

		return list.ToArray();
		}

		#endregion
		}
