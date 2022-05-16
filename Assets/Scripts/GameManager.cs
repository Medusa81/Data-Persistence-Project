using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
#if UNITY_EDITOR
	using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;
	
	public string userName;
	public int highScorePoints;
	public string highScoreName;
	public GameObject changeName, changeUserButton;
	public TextMeshProUGUI currentUser, currentHighscore;
	public TMP_InputField userNameField;
	
	void Awake()
	{
		if(Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);
		
		LoadData();
		currentUser.text = "Current user: " + userName;
		currentHighscore.text = "Highscore: " + highScoreName + ": " + highScorePoints;
	}
	
	public void StartGame()
	{
		SceneManager.LoadScene(1);
	}
	
	public void QuitGame()
	{
		SaveData();
		#if UNITY_EDITOR
			EditorApplication.ExitPlaymode();
		#else
			Application.Quit(); 
		#endif
	}
	
	public void ChangeUserName()
	{
		changeName.SetActive(true);
		changeUserButton.SetActive(false);
	}
	
	public void ApllyUserName()
	{
		changeName.SetActive(false);
		changeUserButton.SetActive(true);
		userName = changeName.GetComponentInChildren<TMP_InputField>().text;
		currentUser.text = "Current user: " + userName;
		SaveData();
	}
	

	class Data
	{
		public string highScoreName;
		public int highScorePoints;
		public string userName;
	}
	
	public void SaveData()
	{
		Data data = new Data();
		data.highScoreName = highScoreName;
		data.highScorePoints = highScorePoints;
		data.userName = userName;
		
		string json = JsonUtility.ToJson(data);
		
		File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
		
	}
	
	public void LoadData()
	{
		string path = Application.persistentDataPath + "/savefile.json";
		
		if(File.Exists(path))
		{
			string json = File.ReadAllText(path);
			Data data = JsonUtility.FromJson<Data>(json);
			
			highScoreName = data.highScoreName;
			highScorePoints = data.highScorePoints;
			userName = data.userName;
		}
	}	
}
