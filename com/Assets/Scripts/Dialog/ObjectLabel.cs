using UnityEngine;
using System.Collections;

public class ObjectLabel : MonoBehaviour 
{
	public Transform target;  // Object that this label should follow
	public GameObject child;
	public Vector3 offset = Vector3.up;    // Units in world space to offset; 1 unit above object by default

	/// <summary>
	/// Lates the update.
	/// </summary>
	private void LateUpdate()
	{
		transform.position = Camera.main.WorldToViewportPoint(target.position + offset);
	}
}