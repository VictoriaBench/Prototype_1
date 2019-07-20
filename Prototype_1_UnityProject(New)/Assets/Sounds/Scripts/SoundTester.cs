using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTester : MonoBehaviour {


	void Update(){
		//Jump mat
		if (Input.GetKeyDown(KeyCode.A)){
            FindObjectOfType<SoundManager>().Play("BG Sound"); //find name of sound

        }

        //Jump mat
        if (Input.GetKeyDown(KeyCode.B))
        {
            //ref audio manager for sound where we need to play sound
            FindObjectOfType<SoundManager>().Play("Enemy Hurt"); //find name of sound
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            //ref audio manager for sound where we need to play sound
            FindObjectOfType<SoundManager>().Play("Paper Walk"); //find name of sound
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            //ref audio manager for sound where we need to play sound
            FindObjectOfType<SoundManager>().Play("Rock Walk"); //find name of sound
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            //ref audio manager for sound where we need to play sound
            FindObjectOfType<SoundManager>().Play("Scissor Walk"); //find name of sound
        }
    }
	

}