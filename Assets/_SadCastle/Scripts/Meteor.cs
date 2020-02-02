using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : Entity
{
    Animator anim;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (move)
            Move();
    }
    public override void Move()
    {
        anim.SetTrigger("DoMeteor");
        Destroy(gameObject, destroyTime);
    }
    public override void OnTriggerEnter(Collider other)
    {
        // if (other.CompareTag("CastleChunk"))
        // {
        //     CastleChunk chunk = other.GetComponent<CastleChunk>();
        //     if (maxChuncks > 0 && !chunk.CanRepair())
        //     {
        //         maxChuncks--;
        //         other.GetComponent<CastleChunk>().Destroy();
        //     }
        // }
    }
    private void EndGame()
    {
        Kernel.instance.EndGame();
    }
}
 