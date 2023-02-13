using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplicationManager : MonoBehaviour {
	
	[SerializeField] private AppDatas choice;
	
	public void Quit () 
	{
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}
	
	public void SelectDifficulty(int choix)
	{
		choice.actualDifficulty = choice.difficultyList[choix];
		SceneManager.LoadScene("2_Level");
	}
}