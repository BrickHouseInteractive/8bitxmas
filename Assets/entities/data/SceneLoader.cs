﻿using UnityEngine;
using System.Collections;

public class SceneLoader : Singleton<SceneLoader> {

	public AudioClip backSound;
	public AudioClip forwardSound;
	public AudioClip windSound;

	private AudioSource audioSource;
	private Animator animator;
	private bool allowButtonPresses = true;

	void Start () {
		DontDestroyOnLoad(gameObject);
		animator = gameObject.GetComponent<Animator>();
		audioSource = gameObject.GetComponent<AudioSource>();
	}

	void Awake() {
		if (Instance != this) {
			Destroy(this.gameObject);
		}
	}

	void Update (){
		if(allowButtonPresses){
			switch(Application.loadedLevelName){
			case "Start":
				break;
			case "CharacterSelect":
				HandleCharacterSelection();
				break;
			case "Game":
				HandleGame();
				break;
			case "Controls":
			case "Leaderboard":
				HandleBack();
				break;
			}
		}
	}

	static public void FadeIn(){
		Instance.transform.Find("Fade").GetComponent<FadeOut>().StartFadeIn();
	}

	static public void FadeOut(){
		Instance.transform.Find("Fade").GetComponent<FadeOut>().StartFadeOut();
	}

	static public void GoToScene(string scene, bool forward){
		Instance.StartCoroutine(Instance.LoadScene(scene));
		if(forward){
			Instance.SceneForward();
		}else{
			Instance.SceneBackward();
		}
	}


	//Private 

	void AllowButtonPresses(){
		allowButtonPresses = true;
	}

	void HandleCharacterSelection(){
		ConfirmCharacterSelect characterManager = GameObject.Find("CharacterManager").GetComponent<ConfirmCharacterSelect>();
		foreach(int playerNumber in characterManager.GetActivePlayers()){
			if(Input.GetButtonDown("Start_P"+playerNumber) || Input.GetButtonDown("Throw_P"+playerNumber)){
				if(GameObject.Find("CharacterManager").GetComponent<ConfirmCharacterSelect>().AllPlayersReady()){
					StartCoroutine(Instance.LoadScene("Game"));
					SceneForward();
				}
			}
		}
		if(Input.GetButtonDown("Cancel")){
			if(GameObject.Find("CharacterManager").GetComponent<ConfirmCharacterSelect>().NoActivePlayers()){
				StartCoroutine(Instance.LoadScene("Start"));
				SceneBackward();
			}
		}
	}

	void HandleBack(){
		if(Input.GetButtonDown("Cancel")){
			StartCoroutine(Instance.LoadScene("Start"));
			SceneBackward();
		}
	}

	void SceneForward(){
		allowButtonPresses = false;
		PlaySound(Instance.windSound);
		PlaySound(Instance.forwardSound);
		animator.CrossFade("SceneForward", 0f);
	}

	void SceneBackward(){
		allowButtonPresses = false;
		PlaySound(Instance.windSound);
		PlaySound(Instance.backSound);
		animator.CrossFade("SceneBackward", 0f);
	}

	IEnumerator LoadScene(string name){
		yield return new WaitForSeconds(1f);
		Debug.Log ("New Level load: " + name);
		Application.LoadLevel (name);
	}

	void HandleGame(){
	
	}

	void PlaySound(AudioClip sound){
		audioSource.PlayOneShot(sound);
	}

}