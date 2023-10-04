namespace _01._BrowserHistory
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using _01._BrowserHistory.Interfaces;

    public class BrowserHistory : IHistory
    {
        private LinkedList<ILink> linkHistory = new LinkedList<ILink>();

        public int Size => linkHistory.Count;

        public void Clear() => linkHistory.Clear();

        public bool Contains(ILink link) => linkHistory.Contains(link);

        public ILink DeleteFirst()
        {
            if (linkHistory.Count == 0)
            {
                throw new InvalidOperationException();
            }

            var firstLink = linkHistory.Last.Value;
            linkHistory.RemoveLast();

            return firstLink;
        }

        public ILink DeleteLast()
        {
            if (linkHistory.Count == 0)
            {
                throw new InvalidOperationException();
            }

            var lastLink = linkHistory.First.Value;
            linkHistory.RemoveFirst();

            return lastLink;
        }

        public ILink GetByUrl(string url) => linkHistory.FirstOrDefault(l => l.Url == url);

        public ILink LastVisited()
        {
            if (linkHistory.Count == 0)
            {
                throw new InvalidOperationException();
            }

            return linkHistory.First.Value;
        }

        public void Open(ILink link) => linkHistory.AddFirst(link);

        public int RemoveLinks(string url)
        {
            if (linkHistory.Count == 0)
            {
                throw new InvalidOperationException();
            }

            var linksToRemove = linkHistory.Where(l => l.Url.ToLower().Contains(url.ToLower())).ToArray();

            if (linksToRemove.Length == 0)
            {
                throw new InvalidOperationException();
            }

            foreach (var link in linksToRemove)
            {
                linkHistory.Remove(link);
            }

            return linksToRemove.Length;
        }

        public ILink[] ToArray() => linkHistory.ToArray();

        public List<ILink> ToList() => linkHistory.ToList();

        public string ViewHistory()
        {
            if (linkHistory.Count == 0)
            {
                return "Browser history is empty!";
            }

            var sb = new StringBuilder(linkHistory.Count);

            foreach (var link in linkHistory)
            {
                sb.AppendLine(link.ToString());
            }

            return sb.ToString();
        }
    }
}
