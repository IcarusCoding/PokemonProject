using UnityEngine;
using System.Collections;

public class NPCSprite : BaseSprite 
{
	public void Start()
	{
		size = new Vector2 (1.0f / UVTieX , 1.0f / UVTieY);
	}
}
