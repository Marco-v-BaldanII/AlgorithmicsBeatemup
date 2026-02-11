using UnityEngine;


//public enum LongmoanState
//{
//    Idle   = 0,
//    Attack = 1,
//    Chase  = 2,
//}

public class Longmoan : Enemy
{

    //private LongmoanState currentState = LongmoanState.Idle;

    

    public float chaseSpeed = 4f;

    public float atkDistance = 2f;

    protected override void Awake()
    {
        base.Awake(); // Call enemy awake method
    }


    private void Update()
    {

        HandleDirection();
    }



}
