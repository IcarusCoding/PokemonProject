using UnityEngine;
using System.Collections;

public class PlayerSprite : BaseSprite
{
	private WWW www;

	public IEnumerator Start()
	{
		www = new WWW("http://www.millsaj.com/TestImage.png");
		yield return www;
		
		size = new Vector2 (1.0f / UVTieX , 1.0f / UVTieY);
		
		if(www.error == null)
		{
			downloadedTexture = www.texture;
			
			texture = downloadedTexture;
			texture.filterMode = FilterMode.Point;
			texture.wrapMode = TextureWrapMode.Clamp;
		}
		else
		{
			texture = defaultTexture;
			texture.filterMode = FilterMode.Point;
			texture.wrapMode = TextureWrapMode.Clamp;
		}
	}
}
