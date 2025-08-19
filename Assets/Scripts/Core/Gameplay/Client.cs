using UnityEngine;

public class Client : MonoBehaviour
{
    public Enums.DishType WantedDish { get; private set; }

    [SerializeField] private float waitTime = 10f; // ������� ��� ������

    private float _timer;
    private System.Action<Client> _onLeave;

    public void Init(Enums.DishType dish, System.Action<Client> onLeave)
    {
        WantedDish = dish;
        _timer = waitTime;
        _onLeave = onLeave;
    }

    private void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0f)
        {
            Leave(false);
        }
    }

    public void TakeDish(Enums.DishType dish)
    {
        if (dish == WantedDish)
        {
            Debug.Log($"������ �������, ������� {dish}!");
            Leave(true);
        }
        else
        {
            Debug.Log($"������ ���������, ������ {WantedDish}, � ������� {dish}");
            Leave(false);
        }
    }

    private void Leave(bool success)
    {
        // ��� ����� ���������� �������� �����, ���������� ����� � �.�.
        if (!success)
            Debug.Log("������ ���� �����������!");

        _onLeave?.Invoke(this);
        Destroy(gameObject);
    }
}
