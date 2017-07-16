using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;

public class HighScore:System.IComparable
	{
	private int playerScore;
	private float playerTime;
	private float playerDistance;
	private string playerName;

	private static char seperatorChar=':';
	private static string highScoresFilePath=@"C:\smatpo\"; // System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments)+@"\SSMMPP\";
	private static string highScoresFileName="highscores.txt";

	public HighScore()
		{
		playerName="name not set";
		playerScore=0;
		playerTime=0;
		playerDistance=0;
		}

	public HighScore(string name,int score,float time,float distance)
		{
		playerScore=score;
		playerTime=time;
		playerDistance=distance;
		playerName=name;
		}


	public override string ToString()
		{
		return "Score: "+playerScore.ToString()+"\nName: "+playerName+"\nTime: "+secondsToString(playerTime)+"\nDistance: "+playerDistance.ToString()+" km";
		}

	public int CompareTo(object otherObject)
		{
		// mine minus other
		HighScore otherHS=(HighScore)otherObject;
		return playerScore-otherHS.playerScore;
		}


	private static void initializeSaveDirectory()
		{
		if (!Directory.Exists(highScoresFilePath)) Directory.CreateDirectory(highScoresFilePath);
		if (!File.Exists(highScoresFilePath)) File.Create(highScoresFilePath+highScoresFileName);
		}


	public static void saveHighScores(List<HighScore> highScores)
		{
		initializeSaveDirectory();
		StreamWriter writer=new StreamWriter(highScoresFilePath+highScoresFileName);
		foreach (HighScore hs in highScores)
			{
			writer.WriteLine(hs.playerName+seperatorChar+hs.playerScore.ToString()+seperatorChar+hs.playerTime.ToString()+seperatorChar+hs.playerDistance.ToString());
			}
		writer.Close();
		}



	public static List<HighScore> loadHighScores_lengthy()
		{
		initializeSaveDirectory();
		// create the list of objects to be read
		List<HighScore> highScores=new List<HighScore>();
		// create the reader object
		StreamReader reader=new StreamReader(highScoresFilePath+highScoresFileName);
		// for each line in the text file
		do // this loop is kinda weird but oh well
			{
			// read a line
			string currentReadLine=reader.ReadLine();
				if (currentReadLine==null) break;
				else
				{
			// break up the line into data
			string[] currentReadLine_data=currentReadLine.Split(seperatorChar);
			// create the object to store the data in
			HighScore currentReadLine_outputHS=new HighScore();
			// for each data item, store it in the appropriate field of the object just set up (shorter alternative: directly set fields using literal constants for the indecies of the array of data strings)
			for (int dataItemIndex=0; dataItemIndex<currentReadLine_data.Length; dataItemIndex++)
				{
				string currentDataItem=currentReadLine_data[dataItemIndex];
				switch (dataItemIndex)
					{
					case 0:
						currentReadLine_outputHS.playerName=currentDataItem;
						break;
					case 1:
						currentReadLine_outputHS.playerScore=int.Parse(currentDataItem);
						break;
					case 2:
						currentReadLine_outputHS.playerTime=float.Parse(currentDataItem);
						break;
					case 3:
						currentReadLine_outputHS.playerDistance=float.Parse(currentDataItem);
						break;
					default:
						// here you could perhaps write the erroneous index to an error field in the object
						break;
					}
				}
			// add that object to the list of objects
			highScores.Add(currentReadLine_outputHS);
			}
			// move on to the next line/object or stop
			}
		while (5==5); // break statement is within loop body
		// stop the reader object
		reader.Close();
		// return the list of objects
		return highScores;
		}





	public static List<HighScore> loadHighScores()
		{
		initializeSaveDirectory();
		// create the list of objects to be read
		List<HighScore> highScores=new List<HighScore>();
		// create the reader object
		StreamReader reader=new StreamReader(highScoresFilePath+highScoresFileName);
		// read the first line of the text file
		string currentLine=reader.ReadLine();
		// for each line in the text file
		while (currentLine!=null)
			{
			// split up the current line into data
			string[] cld=currentLine.Split(seperatorChar); // cld is short for current line data
			// add a new object to the object list, storing each element of cld in a field
			highScores.Add(new HighScore(cld[0],int.Parse(cld[1]),float.Parse(cld[2]),float.Parse(cld[3])));
			}
		// stop the reader object
		reader.Close();
		// return the list of objects
		return highScores;
		}









	// woo this should probably be somewhere else more central or having to do with time or strings or floats but whatever
	public static string secondsToString(float seconds)
		{
		int secondsFloored=Mathf.FloorToInt(seconds);
		float secondsDecimal=0.01f*Mathf.RoundToInt(100*(seconds-secondsFloored));
		int minutes=secondsFloored/60;
		float secondsFinal=(int)(secondsFloored-(60*minutes))+secondsDecimal;
		if (secondsFinal<10) return minutes+"'0"+secondsFinal+"''";
		else return minutes+"'"+secondsFinal+"''";
		}
	}
