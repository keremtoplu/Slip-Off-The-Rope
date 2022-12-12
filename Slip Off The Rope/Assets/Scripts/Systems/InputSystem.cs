using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;

public class InputSystem : MonoSingleton<InputSystem>
{
    // Start is called before the first frame update
   
	public event Action<Touch> Touch;
    
	private void Update() 
	{
		if(Input.touchCount > 0)
		{
			var touch = Input.GetTouch(0);
			Touch?.Invoke(touch);
		}
	}
}
