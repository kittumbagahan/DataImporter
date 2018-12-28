using UnityEngine;
using System.Collections;

public class CarouselMove : MonoBehaviour {

	enum Direction { LEFT, RIGHT }

	[SerializeField]
	float moveSpeed;

	[SerializeField]
	Direction direction;

	bool isMoving;

	// Use this for initialization
	void Start () {
		isMoving = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(direction == Direction.LEFT && isMoving)
		{
			transform.Translate(Vector3.left * (moveSpeed * Time.fixedDeltaTime));
			//transform.position = Vector3.left * moveSpeed * Time.fixedDeltaTime;
		}
		else if(direction == Direction.RIGHT && isMoving)
		{
			transform.Translate(Vector3.right * (moveSpeed * Time.fixedDeltaTime));
			//transform.position = Vector3.right * moveSpeed * Time.fixedDeltaTime;
		}
	}

	public void Stop()
	{
		isMoving = false;
	}

	public bool IsMoving
	{
		set { isMoving = value; }
	}
}
