using UnityEngine;
using System.Collections;
using Mono.Data.Sqlite;

public class MyDatabase : MonoBehaviour
{
	public static Database db;//資料庫物件

	string databaseName = "SCP.db";//資料庫名稱

	static string tableName = "Rank";//資料庫內的資料表名稱

	static SqliteDataReader reader;//搜尋資料表的資料

	static string show = "";//要顯示在螢幕上的內容

	/* Unity預設的函數執行順序為：Awake -> OnEablen -> Start -> FixedUpdate -> Update -> LateUpdate -> OnGUI -> (結束時繼續往下走，否則回到FixedUpdate) -> OnDisable -> OnDestroy */

	void Start ()
	{
		db = new Database(databaseName);//建立資料庫，同時進行連線，也可改寫成下面被註解的那兩行，由於不同平台所使用的連線語法都不一樣，所以我已經先預設好了。若要修改或得知Windows路徑則到Database.cs內搜尋「Windows端的資料庫存放位置」；Android則因為諸多限制，所以不建議修改。

		/*
        db = new Database();
        db.databaseConnection(databaseName);
        */

		db.openDatabase();//開啟資料庫

		if (db.isTableExists(tableName) == false)//確認是否已有指定的資料表，若沒有則創造該資料表同時插入資料
		{
			//db.createTable(tableName, new string[] { "Name", "Score" }, new string[] { "TEXT", "INTEGER" });//TEXT為SQLite的字串型態，INTEGER為SQLite的整數型態

			//db.insertInto(tableName, new string[] { "'Yang'", "21" });//資料庫的字串資料必須使用單引號框起來'Yang'
			//db.insertInto(tableName, new string[] { "'Qing'", "22" });

			db.createTable(tableName, new string[] {"Score" }, new string[] { "INTEGER" });
		}
	}

	public static void insert(int score){
		db.insertInto(tableName, new string[] { score+"" });
	}

	public static int[] loadAll(){
		reader = db.searchFullTableByDESC(tableName);
		int[] data = db.readIntData(reader, "Score");

		//將讀取到的第一筆資料顯示出來
		show = data[0].ToString();
		return data;
	}

	void OnGUI()
	{

		GUI.Label(new Rect(250, 0, 500, 500), show);
		/*
		if (GUI.Button(new Rect(0, 0, 200, 100), "搜尋Name資料並顯示"))
		{
			//搜尋和讀取符合的資料
			reader = db.searchAccordData(tableName, "Name", "=", "'Yang'");
			string[] data = db.readStringData(reader, "Name");

			//將讀取到的第一筆資料顯示出來
			show = data[0].ToString();
		}

		if (GUI.Button(new Rect(0, 150, 200, 100), "搜尋Score資料並顯示"))
		{
			//搜尋和讀取符合的資料
			reader = db.searchAccordData(tableName, "Score", ">", "21");
			int[] data = db.readIntData(reader, "Score");

			//將讀取到的第一筆資料顯示出來
			show = data[0].ToString();
		}*/
	}

	void OnDisable()
	{
		db.closeDatabaseConnecting();//當該程式碼所放置的物件被結束時關閉資料庫連線
	}

	void OnDestroy()
	{
		//釋放資料庫
		db.releaseDatabaseAllResources();
	}
}