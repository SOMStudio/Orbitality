using System;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.InteropServices;

public class UserManager : BaseUserManager {
	
	public static UserManager Instance { get; private set; }
	
	private FileStream filePlayerData;
	
	private bool dataWasRead = false;
	private bool dataNeedwWrite = false;

	void Awake()
	{
		if (!Instance)
		{
			Instance = this;
		}
		else
		{
			Destroy(this.gameObject);
		}
	}

	private void Start()
	{
		DontDestroyOnLoad(this.gameObject);
	}

	public void VisitLevel(int value)
	{
		if (dataWasRead)
		{
			if (GetLevel() < value)
			{
				SetLevel(value);

				dataNeedwWrite = true;
			}
		}
		else
		{
			LoadPrivateDataPlayer();
		}
	}
	
	private void OpenPlayerDataFileForWrite() {
		filePlayerData = File.Create(Application.persistentDataPath + "/playerinfo.dat");
	}

	private void OpenPlayerDataFileForRead() {
		filePlayerData = File.Open (Application.persistentDataPath + "/playerinfo.dat", FileMode.Open);
	}

	public void ClosePlayerDateFile() {
		filePlayerData.Close ();
	}

	/// <summary>
	/// save player data in file with encrypting, not use for Web-application (we can't write file)
	/// </summary>
	public void SavePrivateDataPlayer()
	{
		if (dataWasRead)
		{
			if (dataNeedwWrite)
			{
				BinaryFormatter bf = new BinaryFormatter();
				OpenPlayerDataFileForWrite();

				PlayerData data = new PlayerData();
				data.playerName = playerName;

				data.level = GetLevel();

				bf.Serialize(filePlayerData, data);
				ClosePlayerDateFile();

				dataNeedwWrite = false;
			}
		}
		else
		{
			LoadPrivateDataPlayer();
		}
	}

	/// <summary>
	/// restore player data from encrypting file.
	/// </summary>
	public void LoadPrivateDataPlayer()
	{
		if (!dataWasRead)
		{
			if (File.Exists(Application.persistentDataPath + "/playerinfo.dat"))
			{
				BinaryFormatter bf = new BinaryFormatter();
				OpenPlayerDataFileForRead();

				PlayerData data = (PlayerData) bf.Deserialize(filePlayerData);
				playerName = data.playerName;

				SetLevel(data.level);

				ClosePlayerDateFile();
			}
			else
			{
				GetDefaultData();
			}
			
			dataWasRead = true;
		}
	}
}

[System.Serializable]
class PlayerData
{
	public string playerName;
	
	public int level;
}
