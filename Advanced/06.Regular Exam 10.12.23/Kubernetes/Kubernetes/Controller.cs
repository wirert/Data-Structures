using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace Kubernetes
{
    public class Controller : IController
    {
        private Dictionary<string, Pod> podsById = new Dictionary<string, Pod>();

        public int Size() => podsById.Count;

        public bool Contains(string podId)
            => podsById.ContainsKey(podId);

        public void Deploy(Pod pod)
        {
            if(Contains(pod.Id))
            {
                throw new ArgumentException();
            }

            podsById.Add(pod.Id, pod);
        }

        public Pod GetPod(string podId)
        {
            if (!Contains(podId))
            {
                throw new ArgumentException();
            }

            return podsById[podId];
        }

        public void Uninstall(string podId)
        {
            if (!Contains(podId))
            {
                throw new ArgumentException();
            }

            podsById.Remove(podId);
        }

        public void Upgrade(Pod pod)
        {
            if (!Contains(pod.Id))
            {
                podsById.Add(pod.Id, pod);
                return;
            }

            var podforUpgrade = podsById[pod.Id];

            podforUpgrade.Port = pod.Port;
            podforUpgrade.ServiceName = pod.ServiceName;
            podforUpgrade.Namespace = pod.Namespace;
        }

        public IEnumerable<Pod> GetPodsInNamespace(string @namespace)
            => podsById.Values.Where(p => p.Namespace == @namespace);

        public IEnumerable<Pod> GetPodsBetweenPort(int lowerBound, int upperBound)
        => podsById.Values.Where(p => p.Port >= lowerBound && p.Port <= upperBound);

        public IEnumerable<Pod> GetPodsOrderedByPortThenByName()
            => podsById.Values.OrderByDescending(p => p.Port)
            .ThenBy(p => p.Namespace);

    }
}