namespace Problem04.BalancedParentheses
{
    using System;
    using System.Collections.Generic;

    public class BalancedParenthesesSolve : ISolvable
    {
        public bool AreBalanced(string parentheses)
        {
            if (parentheses.Length % 2 == 1)
            {
                return false;
            }

            var queue = new Stack<char>();

            foreach (var bracket in parentheses)
            {
                switch (bracket)
                {
                    case ')':
                        if (queue.Pop() != '(')
                        {
                            return false;
                        }
                        break;
                    case ']':
                        if (queue.Pop() != '[')
                        {
                            return false;
                        }
                        break;
                    case '}':
                        if (queue.Pop() != '{')
                        {
                            return false;
                        }
                        break;
                    default:
                        queue.Push(bracket);
                        break;
                }
            }

            if (queue.Count != 0)
            {
                return false;
            }

            return true;
        }
    }
}
