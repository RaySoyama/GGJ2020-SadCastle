using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepProjector : MonoBehaviour
{
    [SerializeField]
    private GameObject FootProjectorPrefab;

    [SerializeField]
    private Material defaultFootMat;

    [SerializeField]
    private List<Projector> FootstepOP;

    [SerializeField]
    private GameObject leftFootPos;

    [SerializeField]
    private GameObject rightFootPos;

    [SerializeField]
    private int PoolSizeOnStart = 5;

    [SerializeField]
    private float fadeDuration = 1;

    private void Start()
    {
        FootstepOP.Clear();

        for (int i = 0; i < PoolSizeOnStart; i++)
        {
            Projector newFoot = Instantiate(FootProjectorPrefab).GetComponent<Projector>();
            newFoot.material = new Material(defaultFootMat);
            newFoot.gameObject.SetActive(false);
            FootstepOP.Add(newFoot);
        }
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            SpawnLeftFootstep();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            SpawnRightFootstep();
        }


        foreach (Projector fp in FootstepOP)
        {
            if (fp.gameObject.activeSelf == true)
            {
                float alpha = fp.material.GetFloat("_Alpha");

                if (alpha >= 1.0f)
                {
                    fp.gameObject.SetActive(false);
                }

                fp.material.SetFloat("_Alpha", alpha + 1.0f / fadeDuration * Time.deltaTime);
            }
        }
    }


    [ContextMenu("Spawn Left Foot")]
    public void SpawnLeftFootstep()
    {
        Debug.Log("Left Foot");

        Projector activeProj = GetAvalibleFooprint();
        activeProj.transform.position = new Vector3(leftFootPos.transform.position.x, leftFootPos.transform.position.y + 5, leftFootPos.transform.position.z);
        activeProj.transform.eulerAngles = new Vector3(90,leftFootPos.transform.eulerAngles.y,0);

        activeProj.gameObject.SetActive(true);
        activeProj.material.SetFloat("_LeftRight",0.0f);
        activeProj.material.SetFloat("_Alpha", 0.0f);
    }

    [ContextMenu("Spawn Right Foot")]
    public void SpawnRightFootstep()
    {
        Debug.Log("Right Foot");
        Projector activeProj = GetAvalibleFooprint();
        activeProj.transform.position = new Vector3(rightFootPos.transform.position.x, rightFootPos.transform.position.y + 5, rightFootPos.transform.position.z);
        activeProj.transform.eulerAngles = new Vector3(90, rightFootPos.transform.eulerAngles.y, 0);

        activeProj.gameObject.SetActive(true);
        activeProj.material.SetFloat("_LeftRight", 1.0f);
        activeProj.material.SetFloat("_Alpha", 0.0f);
    }


    private Projector GetAvalibleFooprint()
    {
        foreach (Projector fp in FootstepOP)
        {
            if (fp.gameObject.activeSelf == false)
            {
                return fp;
            }
        }

        Projector newFoot = Instantiate(FootProjectorPrefab).GetComponent<Projector>();
        newFoot.material = new Material(defaultFootMat);
        FootstepOP.Add(newFoot);
        return newFoot;
    }

}
