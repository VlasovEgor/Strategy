using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barack : Building
{
    public Transform Spawn;
    public void CreateUnit(GameObject UnitPrefab)
    {
       GameObject newUnit= Instantiate(UnitPrefab, Spawn.position, Quaternion.identity);
        Vector3 position = Spawn.position+ new Vector3(Random.Range(-2f, 2f), 0, Random.Range(-2f, 2f));
        newUnit.GetComponent<Unit>().WhenClickOnGround(position);
    }
}
