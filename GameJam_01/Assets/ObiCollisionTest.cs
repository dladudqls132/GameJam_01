using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;

public class ObiCollisionTest : MonoBehaviour
{
    [SerializeField] private ObiSolver solver;

    // Start is called before the first frame update
    void Start()
    {

    }

    void OnEnable()
    {
        solver.OnCollision += Solver_OnCollision;
    }

    void OnDisable()
    {
        solver.OnCollision -= Solver_OnCollision;
    }

    void Solver_OnCollision(object sender, Obi.ObiSolver.ObiCollisionEventArgs e)
    {
        var world = ObiColliderWorld.GetInstance();
        //overlapCollisions.Clear();

        // just iterate over all contacts in the current frame:
        foreach (Oni.Contact contact in e.contacts)
        {
            ObiColliderBase col = world.colliderHandles[contact.bodyB].owner;

            // if this one is an actual collision:
            if (contact.distance < 0.01f)
            {
                if (col != null)
                {

                    if (LayerMask.LayerToName(col.gameObject.layer).Equals("Bone"))
                    {
                        if (col.transform.parent != null)
                            col.transform.parent.GetComponent<Enemy>().DecreaseHP(100, col.transform);
                    }

                    // do something with the collider.
                }
            }
            else if (contact.distance >= 0.05f)
            {
                if (col != null)
                {

                    if (LayerMask.LayerToName(col.gameObject.layer).Equals("Bone"))
                    {
                        if(col.transform.parent != null)
                            col.transform.parent.GetComponent<Enemy>().DecreaseHP(100, col.transform);
                    }

                }
            }
        }


    }


}
