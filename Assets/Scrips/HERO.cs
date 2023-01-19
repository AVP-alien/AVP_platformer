using UnityEngine;

public class HERO : MonoBehaviour
{
    [SerializeField]    private float _speedX;
    [SerializeField]    private float _speedY;
    private Vector2 _direction;

    public void SetDirection( Vector2 direction)
    {
        _direction = direction;
    }
    private void Update()
    {
        if (_speedX != 0 || _speedY!=0 )
        {
            var deltax = _direction * _speedX * Time.deltaTime;
            var deltay = _direction * _speedY * Time.deltaTime;
            transform.position += new Vector3(deltax.x, deltay.y,transform.position.z);
        }
    }
    public void SaySomething()
    {
        Debug.Log("Something");
    }
}
