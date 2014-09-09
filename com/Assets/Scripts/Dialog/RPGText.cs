using UnityEngine;
using System.Collections;

public class RPGText : MonoBehaviour 
{	
	public string TextToScreen = "";
	public TextMesh tMesh;

	[SerializeField]
	private string[] _textBlocks;
	
	//Private Variables
	private int _currentTextBlockIndex = 0;
	private string _currentTextBlock;
	private float pauseTimer = 0.0f;
	private bool visible = false;
	
	public void Awake()
	{
		Deactivate ();
	}
	
	private void Update() 
	{
		if(Input.GetKeyDown(KeyCode.A))
		{
			ToggleOnOff();
		}
		if(Input.GetKeyDown(KeyCode.S) && visible)
			FillText();

		//this.guiText.text = TextToScreen;
		tMesh.text = TextToScreen;

		if(!visible)
			return;

		if(PauseCharacterIndex())
		{
			if(TextToScreen.Length < _currentTextBlock.Length)
				TextToScreen = _currentTextBlock.Substring(0, TextToScreen.Length+1);
		}
	}

	/// <summary>
	/// Toggles on and off the visiblity of our text.
	/// </summary>
	public void ToggleOnOff()
	{
		visible = (visible) ? Deactivate() : Activate ();
	}

	private bool Activate()
	{
		Reset ();
		visible = true;
		return true;
	}

	private bool Deactivate()
	{
		Reset ();
		visible = false;
		return false;
	}

	private void Reset()
	{
		TextToScreen = ""; // clear the text
		_currentTextBlockIndex = 0;
		_currentTextBlock = _textBlocks[_currentTextBlockIndex];
	}

	/// <summary>
	/// Pauses the index of the character.
	/// </summary>
	/// <returns><c>true</c>, if character index was paused, <c>false</c> otherwise.</returns>
	private bool PauseCharacterIndex()
	{
		//Increment the pauseTimer to match the deltaTime
		pauseTimer += Time.deltaTime;
		
		//When we cap the PauseTimer, reset the value, and change the VelocityState
		if(pauseTimer > (float)(0.1f))
		{
			pauseTimer = 0.0f;
			return true;
		}
		return false;
	}

	/// <summary>
	/// Fills the text linked to the character .
	/// </summary>
	/// <returns><c>true</c>, if text was filled, <c>false</c> otherwise.</returns>
	private bool FillText()
	{
		if(TextToScreen == _currentTextBlock)
			NextTextBlock();
		else
			TextToScreen = _currentTextBlock;
		
		return visible;
	}
	
	private void NextTextBlock() 
	{
		TextToScreen = ""; // clear the text
		if(_currentTextBlockIndex < _textBlocks.Length-1)
		{
			_currentTextBlockIndex++; 
			_currentTextBlock = _textBlocks[_currentTextBlockIndex]; // set the text
		}
		else
			Deactivate();
	}
}