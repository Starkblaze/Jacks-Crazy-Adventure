using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldenAdder2 : MonoBehaviour {

	Image EmptyBone;
	public Sprite GoldenBone;

	// Use this for initialization
	void Start () {
		
		EmptyBone = GetComponent<Image>();
		
	}
	
	// Update is called once per frame
	void Update () {	
		
		
		if(BoneDestroyer.goldenDestroyed2 == true)
		{
			EmptyBone.sprite = GoldenBone;
		}
	}
}
