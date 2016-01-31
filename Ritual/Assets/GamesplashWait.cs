using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class GamesplashWait : MonoBehaviour {

	public string sceneName;

	// Use this for initialization
	void Start () {
	    this.GetComponent<LayerManagement.Action.Finite.WaitAction>().delegates = a => SceneManager.LoadScene(sceneName);
    }
}
