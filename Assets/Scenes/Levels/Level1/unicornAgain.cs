using UnityEngine;
using System.Collections;

public class unicornAgain : Dialog
{
	private CharacterController cc;
	private Vector3 startPosition;
	private bool goBack;
	private float distance = 0.5f;

	void Start()
	{
		base.Start();
		cc = pc.GetComponent<CharacterController>();
	}

	void OnTriggerEnter(Collider hit)
	{
		base.OnTriggerEnter(hit);
		startPosition = pc.transform.position;
		goBack = true;
	}

	void Update()
	{
		base.Update();
		if (goBack)
		{
			pc.lookRight = false;
			//Bewege Spieler nach links
			cc.Move(Vector3.left * pc.runSpeed * 0.75f * Time.deltaTime);
			
			//Animation des Spielers
			pc.anim((int)PlayerController.AnimationTypes.moveLeft);
			
			//wenn die gewünschte Distanz gelaufen wurde
			if(Vector3.Distance(startPosition, pc.transform.position) > distance)
			{
				goBack = false;
			}
		}
	}
}