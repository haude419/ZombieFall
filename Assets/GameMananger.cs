﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameMananger : MonoBehaviour {

	private int selectedZombiePosition;
	public Text scoreText;
	private int score = 0;
	public GameObject selectedZombie;
	public List<GameObject> zombies;
	public Vector3 selectedSize;
	public Vector3 defaultSize;

	public bool gameOver = false;

	//Game Timer
	public float gameTimer = 120f;
	private float currentGameTime;

	//Adding a udpate timer
	public float updateTimer = 0f;
	private float currentUpdateTime;
	public int updateCount = 0;

	// Use this for initialization
	void Start () {
		currentUpdateTime = updateTimer;
		currentGameTime = gameTimer;

		selectZombie (selectedZombie);
		scoreText.text = "Score: " + score;
	}
	
	// Update is called on	ce per frame
	void Update () {

		//GAME OVER
		if (gameOver == false) {
			currentUpdateTime += Time.deltaTime;
			if(currentUpdateTime >= 5){
				LevelUp ();
				currentUpdateTime = 0f;
			}


			currentGameTime -= Time.deltaTime;
			if (currentGameTime > 0) {
				
			} else {
				StopGame ();
			}
			Debug.Log ("currentGameTime: " + currentGameTime);

		} else {
			StopGame ();
		}

		//TIMERS
		if (currentUpdateTime == 20) {
			doubleMultiplier ();
		}

		if (Input.GetKeyDown ("left")) {
			GetZombieLeft ();	
		}

		if (Input.GetKeyDown ("right")) {
			GetZombieRight ();
		}

		if (Input.GetKeyDown ("up")) {
			PushUp ();
		}
			
	}

	void StopGame()
	{
		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
		#else
			Application.Quit();
		#endif
	} 

	void GetZombieLeft(){
		
		if (selectedZombiePosition == 0) {
			selectedZombiePosition = zombies.Count - 1;
			selectZombie (zombies [selectedZombiePosition]);		
		} else {
			selectedZombiePosition = selectedZombiePosition - 1;
			GameObject newZombie = zombies [selectedZombiePosition];
			selectZombie (newZombie);
		} 

	}

	void GetZombieRight(){

		if ((selectedZombiePosition + 1) == zombies.Count) {
			selectedZombiePosition = 0;
			selectZombie (zombies [selectedZombiePosition]);
		} else if ((selectedZombiePosition + 1) > zombies.Count) {
			selectedZombiePosition = 0;
			selectZombie (zombies [selectedZombiePosition]);
		} else {
			selectedZombiePosition = selectedZombiePosition + 1;
			GameObject newZombie = zombies [selectedZombiePosition];
			selectZombie (newZombie);
		}

	}

	void selectZombie(GameObject newZombie){
		selectedZombie.transform.localScale = defaultSize;
		selectedZombie = newZombie;	
		newZombie.transform.localScale = selectedSize;
	}

	void PushUp(){
		Rigidbody rb = selectedZombie.GetComponent<Rigidbody> ();
		rb.AddForce (0, 0, 10, ForceMode.Impulse);
	}


	public void AddPoint(){
		score++;
		scoreText.text = "Score: " + score;
	}

	public void HandleFallenZombie(GameObject fallenZombie){

		if (zombies != null) {
			
			if ((selectedZombiePosition) > zombies.IndexOf(fallenZombie)) {
				selectedZombiePosition -= 1;
			}

			if (selectedZombie == fallenZombie) {
				int fallIndex = zombies.IndexOf (fallenZombie);

				if (zombies.Count != 1) {
					selectedZombiePosition += 1;
					selectZombie (zombies [selectedZombiePosition]);
				}

			}

			zombies.Remove (fallenZombie);

		} else {
			gameOver = true;
		}

	}

	//LEVEL UPS
	private void LevelUp(){
		int levelInt = Random.Range(1,3);

		//COMMENT THIS LINE
		levelInt = 2;

		if (updateCount == 0) {

			if (levelInt == 1) {
				doubleMultiplier ();
			} else if (levelInt == 2) {
				
			}
		}
			
		updateCount++;
	}

	public void doubleMultiplier (){
		score = score * 2;
		scoreText.text = "Score: " + score;
	}

}
