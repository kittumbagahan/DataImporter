using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScoreManager : MonoBehaviour {

	public static ScoreManager ins;

	public int maxMove; //max move to finish a set


	[SerializeField]
	int numOfMoves, numOfMistakes;



	void Awake()
	{
		ins = this;
	}

	void Start () {
		
	}

	public void AW()
	{
		// print(maxMove + Convert.ToInt32(maxMove * 0.9));
		// print(maxMove + Convert.ToInt32(maxMove * 0.8));
		// print(maxMove + Convert.ToInt32(maxMove * 0.7));
		// print(maxMove + Convert.ToInt32(maxMove * 0.6));
		// print(maxMove + Convert.ToInt32(maxMove * 0.5));
		// print(maxMove + Convert.ToInt32(maxMove * 0.4));
		// print(maxMove + Convert.ToInt32(maxMove * 0.3));
		// print(maxMove + Convert.ToInt32(maxMove * 0.2));
		// print(maxMove + Convert.ToInt32(maxMove * 0.1));
		// print("max=" + maxMove);
	}

	public void IncNumOfMoves()
	{
		numOfMoves++;
	}

	public void IncNumOfMistakes()
	{
		numOfMistakes++;
	}

	public string GetGrade()
	{
		print("num of moves = " + numOfMoves);
		if(numOfMoves <= maxMove)
		{
			return "A++";
		}
		if(numOfMoves >= maxMove + Convert.ToInt32(maxMove * 0.1) && numOfMoves <= maxMove + Convert.ToInt32(maxMove * 0.2)) //do something like a > n && a < n
		{
			return "A";
		}
		else if(numOfMoves >= maxMove + Convert.ToInt32(maxMove * 0.2) && numOfMoves <= maxMove + Convert.ToInt32(maxMove * 0.3)) //do something like a > n && a < n
		{
			return "B+";
		}
		else if(numOfMoves >= maxMove + Convert.ToInt32(maxMove * 0.3) && numOfMoves <= maxMove + Convert.ToInt32(maxMove * 0.4)) //do something like a > n && a < n
		{
			return "B";
		}
		else if(numOfMoves >= maxMove + Convert.ToInt32(maxMove * 0.4) && numOfMoves <= maxMove + Convert.ToInt32(maxMove * 0.5)) //do something like a > n && a < n
		{
			return "C+";
		}
		else if(numOfMoves >= maxMove + Convert.ToInt32(maxMove * 0.5) && numOfMoves <= maxMove +Convert.ToInt32(maxMove * 0.6)) //do something like a > n && a < n
		{
			return "C";
		}
		else if(numOfMoves >=  maxMove +Convert.ToInt32(maxMove * 0.6) && numOfMoves <= maxMove + Convert.ToInt32(maxMove * 0.7)) //do something like a > n && a < n
		{
			return "D+";
		}
		else if(numOfMoves >= maxMove + Convert.ToInt32(maxMove * 0.7) && numOfMoves <= maxMove + Convert.ToInt32(maxMove * 0.8)) //do something like a > n && a < n
		{
			return "D";
		}
		else if(numOfMoves >= maxMove + Convert.ToInt32(maxMove * 0.8) && numOfMoves <=  maxMove + Convert.ToInt32(maxMove * 0.9)) //do something like a > n && a < n
		{
			return "E+";
		}
		else if(numOfMoves >=  maxMove + Convert.ToInt32(maxMove * 0.9) && numOfMoves <= maxMove * 2) //do something like a > n && a < n
		{
			return "E";
		}
		else
		{
			return "F";
		}

		//return "ERROR ON GRADING SYSTEM";
	}

}
