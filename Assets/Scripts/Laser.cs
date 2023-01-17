using UnityEngine;

public class Laser : MonoBehaviour
{
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * 10F);

        if (transform.position.y > 6)
        {
            Destroy(this.gameObject);
        }
    }
}
