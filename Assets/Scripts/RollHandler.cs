using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollHandler : MonoBehaviour
{
	[SerializeField] private GameObject diePrefab;
	private GameObject[] dieInst = null;
	private Rigidbody rig;

    private int diceRolled = 0, diceFinished = 0, totalDamage = 0;

    void Start() { }

    void Update() { }

    public int isDone()
    {
        if (dieInst == null)
            return 0;

        foreach (GameObject die in dieInst)
        {
            if (die == null)
                continue;

            rig = die.GetComponent<Rigidbody>();
            if (rig != null)
            {
                if (rig.IsSleeping())
                {
                    if (!getResult(die))
                        reroll(rig);
                }
            }
        }

        if (diceRolled > diceFinished || diceFinished == 0)
            return 0;
        else
            return totalDamage;
    }

    public void Reset()
    {
        dieInst = null;
        diceRolled = 0;
        diceFinished = 0;
        totalDamage = 0;
    }

    public void rollDice(int numOfDice = 1)
    {
        diceRolled = numOfDice;
        dieInst = new GameObject[numOfDice];

        for (int i = 0; i < numOfDice; ++i)
        {
            Vector3 pos = new Vector3(5.0f, 7.5f + i, 5.0f);

            dieInst[i] = Instantiate(diePrefab, pos, Random.rotation);
            rig = dieInst[i].GetComponent<Rigidbody>();

            rig.AddForce(-5.0f, 0.0f, -5.0f, ForceMode.Impulse);
            rig.AddTorque(Random.Range(30.0f, 40.0f), 
                        Random.Range(30.0f, 40.0f),
                        Random.Range(30.0f, 40.0f));
        }
    }

    private void reroll(Rigidbody dieRig)
    {
        dieRig.AddForce(0.0f, 5.0f, 0.0f, ForceMode.Impulse);
        dieRig.AddTorque(Random.Range(30.0f, 40.0f), 
                    Random.Range(30.0f, 40.0f),
                    Random.Range(30.0f, 40.0f));
    }

    public bool getResult(GameObject die)
    {
    	int diceCount = 0;

    	// Debug.Log(Vector3.Dot (die.transform.forward, Vector3.up));
    	// Debug.Log(Vector3.Dot (die.transform.up, Vector3.up));
    	// Debug.Log(Vector3.Dot (die.transform.right, Vector3.up));

    	if (Vector3.Dot (die.transform.forward, Vector3.up) > 0.9)
    		diceCount = 4; // 5
    	else if (Vector3.Dot (-die.transform.forward, Vector3.up) > 0.9)
    		diceCount = 5; // 2
    	else if (Vector3.Dot (die.transform.up, Vector3.up) > 0.9)
    		diceCount = 3; // 3
    	else if (Vector3.Dot (-die.transform.up, Vector3.up) > 0.9)
    		diceCount = 1; // 4
    	else if (Vector3.Dot (die.transform.right, Vector3.up) > 0.9)
    		diceCount = 6; // 6
    	else if (Vector3.Dot (-die.transform.right, Vector3.up) > 0.9)
    		diceCount = 2; // 1
        else
            return false;

    	Debug.Log ("diceCount :" + diceCount);
        totalDamage += diceCount;
        print(totalDamage);
        diceFinished++;
        Destroy(die);
        return true;
    }
}
