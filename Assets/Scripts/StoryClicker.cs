using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryClicker : MonoBehaviour
{
    [SerializeField] private GameObject newsPanel;
    private Text newsText;
    private bool canChange = true;
    private int moment = 0;
    // Start is called before the first frame update
    void Start()
    {
        newsText = newsPanel.transform.GetChild(0).GetComponent<Text>();
        newsText.text = "News: Everyone have a wonderful day!";
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && canChange)
        {
            moment++;
            NextMoment();
        }
    }

    void NextMoment()
    {
        if(moment == 1)
        {
            newsText.text = "News: Oh Wait! What's this?";
        }
        else if (moment == 2)
        {
            newsText.text = "News: Getting reports that a new variant of Covid-19 is enroute to the United States!";
        }
        else if (moment == 3)
        {
            newsText.text = "News: Death Counts are rising!";
        }
        else if (moment == 4)
        {
            newsText.text = "News: Everyone go get vaccinated!";
        }
    }
}
