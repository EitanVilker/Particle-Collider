using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureManagerChemistry : MonoBehaviour
{
    private static ArrayList _allParticles;
	private float _time;
    public static int magneticFieldState;

    // Use this for initialization
    public void Start()
    {
        _time = 0;
        _allParticles = new ArrayList();
        magneticFieldState = 0; // field initially doesn't exist
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        // update all current creatures
        foreach (ParticleObject particle in _allParticles)
        {
            particle.FixedUpdate();
        }
    }

    public static void AddParticle(GameObject newParticle)
    {
		ParticleObject particle = newParticle.GetComponent<ParticleObject>();
        _allParticles.Add(particle);
    }

	public static void deleteEverything(){
		Debug.Log("Everything must go");
		foreach (ParticleObject particle in _allParticles){
			Debug.Log("You can go!");
			_allParticles.Remove(_allParticles.IndexOf(particle));
			Destroy(particle);
		}
	}

	public static void setMagneticField(bool direction){

		// Magnetic plate on right has positive charge if true and negative charge if false

		Debug.Log("Setting magnetic field");

		foreach (ParticleObject particle in _allParticles){

			if (direction == true){

				particle.fieldState = 1;

				if (particle.charge == -1){
					particle.changeVelocity(new Vector3(0, 0, 0.1f));
				}
				else if (particle.charge == 1){
					particle.changeVelocity(new Vector3(0, 0, -0.1f));
				}
			}
			else{

				particle.fieldState = -1;
				if (particle.charge == -1){
					particle.changeVelocity(new Vector3(0, 0, -0.1f));
				}
				else if (particle.charge == 1){
					particle.changeVelocity(new Vector3(0, 0, 0.1f));
				}
			}
		}
	}
}