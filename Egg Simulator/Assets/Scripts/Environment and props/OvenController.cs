using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvenController : MonoBehaviour
{

    public GameObject[] fireArray;
    public float delay;
    public float playDuration;
    
    void Start()
    {     
        StartCoroutine("FireTrigger");
    }

    IEnumerator FireTrigger()
    {
        while (true)
        {
            fireArray[2].GetComponent<ParticleSystem>().Stop();
            fireArray[2].GetComponent<BoxCollider>().enabled = false;
            fireArray[3].GetComponent<ParticleSystem>().Stop();
            fireArray[3].GetComponent<BoxCollider>().enabled = false;
            yield return new WaitForSeconds(delay);
            fireArray[0].GetComponent<ParticleSystem>().Play();
            fireArray[0].GetComponent<BoxCollider>().enabled = true;
            fireArray[1].GetComponent<ParticleSystem>().Play();
            fireArray[1].GetComponent<BoxCollider>().enabled = true;           
            yield return new WaitForSeconds(playDuration);

            fireArray[0].GetComponent<ParticleSystem>().Stop();
            fireArray[0].GetComponent<BoxCollider>().enabled = false;
            fireArray[1].GetComponent<ParticleSystem>().Stop();
            fireArray[1].GetComponent<BoxCollider>().enabled = false;
            yield return new WaitForSeconds(delay);
            fireArray[2].GetComponent<ParticleSystem>().Play();
            fireArray[2].GetComponent<BoxCollider>().enabled = true;
            fireArray[3].GetComponent<ParticleSystem>().Play();
            fireArray[3].GetComponent<BoxCollider>().enabled = true;
            yield return new WaitForSeconds(playDuration);
        }
    }

}
