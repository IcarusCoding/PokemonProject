using UnityEngine;
using System.Collections;

/// <summary>
/// Sprite Handler
/// Description: Sprite Handler will help determine which image to use depending on
/// 			 Character movement, and the Camera's Position and Angle
/// Last Modified: 8.3.2014
/// Author: Andrew Mills
/// </summary>
public class BaseSprite : MonoBehaviour
{
	public Motor Motor = null;
	public enum DirectionState { Down, Left, Right, Up }
	public DirectionState CurrentState = DirectionState.Down;

	public int UVTieX = 4;
	public int UVTieY = 4;
	public int FPS = 10;

	public Texture2D defaultTexture;
	public Texture2D downloadedTexture;
	public Texture2D texture;

	public string TypeName;
	public bool IsAnimating;

	protected Vector2 size;
	protected Vector2 offset;
	private int lastIndex = -1;

	private Vector3 LeftEulerAngle =  new Vector3(0.0f, 90.0f, 0.0f);
	private Vector3 BackEulerAngle =  new Vector3(0.0f, 180.0f, 0.0f);
	private Vector3 RightEulerAngle = new Vector3(0.0f, 270.0f, 0.0f);
	private Vector3 FaceEulerAngle =  new Vector3(0.0f, 0.0f, 0.0f);
		
	private WWW www;

	public IEnumerator UpdateTexture(string aTexPath)
	{
		www = new WWW(aTexPath);
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
			Debug.Log("ERROR: " + www.error + " : " + aTexPath);

			texture = defaultTexture;
			texture.filterMode = FilterMode.Point;
			texture.wrapMode = TextureWrapMode.Clamp;
		}
	}

	/// <summary>
	/// Lates the update.
	/// </summary>
	private void LateUpdate () 
	{
		// Apply Handling the image rotation
		ApplyImageRotation();	

		if (Motor.MoveVector.x != 0 || Motor.MoveVector.z != 0) 
			IsAnimating = true;
		else
			IsAnimating = false;

		UpdateImage ();
	}

	private void UpdateImage()
	{
		// Calculate index
		int _index = (int)(Time.timeSinceLevelLoad * FPS) % (UVTieX * UVTieY);

		if(_index != lastIndex)
		{
			// split into horizontal and vertical index
			int uIndex = 0;
			
			if(IsAnimating) //Make sure a key is pressed so we can animate
				uIndex = _index % UVTieX;
			
			int vIndex = (int)CurrentState;
			
			// build offset
			// v coordinate is the bottom of the image in opengl so we need to invert.
			offset = new Vector2 (uIndex * size.x, 1.0f - size.y - vIndex * size.y);
			
			renderer.material.mainTexture = texture;
			renderer.material.SetTextureOffset ("_MainTex", offset);
			renderer.material.SetTextureScale ("_MainTex", size);

			lastIndex = _index;
		}	
	}

	/// <summary>
	/// Applies the image rotation depending on the rotation of the main Camera
	/// </summary>
	private void ApplyImageRotation()
	{
		if(Camera.main == null)
			return;

		float _forwardPosition  = 	Vector3.Distance(Camera.main.transform.position, transform.parent.transform.position + transform.parent.transform.forward);
		float _leftPosition     = 	Vector3.Distance(Camera.main.transform.position, transform.parent.transform.position + -transform.parent.transform.right);
		float _rightPosition    = 	Vector3.Distance(Camera.main.transform.position, transform.parent.transform.position + transform.parent.transform.right);
		float _backwardPosition =   Vector3.Distance(Camera.main.transform.position, transform.parent.transform.position + -transform.parent.transform.forward);
		
		var _result = Mathf.Min(Mathf.Min (_forwardPosition, _leftPosition), Mathf.Min(_rightPosition, _backwardPosition));


		if(_result == _leftPosition)
		{
			transform.localEulerAngles = LeftEulerAngle;
			this.CurrentState = DirectionState.Left;
			return;
		}
		else if(_result == _forwardPosition)
		{
			transform.localEulerAngles = BackEulerAngle;
			this.CurrentState = DirectionState.Down;
			return;
		}
		else if(_result == _rightPosition)
		{
			transform.localEulerAngles = RightEulerAngle;
			this.CurrentState = DirectionState.Right;
			return;
		}
		else if(_result == _backwardPosition)
		{
			transform.localEulerAngles = FaceEulerAngle;
			this.CurrentState = DirectionState.Up;
			return;
		}
	}
}
