using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneDestroyer : MonoBehaviour {

	public static bool goldenDestroyed1 = false;
	public static bool goldenDestroyed2 = false;
	public static bool goldenDestroyed3 = false;
	
	void OnCollisionEnter2D (Collision2D col)
	{
		if(col.gameObject.tag.Equals("Player"))
		{			
			Destroy(gameObject);
			
			if(gameObject.name.Equals("Golden Bone 1"))
			{
				goldenDestroyed1 = true;
			}
			if(gameObject.name.Equals("Golden Bone 2"))
			{
				goldenDestroyed2 = true;
			}
			if(gameObject.name.Equals("Golden Bone 3"))
			{
				goldenDestroyed3 = true;
			}
			if(gameObject.layer.Equals(16))
			{
				PointsAdder.scoreValue += 1;
			}		
		}
	}
}
