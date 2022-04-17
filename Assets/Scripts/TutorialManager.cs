/*
 * Conner Ogle
 * Project 5
 * Manages the tutorial
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public TutorialState State;

    public Text Tutorialbox;

    [SerializeField] GameObject CovidCell;
    [SerializeField] GameObject BloodCell;

    bool spawnCovid;
    bool spawnBlood;

    // Start is called before the first frame update
    void Start()
    {
        if (Tutorialbox == null)
        {
            Tutorialbox = FindObjectOfType<Text>();
        }
        State = TutorialState.Movement;
    }

    // Update is called once per frame
    void Update()
    {
        if (State == TutorialState.Movement)
        {
            Tutorialbox.text = "Press WASD to Move \nPress Space to Continue";
            if (SpacePressed() == true)
            {
                State = TutorialState.Shooting;
            }
        }
        else if (State == TutorialState.Shooting)
        {
            Tutorialbox.text = "Use left mouse button to shoot a missile and it fire where the cursor is \nUse your mouse to move the cursor \nPress Space to Continue";
            if (SpacePressed() == true)
            {
                State = TutorialState.CovidCell;
            }
        }
        else if (State == TutorialState.CovidCell)
        {
            Tutorialbox.text = "Here is a Covid Cell! This is the point of our mission, destroy it! \nPress Space to Continue";

            if (!spawnCovid)
            {
                spawnCovid = true;
                SpawnPreab(CovidCell);

            }

            if (SpacePressed() == true)
            {
                State = TutorialState.BloodCell;
            }
        }
        else if (State == TutorialState.BloodCell)
        {
            Tutorialbox.text = "Here is a healthy blood cell. Shooting these will result in damage to the person \nPress Space to Continue";


            if (!spawnBlood)
            {
                spawnBlood = true;
                SpawnPreab(BloodCell);
            }

            if (SpacePressed() == true)
            {
                State = TutorialState.Finished;
            }
        }
        else if (State == TutorialState.Finished)
        {
            Tutorialbox.text = "Tutorial Finished! You are prepared to proceed with operation Big Fish. \nPress Space to Continue";
            if (SpacePressed() == true)
            {
                print("Done");
            }
        }
    }
    bool SpacePressed()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            return true;
        }
        return false;

    }
    void SpawnPreab(GameObject Cell)
    {
        Vector2 SpawnPos = new Vector2(8.5f, 0);

        Instantiate(Cell, SpawnPos, transform.rotation);
    }

}

public enum TutorialState
{
    Movement,
    Shooting,
    CovidCell,
    BloodCell,
    Finished
}
