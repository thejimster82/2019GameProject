using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class opacityController : MonoBehaviour
{
    Color matCol = new Color();
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Transparentize(GameObject obj)
    {
        StartCoroutine(ChangeTransparency(obj.GetComponent<MeshRenderer>(), 0.15f));
    }

    public void Opaquen(GameObject obj)
    {
        StartCoroutine(ChangeTransparency(obj.GetComponent<MeshRenderer>(), 1f));
    }
//TODO: try sending gameobject instead of just meshrenderer
    private IEnumerator ChangeTransparency(MeshRenderer mesh, float alpha)
    {
        Color Col = new Color();
        Col = mesh.material.color;
        while (mesh.material.color.a-alpha > 0.1)
        {
            Col.a = Mathf.Lerp(Col.a, alpha, 0.1f);
            mesh.material.color = Col;
            Debug.Log(Col.a);
        }
        Col.a = Mathf.Lerp(Col.a, alpha, 0.1f);
        mesh.material.color = Col;
        yield return new WaitForSeconds(0f);
    }
}
