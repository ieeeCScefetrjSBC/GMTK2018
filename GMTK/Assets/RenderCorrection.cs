using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderCorrection : MonoBehaviour {

    static List<SpriteRenderer> Everything;
    static List<int> EveryOrder;

    void Start () {
        Everything = new List<SpriteRenderer>();
        EveryOrder = new List<int>();

        Everything.AddRange(FindObjectsOfType<SpriteRenderer>());
        foreach (SpriteRenderer sp in Everything) EveryOrder.Add(sp.sortingOrder);

    }
	
	void Update () {
        for (int i = 0; i < Everything.Count; i++)
        {
            if (Everything[i] != null)
            {
                int importante = 0;
                Transform varTra = Everything[i].transform;
                while (varTra.parent != null) { importante += (int)varTra.parent.position.y; varTra = varTra.parent; }
                Everything[i].sortingOrder = (int)Everything[i].transform.position.y + importante + EveryOrder[i]; 
            }
        }
	}
}
