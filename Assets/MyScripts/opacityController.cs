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
        if (Mathf.Abs(obj.GetComponent<MeshRenderer>().material.color.a - 0.15f) > .01f)
        {
            Debug.Log("transparentize");
            StartCoroutine(ChangeTransparency(obj, 0.15f));
        }
    }

    public void Opaquen(GameObject obj)
    {
        if (Mathf.Abs(obj.GetComponent<MeshRenderer>().material.color.a - 1) > .01f)
        {
            Debug.Log("opaquen");
            StartCoroutine(ChangeTransparency(obj, 1f));
        }
    }
    //TODO: try sending gameobject instead of just meshrenderer
    private IEnumerator ChangeTransparency(GameObject obj, float alpha)
    {
        MeshRenderer mesh = obj.GetComponent<MeshRenderer>();
        Color Col = new Color();
        Col = mesh.material.color;
        while (Mathf.Abs(mesh.material.color.a - alpha) > .01f)
        {
            Col.a = Mathf.Lerp(Col.a, alpha, 0.1f);
            mesh.material.color = Col;
            yield return new WaitForSeconds(0.01f);

        }
        yield return new WaitForSeconds(0f);
    }
}
