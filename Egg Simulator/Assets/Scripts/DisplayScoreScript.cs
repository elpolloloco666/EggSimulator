using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class DisplayScoreScript : MonoBehaviour
{
    public GameObject scoreLayer;
    private StreamReader sr;

    void Start()
    {
        showScores();
    }

    private List<int> getScores()
    {
        if (File.Exists(Application.persistentDataPath + "/Scores"))
        {
            string[] data;
            List<int> scores = new List<int>();
            sr = new StreamReader(Application.persistentDataPath + "/Scores");

            data = sr.ReadToEnd().Split('\n');

            foreach (string score in data)
            {
                int number;
                if (int.TryParse(score, out number))
                    scores.Add(number);

            }

            sr.Close();

            
            scores.Sort();
            scores.Reverse();
            
            return scores;

        }
        else return null;
    }


    private void showScores()
    {
        if(getScores()!= null)
        {
            List<int> scores = getScores();

            if (scores.Count > 10)
            {

                for(int i = 0; i < 10; i++)
                {
                    var row = Instantiate(scoreLayer, transform);
                    row.GetComponentsInChildren<TMP_Text>()[0].text = (i+1).ToString();
                    row.GetComponentsInChildren<TMP_Text>()[1].text = scores[i].ToString();
                }
            }
            else
            {
                for (int i = 0; i < scores.Count; i++)
                {
                    var row = Instantiate(scoreLayer, transform);
                    row.GetComponentsInChildren<TMP_Text>()[0].text = (i + 1).ToString();
                    row.GetComponentsInChildren<TMP_Text>()[1].text = scores[i].ToString();
                }
            }
        }
    }


}
