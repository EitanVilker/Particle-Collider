using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleObject : MonoBehaviour {

	private Vector3 velocity;
	// private Vector3 position;
	public int charge;
	public static float xBoundsLeft;
	public static float xBoundsRight;
	public static float yBoundsForward;
	public static float yBoundsBackward;
	public static float zBoundsUp;
	public static float zBoundsDown;
	private static int currentSeed = 203;
	public GameObject col_1_anim_prefab;
	public GameObject col_2_anim_prefab;
	public GameObject col_3_anim_prefab;
	public int fieldState;

	public void Awake(){

		this.fieldState = 0;
		currentSeed += 1;
		Random.InitState(currentSeed);
        float xVelocity = Random.Range(-0.03f, 0.03f);
        float yVelocity = Random.Range(-0.03f, 0.03f);
        float zVelocity = Random.Range(-0.03f, 0.03f);
        float xPosition = Random.Range(xBoundsLeft, xBoundsRight);
		float yPosition = Random.Range(yBoundsBackward, yBoundsForward);
		float zPosition = Random.Range(zBoundsDown, zBoundsUp);
		yPosition = 0;
		zPosition = 0;
        this.transform.position = new Vector3(xPosition, yPosition, zPosition);
		this.velocity = new Vector3(xVelocity, yVelocity, zVelocity);
    }
	public void FixedUpdate(){
		if (this.transform.position.x + velocity.x < xBoundsLeft | this.transform.position.x + velocity.x > xBoundsRight){
			
			if (fieldState == 1 | fieldState == -1){
				velocity = new Vector3(0f, 0f, 0f);
			}
			else{
				velocity.x *= -1;
			}
		}
		if (this.transform.position.y + velocity.y < yBoundsBackward | this.transform.position.y + velocity.y > yBoundsForward){
			if (fieldState == 1 | fieldState == -1){
				velocity = new Vector3(0f, 0f, 0f);
			}
			else{
				velocity.y *= -1;
			}
		}
		if (this.transform.position.z + velocity.z < zBoundsDown | this.transform.position.z + velocity.z > zBoundsUp){
			if (fieldState == 1 | fieldState == -1){
				velocity = new Vector3(0f, 0f, 0f);
			}
			else{
				velocity.z *= -1;
			}
		}
		this.transform.position += this.velocity;
	}
	public void OnCollisionEnter(Collision collisionObject){

		Debug.Log("COLLIDED");
		GameObject myAnimation;
		ParticleObject collidedWith = collisionObject.gameObject.GetComponent<ParticleObject>();

		int counter = 0;
		
		if (collidedWith.charge == 1){

			if (this.charge == 1){
				myAnimation = GameObject.Instantiate(col_1_anim_prefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
				myAnimation.transform.position = this.transform.position;
				this.changeVelocity(new Vector3(this.velocity.x * -1, this.velocity.y * -1, this.velocity.z * -1));
				Debug.Log("Proton v Proton");
				counter += 1;
			}
			else if (this.charge == -1){
				myAnimation = GameObject.Instantiate(col_2_anim_prefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
				myAnimation.transform.position = this.transform.position;
				Debug.Log("Proton v Electron");
				this.changeVelocity(new Vector3((collidedWith.velocity.x + this.velocity.x) / 2, 
					(collidedWith.velocity.y + this.velocity.y) / 2, 
					(collidedWith.velocity.z + this.velocity.z) / 2));
				counter += 1;
			}
		}
		else if (collidedWith.charge == -1){

			if (this.charge == 1){
				myAnimation = GameObject.Instantiate(col_2_anim_prefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
				myAnimation.transform.position = this.transform.position;
				this.changeVelocity(new Vector3((collidedWith.velocity.x + this.velocity.x) / 2, 
					(collidedWith.velocity.y + this.velocity.y) / 2, 
					(collidedWith.velocity.z + this.velocity.z) / 2));
				counter += 1;
			}
			else if (this.charge == -1){
				myAnimation = GameObject.Instantiate(col_3_anim_prefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
				myAnimation.transform.position = this.transform.position;
				this.changeVelocity(new Vector3(this.velocity.x * -1, this.velocity.y * -1, this.velocity.z * -1));
				Debug.Log("Electron v Electron");
				counter += 1;
			}
		}
		Debug.Log("Counter: " + counter.ToString());
	}
	public void changeVelocity(Vector3 newVelocity){
		this.velocity = newVelocity;
		Debug.Log("New velocity: " + newVelocity.ToString());
	}
	public Vector3 getVelocity(){
		return velocity;
	}
	public Vector3 getPosition(){
		return transform.position;
	}
}
