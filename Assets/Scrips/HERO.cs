using UnityEngine;

public class HERO : MonoBehaviour
{
    [SerializeField] private float _speed;
    private float _direction;


    public void SetDirection(float direction)
    {
        _direction = direction;
    }
    private void Update()
    {
        if (_direction != 0)
        {
            var deltaX = _speed * _direction * Time.deltaTime;
            var newXPosition = transform.position.x + deltaX;
            transform.position = new Vector3(newXPosition, transform.position.y, transform.position.z);
        }
    }
    public void SaySomething()
    {
        Debug.Log("Something");
    }
}
