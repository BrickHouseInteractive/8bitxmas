﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameData : MonoBehaviour {

	private CharacterCollection.Character[] playerCharacters;

	// Use this for initialization
	void Awake () {
		DontDestroyOnLoad(gameObject);
		playerCharacters = new CharacterCollection.Character[4];
	}

	void Start(){
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetCharacter(int playerNumber, CharacterCollection.Character character){
		playerCharacters[playerNumber-1] = character;
	}

	public CharacterCollection.Character GetCharacter(int playerNumber){
		return playerCharacters[playerNumber-1];
	}

	public void RemoveCharacter(int playerNumber){
		playerCharacters[playerNumber-1] = null;
	}

	public void SetLevel(){
		//Set Level
	}

}
