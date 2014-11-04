using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour {



	public static Camera properties;
	public static Animation animations;
	public static string[] animationName = new string[] {"green-to-black", "yellow-to-black", "red-to-black", "blue-to-black"};


	void Start ()
	{
		properties = GetComponent<Camera> ();

		animations = GetComponent<Animation> ();
	}

}
