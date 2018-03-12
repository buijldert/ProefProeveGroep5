using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadTest : MonoBehaviour {

	public void LoadScene(string sceneName)
    {
        print("loading");
        SceneManager.LoadScene(sceneName);
    }
}
