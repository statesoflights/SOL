using UnityEngine;
using System.Collections;

public class MainMenu_GUI : MonoBehaviour {

		void Update ()
		{
			if (Input.GetButton("Fire1"))
			{
				Application.LoadLevel(0);
			}
		}

}
