using System.Collections;
using UnityEngine;

public class Growing : MonoBehaviour
{
    [SerializeField] GameObject VinesObj;
    public int Lenght;
    public IEnumerator Growth()
    {
        for(int i = 1; i < Lenght; i++)
        {
            GameObject newObj = Instantiate(VinesObj, gameObject.transform);
            newObj.transform.position = gameObject.transform.position + new Vector3(-i,0,0);
            yield return new WaitForSeconds(0.1f);
        }
    } 
}