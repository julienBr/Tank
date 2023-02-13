using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplicationManager : MonoBehaviour {
	
	[SerializeField] private AppDatas choice;
	[SerializeField] private GameObject fade;
	
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
		StartCoroutine(ThrowFade());
	}

	private IEnumerator ThrowFade()
	{
		fade.GetComponent<Animator>().SetTrigger("FadeOut");
		yield return new WaitForSeconds(1f);
		SceneManager.LoadScene("2_Level");
	}
}