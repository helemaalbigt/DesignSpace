using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Pagination : MonoBehaviour {

    public Transform _NodeWrapper;
    public GameObject _PaginationWrapper;
    public int _NodesPerPage;
    public Text _PageText;

    private List<Transform> _Nodes = new List<Transform>();

    private int _NumberOfPages;

    private int currentPage = 1;
    public int _CurrentPage{
        get { return currentPage; }
        set
        {
            currentPage = Mathf.Clamp(value, 1, _NumberOfPages);
            UpdateVisibleNodes();
            UpdatePageText();
        }
    }

	// Use this for initialization
	void Start () {
        if (_NodeWrapper == null)
            _NodeWrapper = transform;

        UpdateNodes();
    //    Invoke("UpdateNodes", 0.1f);
	}

    private void UpdateNodes()
    {
        foreach(Transform child in _NodeWrapper)
        {
            _Nodes.Add(child);
        }

        _NumberOfPages = Mathf.CeilToInt((float)_Nodes.Count / (float)_NodesPerPage);

        UpdateVisibleNodes();
        UpdatePageText();
        UpdateVisibility();
    }

    public void PageUp()
    {
        _CurrentPage++;
    }

    public void PageDown()
    {
        _CurrentPage--;
    }

    private void UpdateVisibleNodes()
    {
        for(int i = 0; i<_Nodes.Count; i++)
        {
            if( (i >= (_CurrentPage - 1) * _NodesPerPage) && (i < _CurrentPage * _NodesPerPage) )
            {
                _Nodes[i].gameObject.SetActive(true);
            }
            else
            {
                _Nodes[i].gameObject.SetActive(false);
            }
        }
    }

    private void UpdatePageText()
    {
        _PageText.text = _CurrentPage + "/" + _NumberOfPages;
    }

    private void UpdateVisibility()
    {
        _PaginationWrapper.SetActive(_NumberOfPages > 1);
    }
}
