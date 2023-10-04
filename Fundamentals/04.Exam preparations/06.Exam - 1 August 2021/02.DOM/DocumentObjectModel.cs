namespace _02.DOM
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using _02.DOM.Interfaces;
    using _02.DOM.Models;

    public class DocumentObjectModel : IDocument
    {
        public DocumentObjectModel(IHtmlElement root)
        {
            this.Root = root;
        }

        public DocumentObjectModel()
        {
            Root = new HtmlElement(ElementType.Document, 
                                    new HtmlElement(ElementType.Html, 
                                                    new HtmlElement(ElementType.Head), 
                                                    new HtmlElement(ElementType.Body))
                                  );
        }

        public IHtmlElement Root { get; private set; }

        public bool Contains(IHtmlElement htmlElement)
        {
            var queue = new Queue<IHtmlElement>();
            queue.Enqueue(Root);

            while (queue.Count > 0)
            {
                var currElement = queue.Dequeue();

                if (currElement == htmlElement)
                {
                    return true;
                }

                foreach (var child in currElement.Children)
                {
                    queue.Enqueue(child);
                }
            }

            return false;
        }

        public IHtmlElement GetElementByType(ElementType type)
        {
            var queue = new Queue<IHtmlElement>();
            queue.Enqueue(Root);

            while (queue.Count > 0)
            {
                var currElement = queue.Dequeue();

                if (currElement.Type == type)
                {
                    return currElement;
                }

                foreach (var child in currElement.Children)
                {
                    queue.Enqueue(child);
                }
            }

            return null;
        }

        public List<IHtmlElement> GetElementsByType(ElementType type)
        {
            var stack = new Stack<IHtmlElement>();
            var elements = new List<IHtmlElement>();

            stack.Push(Root);

            while (stack.Count > 0)
            {
                var currElement = stack.Pop();

                if (currElement.Type == type)
                {
                    elements.Add(currElement);
                }

                foreach (var child in currElement.Children)
                {
                    stack.Push(child);
                }
            }

            elements.Reverse();

            return elements;
        }

        public void InsertFirst(IHtmlElement parent, IHtmlElement child)
        {
            if (Contains(parent) == false)
            {
                throw new InvalidOperationException();
            }

            parent.Children.Insert(0, child);
            child.Parent = parent;
        }

        public void InsertLast(IHtmlElement parent, IHtmlElement child)
        {
            if (Contains(parent) == false)
            {
                throw new InvalidOperationException();
            }

            parent.Children.Add(child);
            child.Parent = parent;
        }

        public void Remove(IHtmlElement htmlElement)
        {
            if (Contains(htmlElement) == false)
            {
                throw new InvalidOperationException();
            }

            htmlElement.Parent.Children.Remove(htmlElement);
            htmlElement.Parent = null;
        }

        public void RemoveAll(ElementType elementType)
        {
            var elementsToRemove = GetElementsByType(elementType);

            foreach (var element in elementsToRemove)
            {
                Remove(element);
            }
        }

        public bool AddAttribute(string attrKey, string attrValue, IHtmlElement htmlElement)
        {
            if (Contains(htmlElement) == false)
            {
                throw new InvalidOperationException();
            }

            if (htmlElement.Attributes.ContainsKey(attrKey))
            {
                return false;
            }

            htmlElement.Attributes.Add(attrKey, attrValue);

            return true;
        }

        public bool RemoveAttribute(string attrKey, IHtmlElement htmlElement)
        {
            if (Contains(htmlElement) == false)
            {
                throw new InvalidOperationException();
            }

            if (htmlElement.Attributes.ContainsKey(attrKey) == false)
            {
                return false;
            }

            htmlElement.Attributes.Remove(attrKey);

            return true;
        }

        public IHtmlElement GetElementById(string idValue)
        {
            var queue = new Queue<IHtmlElement>();
            queue.Enqueue(Root);

            while (queue.Count > 0)
            {
                var curElement = queue.Dequeue();

                if (curElement.Attributes.ContainsKey("id"))
                {
                    if (curElement.Attributes["id"] == idValue)
                    {
                        return curElement;
                    }
                }

                foreach (var child in curElement.Children)
                {
                    queue.Enqueue(child);
                }
            }

            return null;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            PreOrderDfsToString(Root, sb, 0);

            return sb.ToString().Trim();
        }

        private void PreOrderDfsToString(IHtmlElement node, StringBuilder sb, int indent)
        {
            if (node == null)
            {
                return;
            }

            sb.AppendLine($"{new string(' ', indent)}{node.Type}");

            foreach (var child in node.Children)
            {
                PreOrderDfsToString(child, sb, indent + 2);
            }
        }
    }
}
