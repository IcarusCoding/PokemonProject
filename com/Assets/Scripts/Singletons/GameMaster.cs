using UnityEngine;
using System.Collections;
using System.Net;

public class GameMaster : MonoBehaviour 
{
	public static GameMaster Instance = null;

	public GameObject Player = null;
	public bool AllowPokemonTags = false;
	
	// Use this for initialization
	void Start ()
	{
		if(Instance == null)
			Instance = this;
	}

	/// <summary>
	/// Uploads a Screenshot <typeparam PNG>.
	/// </summary>
	/// <returns>The PN.</returns>
	public IEnumerator CreateScreenshot ()
	{
		// We should only read the screen buffer after rendering is complete
		yield return new WaitForEndOfFrame();

		if(!System.IO.Directory.Exists(Application.dataPath + "/../ScreenShots"))
			System.IO.Directory.CreateDirectory(Application.dataPath + "/../ScreenShots");

		Application.CaptureScreenshot (Application.dataPath + "/../ScreenShots/Sreenshot.png");
	}

	// Update is called once per frame
	void Update () 
	{

	}
}
