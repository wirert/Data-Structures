using System;
using System.Collections.Generic;
using System.Linq;

namespace Exam.ViTube
{
    public class ViTubeRepository : IViTubeRepository
    {
        private HashSet<User> users = new HashSet<User>();
        private HashSet<Video> videos = new HashSet<Video>();

        public bool Contains(User user) => users.Contains(user);

        public bool Contains(Video video) => videos.Contains(video);

        public void DislikeVideo(User user, Video video)
        {
            if (!Contains(user) || !Contains(video))
            {
                throw new ArgumentException();
            }

            user.Activity++;
            video.Dislikes++;
        }

        public IEnumerable<User> GetPassiveUsers()
            => users
                .Where(u => u.Watched == 0 && u.Activity == 0);

        public IEnumerable<User> GetUsersByActivityThenByName()
            => users.OrderByDescending(u => u.Watched)
                    .OrderByDescending(u => u.Activity)
                    .ThenBy(u => u.Username);

        public IEnumerable<Video> GetVideos() => videos;

        public IEnumerable<Video> GetVideosOrderedByViewsThenByLikesThenByDislikes()
            => videos.OrderByDescending(v => v.Views)
                     .ThenByDescending(v => v.Likes)
                     .ThenBy(v => v.Dislikes);

        public void LikeVideo(User user, Video video)
        {
            if (!Contains(user) || !Contains(video))
            {
                throw new ArgumentException();
            }

            user.Activity++;
            video.Likes++;
        }

        public void PostVideo(Video video)
        {
            if (!videos.Contains(video))
            {
                videos.Add(video);
            }
        }

        public void RegisterUser(User user)
        {
            if (!users.Contains(user))
            {
                users.Add(user);
            }
        }

        public void WatchVideo(User user, Video video)
        {
            if (!Contains(user) || !Contains(video))
            {
                throw new ArgumentException();
            }

            user.Watched++;
            video.Views++;
        }
    }
}
