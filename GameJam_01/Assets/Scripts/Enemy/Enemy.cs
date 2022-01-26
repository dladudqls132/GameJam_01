using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    [SerializeField] protected float hp;
    [SerializeField] protected float speed;
    [SerializeField] protected bool isDead;
    [SerializeField] protected Bone weaknessBone;

    protected float currentHp;
    protected float currentSpeed;

    public void DecreaseHP(float damage, Transform part)
    {
        if (isDead) return;

        currentHp -= damage;

        currentHp = Mathf.Clamp(currentHp, 0, hp);

        if(part.GetComponent<EnemyBone>().bone == weaknessBone)
        {
            Debug.Log("Critical!!");
        }
        else
        {
            Debug.Log("Damaged");
        }

        if(currentHp <= 0)
        {
            isDead = true;

            part.gameObject.SetActive(false);

            for (int i = 0; i < this.transform.childCount; i++)
            {
                if (transform.GetChild(i).gameObject.activeSelf)
                {
                    if (transform.GetChild(i).GetComponent<CharacterJoint>() != null)
                    {
                        if (!transform.GetChild(i).GetComponent<CharacterJoint>().connectedBody.gameObject.activeSelf)
                            transform.GetChild(i).GetComponent<CharacterJoint>().connectedBody = null;
                    }

                    transform.GetChild(i).GetComponent<Rigidbody>().useGravity = true;
                    transform.GetChild(i).GetComponent<Rigidbody>().isKinematic = false;
                }
            }

            transform.DetachChildren();
        }
    }
}
