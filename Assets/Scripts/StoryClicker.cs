/*
 * Gerard Lamoureux
 * Project 5
 * Handles the Story
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryClicker : MonoBehaviour
{
    [SerializeField] private GameObject newsPanel;
    [SerializeField] private GameObject usa;
    [SerializeField] private GameObject tv;
    [SerializeField] private GameObject plane;
    [SerializeField] private GameObject couch;
    [SerializeField] private GameObject syringe;
    [SerializeField] private GameObject skin;
    [SerializeField] private GameObject subParent;
    [SerializeField] private List<GameObject> submarines;
    private Text newsText;
    private bool canChange = false;
    public int moment = 0;
    // Start is called before the first frame update
    void Start()
    {
        newsText = newsPanel.transform.GetChild(0).GetComponent<Text>();
        newsText.text = "News: Everyone have a wonderful day!";
        StartCoroutine(StartWait(.25f));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && canChange)
        {
            moment++;
            canChange = false;
            NextMoment();
        }
        if (moment == 5)
        {
            syringe.transform.position = Vector3.MoveTowards(syringe.transform.position,
                                                             new Vector3(-3.6f, -4.3f, 0f),
                                                             25f * Time.deltaTime);
            if (canChange)
            {
                newsPanel.SetActive(true);
            }
        }
        if (moment == 6)
        {
            foreach (GameObject sub in submarines)
            {
                sub.transform.Rotate(Vector3.forward * 100f * Time.deltaTime);
                sub.transform.position = Vector3.MoveTowards(sub.transform.position,
                                                             new Vector3(7.53f, -6f, 0f),
                                                             3f * Time.deltaTime);
            }
        }
    }

    void NextMoment()
    {
        switch (moment)
        {
            case 1:
                usa.SetActive(false);
                plane.SetActive(true);
                newsText.text = "News: Oh Wait! What's this?";
                StartCoroutine(StartWait(.25f));
                break;
            case 2:
                newsText.text = "News: Getting reports that a new variant of Covid-19 is enroute to the United States!";
                StartCoroutine(StartWait(.25f));
                break;
            case 3:
                usa.SetActive(true);
                plane.SetActive(false);
                newsText.text = "News: The Infection and Death Counts are Rising!!!";
                StartCoroutine(StartWait(.25f));
                break;
            case 4:
                newsText.text = "News: Everyone go get vaccinated!";
                StartCoroutine(StartWait(.25f));
                break;
            case 5:
                newsText.text = "";
                newsPanel.SetActive(false);
                usa.SetActive(false);
                tv.SetActive(false);
                couch.SetActive(false);
                syringe.SetActive(true);
                skin.SetActive(true);
                StartCoroutine(StartWait(.25f));
                break;
            case 6:
                subParent.SetActive(true);
                newsText.text = "Release the Submarine Army!";
                StartCoroutine(StartWait(.45f));
                break;
            case 7:
                GameManager.Instance.UnloadCurrentLevel();
                GameManager.Instance.LoadLevel("OperationMissionSubmarineBigfish");
                Destroy(gameObject);
                break;
        }
    }

    IEnumerator StartWait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        canChange = true;
    }
}
