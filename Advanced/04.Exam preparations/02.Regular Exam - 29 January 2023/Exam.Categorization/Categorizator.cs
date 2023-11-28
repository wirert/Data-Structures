using System;
using System.Collections.Generic;
using System.Linq;

namespace Exam.Categorization
{
    public class Categorizator : ICategorizator
    {
        private Dictionary<string, Category> categoriesById = new Dictionary<string, Category>();

        public void AddCategory(Category category)
        {
            if (categoriesById.ContainsKey(category.Id))
            {
                throw new ArgumentException();
            }

            categoriesById.Add(category.Id, category);
        }

        public void AssignParent(string childCategoryId, string parentCategoryId)
        {
            if (!categoriesById.ContainsKey(childCategoryId) ||
                !categoriesById.ContainsKey(parentCategoryId))
            {
                throw new ArgumentException();
            }

            var child = categoriesById[childCategoryId];
            var parent = categoriesById[parentCategoryId];
            if (parent.Children.Contains(child))
            {
                throw new ArgumentException();
            }

            child.Parent = parent;
            parent.Children.Add(child);

            FixMaxDepthOfParents(child);
        }

        public bool Contains(Category category) => categoriesById.ContainsKey(category.Id);

        public IEnumerable<Category> GetChildren(string categoryId)
        {
            if (!categoriesById.ContainsKey(categoryId))
            {
                throw new ArgumentException();
            }

            var queue = new Queue<Category>();
            var allChildren = new List<Category>();
            queue.Enqueue(categoriesById[categoryId]);

            while (queue.Count > 0)
            {
                var currnet = queue.Dequeue();

                foreach (var child in currnet.Children)
                {
                    allChildren.Add(child);
                    queue.Enqueue(child);
                }
            }

            return allChildren;
        }

        public IEnumerable<Category> GetHierarchy(string categoryId)
        {
            if (!categoriesById.ContainsKey(categoryId))
            {
                throw new ArgumentException();
            }

            var category = categoriesById[categoryId];

            var hierarcy = new Stack<Category>();
            while (category != null)
            {
                hierarcy.Push(category);
                category = category.Parent;
            }

            return hierarcy;
        }

        public IEnumerable<Category> GetTop3CategoriesOrderedByDepthOfChildrenThenByName()
            => categoriesById.Values
            .OrderByDescending(c => c.MaxDepth)
            .ThenBy(c => c.Name)
            .Take(3);

        public void RemoveCategory(string categoryId)
        {
            if (!categoriesById.ContainsKey(categoryId))
            {
                throw new ArgumentException();
            }

            var category = categoriesById[categoryId];

            RemoveCategoryAndChildren(category);


            var parent = category.Parent;

            if (parent == null)
            {
                return;
            }

            parent.Children.Remove(category);

            var childWithMaxDepth = parent.Children.OrderByDescending(c => c.MaxDepth).FirstOrDefault();

            var restChildernMaxDepth = childWithMaxDepth == null ? -1 : childWithMaxDepth.MaxDepth;

            if (category.MaxDepth > restChildernMaxDepth)
            {
                parent.MaxDepth = restChildernMaxDepth++;
            }
        }

        public int Size() => categoriesById.Count;

        private void FixMaxDepthOfParents(Category category)
        {
            while (category.Parent != null && category.Parent.MaxDepth <= category.MaxDepth + 1)
            {
                category.Parent.MaxDepth = category.MaxDepth + 1;
                category = category.Parent;
            }
        }

        private void RemoveCategoryAndChildren(Category category)
        {

            var queue = new Queue<Category>();
            queue.Enqueue(category);

            while (queue.Count > 0)
            {
                var currnet = queue.Dequeue();
                categoriesById.Remove(currnet.Id);
                foreach (var child in currnet.Children)
                {
                    queue.Enqueue(child);
                }
            }
        }
    }
}
