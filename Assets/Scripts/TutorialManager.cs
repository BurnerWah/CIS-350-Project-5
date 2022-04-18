/*
 * Conner Ogle, Jaden Pleasants
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
        switch (State)
        {
            case TutorialState.Movement:
                Tutorialbox.text = "Press WASD to Move \nPress Space to Continue";
                if (GetSpacePressed())
                {
                    State = TutorialState.Shooting;
                }
                break;
            case TutorialState.Shooting:
                Tutorialbox.text = "Use left mouse button to shoot a missile and it fire where the cursor is \nUse your mouse to move the cursor \nPress Space to Continue";
                if (GetSpacePressed())
                {
                    State = TutorialState.CovidCell;
                }
                break;
            case TutorialState.CovidCell:
                Tutorialbox.text = "Here is a Covid Cell! This is the point of our mission, destroy it! \nPress Space to Continue";

                if (!spawnCovid)
                {
                    spawnCovid = true;
                    SpawnPreab(CovidCell);

                }

                if (GetSpacePressed())
                {
                    State = TutorialState.BloodCell;
                }
                break;
            case TutorialState.BloodCell:
                Tutorialbox.text = "Here is a healthy blood cell. Shooting these will result in damage to the person \nPress Space to Continue";


                if (!spawnBlood)
                {
                    spawnBlood = true;
                    SpawnPreab(BloodCell);
                }

                if (GetSpacePressed())
                {
                    State = TutorialState.Finished;
                }
                break;
            case TutorialState.Finished:
                Tutorialbox.text = "Tutorial Finished! You are prepared to proceed with operation Big Fish. \nPress Space to Continue";
                if (GetSpacePressed())
                {
                    Debug.Log("Done");
                }
                break;
        }
    }

    bool GetSpacePressed() => Input.GetKeyDown(KeyCode.Space);

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
