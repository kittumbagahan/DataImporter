using UnityEngine;
using System.Collections;
using AsPerSpec;

public class SlotsControl : MonoBehaviour {
	public CarouselToggler SlotA;
	public CarouselToggler SlotB;
	public CarouselToggler SlotC;
	public float Speed;
	public float SpeedVariance;

	IEnumerator Spin_Coroutine() {
		SlotA.AddImpulse(new Vector2(0,getRandomSpeed()));
		yield return new WaitForSeconds(0.5F);
		SlotB.AddImpulse(new Vector2(0,getRandomSpeed()));
		yield return new WaitForSeconds(0.5F);
		SlotC.AddImpulse(new Vector2(0,getRandomSpeed()));

//		SlotA.AddImpulse(new Vector2(getRandomSpeed(),0));
//		yield return new WaitForSeconds(0.5F);
//		SlotB.AddImpulse(new Vector2(getRandomSpeed(), 0));
//		yield return new WaitForSeconds(0.5F);
//		SlotC.AddImpulse(new Vector2(getRandomSpeed(), 0));	
//		SlotA.Stop();
	}

	public void Spin() {
		if (!SlotA.moving && !SlotB.moving && !SlotC.moving) {
			StartCoroutine (Spin_Coroutine ());
		}
		//SlotA.SnapToClosest();
	}

	float getRandomSpeed() {
		float s = Random.Range (Speed - (Speed * SpeedVariance), Speed + (Speed * SpeedVariance));
		Debug.Log("Random speed is: "+s);
		return s;
	}

}
