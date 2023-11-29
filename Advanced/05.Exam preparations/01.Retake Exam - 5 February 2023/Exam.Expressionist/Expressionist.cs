using System;
using System.Collections.Generic;
using System.Text;

namespace Exam.Expressionist
{
    public class Expressionist : IExpressionist
    {
        private Expression root;
        private Dictionary<string, Expression> expressionsById;

        public Expressionist()
        {
            expressionsById = new Dictionary<string, Expression>();
        }

        public void AddExpression(Expression expression)
        {
            if (root != null)
            {
                throw new ArgumentException();
            }

            root = expression;           

            BfsAddToCollection(expression);
        }

        public void AddExpression(Expression expression, string parentId)
        {
            if (!expressionsById.ContainsKey(parentId))
            {
                throw new ArgumentException();
            }
            var parent = expressionsById[parentId];

            if (parent.RightChild != null)
            {
                throw new ArgumentException();
            }

            if (parent.LeftChild == null)
            {
                parent.LeftChild = expression;
            }
            else
            {
                parent.RightChild = expression;
            }

            expression.Parent = parent;

            BfsAddToCollection(expression);
        }

        public bool Contains(Expression expression)
            => expressionsById.ContainsKey(expression.Id);

        public int Count() => expressionsById.Count;

        public string Evaluate() => InOrderEvaluate(root, "");

        public Expression GetExpression(string expressionId)
        {
            if (!expressionsById.ContainsKey(expressionId))
            {
                throw new ArgumentException();
            }

            return expressionsById[expressionId];
        }

        public void RemoveExpression(string expressionId)
        {
            if (!expressionsById.ContainsKey(expressionId))
            {
                throw new ArgumentException();
            }
            var expression = expressionsById[expressionId];
            if (expression.Parent == null)
            {
                root = null;
                expressionsById.Remove(expressionId);
            }

            var parent = expressionsById[expression.Parent.Id];

            if (parent.LeftChild == expression)
            {
                parent.LeftChild = parent.RightChild;
            }

            parent.RightChild = null;

            Bfs(expression, e => expressionsById.Remove(e.Id));
        }

        private string InOrderEvaluate(Expression expression, string result)
        {
            if (expression == null)
            {
                return result;
            }

            if (expression.Type == ExpressionType.Value)
            {
                result += expression.Value;
            }
            else
            {
                result += $"({InOrderEvaluate(expression.LeftChild, "")} {expression.Value} {InOrderEvaluate(expression.RightChild, "")})";
            }

            return result;
        }

        private void Bfs(Expression expression, Action<Expression> action)
        {
            var queue = new Queue<Expression>();
            queue.Enqueue(expression);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                action(node);

                if (node.LeftChild != null)
                {
                    queue.Enqueue(node.LeftChild);
                }

                if (node.RightChild != null)
                {
                    queue.Enqueue(node.RightChild);
                }
            }
        }

        private void BfsAddToCollection(Expression expression)
        {
            var queue = new Queue<Expression>();
            queue.Enqueue(expression);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                expressionsById.Add(node.Id, node);

                if (node.LeftChild != null)
                {
                    queue.Enqueue(node.LeftChild);
                    node.LeftChild.Parent = node;
                }

                if (node.RightChild != null)
                {
                    queue.Enqueue(node.RightChild);
                    node.RightChild.Parent = node;
                }
            }
        }
    }
}
