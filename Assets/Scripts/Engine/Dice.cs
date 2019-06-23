using System;
using System.Collections;
using UnityEngine;

public class Dice : MonoBehaviour {
	private int result;
	
	public enum DiceType{
		D2 = 2,
		D3 = 3,
		D4 = 4,
		D6 = 6,
		D8 = 8,
		D10 = 10,
		D12 = 12,
		D20 = 20,
		D30 = 30,
		D100 = 100
	}

    public static ArrayList RollType(){
        ArrayList rollList = new ArrayList();
        rollList.Add("Initiative");
        rollList.Add("Attack");
        rollList.Add("Damage");
        rollList.Add("Skill");

        return rollList;
    }
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public static int roll (int amt, DiceType type){
		int res = 0;
		for(int i = 0; i < amt; i++){
			int die = Convert.ToInt32(UnityEngine.Random.Range(1f, (float)type));
			res += die;
		}

		return res;
	}
}
