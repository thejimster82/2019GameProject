using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class opacityController : MonoBehaviour
{
    Color matCol = new Color();
    Dictionary<int, Coroutine> affectedObjs = new Dictionary<int, Coroutine>();

    public void Transparentize(GameObject obj)
    {
        // Debug.Log("transparentize");
        ChangeTransparency(obj, 0.15f);
    }

    public void Opaquen(GameObject obj)
    {
        // Debug.Log("opaquen");
        ChangeTransparency(obj, 1);
    }
    public void ChangeTransparency(GameObject obj, float alpha)
    {
        if (Mathf.Abs(obj.GetComponent<MeshRenderer>().material.color.a - alpha) > .01f)
        {
            int instanceID = obj.GetInstanceID();
            if (affectedObjs.ContainsKey(instanceID))
            {
                StopCoroutine(affectedObjs[instanceID]);
                affectedObjs.Remove(instanceID);
            }
            affectedObjs.Add(instanceID, StartCoroutine(ChangeTransparencyCR(obj, alpha)));
        }
    }
    //TODO: try sending gameobject instead of just meshrenderer
    private IEnumerator ChangeTransparencyCR(GameObject obj, float alpha)
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
