using UnityEngine;
using System.Collections;

public class TagHandler : MonoBehaviour 
{
	public Transform target;
	public GameObject ObjectToHide;
	public GameObject Sprite;

	public int MaxDistance = 15;

	private bool showLabel;
	// Update is called once per frame
	void Update () 
	{
		if(CheckDistance())
			showLabel = true;
		else
			showLabel= false;

		if(showLabel && Sprite.renderer.isVisible)
			ObjectToHide.SetActive(true);
		else
			ObjectToHide.SetActive(false);
	}

	/// <summary>
	/// Checks the distance between self and the Camera in the game.
	/// </summary>
	/// <returns><c>true</c>, if distance was checked, <c>false</c> otherwise.</returns>
	private bool CheckDistance()
	{
		return Vector3.Distance (target.position, Camera.main.transform.position) < MaxDistance;
	}
}
