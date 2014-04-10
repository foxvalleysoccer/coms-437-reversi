using UnityEngine;
using System.Collections;

public class GamePiece : MonoBehaviour {
	Vector3 boundXZ;

	public bool Stable = true;

	public bool whiteUp = true;

	// Use this for initialization
	void Start () {
		boundXZ = new Vector3(rigidbody.position.x, 0, rigidbody.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonUp(1))
		{
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, 100)) {
				rigidbody.AddExplosionForce(2000f, hit.point + Vector3.down/2, 8f);
			}
			
		}



		if(rigidbody.velocity.magnitude < .0001 && rigidbody.angularVelocity.magnitude < .00001)
		{
			//If we're not right-side up, flip
			if(whiteUp && rigidbody.rotation.eulerAngles.z > 30 && rigidbody.rotation.eulerAngles.z < 330)
			{
				StartCoroutine (flipTileCoroutine ());
				return;
			}
			if(!whiteUp && rigidbody.rotation.eulerAngles.z < 150)
			{
				StartCoroutine (flipTileCoroutine ());
				return;
			}
			
			if (!EqualWithTolerance(rigidbody.position.x, boundXZ.x))
			{
				float xVal = 100 * (boundXZ.x - rigidbody.position.x);
				rigidbody.velocity += Vector3.up;
				rigidbody.AddForce(new Vector3(xVal, 400, 0));
				Stable = false;
			}
			else if (!EqualWithTolerance(rigidbody.position.z, boundXZ.z))
			{ 
				float zVal = 100 * (boundXZ.z - rigidbody.position.z);
				rigidbody.velocity += Vector3.up;
				rigidbody.AddForce(new Vector3(0, 400, zVal));
				Stable = false;
			}
			else
			{
				Stable = true;
			}
		}
		else
		{
			Stable = false;
		}
	}

	void FixedUpdate()
	{
//		if(rigidbody.velocity.magnitude < .0001 && rigidbody.angularVelocity.magnitude < .00001)
//		{
//			//If we're not right-side up, flip
//			if(whiteUp && rigidbody.rotation.eulerAngles.z > 30 && rigidbody.rotation.eulerAngles.z < 330)
//			{
//				StartCoroutine (flipTileCoroutine ());
//				return;
//			}
//			if(!whiteUp && rigidbody.rotation.eulerAngles.z < 150)
//			{
//				StartCoroutine (flipTileCoroutine ());
//				return;
//			}
//
//			if (!EqualWithTolerance(rigidbody.position.x, boundXZ.x))
//			{
//				float xVal = 100 * (boundXZ.x - rigidbody.position.x);
//				rigidbody.velocity += Vector3.up;
//				rigidbody.AddForce(new Vector3(xVal, 400, 0));
//				Stable = false;
//			}
//			else if (!EqualWithTolerance(rigidbody.position.z, boundXZ.z))
//			{ 
//				float zVal = 100 * (boundXZ.z - rigidbody.position.z);
//				rigidbody.velocity += Vector3.up;
//				rigidbody.AddForce(new Vector3(0, 400, zVal));
//				Stable = false;
//			}
//			else
//			{
//				Stable = true;
//			}
//		}
//		else
//		{
//			Stable = false;
//		}
	}

	bool EqualWithTolerance(float one, float two)
	{
		float tolerance = 0.05f;
		return (two < one + tolerance && two > one - tolerance);
	}

	void OnMouseUp()
	{
		//StartCoroutine (flipTileCoroutine ());
		//flipTile ();
	}

	public void flipTile()
	{
		rigidbody.velocity += Vector3.up;
		StartCoroutine (flipTileCoroutine ());
		whiteUp = !whiteUp;
	}

	private IEnumerator flipTileCoroutine()
	{
		rigidbody.useGravity = false;
		while (rigidbody.position.y < 2)
		{
			rigidbody.AddForce (Vector3.up * 50 * (2 - rigidbody.position.y));
			//rigidbody.AddForce (Vector3.up * 560);
			yield return new WaitForSeconds(.05f);
		}
		yield return new WaitForSeconds(.15f);

		//rigidbody.AddForce (Vector3.up * 100);
		rigidbody.AddTorque (0, 0, 8);
		rigidbody.useGravity = true;
	}
}
