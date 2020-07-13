using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{
    public Transform parent;
    public GameObject[] spawnObjects;

    public Material sonarShader;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
           // RayCast();
        }
    }

    private void RayCast()
    {
        Vector3 orginPoint = Camera.main.ViewportToWorldPoint(Input.mousePosition); 
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log("or: " + ray.origin + " hit: " + hit.point);
            PlaceRandomObject(hit.point);
        }
        else
        {
            Debug.Log("or: " + orginPoint);
        }
    }

    private void PlaceRandomObject(Vector3 _pos)
    {
        int x = Random.Range(0, spawnObjects.Length);
        GameObject _pla = Instantiate(spawnObjects[x], _pos, Quaternion.identity, parent);
       //_pla.GetComponent<Renderer>().material = sonarShader;
        _pla.AddComponent<SonarObject>();

        ApplyRandomRotation(_pla);
        ApplyRandomScale(_pla);
        ApplyOffset(_pla);
    }

    private void ApplyRandomRotation(GameObject _obj)
    {
        float _randomRotY = Random.Range(0,360);
        _obj.transform.Rotate(new Vector3(0,_randomRotY,0));
    }

    private void ApplyRandomScale(GameObject _obj)
    {
        float _scale = Random.Range(0.8f, 3.6f);
        _obj.transform.localScale = new Vector3(_scale,_scale,_scale);
    }

    private void ApplyOffset(GameObject _obj)
    {
        float offSetX = Random.Range(-1f,1f);
        float offSetZ = Random.Range(-1f, 1f);
        Vector3 _temp = new Vector3(offSetX,0,offSetZ);
        _obj.transform.localPosition += _temp;
    }
         
}
