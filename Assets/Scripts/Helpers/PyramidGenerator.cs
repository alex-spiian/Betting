using UnityEngine;

public class PyramidGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject _spherePrefab;
    
    [SerializeField]
    private int _rows;
    
    [SerializeField]
    private float _spacing;
    
    [SerializeField]
    private Transform _container;

    private void Start()
    {
        GeneratePyramid();
    }

    private void GeneratePyramid()
    {
        for (var row = 0; row < _rows; row++)
        {
            var spheresInRow = _rows - row;
            var offset = (_rows - spheresInRow) * _spacing / 2f;

            for (var i = 0; i < spheresInRow; i++)
            {
                var position = new Vector3(i * _spacing + offset, row * _spacing, 0);
                var circle = Instantiate(_spherePrefab, position, Quaternion.identity);
                circle.transform.SetParent(_container);
            }
        }
    }
}