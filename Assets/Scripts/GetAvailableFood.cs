using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAvailableFood : GAction
{
    public override bool PostPerform()
    {
        throw new System.NotImplementedException();
    }

    public override bool PrePerform()
    {
        
        target = GWorld.Instance.GetClosestAvaialableFood(gameObject);
        return false;

    }

    public override void Reset()
    {
        preconditions.Add("AvailableFood", 1);
        preconditions.Add("IsHungry", 1);  
}
