using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioActionGiftConfig : MonoBehaviour
{
    [TextArea(3, 10)]
    public string description;
    public Card[] cards;
    public ScenarioNode[] nodes;

    public delegate List<Card> ListCardDelegate();
    public delegate List<ScenarioNode> ListNodesDelegate();
    public ListNodesDelegate listNodesDelegate;
    public ListCardDelegate listCardDelegate;

    private void Start()
    {
        listCardDelegate += GetDefaultCards;
        listNodesDelegate += GetDefaultNodes;
    }

    private List<Card> GetDefaultCards()
    {
        List<Card> result = new List<Card>();
        foreach(Card card in cards)
        {
            result.Add(card);
        }
        return result;
    }

    private List<ScenarioNode> GetDefaultNodes()
    {
        List<ScenarioNode> result = new List<ScenarioNode>();
        foreach(ScenarioNode node in nodes)
        {
            result.Add(node);
        }
        return result;
    }

    public List<Card> GetCards()
    {
        List<Card> result = new List<Card>();
        foreach(ListCardDelegate listDelegate in listCardDelegate.GetInvocationList())
        {
            foreach(Card card in listDelegate())
            {
                result.Add(card);
            }
        }
        return result;
    }

    public List<ScenarioNode> GetNodes()
    {
        List<ScenarioNode> result = new List<ScenarioNode>();
        foreach(ListNodesDelegate listDelegate in listNodesDelegate.GetInvocationList())
        {
            foreach(ScenarioNode card in listDelegate())
            {
                result.Add(card);
            }
        }
        return result;
    }
}
