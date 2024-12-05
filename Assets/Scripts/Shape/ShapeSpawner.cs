using System.Collections.Generic;
using System.Linq;
using Pools;
using UnityEngine;

public class ShapeSpawner : MonoBehaviour
{
    [SerializeField] private Shape[] _shapes;

    private Shape _currentShape;
    private ShapeType _currentShapeType;
    
    private readonly Dictionary<ShapeType, MonoBehaviourPool<Shape>> _poolsDictionary = new ();

    public void Spawn(ShapeType shapeType = default)
    {
        Reset();
        
        var requiredShape = _shapes.FirstOrDefault(shape => shape.Type == shapeType);
        if (requiredShape != null)
        {
            var pool = GetPool(shapeType);
            _currentShape = pool.Take();
        }
    }

    public MultiplierBundle[] GetMultiplierBundles()
    {
        return _currentShape.MultiplierBundles;
    }

    private void Reset()
    {
        if (_currentShape != null)
        {
            var pool = GetPool(_currentShapeType);
            pool.Release(_currentShape);
        }
    }
    
    private MonoBehaviourPool<Shape> GetPool(ShapeType type)
    {
        if (_poolsDictionary.ContainsKey(type))
        {
            return _poolsDictionary[type];
        }

        var requiredShape = _shapes.FirstOrDefault(shape => shape.Type == type);
        var projectilePool = new MonoBehaviourPool<Shape>(requiredShape, transform);
        
        _poolsDictionary.Add(type, projectilePool);
        return projectilePool;
    }
}