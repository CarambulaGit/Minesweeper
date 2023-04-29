using System.Collections.Generic;
using System.Linq;
using MyBox;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public enum VerticalAlignment
    {
        Bottom,
        Center,
        Top
    }

    public VerticalAlignment verticalAlignment;
    public int columns;
    public int rows;
    public float topOffset;
    public float leftOffset;
    public float xOffset;
    public float yOffset;
    public bool calculateCellSize = true;
    public Vector2 cellSize;
    [ReadOnly, SerializeField] private Vector2 center;

    private List<Transform> _activeChilds = new List<Transform>();

    [ContextMenu("UpdateGrid")]
    public void UpdateGrid()
    {
        var cellSize = calculateCellSize ? CalculateCellSize() : this.cellSize;
        this.cellSize = cellSize;
        var center = CalculateCenter();
        this.center = center;
        ActiveChildList();
        for (var i = 0; i < rows; i++)
        {
            var curIndOffset = i * columns;
            for (var j = 0; j < columns; j++)
            {
                var curIndex = curIndOffset + j;
                if (curIndex >= ActiveChildCount()) break;
                var child = _activeChilds[curIndex];
                var newX = leftOffset + (cellSize.x + xOffset) * j - center.x + cellSize.x / 2f;
                var newY = -(topOffset + (cellSize.y + xOffset) * i - center.y + cellSize.y / 2f);
                child.localPosition = new Vector3(newX, newY);
            }
        }
    }

    private Vector2 CalculateCenter()
    {
        if (ActiveChildCount() == 0)
        {
            return Vector2.zero;
        }

        var cellSize = calculateCellSize ? CalculateCellSize() : this.cellSize;
        var x = (leftOffset + (cellSize.x + xOffset) * columns - xOffset) / 2f;
        var y = verticalAlignment switch
        {
            VerticalAlignment.Bottom => 0,
            VerticalAlignment.Center => (topOffset + (cellSize.y + yOffset) * rows - yOffset) / 2f,
            VerticalAlignment.Top => (topOffset + (cellSize.y + yOffset) * rows - yOffset),
            _ => 0
        };

        return new Vector2(x, y);
    }

    private Vector2 CalculateCellSize()
    {
        if (transform.childCount == 0)
        {
            return Vector2.zero;
        }

        var spriteTransform = transform.GetChild(0);
        var sprite = spriteTransform.GetComponent<SpriteRenderer>();
        // todo make size changeable
        return sprite.bounds.size.ToVector2();
    }

    private int ActiveChildCount() => ActiveChildList().Count;

    private List<Transform> ActiveChildList()
    {
        ActiveChildList(ref _activeChilds);
        return _activeChilds;
    }

    private void ActiveChildList(ref List<Transform> childs)
    {
        childs.Clear();
        childs.AddRange(transform.Cast<Transform>().Where(child => child.gameObject.activeSelf));
    }
}