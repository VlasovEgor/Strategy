using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : Building
{
    private float _timer = 0;
    public int MiningTime;
    public override void Start()
    {
        base.Start();
        
    }   
    private void Update()
    {
        _timer += Time.deltaTime;
        if(_timer> MiningTime)
        {
            _timer = 0;
            FindObjectOfType<Resource>().Money++;
        }
    }
}
