using UnityEngine;
using System.Collections;

public class GamePiece : MonoBehaviour {
	Vector3 boundXZ;

	public bool Stable = true;

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
	}

	void FixedUpdate()
	{
		if(rigidbody.velocity.magnitude < .001 && rigidbody.angularVelocity.magnitude < .0001)
		{
			if (!EqualWithTolerance(rigidbody.position.x, boundXZ.x))
			{
				rigidbody.AddForce(new Vector3(rigidbody.position.x> boundXZ.x?-100:100, 0, 0));
				Stable = false;
			}
			else if (!EqualWithTolerance(rigidbody.position.z, boundXZ.z))
			{ 
				rigidbody.AddForce(new Vector3(0, 0, rigidbody.position.z>boundXZ.z? -100:100));
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
		StartCoroutine (flipTileCoroutine ());
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

	IEnumerator doubleFlipTile()
	{
		//TODO fix
		while (rigidbody.position.y < 2)
		{
			rigidbody.AddForce (Vector3.up * 50);
			yield return new WaitForSeconds(.05f);
		}
		yield return new WaitForSeconds(.25f);
		rigidbody.AddForceAtPosition (Vector3.up * 140, rigidbody.position + Vector3.left * rigidbody.transform.localScale.x);
	}
}
