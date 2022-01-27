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

    int m_ActorParticles;
    public int FindClosestRopeParticle(Vector3 targetPos)
    {

        var closestIndexDistance = 100f;
        int closestIndex = 0;

        //Find nearest particle to players hand
        foreach (int solverIndex in solver.actors[0].solverIndices)
        {

            Vector3 pos = solver.actors[0].GetParticlePosition(solverIndex);

            float distance = Vector3.Distance(pos, targetPos);
            if (distance < closestIndexDistance)
            {
                closestIndexDistance = distance;
                closestIndex = solverIndex;
            }
        }
        return closestIndex;
    }

    void Solver_OnCollision(object sender, Obi.ObiSolver.ObiCollisionEventArgs e)
    {
        var world = ObiColliderWorld.GetInstance();

        foreach (Oni.Contact contact in e.contacts)
        {
            ObiColliderBase col = world.colliderHandles[contact.bodyB].owner;

            if (contact.distance < 0.01f)
            {
                if (col != null)
                {
                    if (LayerMask.LayerToName(col.gameObject.layer).Equals("Bone"))
                    {


                        if (col.transform.parent != null)
                        {
                            Vector3 velocity = solver.velocities[FindClosestRopeParticle(col.transform.position)];
                            col.transform.parent.GetComponent<Enemy>().DecreaseHP(100, col.transform, velocity);
                        }
                    }
                }
            }
            else if (contact.distance >= 0.05f)
            {
                if (col != null)
                {

                    if (LayerMask.LayerToName(col.gameObject.layer).Equals("Bone"))
                    {
                        if (col.transform.parent != null)
                        {
                            Vector3 velocity = solver.velocities[FindClosestRopeParticle(col.transform.position)];
                            col.transform.parent.GetComponent<Enemy>().DecreaseHP(100, col.transform, velocity);
                        }
                    }

                }
            }
        }


    }


}
